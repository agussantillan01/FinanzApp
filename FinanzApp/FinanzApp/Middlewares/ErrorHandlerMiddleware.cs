using Business.Exceptions;
using Domain.Wrappers;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics;
using System.Net;

namespace FinanzApp.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };
                var stacktrace = new StackTrace();
                var message = stacktrace.GetFrame(1).GetMethod().Name + " - " + error.Message;

                switch (error)
                {
                    case Business.Exceptions.ApiException e:
                    case ValidationException ex:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        Log.Information(message);
                        break;
                    //case ValidationException e:
                    //    // custom application error
                    //    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //    responseModel.Errors = e.Errors;
                    //    message = e.Errors != null ? message + JsonConvert.SerializeObject(e.Errors) : message;
                    //    Log.Information(message);
                    //    break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        Log.Information(message);
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Message = "Ha ocurrido un error al intentar realizar la operación. Por favor comuníquese con Help Desk Softlatam.";
                        Log.Error(error, message);
                        break;
                }


                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base ("Ocurrieron uno o más errores de validacion.")
        {
            Errors = new List<string>();

        }
        public List<string> Errors { get;}
        public List<ValidationException> ValidationFailures { get; set; }

        public ValidationException(IEnumerable<ValidationException> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.Message);
            }
            ValidationFailures = failures.ToList() ;
        }   
    }
}

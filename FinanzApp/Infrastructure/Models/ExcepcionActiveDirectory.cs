﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class ExcepcionActiveDirectory
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public bool Activo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EjemploIdentity.Models
{
    public class Transporte
    {
        public int ID { get; set; }

        [Required]
        [StringLength(15)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }
    }
}
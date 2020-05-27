using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjemploIdentity.Models
{
    [Table("Producto")]
    public class Producto
    {
        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public Estado Estado { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Fecha de Creación")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

    }
}
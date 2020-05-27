using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjemploIdentity.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Required]
        [BithDateRange]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Display(Name = "Estado")]
        [DefaultValue(Estado.Activo)]
        public Estado Estado { get; set; }
    }
}
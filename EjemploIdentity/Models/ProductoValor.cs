using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjemploIdentity.Models
{
    [Table("ProductoValor")]
    public class ProductoValor
    {
        public int ID { get; set; }

        [Required]
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        [Required]
        [Display(Name ="Valor Minimo")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double ValorMinimo { get; set; }

        [Required]
        [Display(Name = "Valor Máximo")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public double ValorMaximo { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Fecha de Creación")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public Producto Producto { get; set; }
    }
}
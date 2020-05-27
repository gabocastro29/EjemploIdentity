using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjemploIdentity.Models
{
    [Table("Pedido")]
    public class Pedido
    {
        public int ID { get; set; }

        [Required]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        public Cliente Cliente { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Fecha de Creación")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        [DeliverDate]
        [Display(Name ="Fecha de Entrega")]
        [DataType(DataType.Date, ErrorMessage = "El formato de fecha no es válido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaEntrega { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Estado")]
        [DefaultValue(EstadoPedido.Creado)]
        public EstadoPedido EstadoPedido { get; set; }

        [InverseProperty("PedidoId")]
        public List<PedidoProducto> ProductosPedido;

        [NotMapped]
        public long Token { get; set; }
    }
}
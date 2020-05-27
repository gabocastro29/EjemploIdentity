using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EjemploIdentity.Models
{
    [Table("PedidoProducto")]
    public class PedidoProducto
    {
        [Key]
        [Column(Order =1)]
        [Required]
        [ForeignKey("Pedido")]
        [Display(Name = "Pedido")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        [ForeignKey("Producto")]
        [Display(Name = "Producto")]
        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        [Required]
        [PriceRange("ProductoId")]
        [Display(Name ="Valor Unitario")]
        public double ValorUnitario { get; set; }

        [Required]
        [Display(Name = "Unidades")]
        public int Cantidad { get; set; }


    }
}
using System.Data.Entity;

namespace EjemploIdentity.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base("DefaultConnection") { 
        }

        public DbSet<Transporte> Transportes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProducto> PedidoProductos { get; set; }
        public DbSet<ProductoValor> ProductoValores { get; set; }
    }
}
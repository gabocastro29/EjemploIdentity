using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace EjemploIdentity.Models
{
    public class OrdenCompraViewModel
    {
        public PedidoProducto PedidoProductoBase { get; set; }
        public Pedido Pedido { get; set; }
        public long Token { get; set; }

        public IEnumerable<SelectListItem> Productos { get; set; }
        public IEnumerable<SelectListItem> Clientes { get; set; }

        public OrdenCompraViewModel() { }

        public OrdenCompraViewModel(Contexto db) {
            Pedido = new Pedido();
            PedidoProductoBase = new PedidoProducto();
            Pedido.ProductosPedido = new List<PedidoProducto>();
            Productos = new SelectList(db.Productos, "ID", "Descripcion");
            Clientes = new SelectList(db.Clientes, "ID", "NombreCompleto");
            Token = Util.GetTimeInMillis();
        }

        public void SetearBases(Contexto db)
        {
            PedidoProductoBase = new PedidoProducto();
            Productos = new SelectList(db.Productos, "ID", "Descripcion");
            Clientes = new SelectList(db.Clientes, "ID", "NombreCompleto");
        }

    }
}
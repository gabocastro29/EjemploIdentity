using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjemploIdentity.Models
{
    public class GestionProductoPedidoViewModel
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public double ValorUnitario { get; set; }
        public int Cantidad { get; set; }

        public long Token { get; set; }

        public GestionProductoPedidoViewModel() { }
    }
}
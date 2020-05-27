using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EjemploIdentity.Models
{
    public class CrearListarTransporteViewModel
    {
        public Transporte TransporteModelo { get; set; }
        public IEnumerable<Transporte> ListaTransportes { get; set; }

        public string MensajeExito = null;

        public CrearListarTransporteViewModel() { }

        public CrearListarTransporteViewModel(Contexto db) {
            TransporteModelo = new Transporte();
            ListaTransportes = db.Transportes.ToList();
            MensajeExito = null;
        }
    }
}
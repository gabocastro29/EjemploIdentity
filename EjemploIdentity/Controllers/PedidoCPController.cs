﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EjemploIdentity.Models;
using PagedList;

namespace EjemploIdentity.Controllers
{
    public class PedidoCPController : Controller
    {
        private Contexto db = new Contexto();


        // Método Index con paginación
        // GET: Pedidos
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "cliente" : "";
            ViewBag.FechaRSortParm = "fechaRegistro";
            ViewBag.FechaESortParm = "fechaEntrega";
            ViewBag.EstadoSortParm = "Estado";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var pedidos = db.Pedidos.Include(p => p.Cliente);

            //Casos de ordenamiento segun la columna que se seleccione o por defecto si no se hace
            switch (sortOrder)
            {
                case "cliente":
                    pedidos = pedidos.OrderBy(s => s.Cliente.NombreCompleto);
                    break;
                case "fechaRegistro":
                    pedidos = pedidos.OrderBy(s => s.FechaRegistro);
                    break;
                case "fechaEntrega":
                    pedidos = pedidos.OrderBy(s => s.FechaEntrega);
                    break;
                case "Estado":
                    pedidos = pedidos.OrderBy(s => s.EstadoPedido);
                    break;
                default:
                    pedidos = pedidos.OrderByDescending(s => s.FechaRegistro);
                    break;
            }

            //El tamaño de la lista estará de 10 en 10
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(pedidos.ToPagedList(pageNumber, pageSize));
        }

        // GET: Pedidos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Include(x => x.Cliente).FirstOrDefault();
            if (pedido == null)
            {
                return HttpNotFound();
            }

            List<PedidoProducto> productos = db.PedidoProductos.Include(x => x.Producto).Include(x => x.Pedido).Where(x => x.PedidoId == pedido.ID).ToList();
            pedido.ProductosPedido = productos;

            return View(pedido);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            OrdenCompraViewModel orden = new OrdenCompraViewModel(db);
            if (AppViewModel.PedidosEnProceso == null) {
                AppViewModel.PedidosEnProceso = new Dictionary<long, List<GestionProductoPedidoViewModel>>();
            }
            AppViewModel.PedidosEnProceso.Add(orden.Token, new List<GestionProductoPedidoViewModel>());
            return View(orden);
        }

        // GET: Pedidos/Create
        public JsonResult AgregarProducto(GestionProductoPedidoViewModel orden)
        {
            List<GestionProductoPedidoViewModel> productos = AppViewModel.PedidosEnProceso[orden.Token];
            if (ModelState.IsValid)
            {
                productos.Add(orden);
                return Json("Ok");
            }
            return Json("Not Ok");
        }

        // GET: Pedidos/Create
        public JsonResult EliminarProducto(GestionProductoPedidoViewModel orden)
        {
            List<GestionProductoPedidoViewModel> productos = AppViewModel.PedidosEnProceso[orden.Token];
            if (productos != null || productos.Count > 0) {
                productos.Remove(orden);
            }
            return Json("Ok");
        }

        // POST: Pedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "Token, Pedido")] OrdenCompraViewModel orden)
        {
            orden.Pedido.ProductosPedido = new List<PedidoProducto>();
            ICollection<ValidationResult> resultado = new List<ValidationResult>(); // Will contain the results of the validation

            List<GestionProductoPedidoViewModel> productos = AppViewModel.PedidosEnProceso[orden.Token];
            if (productos != null)
            {
                foreach (var item in productos)
                {
                    PedidoProducto objeto = new PedidoProducto();
                    objeto.Pedido = orden.Pedido;
                    objeto.Pedido.ID = orden.Pedido.ID;
                    objeto.Producto = db.Productos.Find(item.Id);
                    objeto.ProductoId = objeto.Producto.ID;
                    objeto.ValorUnitario = item.ValorUnitario;
                    objeto.Cantidad = item.Cantidad;

                    ValidationContext vc = new ValidationContext(objeto);
                    bool isValid = Validator.TryValidateObject(objeto, vc, resultado, true); 


                    if (!isValid) {
                        var precios = db.ProductoValores.Where(x => x.ProductoId == objeto.ProductoId).First();
                        string validacion = string.Format("El precio de {0} debe estar entre {1} y {2}", objeto.Producto.Descripcion, precios.ValorMinimo, precios.ValorMaximo);
                        ModelState.AddModelError("", validacion);
                    }
                    orden.Pedido.ProductosPedido.Add(objeto);
                }
            }
            if (ModelState.IsValid)
            {
                if (productos != null)
                {
                    AppViewModel.PedidosEnProceso.Remove(orden.Token);
                }
                orden.Pedido.EstadoPedido = EstadoPedido.Creado;
                db.Pedidos.Add(orden.Pedido);
                foreach (var i in orden.Pedido.ProductosPedido)
                {
                    db.PedidoProductos.Add(i);
                }
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            
            orden.SetearBases(db);
            
            return View(orden);
        }

        // GET: Pedidos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Find(id);
            List<PedidoProducto> productos = db.PedidoProductos.Include(x => x.Pedido).Where(x => x.PedidoId == pedido.ID).ToList();
            pedido.ProductosPedido = productos;
            if (pedido == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ID", "NombreCompleto", pedido.ClienteId);
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ClienteId,FechaRegistro,FechaEntrega,EstadoPedido")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedido).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(db.Clientes, "ID", "NombreCompleto", pedido.ClienteId);
            return View(pedido);
        }

        // GET: Pedidos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = db.Pedidos.Include(x=>x.Cliente).Where(x=>x.ID==id).SingleOrDefault();
            if (pedido == null)
            {
                return HttpNotFound();
            }
            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            db.Pedidos.Remove(pedido);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

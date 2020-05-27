using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EjemploIdentity.Models;

namespace EjemploIdentity.Controllers
{
    public class PedidoProductosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: PedidoProductos
        public ActionResult Index()
        {
            var pedidoProductos = db.PedidoProductos.Include(p => p.Pedido).Include(p => p.Producto);
            return View(pedidoProductos.ToList());
        }

        // GET: PedidoProductos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoProducto pedidoProducto = db.PedidoProductos.Find(id);
            if (pedidoProducto == null)
            {
                return HttpNotFound();
            }
            return View(pedidoProducto);
        }

        // GET: PedidoProductos/Create
        public ActionResult Create()
        {
            ViewBag.PedidoId = new SelectList(db.Pedidos, "ID", "ID");
            ViewBag.ProductoId = new SelectList(db.Productos, "ID", "Descripcion");
            return View();
        }

        // POST: PedidoProductos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PedidoId,ProductoId,ValorUnitario,Cantidad")] PedidoProducto pedidoProducto)
        {
            if (ModelState.IsValid)
            {
                db.PedidoProductos.Add(pedidoProducto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PedidoId = new SelectList(db.Pedidos, "ID", "ID", pedidoProducto.PedidoId);
            ViewBag.ProductoId = new SelectList(db.Productos, "ID", "Descripcion", pedidoProducto.ProductoId);
            return View(pedidoProducto);
        }

        // GET: PedidoProductos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoProducto pedidoProducto = db.PedidoProductos.Find(id);
            if (pedidoProducto == null)
            {
                return HttpNotFound();
            }
            ViewBag.PedidoId = new SelectList(db.Pedidos, "ID", "ID", pedidoProducto.PedidoId);
            ViewBag.ProductoId = new SelectList(db.Productos, "ID", "Descripcion", pedidoProducto.ProductoId);
            return View(pedidoProducto);
        }

        // POST: PedidoProductos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PedidoId,ProductoId,ValorUnitario,Cantidad")] PedidoProducto pedidoProducto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidoProducto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PedidoId = new SelectList(db.Pedidos, "ID", "ID", pedidoProducto.PedidoId);
            ViewBag.ProductoId = new SelectList(db.Productos, "ID", "Descripcion", pedidoProducto.ProductoId);
            return View(pedidoProducto);
        }

        // GET: PedidoProductos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PedidoProducto pedidoProducto = db.PedidoProductos.Find(id);
            if (pedidoProducto == null)
            {
                return HttpNotFound();
            }
            return View(pedidoProducto);
        }

        // POST: PedidoProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PedidoProducto pedidoProducto = db.PedidoProductos.Find(id);
            db.PedidoProductos.Remove(pedidoProducto);
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

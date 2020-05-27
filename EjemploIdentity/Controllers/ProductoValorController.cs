using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EjemploIdentity.Models;

namespace EjemploIdentity.Controllers
{
    public class ProductoValorController : Controller
    {
        private Contexto db = new Contexto();

        // GET: ProductoValor
        public ActionResult Index()
        {
            var productoValores = db.ProductoValores.Include(p => p.Producto);
            return View(productoValores.ToList());
        }

        // GET: ProductoValor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoValor productoValor = db.ProductoValores.Find(id);
            if (productoValor == null)
            {
                return HttpNotFound();
            }
            return View(productoValor);
        }

        // GET: ProductoValor/Create
        public ActionResult Create()
        {
            ViewBag.ProductoId = new SelectList(db.Productos, "ID", "Descripcion");
            return View();
        }

        // POST: ProductoValor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ProductoId,ValorMinimo,ValorMaximo,FechaRegistro")] ProductoValor productoValor)
        {
            if (ModelState.IsValid)
            {
                db.ProductoValores.Add(productoValor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductoId = new SelectList(db.Productos, "ID", "Descripcion", productoValor.ProductoId);
            return View(productoValor);
        }

        // GET: ProductoValor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoValor productoValor = db.ProductoValores.Find(id);
            if (productoValor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "ID", "Descripcion", productoValor.ProductoId);
            return View(productoValor);
        }

        // POST: ProductoValor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ProductoId,ValorMinimo,ValorMaximo,FechaRegistro")] ProductoValor productoValor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productoValor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductoId = new SelectList(db.Productos, "ID", "Descripcion", productoValor.ProductoId);
            return View(productoValor);
        }

        // GET: ProductoValor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductoValor productoValor = db.ProductoValores.Find(id);
            if (productoValor == null)
            {
                return HttpNotFound();
            }
            return View(productoValor);
        }

        // POST: ProductoValor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductoValor productoValor = db.ProductoValores.Find(id);
            db.ProductoValores.Remove(productoValor);
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

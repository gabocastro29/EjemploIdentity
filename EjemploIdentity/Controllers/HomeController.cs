using EjemploIdentity.Models;
using System.Web.Mvc;

namespace IdentitySample.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult MensajeAlert()
        {
            AlertViewModel avm = new AlertViewModel();
            avm.Mensaje = "Contenido del Mensaje de Prueba desde el Servidor.";
            avm.Tipo = "N";

            return Json(avm);
        }

        [HttpGet]
        [Authorize]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

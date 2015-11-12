using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVCHelp.DAL.Interfaces;
using WebMVCHelp.Models;

namespace WebMVCHelp.Controllers
{
    public class HomeController : Controller
    {
        public IDalCredit DalCredit;
        public HomeController(IDalCredit DalCredit)
        {
            this.DalCredit = DalCredit;
        }
        public ActionResult Index()
        {
            //Primeira forma (muito usado essa)
            ViewBag.Drop1 = new SelectList(DalCredit.All(), "CreditId", "Description");
            //Segunda Forma posicionando o item
            //3 é aonde está gravado
            ViewBag.Drop2 = new SelectList(DalCredit.All(), "CreditId", "Description", 3);
            //Terceira Forma
            ViewBag.Drop3 = DalCredit.All();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            DateTime date = DateTime.Now;
            var c = date.ToString("dd/MM/yyyy");
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult PostItems(HttpPostedFileBase file)
        {

            long length = file.InputStream.Length;

            byte[] arrayByteFile = new byte[length];

            file.InputStream.Read(arrayByteFile, 0, (int)length);

            //na variavel arrayByteFile você tem o array de byte para gravar no seu banco

            return RedirectToAction("Index");
        }

        [HttpGet()]
        public ActionResult Sexo()
        {            
            return View();
        }

        [HttpPost()]
        public ActionResult Sexo(Peoples people)
        {
            Peoples p = people;
            return View();
        }

        [HttpPost()]
        public JsonResult GetSexo()
        {

            IList<Sexo> SexoList = new List<Sexo>()
            {
                new Sexo() { Id = 1, Description = "MASCULINO" },
                new Sexo() { Id = 2, Description = "FEMININO" }
            };

            return Json(SexoList, JsonRequestBehavior.DenyGet);

        }
    }
}
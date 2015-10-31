using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVCHelp.DAL.Interfaces;

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
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
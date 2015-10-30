using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVCHelp.DAL.Interfaces;
using WebMVCHelp.Models;

namespace WebMVCHelp.Controllers
{
    public class CreditsController : Controller
    {
        public IDalCredit DalCredit;
        public CreditsController(IDalCredit DalCredit)
        {
            this.DalCredit = DalCredit;
        }
        public ActionResult Index()
        {
            return View(DalCredit.All());
        }

        // GET: Credits/Details/5
        public ActionResult Details(int id)
        {
            return View(DalCredit.Find(id));
        }

        // GET: Credits/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Credits/Create
        [HttpPost]
        public ActionResult Create(Credit credit)
        {
            try
            {
                DalCredit.Add(credit);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Credits/Edit/5
        public ActionResult Edit(int id)
        {
            return View(DalCredit.Find(id));
        }

        // POST: Credits/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Credit credit)
        {
            try
            {
                
                DalCredit.Edit(credit);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Credits/Delete/5
        public ActionResult Delete(int id)
        {
            return View(DalCredit.Find(id));
        }

        // POST: Credits/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Credit credit)
        {
            try
            {                
                DalCredit.Remove(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

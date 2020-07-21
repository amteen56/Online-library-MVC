using Online_library_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineLibraryClass;

namespace Online_library_MVC.Controllers
{
    public class OnlineLibraryController : Controller
    {
        ServiceReference1.OneLibraryClient Service = new ServiceReference1.OneLibraryClient();

        // GET: OnlineLibrary
        public ActionResult Index()
        {
            TempData["deleted"] = "fa";
            TempData["showdt"] = "d";
            var list = Service.getData();
            return View(list);
        }
        public ActionResult BorrowItem()
        {
            var list = Service.getData();

            return View(list.Where(s=> s.itemtype== "Journal" || s.itemtype== "Book").ToList());
        }
        
        public ActionResult Borrow(string id)
        {
            TempData["borrowed"] = "fa";
            TempData["showbr"] = "true";
            var list = Service.getData();
            var a = list.Where(s => s.itemid.ToString() == id).SingleOrDefault();
            return View(list.Where(s => s.itemid.ToString() == id).SingleOrDefault());
        }

        [HttpPost]
        public ActionResult Borrow(OnlineLibraryClass.OnlineLibData data,FormCollection form)
        {
            if (Service.BorrowItem(Convert.ToInt32(data.itemid), form["dateobj"].ToString(),data.uname))
            {
                TempData["borrowed"] = "true";
                TempData["showbr"] = "false";
            }
            else
            {
                TempData["borrowed"] = "false";
                TempData["showbr"] = "false";
            }
            return View();
        }

        public ActionResult CreateItem()
        {
            TempData["added"] = "fa";
            TempData["showen"] = "true";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateItem(OnlineLibraryDataModel data,FormCollection form)
        {
            Online_library_MVC.ServiceReference1.OnlineLibData obj = new Online_library_MVC.ServiceReference1.OnlineLibData();
            obj.cost = data.cost;
            obj.itemtitle = data.itemtitle;
            obj.itemtype = form["type"];
            obj.noofissue = data.noofissue;
            if (Service.AddItem(obj))
            {
                TempData["added"] = "true";
                TempData["showen"] = "false";
            }
            else
            {
                TempData["added"] = "false";
                TempData["showen"] = "false";
            }
        
            return View();
        }
        public ActionResult ReturnItem()
        {
            TempData["return"] = "fa";
            TempData["showenr"] = "true";
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnItem(OnlineLibraryDataModel data, FormCollection form)
        {
            double a = Service.ReturnItem(Convert.ToInt32(data.itemid), data.uname);
            TempData["price"] = a;
            if (a!=-1)
            {
                TempData["return"] = "true";
                TempData["showenr"] = "false";
                
            }
            else
            {
                TempData["return"] = "false";
                TempData["showenr"] = "false";
            }

            return View();
        }
        public ActionResult EditItem(string id)
        {
            TempData["showened"] = "fa";
            TempData["showened"] = "true";
            var list = Service.getData();

            var a = list.Where(s => s.itemid.ToString() == id).SingleOrDefault();
            return View(list.Where(s => s.itemid.ToString() == id).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult EditItem(Online_library_MVC.ServiceReference1.OnlineLibData data)
        {
            if (Service.UpdateItem(data))
            {
                TempData["edit"] = "true";
                TempData["showened"] = "false";
            }
            else
            {
                TempData["edit"] = "false";
                TempData["showened"] = "false";
            }
            return View();
        }
        public ActionResult DeletItem(string id)
        {
            if (Service.DeleteItem(Convert.ToInt32(id)))
            {
                TempData["deleted"] = "true";
                TempData["showdt"] = "false";
            }
            else
            {
                TempData["deleted"] = "false";
                TempData["showdt"] = "false";
            }
            var list = Service.getData();
            return View("index",list);
        }
    }
}
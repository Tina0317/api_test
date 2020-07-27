using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_Practice.Models;
using LinqKit;

namespace API_Practice.Controllers
{
    public class HomeController : Controller
    {
        Database1Entities db = new Database1Entities();
        [HttpPost]
        public ActionResult Index(int No=0,string BookName="",string Type="",string Writer="")
        {
            var filter = PredicateBuilder.New<BookInfo>();
            if (No != 0)
            { filter = filter.And(a => a.No == No); }
            if (BookName.Trim() != "")
            { filter = filter.And(a => a.BookName == BookName); }
            if (Type.Trim() != "")
            { filter = filter.And(a => a.Type == Type); }
            if (Writer.Trim() != "")
            { filter = filter.And(a => a.Writer == Writer); }           
            var all = db.BookInfo.Where(filter).OrderBy(m => m.No).ToList();
            return View(all);
        }

        public ActionResult Delete(int No )
        {
            var book = db.BookInfo.Where(a => a.No == No).FirstOrDefault();
            db.BookInfo.Remove(book);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpPost]
        public ActionResult Edit(int No = 0, string BookName = "", string Type = "", string Writer = "")
        {
            var book = db.BookInfo.Where(a => a.No == No).FirstOrDefault();
            book.BookName = BookName;
            book.Type = Type;
            book.Writer = Writer;
            db.SaveChanges();
            return RedirectToAction("index");

        }

        public ActionResult Edit(int No)
        {
            var book = db.BookInfo.Where(a => a.No == No).FirstOrDefault();
            return View(book);

        }

        [HttpPost]
        public ActionResult Create(int No = 0, string BookName = "", string Type = "", string Writer = "")
        {
            BookInfo book = new BookInfo();
            book.No = No;
            book.BookName = BookName;
            book.Type = Type;
            book.Writer = Writer;
            db.BookInfo.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var book = db.BookInfo.FirstOrDefault();
            return View(book);
        }

        public ActionResult Index()
        {
            var all = db.BookInfo.ToList();
            return View(all);
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
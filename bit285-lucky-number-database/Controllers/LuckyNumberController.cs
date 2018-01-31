using bit285_lucky_number_database.Models;
using lucky_number_database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bit285_lucky_number_database.Controllers
{
    public class LuckyNumberController : Controller
    {
        private LuckyNumberDbContext dbc = new LuckyNumberDbContext();
        // GET: LuckyNumber
        public ActionResult Spin()
        {
            int luckID = (int)Session["currentID"];
            LuckyNumber myLuck = dbc.LuckyNumbers.Where(m => m.LuckyNumberID == luckID).First();
            dbc.LuckyNumbers.Add(myLuck);
            dbc.SaveChanges();
            return View(myLuck);
        }

        [HttpPost]
        public ActionResult Spin(LuckyNumber lucky)
        {
            int luckID = (int)Session["currentID"];
            LuckyNumber databaseLuck = dbc.LuckyNumbers.Where(m => m.LuckyNumberID == luckID).First();

            if (databaseLuck.Balance > 0)
            {
                databaseLuck.Balance -= 1;
            }
            databaseLuck.Number = lucky.Number;
            dbc.SaveChanges();
            return View(databaseLuck);
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LuckyNumber lucky)
        {
            dbc.LuckyNumbers.Add(lucky);
            dbc.SaveChanges();
            Session["currentID"] = lucky.LuckyNumberID;
            return View("Spin",lucky);
        }
    }
}
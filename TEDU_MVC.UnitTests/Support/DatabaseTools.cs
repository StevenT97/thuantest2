using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.FrameWork;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TEDU_MVC.Controllers;
using TEDU_MVC.Models;
using TEDU_MVC.UnitTests.Support;
namespace TEDU_MVC.UnitTests.Support
{
    [TestClass]
    public static class DatabaseTools
    {
        [AssemblyInitialize]
        public static void CleanDatabase(TestContext context)
        {
            using (var db = new DemoPPCRentalEntities())
            {
                //db.OrderLines.RemoveRange(db.OrderLines);
                //db.Orders.RemoveRange(db.Orders);
                //db.Books.RemoveRange(db.Books);
                db.SaveChanges();
            }
        }
    }
}

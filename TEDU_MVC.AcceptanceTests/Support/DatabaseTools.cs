using TEDU_MVC.Models;
using TechTalk.SpecFlow;
using Models.FrameWork;

namespace TEDU_MVC.AcceptanceTests.Support
{
    [Binding]
    public class DatabaseTools
    {
        [BeforeScenario]
        public void CleanDatabase()
        {
            //using (var db = new DemoPPCRentalEntities())
            //{
            //    db.OrderLines.RemoveRange(db.OrderLines);
            //    db.Orders.RemoveRange(db.Orders);
            //    db.Books.RemoveRange(db.Books);
            //    db.SaveChanges();
            //}
        }
    }
}

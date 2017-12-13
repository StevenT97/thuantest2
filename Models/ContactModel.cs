using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.FrameWork;

namespace Models
{
   public class ContactModel
    {
        DemoPPCRentalEntities db = null;
        public ContactModel()
        {
            db = new DemoPPCRentalEntities();
        }

        //public Contact GetActiveContact()
        //{
        //    return db.Contacts.Single(x => x.Status == true);
        //}

        public long InsertFeedBack(Contact fb)
        {
            db.Contacts.Add(fb);
            db.SaveChanges();
            return fb.ID;
        }
    }
}

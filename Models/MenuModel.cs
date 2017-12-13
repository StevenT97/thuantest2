using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.FrameWork;

namespace Models
{
   public class MenuModel
    {
        DemoPPCRentalEntities db = new DemoPPCRentalEntities();
        public List<Menu> ListByGroupId(int groupID)
        {
            return db.Menus.Where(x => x.TypeID == groupID).ToList();
        }
    }
}

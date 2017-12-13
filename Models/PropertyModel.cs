using Models.FrameWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class PropertyModel
    {
        private DemoPPCRentalEntities context = null;
        public PropertyModel()
        {
            context = new DemoPPCRentalEntities();
        }
        public List<PROPERTy> ListAll()
        {
            var list = context.Database.SqlQuery<PROPERTy>("Sp_Project_Type_ListAll").ToList();
            return list;
        }
        //public int Create(string name, bool? status)
        //{
        //    object[] parameters =
        //        {
        //        new SqlParameter("@Name",name),
        //        new SqlParameter("@Status", status)
        //    };
        //    int res = context.Database.ExecuteSqlCommand("Sp_Project_Type_Insert @Name,@Status", parameters);
        //    return res;
        //}
    }
}

using Models.FrameWork;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Project_TypeModel
    {
        private DemoPPCRentalEntities context = null;
        public Project_TypeModel()
        {
            context = new DemoPPCRentalEntities();
        }
        public List<PROPERTY_TYPE> ListAll() {
            var list = context.Database.SqlQuery<PROPERTY_TYPE>("Sp_Project_Type_ListAll").ToList();
            return list;
        }
        public int Create(string name, bool? status)
        {
            object[] parameters =
                {
                new SqlParameter("@Name",name),
                new SqlParameter("@Status", status)
            };
            int res = context.Database.ExecuteSqlCommand("Sp_Project_Type_Insert @Name,@Status", parameters);
            return res;
        }
    }
}

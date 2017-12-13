using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEDU_MVC.Areas.Admin.Code
{
    [Serializable]
    public class UserSession
    {
        public long UserID { set; get; }
        public string UserName { set; get; }
        public string GroupID { set; get; }
    }
}
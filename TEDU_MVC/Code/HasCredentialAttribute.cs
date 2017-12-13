using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEDU_MVC.Areas.Admin.Code;
using TEDU_MVC.Areas.Admin.Controllers;


namespace TEDU_MVC.Code
{
    public class HasCredentialAttribute: AuthorizeAttribute
    {
        public string RoleID { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //var isAuthorized = base.AuthorizeCore(httpContext);
            //if (!isAuthorized)
            //{
            //    return false;
            //}
            var session = (UserSession)HttpContext.Current.Session[CommonConstant.USER_SESSION];
            if (session == null)
            {
                return false;
            }
            //string privilegeLevels = string.Join(";", this.GetCredentialByLoggedInUser(session.UserName));
            List<string> privilegeLevels = this.GetCredentialByLoggedInUser(session.UserName);
            if (privilegeLevels.Contains(this.RoleID) || session.GroupID==CommonConstants.SALE_GROUP)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult
            {
                ViewName= "~/Areas/Admin/Views/Shared/401.cshtml"
            };
        }
        private List<string> GetCredentialByLoggedInUser(string userName)
        {
            var credentials = (List<string>)HttpContext.Current.Session[CommonConstant.SESSION_CREDENTIALS];
            return credentials;
        }
    }
}
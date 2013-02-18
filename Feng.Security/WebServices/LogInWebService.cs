using System;
using System.Web;
using System.Web.Services;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;
using System.Web.Security;

namespace Feng.WebServices
{
    /// <summary>
    /// ���¼ʹ�õ�WebService
    /// </summary>
    public abstract class LogInWebService : WebService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected LogInWebService()
        {
            if (IsAuthenticated)
            {
                IIdentity identity = new GenericIdentity(UserName);
                Thread.CurrentPrincipal = new RolePrincipal(identity);
            }
        }

        /// <summary>
        /// �û���
        /// </summary>
        protected virtual string UserName
        {
            get
            {
                if (IsAuthenticated)
                {
                    return (string) Session["UserName"];
                }
                return String.Empty;
            }
            set
            {
                if (IsAuthenticated)
                {
                    Session["UserName"] = value;
                }
                else
                {
                    throw new UnauthorizedAccessException("Cannot set unauthenticated user name");
                }
            }
        }

        /// <summary>
        /// �Ƿ���ͨ����֤
        /// </summary>
        protected virtual bool IsAuthenticated
        {
            get
            {
                object state = Session["IsAuthenticated"];
                if (state != null)
                {
                    return (bool) state;
                }
                //Must be first request in session
                IsAuthenticated = false;
                return false;
            }
            set { Session["IsAuthenticated"] = value; }
        }

        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public virtual bool LogIn(string userName, string password)
        {
            bool authenticated = Membership.ValidateUser(userName, password);
            IsAuthenticated = authenticated;

            if (authenticated)
            {
                UserName = userName;
            }
            return authenticated;
        }

        /// <summary>
        /// �ǳ�
        /// </summary>
        [WebMethod(EnableSession = true)]
        public virtual void LogOut()
        {
            UserName = String.Empty;
            IsAuthenticated = false;
        }
    }
}
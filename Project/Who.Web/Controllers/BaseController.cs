using System.Web.Mvc;

namespace Who.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        private const string SESSION_USER_ID = "SESSION_USER_ID";

        protected void AddUserIdToSessionStorage(int userId)
        {
            //TODO: session? claims?
            Session[SESSION_USER_ID] = userId;
        }

        protected int GetUserIdFromSessionStorage()
        {
            int? userId = Session[SESSION_USER_ID] as int?;
            if (!userId.HasValue)
            {
                return -1;
            }
            return userId.Value;
        }
    }
}
using System.Web.Mvc;

namespace AllTheSame.WebAPI.Controllers
{
    /// <summary>
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        ///     Indexes this instance.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
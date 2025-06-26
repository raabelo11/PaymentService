using Microsoft.AspNetCore.Mvc;

namespace PaymentService.Api.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

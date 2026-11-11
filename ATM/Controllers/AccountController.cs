
using ATM.Models.ViewModels;
using ATM.Services;
using Microsoft.AspNetCore.Mvc;

namespace ATM.Controllers
{
    public class AccountController : Controller
    {
        private readonly AtmAuthenticationService _authenticationService;

        public AccountController(
            AtmAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            // Nếu đã đăng nhập thì chuyển đến màn hình ATM
            if (HttpContext.Session.GetInt32("AccountId") != null)
            {
                return RedirectToAction("Index", "ATM");
            }

            return View(new CardLoginViewModel());
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(CardLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string? ipAddress = HttpContext.Connection.RemoteIpAddress?
                .ToString();

            var result = await _authenticationService.AuthenticateAsync(
                model,
                ipAddress
            );

            if (!result.Success || result.Card == null)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View(model);
            }

            var card = result.Card;

            // Lưu thông tin cần thiết vào Session
            HttpContext.Session.SetInt32("CardId", card.CardId);
            HttpContext.Session.SetInt32("AccountId", card.AccountId);

            HttpContext.Session.SetString(
                "CardNumberLast4",
                card.CardNumberLast4
            );

            HttpContext.Session.SetString(
                "AccountNumber",
                card.Account?.AccountNumber ?? string.Empty
            );

            HttpContext.Session.SetString(
                "CardType",
                card.CardType
            );

            HttpContext.Session.SetString(
                "ATMLoggedIn",
                "true"
            );

            return RedirectToAction("Index", "ATM");
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Account");
        }
    }
}
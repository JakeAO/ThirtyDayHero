using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Firebase.Database;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FirebaseAuthProvider _authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDHcCjJupg3v6rLAkwFcPuatQyamVVuE9M"));
        private User _fbUser = null;
        
        // GET
        public async Task<IActionResult> Index()
        {
            if (_fbUser == null)
                return RedirectToAction("_Login");
            
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyDHcCjJupg3v6rLAkwFcPuatQyamVVuE9M"));
            var auth = await authProvider.SignInAnonymouslyAsync();
            var authToken = auth.FirebaseToken;
            var user = auth.User;
            
            ViewBag.User = user;
            ViewBag.UserName = string.IsNullOrWhiteSpace(user.DisplayName) ? user.LocalId : user.DisplayName;
            
            var fbClient = new FirebaseClient(
                "thirty-day-hero.firebaseapp.com",
                new FirebaseOptions()
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(authToken)
                });
            
            return View();
        }

        public IActionResult _Login()
        {
            return View();
        }
    }
}
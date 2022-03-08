using EvrikaProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading.Tasks;

namespace EvrikaProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult Index(User user)
        {
            ViewBag.name = GetUser(user.Name);
            return View("UserCheck");
        }
        public string GetUser(String user)
        {
            string str;
            str = "";
            var usersSearcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_UserAccount");
            var users = usersSearcher.Get();
            foreach (var windowsUser in users)
            {
                string сurrentUser = windowsUser.ToString();
                string substring = ",Name=";
                int indexOfSubstring = сurrentUser.IndexOf(substring);
                string userName = сurrentUser.Remove(0, indexOfSubstring + 7);
                userName = userName.Substring(0, userName.Length - 1);
                if(userName == user)
                {
                    str = "Пользователь " + user + " есть";
                    return str;
                }
            }
            if (str == "")
            {
                str = "Пользователя " + user + " нет";
            }
            return str;
        }
    }
}

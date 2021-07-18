using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Serilog;
using SerilogFeatures.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;

namespace SerilogFeatures.Controllers
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
            _logger.LogInformation(@"Log message generated with
                INFORMATION severity level.");
            _logger.LogWarning(@"Log message generated with
                 WARNING severity level.");
            _logger.LogError(@"Log message generated with
                ERROR severity level.");
            _logger.LogCritical(@"Log message log generated with
                CRITICAL severity level.");
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId =
                Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}

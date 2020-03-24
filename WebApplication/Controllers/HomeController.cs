using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult GetDataTransferProcessResult([FromServices] IDataTransferProcess dataTransferProcess)
        {
            var min = double.Parse(Request.Query["min"]);
            var max = double.Parse(Request.Query["max"]);
            var count = int.Parse(Request.Query["count"]);
            var a = double.Parse(Request.Query["a"]);
            var b = double.Parse(Request.Query["b"]);
            var c = double.Parse(Request.Query["c"]);
            var significanceLevel = double.Parse(Request.Query["sl"]);
            var functionType = Request.Query["ft"];

            var result = dataTransferProcess.DataTransferProcess(min, max, count, a, b, c, 
                functionType, significanceLevel);

            return Json(result);
        }
        
    }
}
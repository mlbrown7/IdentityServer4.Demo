using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class FooController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return $"Bar {DateTime.Now}";
        }
    }
}

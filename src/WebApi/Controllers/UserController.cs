﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UsersController
    {
        [HttpGet]
        public List<string> Get()
        {
            return new List<string> { "joe", "nancy", "scott" };
        }
    }
}

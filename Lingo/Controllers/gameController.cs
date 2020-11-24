using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class gameController : ControllerBase
    {

        [HttpGet]
        public ActionResult<string> test() {
            return "hey";
        }

        [HttpGet]
        public ActionResult<string> test2(string test) {
            return test;
        }
    }
}

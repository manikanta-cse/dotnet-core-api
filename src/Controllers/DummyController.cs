using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using city_info_api.Entities;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace city_info_api.Controllers
{
    public class DummyController : Controller
    {
        private CityInfoContext _ctx;


        public DummyController(CityInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdb")]
        public IActionResult TestDb()
        {
            return Ok();
        }

    }
}

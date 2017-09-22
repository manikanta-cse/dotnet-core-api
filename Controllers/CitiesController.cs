using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace city_info_api.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            // return new JsonResult(new List<object>()
            // {
            //     new {id=1,Name="New York"},
            //     new {id=2,Name="Ante"}
            // });

            //return new JsonResult(CitiesDataStore.Current.Cities);
            return Ok(CitiesDataStore.Current.Cities);
        }

         [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {   
            var cityToReturn=CitiesDataStore.Current.Cities.FirstOrDefault(c=>c.Id == id);
            if(cityToReturn==null){
                return NotFound();
            }
            return Ok(cityToReturn);           
        }
        

    }
}
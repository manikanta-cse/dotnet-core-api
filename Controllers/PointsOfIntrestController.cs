using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace city_info_api.Controllers
{
    [Route("api/cities")]

      public class PointsOfIntrestController : Controller
    {
        [HttpGet("{cityId}/pointsOfIntrest")]
        public IActionResult GetPointsOfIntrests(int cityId)
        {
            var result=CitiesDataStore.Current.Cities.FirstOrDefault(i=>i.Id == cityId);
            if(result==null){
                return NotFound();
            }
            return Ok(result.PointsOfIntrest);
        }

         [HttpGet("{cityId}/pointsOfIntrest/{id}")]
        public IActionResult GetCity(int cityId,int id)
        {   
            var city=CitiesDataStore.Current.Cities.FirstOrDefault(c=>c.Id == cityId);
            if(city==null){
                return NotFound();
            }

            var pointsOfIntrest= city.PointsOfIntrest.FirstOrDefault(c=>c.Id == id);

            if(pointsOfIntrest ==null){
                return NotFound();
            }
            return Ok(pointsOfIntrest);           
        }
        

    }
}
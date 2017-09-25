using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using city_info_api.Data;
using city_info_api.Models;

namespace city_info_api.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            // return new JsonResult(new List<object>()
            // {
            //     new {id=1,Name="New York"},
            //     new {id=2,Name="Ante"}
            // });

            //return new JsonResult(CitiesDataStore.Current.Cities);

            var cityEntities=  _cityInfoRepository.GetCities();

            var results = Mapper.Map<IEnumerable<CityWithooutPointOfIntrestDto>>(cityEntities);

            return Ok(results);

            //var results= new List<CityWithooutPointOfIntrestDto>();

            //foreach (var cityEntity in cityEntities)
            //{
            //    results.Add(new CityWithooutPointOfIntrestDto
            //    {
            //        Id = cityEntity.Id,
            //        Name = cityEntity.Name,
            //        Description = cityEntity.Description
            //    });
            //}
            //return Ok(results);
        }

         [HttpGet("{id}")]
        public IActionResult GetCity(int id,bool includePointsOfIntrest=false)
        {   
            //var cityToReturn=CitiesDataStore.Current.Cities.FirstOrDefault(c=>c.Id == id);
            //if(cityToReturn==null){
            //    return NotFound();
            //}
            //return Ok(cityToReturn);    

            var city = _cityInfoRepository.GetCity(id, includePointsOfIntrest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfIntrest)
            {
                //var cityResult = new CityDto()
                //{
                //    Id = city.Id,
                //    Name = city.Name,
                //    Description = city.Description
                //};

                //foreach (var poi in city.PointsOfIntrest)
                //{
                //    cityResult.PointsOfIntrest.Add(new PointOfIntrestDto
                //    {
                //        Id=poi.Id,
                //        Name =poi.Name,
                //        Description = poi.Description
                //    });
                //}

                var cityResult = Mapper.Map<CityDto>(city);

                return Ok(cityResult);
            }

            //var cityWihtoutPointOfIntrestResult = new CityWithooutPointOfIntrestDto()
            //{
            //    Id = city.Id,
            //    Description = city.Description,
            //    Name = city.Name
            //};

            var cityWihtoutPointOfIntrestResult = Mapper.Map<CityWithooutPointOfIntrestDto>(city);

            return Ok(cityWihtoutPointOfIntrestResult);

        }
        

    }
}
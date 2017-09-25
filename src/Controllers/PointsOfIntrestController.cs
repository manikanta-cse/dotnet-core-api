using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using city_info_api.Data;
using city_info_api.Entities;
using city_info_api.Models;
using city_info_api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace city_info_api.Controllers
{
    [Route("api/cities")]
    public class PointsOfIntrestController : Controller
    {
        private ILogger<PointsOfIntrestController> _logger;
        private IMailService _localMailService;
        private ICityInfoRepository _cityInfoRepository;

        public PointsOfIntrestController(ILogger<PointsOfIntrestController> logger, IMailService localMailService,ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _localMailService = localMailService;
            _cityInfoRepository = cityInfoRepository;
        }


        [HttpGet("{cityId}/pointsOfIntrest")]
        public IActionResult GetPointsOfIntrests(int cityId)
        {
            try
            {
                ////throw new Exception("simple ex");
                //var result = CitiesDataStore.Current.Cities.FirstOrDefault(i => i.Id == cityId);
                //if (result == null)
                //{
                //    _logger.LogInformation($"City with id {cityId} wasn't found when accessing points of intrest.");

                //    return NotFound();
                //}
                //return Ok(result.PointsOfIntrest);

                if (!_cityInfoRepository.CityExists(cityId))
                {
                    return NotFound();
                }

                var pointsOfIntrestForCity = _cityInfoRepository.GetPointsOfIntrestsForCity(cityId);

                var pointsOfIntrestForCityResults = Mapper.Map<IEnumerable<PointOfIntrestDto>>(pointsOfIntrestForCity);

                //var pointsOfIntrestForCityResults= new List<PointOfIntrestDto>();

                //foreach (var poi in pointsOfIntrestForCity)
                //{
                //       pointsOfIntrestForCityResults.Add(new PointOfIntrestDto
                //       {
                //           Id = poi.Id,
                //           Description = poi.Description,
                //           Name =poi.Name
                //       });
                //}

                return Ok(pointsOfIntrestForCityResults);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Exception while getting points of intrest for city with id {cityId} .",e);

                return StatusCode(500, "A unknown error occured while processing yor request.");
            }

           
        }

        [HttpGet("{cityId}/pointsOfIntrest/{id}", Name = "GetPointsOfIntrest"),]
        public IActionResult GetPointsOfIntrest(int cityId, int id)
        {
            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            //if (city == null)
            //{
            //    return NotFound();
            //}

            //var pointsOfIntrest = city.PointsOfIntrest.FirstOrDefault(c => c.Id == id);

            //if (pointsOfIntrest == null)
            //{
            //    return NotFound();
            //}
            //return Ok(pointsOfIntrest);

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfIntrest = _cityInfoRepository.GetPointOfIntrestForCity(cityId, id);

            if (pointOfIntrest == null)
            {
                return NotFound();
            }

            var pointOfIntrestResult = Mapper.Map<PointOfIntrestDto>(pointOfIntrest);

            //var pointOfIntrestResult = new PointOfIntrestDto
            //{
            //    Id = pointOfIntrest.Id,
            //    Description = pointOfIntrest.Description,
            //    Name = pointOfIntrest.Name
            //};

            return Ok(pointOfIntrestResult);
        }

        [HttpPost("{cityId}/pointsOfIntrest")]
        public IActionResult CreatePointOfIntrest(int cityId, [FromBody] PointOfIntrestCreationDto pointsOfIntrest)
        {
            if (pointsOfIntrest == null)
            {
                return BadRequest();
            }
            if (pointsOfIntrest.Name == pointsOfIntrest.Description)
            {
                ModelState.AddModelError("Description", "Description should be diffrent from Name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(i => i.Id == cityId);

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            // var maxPointOfIntrestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfIntrest).Max(p => p.Id);

            //var finalPoinOfIntrest = new PointOfIntrestDto
            //{
            //    Id = ++maxPointOfIntrestId,
            //    Description = pointsOfIntrest.Description,
            //    Name = pointsOfIntrest.Name
            //};

            //city.PointsOfIntrest.Add(finalPoinOfIntrest);

            var finalPoinOfIntrest = Mapper.Map<PointOfIntrest>(pointsOfIntrest);

            _cityInfoRepository.AddPointOfIntrestForCity(cityId,finalPoinOfIntrest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "An error occured while handling your request");
            }

            var createdPointOfIntrestToReturn = Mapper.Map<PointOfIntrestDto>(finalPoinOfIntrest);
            return CreatedAtRoute("GetPointsOfIntrest", new { cityId = cityId, id = finalPoinOfIntrest.Id }, createdPointOfIntrestToReturn);


        }

        [HttpPut("{cityId}/pointsOfIntrest/{id}")]

        public IActionResult UpdatePointsOfIntrest(int cityId, int id,
            [FromBody] PointOfIntrestUpdationDto pointOfIntrestUpdationDto)
        {
            if (pointOfIntrestUpdationDto == null)
            {
                return BadRequest();
            }

            if (pointOfIntrestUpdationDto.Name == pointOfIntrestUpdationDto.Description)
            {
                ModelState.AddModelError("Description", "Description should be diffrent from Name");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(i => i.Id == cityId);

            //if (city == null)
            //{
            //    return NotFound();
            //}

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfIntrestFromStore = _cityInfoRepository.GetPointOfIntrestForCity(cityId,id);

            if (pointOfIntrestFromStore == null)
            {
                return NotFound();
            }

            //pointOfIntrestFromStore.Name = pointOfIntrestUpdationDto.Name;
            //pointOfIntrestFromStore.Description = pointOfIntrestUpdationDto.Description;

            Mapper.Map(pointOfIntrestUpdationDto, pointOfIntrestFromStore); //actual update

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "An error occured while handling request");
            }

            return NoContent();


        }

        [HttpPatch("{cityId}/pointsOfIntrest/{id}")]

        public IActionResult PartiallyUpdatePointOfIntrest(int cityId, int id,
            [FromBody] JsonPatchDocument<PointOfIntrestUpdationDto> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(i => i.Id == cityId);

            //if (city == null)
            //{
            //    return NotFound();
            //}

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfIntrestFromStore = _cityInfoRepository.GetPointOfIntrestForCity(cityId, id);

            if (pointOfIntrestFromStore == null)
            {
                return NotFound();
            }

            //var pointOfIntrestToPatch = new PointOfIntrestUpdationDto
            //{
            //    Name = pointOfIntrestFromStore.Name,
            //    Description = pointOfIntrestFromStore.Description
            //};

            var pointOfIntrestToPatch = Mapper.Map<PointOfIntrestUpdationDto>(pointOfIntrestFromStore);

            patchDocument.ApplyTo(pointOfIntrestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfIntrestToPatch.Name == pointOfIntrestToPatch.Description)
            {
                ModelState.AddModelError("Description", "Description should be diffrent from Name");
            }

            TryValidateModel(pointOfIntrestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //pointOfIntrestFromStore.Name = pointOfIntrestToPatch.Name;
            //pointOfIntrestFromStore.Description = pointOfIntrestToPatch.Description;
            Mapper.Map(pointOfIntrestToPatch, pointOfIntrestFromStore);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "An error occured while handling request");
            }

            return NoContent();

        }

        [HttpDelete("{cityId}/pointsOfIntrest/{id}")]

        public IActionResult DeletePointOfIntrest(int cityId, int id)
        {
            //var city = CitiesDataStore.Current.Cities.FirstOrDefault(i => i.Id == cityId);

            //if (city == null)
            //{
            //    return NotFound();
            //}

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfIntrestFromStore = _cityInfoRepository.GetPointOfIntrestForCity(cityId,id);

            if (pointOfIntrestFromStore == null)
            {
                return NotFound();
            }


            _cityInfoRepository.DeletePointOfIntrest(pointOfIntrestFromStore);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "An error occured while handling request");
            }

            //city.PointsOfIntrest.Remove(pointOfIntrestFromStore);

            _localMailService.Send("Point of Intrest deleted",$"Point of Intrest {pointOfIntrestFromStore.Name} with id {pointOfIntrestFromStore.Id} was deleted");

            return NoContent();
        }



    }
}
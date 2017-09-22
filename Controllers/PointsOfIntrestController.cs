using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using city_info_api.Models;
using Microsoft.AspNetCore.JsonPatch;

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

         [HttpGet("{cityId}/pointsOfIntrest/{id}",Name = "GetPointsOfIntrest"),]
        public IActionResult GetPointsOfIntrest(int cityId,int id)
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

         [HttpPost("{cityId}/pointsOfIntrest")]
        public IActionResult CreatePointOfIntrest(int cityId,[FromBody] PointOfIntrestCreationDto pointsOfIntrest)
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

            
            var city=CitiesDataStore.Current.Cities.FirstOrDefault(i=>i.Id == cityId);

            if(city==null){
                return NotFound();
            }

            var maxPointOfIntrestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointsOfIntrest).Max(p => p.Id);

            var finalPoinOfIntrest = new PointOfIntrestDto
            {
                Id = ++maxPointOfIntrestId,
                Description = pointsOfIntrest.Description,
                Name = pointsOfIntrest.Name
            };

            city.PointsOfIntrest.Add(finalPoinOfIntrest);

            return CreatedAtRoute("GetPointsOfIntrest",new {cityId=city.Id,id=finalPoinOfIntrest.Id},finalPoinOfIntrest);


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

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(i => i.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfIntrestFromStore = city.PointsOfIntrest.FirstOrDefault(c => c.Id == id);

            if (pointOfIntrestFromStore == null)
            {
                return NotFound();
            }

            pointOfIntrestFromStore.Name = pointOfIntrestUpdationDto.Name;
            pointOfIntrestFromStore.Description = pointOfIntrestUpdationDto.Description;

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

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(i => i.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfIntrestFromStore = city.PointsOfIntrest.FirstOrDefault(c => c.Id == id);

            if (pointOfIntrestFromStore == null)
            {
                return NotFound();
            }

            var pointOfIntrestToPatch = new PointOfIntrestUpdationDto
            {
                Name = pointOfIntrestFromStore.Name,
                Description = pointOfIntrestFromStore.Description
            };

            patchDocument.ApplyTo(pointOfIntrestToPatch,ModelState);

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


            pointOfIntrestFromStore.Name = pointOfIntrestToPatch.Name;
            pointOfIntrestFromStore.Description = pointOfIntrestToPatch.Description;

            return NoContent();

        }

        [HttpDelete("{cityId}/pointsOfIntrest/{id}")]

        public IActionResult DeletePointOfIntrest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(i => i.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfIntrestFromStore = city.PointsOfIntrest.FirstOrDefault(c => c.Id == id);

            if (pointOfIntrestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfIntrest.Remove(pointOfIntrestFromStore);

            return NoContent();
        }

        

    }
}
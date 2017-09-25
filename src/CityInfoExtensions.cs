using city_info_api.Entities;
using city_info_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace city_info_api
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }

            var cities = new List<City>()
            {
                new City()
                {

                Name="New York City",
                Description="NY",
                PointsOfIntrest= new List<PointOfIntrest>()
                {
                    new PointOfIntrest()
                    {
                       
                        Name="Central Park",
                        Description="Most Visited"
                    },
                    new PointOfIntrest()
                    {
                       
                        Name="Empire State Building",
                        Description="Most Visited"
                    }

                }
                },                
                new City()
                {
                     
                Name="Paris",
                Description="Paris",
                 PointsOfIntrest= new List<PointOfIntrest>()
                {
                    new PointOfIntrest()
                    {
                      
                        Name="Eiffel Tower",
                        Description="Most Visited"
                    },
                    new PointOfIntrest()
                    {
                      
                        Name="Lourve",
                        Description="Most Visited"
                    }

                }
                }
            };

            context.Cities.AddRange(cities);
            context.SaveChanges();


        }
    }
}

using System.Collections.Generic;
using city_info_api.Models;


namespace city_info_api
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current {get;} =new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                      Id=1,
                Name="NY",
                Description="NY",
                PointsOfIntrest= new List<PointOfIntrestDto>()
                {
                    new PointOfIntrestDto()
                    {
                        Id=1,
                        Name="CP",
                        Description="Most Visited"
                    },
                    new PointOfIntrestDto()
                    {
                        Id=2,
                        Name="CP1",
                        Description="Most Visited"
                    }

                }
                },
                new CityDto()
                {
                      Id=2,
                Name="NY",
                Description="NY",
                 PointsOfIntrest= new List<PointOfIntrestDto>()
                {
                    new PointOfIntrestDto()
                    {
                        Id=3,
                        Name="test3",
                        Description="Most Visited"
                    },
                    new PointOfIntrestDto()
                    {
                        Id=4,
                        Name="test4",
                        Description="Most Visited"
                    }

                }
                },
                new CityDto()
                {
                      Id=3,
                Name="NY",
                Description="NY",
                 PointsOfIntrest= new List<PointOfIntrestDto>()
                {
                    new PointOfIntrestDto()
                    {
                        Id=5,
                        Name="test5",
                        Description="Most Visited"
                    },
                    new PointOfIntrestDto()
                    {
                        Id=6,
                        Name="test6",
                        Description="Most Visited"
                    }

                }
                }
            };
        }

    }

}


using System.Collections.Generic;

namespace city_info_api.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfPointsOfIntrest { get
        {
            return PointsOfIntrest.Count;
        } }

        public ICollection<PointOfIntrestDto> PointsOfIntrest {get;set;} = new List<PointOfIntrestDto>();
    }
}

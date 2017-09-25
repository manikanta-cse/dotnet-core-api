using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using city_info_api.Entities;

namespace city_info_api.Data
{
    public interface ICityInfoRepository
    {
        bool CityExists(int cityId);
        IEnumerable<City> GetCities();
        City GetCity(int cityId, bool includePointOfIntrest);

        IEnumerable<PointOfIntrest> GetPointsOfIntrestsForCity(int cityId);

        PointOfIntrest GetPointOfIntrestForCity(int cityId, int pointOfIntrestId);

        void AddPointOfIntrestForCity(int cityId,PointOfIntrest pointOfIntrest);

        bool Save();

        void DeletePointOfIntrest(PointOfIntrest pointOfIntrest);


    }
}

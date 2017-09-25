using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using city_info_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace city_info_api.Data
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private CityInfoContext _context;


        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public void AddPointOfIntrestForCity(int cityId, PointOfIntrest pointOfIntrest)
        {
            var city = GetCity(cityId, false);
            city.PointsOfIntrest.Add(pointOfIntrest);
        }

        public bool CityExists(int cityId)
        {
            return _context.Cities.Any(c => c.Id == cityId);
        }

        public PointOfIntrest GetPointOfIntrestForCity(int cityId, int pointOfIntrestId)
        {
            return _context.PointOfIntrest.Where(i=>i.Id == pointOfIntrestId && i.CityId==cityId).FirstOrDefault();
        }

        public IEnumerable<City> GetCities()
        {
            return _context.Cities.OrderBy(i => i.Name).ToList();
            
        }

        public City GetCity(int cityId,bool includePointOfIntrest)
        {
            if (includePointOfIntrest)
            {
                return _context.Cities.Include(i => i.PointsOfIntrest).Where(i => i.Id == cityId).FirstOrDefault();
            }

            return _context.Cities.Where(i => i.Id == cityId).FirstOrDefault();
        }

        public IEnumerable<PointOfIntrest> GetPointsOfIntrestsForCity(int cityId)
        {
            return _context.PointOfIntrest.Where(i => i.CityId == cityId).ToList();
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void DeletePointOfIntrest(PointOfIntrest pointOfIntrest)
        {
            _context.PointOfIntrest.Remove(pointOfIntrest);
        }
    }
}

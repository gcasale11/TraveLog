using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraveLog.Data;
using TraveLog.Models;

namespace TraveLog.Services
{
    public class LocationService
    {
        private readonly string _userId;

        public LocationService(string UserId)
        {
            _userId = UserId;
        }

        public bool CreateLocation(LocationCreate model)
        {
            var entity =
                new Location()
                {
                    UserId = _userId,
                    Cities = model.Cities
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Locations.Add(entity);
                
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<LocationListItem> GetLocation()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Locations
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                        new LocationListItem
                        {
                            LocationId = e.LocationId,
                            Cities = e.Cities

                        }
                        );
                return query.ToArray();
            }
        }

        public LocationDetail GetLocationById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == id && e.UserId == _userId);
                return
                    new LocationDetail
                    {
                        LocationId = entity.LocationId,
                        Cities = entity.Cities,
                    };
            }
        }

        public bool UpdateLocation(LocationEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == model.LocationId && e.UserId == _userId);

                entity.Cities = model.Cities;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteLocation(int locationId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Locations
                    .Single(e => e.LocationId == locationId && e.UserId == _userId);

                ctx.Locations.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

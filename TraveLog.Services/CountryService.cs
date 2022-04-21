using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraveLog.Data;
using TraveLog.Models;

namespace TraveLog.Services
{
    public class CountryService
    {
        private readonly string _userId;
        public CountryService(string UserId)
        {
            _userId = UserId;
        }

        public bool CreateCountry(CountryCreate model)
        {
            var entity =
                new Country()
                {
                    UserId = _userId,
                    CountryName = model.CountryName,
                    Continent = model.Continent,
                };
            
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Countries.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CountryListItem> GetCountry()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Countries
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                        new CountryListItem
                        {
                            CountryId = e.CountryId,
                            CountryName = e.CountryName,
                            Continent = e.Continent,
                        }
                        );
                return query.ToArray();
            }
        }

        public CountryDetail GetCountryById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Countries
                    .Single(e => e.CountryId == id && e.UserId == _userId);
                return
                    new CountryDetail
                    {
                        CountryId = entity.CountryId,
                        CountryName = entity.CountryName,
                        Continent = entity.Continent
                    };
            }
        }

        public bool UpdateCountry(CountryEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Countries
                    .Single(e => e.CountryId == model.CountryId && e.UserId == _userId);

                entity.CountryName = model.CountryName;
                entity.Continent = model.Continent;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCountry(int countryId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Countries
                    .Single(e => e.CountryId == countryId && e.UserId == _userId);

                ctx.Countries.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

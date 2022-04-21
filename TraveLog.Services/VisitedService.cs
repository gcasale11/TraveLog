using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraveLog.Data;
using TraveLog.Models;

namespace TraveLog.Services
{
   public class VisitedService
    {
        private readonly string _userId;
        public VisitedService(string UserId)
        {
            _userId = UserId;
        }

        
        public bool CreateVisited(VisitedCreate model)
        {
            var entity =
                new Visited()
            {
                DateVisited = model.DateVisited,
                InitialThoughts = model.InitialThoughts,

            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Visitedd.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<VisitedListItem> GetVisit()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Visitedd
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                        new VisitedListItem
                        {
                            VisitedId = e.VisitedId,
                            DateVisited = e.DateVisited,
                            Cities = e.Location.Cities,
                            CountryName = e.Country.CountryName,
                            InitialThoughts = e.InitialThoughts
                            
                        }
                        );
                return query.ToArray();
            }
        }

        public VisitedDetail GetVisitedById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Visitedd
                    .Single(e => e.VisitedId == id && e.UserId == _userId);
                return
                    new VisitedDetail
                    {
                        VisitedId = entity.VisitedId,
                        DateVisited = entity.DateVisited,
                        Cities = entity.Location.Cities,
                        CountryName = entity.Country.CountryName,
                        Continent = entity.Country.Continent,
                        InitialThoughts = entity.InitialThoughts,
                        Thoughts = entity.Blog.Thoughts,
                        UserId = entity.UserId

                    };
            }
        }

        public bool UpdateVisited(VisitedEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Visitedd
                    .Single(e => e.VisitedId == model.VisitedId && e.UserId == _userId);

                entity.DateVisited = model.DateVisited;
                entity.InitialThoughts = model.InitialThoughts;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteVisited(int visitedId)
        {
            using (var ctx = new ApplicationDbContext())
                    {
                var entity =
                    ctx
                    .Visitedd
                    .Single(e => e.VisitedId == visitedId && e.UserId == _userId);

                ctx.Visitedd.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

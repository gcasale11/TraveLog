using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraveLog.Data;
using TraveLog.Models;

namespace TraveLog.Services
{
   public class BlogService
    {
        private readonly string _userId;
        public BlogService(string UserId)
        {
            _userId = UserId;
        }

        public bool CreateBlog(BlogCreate model)
        {
            var entity =
                new Blog()
                {
                    UserId = _userId,
                    LocationId = model.LocationId,
                    CountryId = model.CountryId,
                    VisitedId = model.VisitedId,
                    Thoughts = model.Thoughts
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Blogs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<BlogListItem> GetBlogs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Blogs
                    .Where(e => e.UserId == _userId)
                    .Select(
                        e =>
                        new BlogListItem
                        {
                            BlogId = e.BlogId,
                            DateVisited = e.Visited.DateVisited,
                            InitialThoughts = e.Visited.InitialThoughts,
                            Cities = e.Location.Cities,
                            CountryName = e.Country.CountryName,
                            Thoughts = e.Thoughts,
                        }
                        );
                return query.ToArray();
            }
        }

        public BlogDetail GetBlogById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Blogs
                    .Single(e => e.BlogId == id && e.UserId == _userId);
                return
                    new BlogDetail
                    {
                        BlogId = entity.BlogId,
                        Thoughts = entity.Thoughts
                    };
            }
        }

        public bool UpdateBlog(BlogEdit model)
        {
            using(var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Blogs
                    .Single(e => e.BlogId == model.BlogId && e.UserId == _userId);

               
                entity.Thoughts = model.Thoughts;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteBlog(int blogId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Blogs
                    .Single(e => e.BlogId == blogId && e.UserId == _userId);

                ctx.Blogs.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }

    }
}

using BaseProject.Models;

namespace BaseProject.Extensions
{
    public static class CinemaExtensions
    {


        public static IQueryable<Cinema> Sort(this IQueryable<Cinema> query, string orderBy = null)
        {

            if (string.IsNullOrEmpty(orderBy)) return query.OrderBy(c => c.CName);

            query = orderBy switch
            {
                _ => query.OrderBy(c => c.CName)
            };
            return query;

        }

    }
}

using BaseProject.Models;

namespace BaseProject.Extensions
{
    public static class MovieExtension
    {
        // Sort Movie By Name or Time Or Date
        public static IQueryable<Movies> Sort(this IQueryable<Movies> query, string? orderBy= null)
        {
            if (string.IsNullOrEmpty(orderBy)) return query.OrderBy(m => m.MName);

            query = orderBy switch
            {
                "duration" => query.OrderBy(m => m.duration),
                _ => query.OrderBy(m => m.MName)

            };

            return query;
        }




        // Search movies by name

        public static IQueryable<Movies> Search(this IQueryable<Movies> query, string? searchTerm = null)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearch = searchTerm.ToLower();

            return query.Where(m => m.MName.ToLower().Contains(lowerCaseSearch));



        }



        // filter movies by Type

        public static IQueryable<Movies> Fillter(this IQueryable<Movies> query, string? mtype = null)
        {
          var  mtypeList =new List<string>();

          if(!string.IsNullOrEmpty(mtype))
                mtypeList.AddRange(mtype.Split(',').ToList());


            query = query.Where(m => mtypeList.Count == 0 || mtypeList.Contains(m.Mtype.ToString()));

            return query;
        }




    }
}

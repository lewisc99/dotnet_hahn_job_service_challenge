using Domain.Entities.Pagination;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Helpers
{
    public static class HateoasLinkHelper
    {
        public static List<Link> CreateHateoasLinks(HttpContext httpContext, string controllerName, PaginationFilter<object> filter, int totalPages)
        {
            var links = new List<Link>();

            var baseUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path}";

            links.Add(new Link
            {
                Href = $"{baseUrl}?page={filter.Page}&pageSize={filter.PageSize}&sortBy={filter.SortBy}&orderByAscending={filter.OrderByAscending}",
                Rel = "self",
                Method = "GET"
            });

            links.Add(new Link
            {
                Href = $"{baseUrl}?page=1&pageSize={filter.PageSize}&sortBy={filter.SortBy}&orderByAscending={filter.OrderByAscending}",
                Rel = "first",
                Method = "GET"
            });

            links.Add(new Link
            {
                Href = $"{baseUrl}?page={totalPages}&pageSize={filter.PageSize}&sortBy={filter.SortBy}&orderByAscending={filter.OrderByAscending}",
                Rel = "last",
                Method = "GET"
            });

            if (filter.Page > 1)
            {
                links.Add(new Link
                {
                    Href = $"{baseUrl}?page={filter.Page - 1}&pageSize={filter.PageSize}&sortBy={filter.SortBy}&orderByAscending={filter.OrderByAscending}",
                    Rel = "previous",
                    Method = "GET"
                });
            }

            if (filter.Page < totalPages)
            {
                links.Add(new Link
                {
                    Href = $"{baseUrl}?page={filter.Page + 1}&pageSize={filter.PageSize}&sortBy={filter.SortBy}&orderByAscending={filter.OrderByAscending}",
                    Rel = "next",
                    Method = "GET"
                });
            }

            return links;
        }
    }

    public class Link
    {
        public string Href { get; set; } = string.Empty;
        public string Rel { get; set; } = string.Empty;
        public string Method { get; set; } = "GET";
    }
}

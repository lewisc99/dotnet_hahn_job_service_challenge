using Domain.Entities.Pagination;
using Domain.Interfaces;
using Infrastructure.Extensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Pagination
{
    public class Repository<T> : IRepositoryPagination<T> where T : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public ApiResponse<T> GetPaginatedData(PaginationFilter<T> filter)
        {
            IQueryable<T> query = _context.Set<T>();

            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                query = ApplySorting(query, filter.SortBy, filter.OrderByAscending);
            }

            if (filter.FilterBy != null && filter.FilterBy.Any())
            {
                query = ApplyFiltering(query, filter.FilterBy);
            }

            if (!string.IsNullOrEmpty(filter.SearchBy))
            {
                query = ApplySearch(query, filter.SearchBy);
            }

            int totalSize = query.Count();

            query = ApplyPagination(query, filter.Page, filter.PageSize);

            List<T> paginatedData = query.ToList();

            return new ApiResponse<T>
            {
                Data = paginatedData.ToList(),
                TotalSize = totalSize
            };
        }

        private IQueryable<T> ApplySorting(IQueryable<T> query, string? sortBy, bool orderByAscending)
        {
            if (!string.IsNullOrEmpty(sortBy))
            {
                return orderByAscending
                    ? query.OrderByDynamic(sortBy)
                    : query.OrderByDescendingDynamic(sortBy);
            }

            return query;
        }

        private IQueryable<T> ApplyFiltering(IQueryable<T> query, List<FilterBy> filterCriteria)
        {
            foreach (var criteria in filterCriteria)
            {
                query = query.Where(BuildFilterExpression(criteria));
            }
            return query;
        }

        private Expression<Func<T, bool>> BuildFilterExpression(FilterBy criteria)
        {
            var parameter = Expression.Parameter(typeof(T), "c");
            var property = Expression.Property(parameter, criteria.PropertyName);
            var constant = Expression.Constant(criteria.FilterValue);

            if (property.Type == typeof(string))
            {
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var propertyToLower = Expression.Call(property, toLowerMethod);
                var constantToLower = Expression.Call(constant, toLowerMethod);
                var containsExpression = Expression.Call(propertyToLower, containsMethod, constantToLower);
                return Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
            }
            else
            {
                if (property.Type.IsNumericType() && constant.Type == typeof(string))
                {
                    var parseMethod = property.Type.GetMethod("Parse", new[] { typeof(string) });
                    var convertedConstant = Expression.Call(parseMethod, constant);
                    var equalExpression = Expression.Equal(property, convertedConstant);
                    return Expression.Lambda<Func<T, bool>>(equalExpression, parameter);
                }
                else
                {
                    throw new NotSupportedException($"Unsupported property type: {property.Type}");
                }
            }
        }

        private IQueryable<T> ApplyPagination(IQueryable<T> query, int page, int pageSize)
        {
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        private IQueryable<T> ApplySearch(IQueryable<T> query, string searchBy)
        {
            if (string.IsNullOrEmpty(searchBy))
            {
                return query;
            }

            var properties = typeof(T).GetProperties();

            var predicate = PredicateBuilder.New<T>();

            foreach (var prop in properties.Where(p => p.PropertyType == typeof(string)))
            {
                var parameter = Expression.Parameter(typeof(T), "e");
                var property = Expression.Property(parameter, prop);
                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                var propertyToLower = Expression.Call(property, toLowerMethod);
                var constantToLower = Expression.Constant(searchBy.ToLower());
                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var containsExpression = Expression.Call(propertyToLower, containsMethod, constantToLower);

                var propertyCondition = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
                predicate = predicate.Or(propertyCondition);
            }

            return query.Where(predicate);
        }

    }
}

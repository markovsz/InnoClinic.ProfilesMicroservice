using Domain.RequestParameters;

namespace Infrastructure.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> entities, PaginationParameters parameters)
        {
            entities = entities
                .Skip((parameters.Page - 1) * parameters.Size)
                .Take(parameters.Size);
            return entities;
        }
    }
}

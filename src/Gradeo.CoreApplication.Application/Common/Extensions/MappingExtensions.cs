using Gradeo.CoreApplication.Application.Common.Models;

namespace Gradeo.CoreApplication.Application.Common.Extensions;

public static class MappingExtensions
{
    public static PaginatedList<TDestination> PaginateList<TDestination>(this IEnumerable<TDestination> enumerable, int pageNumber, int pageSize)
        => PaginatedList<TDestination>.Create(enumerable, pageNumber, pageSize);

    public static Task<PaginatedList<TDestination>> PaginateListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize, CancellationToken cancellationToken = default(CancellationToken))
        => PaginatedList<TDestination>.CreateAsync(queryable, pageNumber, pageSize, cancellationToken);
}
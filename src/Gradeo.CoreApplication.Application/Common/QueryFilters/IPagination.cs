using Gradeo.CoreApplication.Application.Common.Constants;

namespace Gradeo.CoreApplication.Application.Common.QueryFilters;

public interface IPagination
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
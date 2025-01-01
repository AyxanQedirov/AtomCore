using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AtomCore.Pagination;

public static class PaginationExtensions
{
    public static PageResponse<T> ToPageable<T>(this IQueryable<T> queryable, int index, int size) where T : class
    {
        PageResponse<T> pageResponse = new();

        double pageCountDouble = (double)queryable.Count() / size;

        pageResponse.Index = index;
        pageResponse.Size = size;
        pageResponse.Total = queryable.Count();
        pageResponse.Max = pageCountDouble != (int)pageCountDouble ? (int)pageCountDouble + 1 : (int)pageCountDouble;
        pageResponse.Min = pageResponse.Max > 0 ? 1 : 0;
        pageResponse.HasNext = pageResponse.Max > index ? true : false;
        pageResponse.HasPrev = pageResponse.Min < index ? true : false;
        pageResponse.Datas = queryable.Skip(size * (index - 1)).Take(size).ToList();

        return pageResponse;
    }

}

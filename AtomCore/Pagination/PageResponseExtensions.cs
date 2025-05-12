namespace AtomCore.Pagination;

public static class PageResponseExtensions
{
    public static PageResponse<TNew> ReplaceDatas<T, TNew>(this PageResponse<T> pageResponse, List<TNew> newDatas)
        where T : class
        where TNew : class
    {
        PageResponse<TNew> result = new()
        {
            Size = pageResponse.Size,
            Index = pageResponse.Index,
            Total = pageResponse.Total,
            HasNext = pageResponse.HasNext,
            HasPrev = pageResponse.HasPrev,
            Min = pageResponse.Min,
            Max = pageResponse.Max,
            Datas = newDatas
        };

        return result;
    }
}
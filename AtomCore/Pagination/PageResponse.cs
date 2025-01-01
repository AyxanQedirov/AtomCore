
namespace AtomCore.Pagination;

public class PageResponse<T> where T : class
{
    public int Size { get; set; }
    public int Index { get; set; }
    public int Total { get; set; }
    public bool HasNext { get; set; }
    public bool HasPrev { get; set; }
    public List<T> Datas { get; set; }
    public int Min { get; set; }
    public int Max { get; set; }
}

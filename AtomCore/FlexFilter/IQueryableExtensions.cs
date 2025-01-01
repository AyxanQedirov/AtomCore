using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using AtomCore.Extensions;

namespace AtomCore.FlexFilter;

public static class IQueryableExtensions
{
    private static Dictionary<string, string> Operations = new() {
        {"eq","{{field}}=={{value}}" },
        {"neq","{{field}}!={{value}}" },
        {"gt","{{field}}>{{value}}" },
        {"gte","{{field}}>={{value}}" },
        {"lt","{{field}}<{{value}}" },
        {"lte","{{field}}<={{value}}" },
        {"like","{{field}}.Contains({{value}})" }
    };
    private static Dictionary<string, string> Sorts = new() {
        {"asc","asc" },
        {"desc","desc" }
    };

    private static Dictionary<string, string> LogicalOperations = new() {
        {"and","and" },
        {"or","or" }
    };

    public static IQueryable<T> ApplyFlexFilter<T>(this IQueryable<T> queryable, FlexFilter flexFilter)
    {

        queryable = Sort(queryable, flexFilter.Sort);

        List<object> whereValues = new();
        string whereFilter = "";
        GenerateWhere(flexFilter.Filter, ref whereFilter, ref whereValues);
        queryable = queryable.Where(whereFilter, whereValues.ToArray());

        return queryable;
    }

    private static IQueryable<T> Sort<T>(IQueryable<T> queryable, Sort sort)
    {
        if (sort is null)
            return queryable;

        if (sort.Field is null && sort.SortType is null)
            return queryable;

        if (sort.Field.IsNullOrEmpty() && sort.SortType is not null)
            throw new ArgumentException("Field had to insert for sorting.");

        if (sort.Field is not null && sort.SortType.IsNullOrEmpty())
            throw new ArgumentException("SortType had to insert for sorting.");

        queryable = queryable.OrderBy($"{sort.Field} {Sorts.GetValueOrDefault(sort.SortType)}");

        return queryable;
    }
    private static void GenerateWhere(Filter filter, ref string whereFilter, ref List<object> whereValues)
    {
        whereValues.Add(filter.Value);
        int index = whereValues.Count - 1;
        string logicOp = filter.LogicOp is null ? "" : LogicalOperations.GetValueOrDefault(filter.LogicOp);

        string newFilter = Operations[filter.Operation].Replace("{{field}}", filter.Field).Replace("{{value}}", $"@{index}");

        whereFilter += $"{newFilter} {logicOp} ";

        if (filter.NextFilter is not null)
            GenerateWhere(filter.NextFilter, ref whereFilter, ref whereValues);
    }

}

namespace _2_Blog_CQRS.Common;

public static class EnumerableAsync
{
    public static async IAsyncEnumerable<U> SelectAsync<T, U>(this IEnumerable<T> source, Func<T, Task<U>> select)
    {
        foreach (var item in source)
            yield return await select(item);
    }

    public static async IAsyncEnumerable<U> SelectManyAsync<T, U>(this IAsyncEnumerable<T> source, Func<T, IEnumerable<U>> select)
    {
        await foreach (var item in source)
            foreach (var subItem in select(item))
                yield return subItem;
    }

    public static async IAsyncEnumerable<T> WhereAsync<T>(this IAsyncEnumerable<T> source, Func<T, bool> predicate)
    {
        await foreach (var item in source)
            if (predicate(item))
                yield return item;
    }

    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> source)
    {
        List<T> result = new();
        await foreach (var item in source)
            result.Add(item);

        return result;
    }
}
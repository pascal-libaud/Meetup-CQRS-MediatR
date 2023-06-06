using System.Diagnostics;

namespace _1_Blog_CQRS_Less.Services;

public class PostService
{
    private static T MeasurePerformances<T>(Func<T> func)
    {
        Stopwatch sw = Stopwatch.StartNew();
        try
        {
            return func();
        }
        finally
        {
            sw.Stop();
            if (sw.ElapsedMilliseconds > 500)
                Console.WriteLine("Patin couffin");
        }
    }

    public Post[] GetPosts()
    {
        return MeasurePerformances(() => _GetPosts());
    }

    private Post[] _GetPosts()
    {
        return null;
    }
}
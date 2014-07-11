namespace Strathweb.TypedRouting
{
    public static class Param
    {
        public static TValue Any<TValue>()
        {
            return default(TValue);
        }
    }
}
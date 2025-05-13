namespace HC.Application.Common.Extensions;

public static class MathExtensions
{
    public static decimal PercentageOf(this int currentCount, int totalCount)
    {
        if (currentCount > totalCount)
        {
            return 100;
        }

        if (totalCount == 0 || currentCount == 0)
        {
            return 0;
        }

        return ((currentCount * 1m) / (totalCount * 1m)) * 100;
    }
}

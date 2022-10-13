using System.Collections.Generic;

namespace yield
{
    public static class MovingAverageTask
    {
        public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
        {
            double changedY;
            DataPoint result;
            int length = 0;
            Queue<DataPoint> queue = new Queue<DataPoint>();
            var summ = 0.0;
            foreach (var point in data)
            {
                summ += point.OriginalY;
                if (length >= windowWidth)
                    summ -= queue.Dequeue().OriginalY;
                else length++;
                changedY = summ / length;
                result = point.WithAvgSmoothedY(changedY);
                queue.Enqueue(result);
                yield return result;
            }
        }
    }
}
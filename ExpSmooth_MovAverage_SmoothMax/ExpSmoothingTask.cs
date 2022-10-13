using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
			DataPoint changedPoint = null;
			double smoothedY;
			foreach(var point in data)
            {
				if (changedPoint == null)
				{
					smoothedY = point.OriginalY;
					changedPoint = new DataPoint(point.WithExpSmoothedY(smoothedY));
					yield return changedPoint;
					continue;
				}
				smoothedY = alpha * point.OriginalY + (1 - alpha) * changedPoint.ExpSmoothedY;
				changedPoint = new DataPoint(point.WithExpSmoothedY(smoothedY)); 
				yield return point.WithExpSmoothedY(smoothedY);
            }
			
		}
	}
}
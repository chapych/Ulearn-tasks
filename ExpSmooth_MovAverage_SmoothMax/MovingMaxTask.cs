using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
			Queue<DataPoint> queue = new Queue<DataPoint>();
			LinkedList<double> possibleMax = new LinkedList<double>();
			int length = 0;
			double changedY;
			DataPoint dequeued;
			foreach (var point in data)
			{
				queue.Enqueue(point);
				if (length >= windowWidth)
				{
					dequeued = queue.Dequeue();
					if (dequeued.OriginalY == possibleMax.First.Value)
						possibleMax.RemoveFirst();
				}
				else length++;
				PushingToList(point, possibleMax);
				changedY = possibleMax.First.Value;
				yield return point.WithMaxY(changedY);
			}
		}

		public static void PushingToList(DataPoint element, LinkedList<double> list)
		{
			double yValue = element.OriginalY;
			var comparingElement = list.Last;
			if (list.Count == 0)
			{
				list.AddFirst(yValue);
				return;
			}
			while (true)
			{
				if (yValue <= comparingElement.Value)
					break;
				if (comparingElement.Previous == null)
				{
					list.Clear();
					list.AddFirst(yValue);
					return;
				}
				comparingElement = comparingElement.Previous;
			}
			var newElement = list.AddAfter(comparingElement, yValue);
			RemovingAfter(newElement, list);
		}

		public static void RemovingAfter<T>(LinkedListNode<T> lastNode, LinkedList<T> list)
		{
			var current = list.Last;
			while (!current.Equals(lastNode))
			{
				current = current.Previous;
				list.RemoveLast();
			}
		}
	}
}
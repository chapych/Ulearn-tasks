using System;
using System.Collections.Generic;

namespace Clones
{
	public class Item<T>
	{
		public object Value { get; set; }
		public Item<T> Previous { get; set; }
	}

	public class LinkedStack<T>
	{
		public Item<T> tail { get; set; }

		public LinkedStack(object value)
		{
			tail = new Item<T> { Value = value, Previous = null };
		}

		public LinkedStack(LinkedStack<T> other)
		{
			var copyingTail = other.tail;
			//if(copyingTail == null)
   //         {
			//	tail = new Item<T> { Value = null, Previous = null };
			//}
			tail = new Item<T> { Value = copyingTail.Value, Previous = copyingTail.Previous };
		}

		public LinkedStack()
		{
			tail = new Item<T> { Value = null, Previous = null };
		}

		public void Push(object value)
		{
			if (tail == null)
			{
				tail = new Item<T> { Value = value, Previous = null };
			}
			else
			{
				var oldTail = tail;
				tail = new Item<T> { Value = value, Previous = oldTail };
			}
		}

		public object Pop()
		{
			if (tail == null) throw new Exception("Empty stack");
			var oldTail = tail;
			tail = tail.Previous;
			return oldTail.Value;
		}

	}

	public class Clone
	{
		public int Number { get; set; } // clone number
		public LinkedStack<string> Courses { get; set; }
		public LinkedStack<string> ForgottenCourses { get; set; }

		public Clone(int number)
		{
			Number = number;
			Courses = new LinkedStack<string>("basic");
			ForgottenCourses = new LinkedStack<string>();
		}

		public Clone(Clone other)
		{
			Number = other.Number + 1;
			Courses = new LinkedStack<string>(other.Courses); //
			ForgottenCourses = new LinkedStack<string>(other.ForgottenCourses);
		}

		public void Learn(string name)
		{
			Courses.Push(name);
		}

		public void Rollback()
		{
			var forgottenCourse = Courses.Pop();
			ForgottenCourses.Push(forgottenCourse);
		}

		public void Relearn()
		{
			var newOldCourse = ForgottenCourses.Pop();
			Courses.Push(newOldCourse);
		}

		public string Check()
		{
			var result = Courses.Pop();
			Courses.Push(result);
			return result.ToString();
		}
	}

	public class CloneVersionSystem : ICloneVersionSystem
	{
		List<Clone> clones = new List<Clone>();
		public string Execute(string query)
		{
			
			var commands = query.Split(' ');
			if (commands.Length == 0) throw new ArgumentException("Empty string");
			var action = commands[0];
			var cloneName = Int32.Parse(commands[1]);
			string course;

			if(cloneName > clones.Count)
            {
				clones.Add(new Clone(cloneName));
            }

			if(action == "learn")
            {
				course = commands[2];
				clones[cloneName - 1].Learn(course);
				return null;

			}

			if(action == "rollback")
            {
				clones[cloneName - 1].Rollback();
				return null;
			}

			if(action == "relearn")
            {
				clones[cloneName - 1].Relearn();
				return null;
			}

			if(action == "clone")
            {
				var original = clones[cloneName - 1];
				clones.Add(new Clone(original));
				return null;
			}

			if(action == "check")
            {
				return clones[cloneName - 1].Check();
            }

			return null;

		}
	}
}

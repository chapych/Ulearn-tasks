using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		Dictionary<char, Action<IVirtualMachine>> ActionDictionary { get; set; }

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			InstructionPointer = 0;
			Memory = new byte[memorySize];
			MemoryPointer = 0;
			ActionDictionary = new Dictionary<char, Action<IVirtualMachine>>();
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			ActionDictionary[symbol] = execute;
		}

		public void Run()
		{
			while(InstructionPointer < Instructions.Length)
            {
				int pointer = InstructionPointer;
				var current = Instructions[pointer];
				if (!ActionDictionary.ContainsKey(current))
				{
					InstructionPointer++;
					continue;
				}
				var action = ActionDictionary[current];
				action(this);
				InstructionPointer++;
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b =>
			{
				write((char)b.Memory[b.MemoryPointer]);
			}
			);
			vm.RegisterCommand('+', b =>
			{
				var array = b.Memory;
				var pointer = b.MemoryPointer;
				if (array[pointer] == 255)
				{
					array[pointer] = 0; ;
					return;
				}
				array[pointer]++;
			}
			);
			vm.RegisterCommand('-', b =>
			{
				var array = b.Memory;
				var pointer = b.MemoryPointer;
				if (array[pointer] == 0)
				{
					array[pointer] = 255; ;
					return;
				}
				array[pointer]--;
			}
			);
			vm.RegisterCommand('>', b =>
			{
				if (b.MemoryPointer == b.Memory.Length - 1)
				{
					b.MemoryPointer = 0;
					return;
				}
				b.MemoryPointer++;
			}
			);
			vm.RegisterCommand('<', b =>
			{
				if (b.MemoryPointer == 0)
				{
					b.MemoryPointer = b.Memory.Length - 1;
					return;
				}
				b.MemoryPointer--;
			}
			);
			vm.RegisterCommand(',', b =>
			{
				var symbol = read();
				b.Memory[b.MemoryPointer] = (byte)symbol;
			}
			);

			for (char c = 'A'; c <= 'Z'; c++)
			{
				var current = c;
				vm.RegisterCommand(current, b => b.Memory[b.MemoryPointer] = (byte)current);
			}

			for(char c = 'a'; c <='z'; c++)
            {
				var current = c;
				vm.RegisterCommand(current, b => b.Memory[b.MemoryPointer] = (byte)current);
			}

			for(char i = '0'; i <= '9'; i++)
            {
				var current = i;
				vm.RegisterCommand(current, b => b.Memory[b.MemoryPointer] = (byte)current);
			}
		}
	}
}
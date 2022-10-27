using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		static Dictionary<int, int> dictionary;
        public static void RegisterTo(IVirtualMachine vm)
		{
			dictionary = FindAllBracketsPairs(vm);
			vm.RegisterCommand('[', b => 
			{
				if (b.Memory[b.MemoryPointer] == 0)
                {
					b.InstructionPointer = dictionary[b.InstructionPointer];
                    
                }
			});
			vm.RegisterCommand(']', b => 
			{
				if (b.Memory[b.MemoryPointer] != 0)
				{
					b.InstructionPointer = dictionary[b.InstructionPointer];
                    
                }
            }
			);
		}
		public static Dictionary<int, int> FindAllBracketsPairs(IVirtualMachine vm)
        {
			Stack<int> stack = new Stack<int>();
			var length = vm.Instructions.Length;
			var dictionary = new Dictionary<int, int>(); 
			for (int i = 0; i < length; i++)
            {
				if (vm.Instructions[i] == '[')
				{
					stack.Push(i);
					continue;
				}
				if(vm.Instructions[i] == ']')
                {
					var positionOfOpeningBracket = stack.Pop();
					dictionary.Add(positionOfOpeningBracket, i);
					dictionary.Add(i, positionOfOpeningBracket);
                }
            }
			return dictionary;
        }
		
	}
}
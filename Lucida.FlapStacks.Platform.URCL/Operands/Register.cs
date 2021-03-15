namespace Lucida.FlapStacks.Platform.URCL.Operands
{
	public class Register : Operand
	{
		public ulong Index { get; }

		public override ulong MaxRegister => Index;

		public override Value Value => new Constant(MaxRegister);

		public Register() { }

		public Register(ulong index)
		{
			Index = index;
		}

		public override string GetString()
		{
			return $"R{Index}";
		}

		public override void Pop(Emitter e)
		{
			if (Index == 0)
			{
				e.Pop();
			}
			else
			{
				e.Push(new Constant(Index + 1));
				e.StoreHeap();
			}
		}

		public override void Push(Emitter e)
		{
			if (Index == 0)
			{
				e.PushZero();
			}
			else
			{
				e.Push(new Constant(Index + 1));
				e.LoadHeap();
			}
		}

		public override bool TryParse(string str, out Operand operand)
		{
			str = str.ToUpper();

			if ((str.StartsWith("R") || str.StartsWith("$")) && str.Length > 1 && ulong.TryParse(str.Substring(1), out ulong index))
			{
				operand = new Register(index);
				return true;
			}
			else
			{
				operand = null;
				return false;
			}
		}
	}
}

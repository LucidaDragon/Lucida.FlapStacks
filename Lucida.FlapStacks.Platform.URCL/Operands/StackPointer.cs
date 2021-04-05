namespace Lucida.FlapStacks.Platform.URCL.Operands
{
	public class StackPointer : Operand
	{
		public override Value Value => new Constant(0);

		public override string GetString()
		{
			return "SP";
		}

		public override void Pop(Emitter e)
		{
			e.PopBasePointer();
		}

		public override void Push(Emitter e)
		{
			e.PushBasePointer();
		}

		public override bool TryParse(string str, out Operand operand)
		{
			if (str.ToUpper() == "SP")
			{
				operand = new StackPointer();
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

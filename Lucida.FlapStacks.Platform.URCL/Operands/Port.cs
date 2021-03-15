namespace Lucida.FlapStacks.Platform.URCL.Operands
{
	public class Port : Operand
	{
		public ulong Tag { get; }

		public override Value Value => new Constant(Tag);

		public Port() { }

		public Port(ulong tag)
		{
			Tag = tag;
		}

		public override string GetString()
		{
			return $"%{Tag}";
		}

		public override void Pop(Emitter e)
		{
			e.WriteDevice(Tag);
		}

		public override void Push(Emitter e)
		{
			e.ReadDevice(Tag);
		}

		public override bool TryParse(string str, out Operand operand)
		{
			if (str.ToUpper().StartsWith("%") && str.Length > 1 && ulong.TryParse(str.Substring(1), out ulong portValue))
			{
				operand = new Port(portValue);
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

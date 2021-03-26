namespace Lucida.FlapStacks.Platform.URCL.Operands
{
	public class PragmaVar : Operand
	{
		public override Value Value => Child.Value;

		public string Name { get; private set; }

		private Operand Child;

		public PragmaVar() { }

		public PragmaVar(string name)
		{
			Name = name;
		}

		public override string GetString()
		{
			return Child.GetString();
		}

		public override void Pop(Emitter e)
		{
			Child.Pop(e);
		}

		public override void Push(Emitter e)
		{
			Child.Push(e);
		}

		public override bool Validate(Parser parser)
		{
			return parser.Variables.TryGetValue(Name, out Child);
		}

		public override bool TryParse(string str, out Operand operand)
		{
			if (str.StartsWith("@") && str.Length > 1)
			{
				operand = new PragmaVar(str.Substring(1));
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

namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class PragmaVarDef : Instruction
	{
		public override string Keyword => $"@{Name}";

		protected override int OperandCount => 1;

		private readonly string Name;

		public PragmaVarDef() { }

		public PragmaVarDef(string name)
		{
			Name = name;
		}

		protected override Instruction CreateNew(string keyword)
		{
			return new PragmaVarDef(keyword.Substring(1));
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e) { }

		public override Instruction Create(Parser parser, UrclConfig config, string keyword, Operand[] operands)
		{
			parser.Variables.Add(keyword.Substring(1), operands[0]);

			var result = CreateNew(keyword);

			result.Operands[0] = operands[0];

			return result;
		}

		public override bool IsValid(string keyword, int operands)
		{
			return keyword.StartsWith("@") && operands == 1;
		}
	}
}

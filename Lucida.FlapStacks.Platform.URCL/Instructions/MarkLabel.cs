namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class MarkLabel : Instruction
	{
		public override string Keyword => "mark";

		protected override int OperandCount => 0;

		private readonly Label Label;

		public MarkLabel() { }

		public MarkLabel(Label label)
		{
			Label = label;
		}

		protected override Instruction CreateNew(string keyword)
		{
			return null;
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Label.Mark(e);
		}

		public override bool IsValid(string keyword, int operands)
		{
			return false;
		}

		public override string GetString()
		{
			return $".{Label.Name}";
		}
	}
}

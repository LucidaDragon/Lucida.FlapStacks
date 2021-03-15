namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Div : Instruction
	{
		public override string Keyword => "div";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Div();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Divide();
			SaveResult(e);
		}
	}
}

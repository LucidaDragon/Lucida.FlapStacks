namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Xnor : Instruction
	{
		public override string Keyword => "xnor";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Xnor();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Xor();
			e.Not();
			SaveResult(e);
		}
	}
}

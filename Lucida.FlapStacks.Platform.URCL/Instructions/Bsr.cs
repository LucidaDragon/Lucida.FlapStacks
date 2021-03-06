namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Bsr : Instruction
	{
		public override string Keyword => "bsr";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Bsr();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.LeftShift();
			SaveResult(e);
		}
	}
}

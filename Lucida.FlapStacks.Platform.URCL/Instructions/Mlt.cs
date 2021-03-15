namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Mlt : Instruction
	{
		public override string Keyword => "mlt";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Mlt();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Multiply();
			SaveResult(e);
		}
	}
}

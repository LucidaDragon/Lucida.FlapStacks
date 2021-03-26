namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Hlt : Instruction
	{
		public override string Keyword => "hlt";

		protected override int OperandCount => 0;

		protected override Instruction CreateNew(string keyword)
		{
			return new Hlt();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Core();
			e.Stop();
		}
	}
}

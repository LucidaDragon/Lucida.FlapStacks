namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Ret : Instruction
	{
		public override string Keyword => "ret";

		protected override int OperandCount => 0;

		protected override Instruction CreateNew(string keyword)
		{
			return new Ret();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Goto();
		}
	}
}

namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Nand : Instruction
	{
		public override string Keyword => "nand";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Nand();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.And();
			e.Not();
			SaveResult(e);
		}
	}
}

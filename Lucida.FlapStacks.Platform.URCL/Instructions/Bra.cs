namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Bra : Instruction
	{
		public override string Keyword => "bra";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Bra();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Goto();
		}
	}
}

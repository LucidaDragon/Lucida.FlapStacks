namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Nop : Instruction
	{
		public override string Keyword => "nop";

		protected override int OperandCount => 0;

		protected override Instruction CreateNew(string keyword)
		{
			return new Nop();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }
	}
}

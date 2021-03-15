namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Dw : Instruction
	{
		public override string Keyword => "dw";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Dw();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.WriteWord(Operands[0].Value);
		}
	}
}

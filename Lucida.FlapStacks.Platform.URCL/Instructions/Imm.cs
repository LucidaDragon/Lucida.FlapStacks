namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Imm : Instruction
	{
		public override string Keyword => "imm";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Imm();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }
	}
}

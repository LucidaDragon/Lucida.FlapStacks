namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Mov : Instruction
	{
		public override string Keyword => "mov";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Mov();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }
	}
}

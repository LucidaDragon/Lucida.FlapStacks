namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Out : Instruction
	{
		public override string Keyword => "out";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Out();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }
	}
}

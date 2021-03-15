namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Jmp : Instruction
	{
		public override string Keyword => "jmp";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Jmp();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Goto();
		}
	}
}

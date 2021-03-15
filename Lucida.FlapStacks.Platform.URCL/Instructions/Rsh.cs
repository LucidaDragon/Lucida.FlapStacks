namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Rsh : Instruction
	{
		public override string Keyword => "rsh";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Rsh();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Push(new Constant(1));
			e.RightShift();
			SaveResult(e);
		}
	}
}

namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Lsh : Instruction
	{
		public override string Keyword => "lsh";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Lsh();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Push(new Constant(1));
			e.LeftShift();
			SaveResult(e);
		}
	}
}

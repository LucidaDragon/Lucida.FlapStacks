namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class LshInst : Instruction
	{
		public override string Keyword => "lsh";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.LeftShift();
		}

		protected override Instruction CreateNew()
		{
			return new LshInst();
		}
	}
}

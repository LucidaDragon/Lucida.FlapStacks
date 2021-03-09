namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class RshInst : Instruction
	{
		public override string Keyword => "rsh";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.RightShift();
		}

		protected override Instruction CreateNew()
		{
			return new RshInst();
		}
	}
}

namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class SbbInst : Instruction
	{
		public override string Keyword => "sbb";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.SubtractWithBorrow();
		}

		protected override Instruction CreateNew()
		{
			return new SbbInst();
		}
	}
}

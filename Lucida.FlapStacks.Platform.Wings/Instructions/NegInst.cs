namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class NegInst : Instruction
	{
		public override string Keyword => "neg";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Negate();
		}

		protected override Instruction CreateNew()
		{
			return new NegInst();
		}
	}
}

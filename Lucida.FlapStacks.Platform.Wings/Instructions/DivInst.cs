namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class DivInst : Instruction
	{
		public override string Keyword => "div";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Divide();
		}

		protected override Instruction CreateNew()
		{
			return new DivInst();
		}
	}
}

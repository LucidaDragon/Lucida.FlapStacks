namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class RemInst : Instruction
	{
		public override string Keyword => "rem";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Remainder();
		}

		protected override Instruction CreateNew()
		{
			return new RemInst();
		}
	}
}

namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class NotInst : Instruction
	{
		public override string Keyword => "not";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Not();
		}

		protected override Instruction CreateNew()
		{
			return new NotInst();
		}
	}
}

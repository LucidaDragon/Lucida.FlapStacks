namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class AndInst : Instruction
	{
		public override string Keyword => "and";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.And();
		}

		protected override Instruction CreateNew()
		{
			return new AndInst();
		}
	}
}

namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class OrInst : Instruction
	{
		public override string Keyword => "or";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Or();
		}

		protected override Instruction CreateNew()
		{
			return new OrInst();
		}
	}
}

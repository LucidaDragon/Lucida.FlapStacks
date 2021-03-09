namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class GotoInst : Instruction
	{
		public override string Keyword => "goto";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Goto();
		}

		protected override Instruction CreateNew()
		{
			return new GotoInst();
		}
	}
}

namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class StartInst : Instruction
	{
		public override string Keyword => "start";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Start();
		}

		protected override Instruction CreateNew()
		{
			return new StartInst();
		}
	}
}

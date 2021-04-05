namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class StopInst : Instruction
	{
		public override string Keyword => "stop";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Stop();
		}

		protected override Instruction CreateNew()
		{
			return new StopInst();
		}
	}
}

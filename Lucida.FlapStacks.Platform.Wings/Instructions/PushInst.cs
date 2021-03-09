namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class PushInst : Instruction
	{
		public override string Keyword => "push";

		protected override int ArgumentCount => 1;

		public override void Emit(Emitter emitter)
		{
			emitter.Push(Arguments[0]);
		}

		protected override Instruction CreateNew()
		{
			return new PushInst();
		}
	}
}

namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class PopInst : Instruction
	{
		public override string Keyword => "pop";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Pop();
		}

		protected override Instruction CreateNew()
		{
			return new PopInst();
		}
	}
}

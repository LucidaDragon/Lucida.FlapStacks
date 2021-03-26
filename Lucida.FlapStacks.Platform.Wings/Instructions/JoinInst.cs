namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class JoinInst : Instruction
	{
		public override string Keyword => "join";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Join();
		}

		protected override Instruction CreateNew()
		{
			return new JoinInst();
		}
	}
}

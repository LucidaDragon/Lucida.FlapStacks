namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class LockInst : Instruction
	{
		public override string Keyword => "lock";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Lock();
		}

		protected override Instruction CreateNew()
		{
			return new LockInst();
		}
	}
}

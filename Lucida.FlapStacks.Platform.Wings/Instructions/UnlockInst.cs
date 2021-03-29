namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class UnlockInst : Instruction
	{
		public override string Keyword => "unlock";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Unlock();
		}

		protected override Instruction CreateNew()
		{
			return new UnlockInst();
		}
	}
}

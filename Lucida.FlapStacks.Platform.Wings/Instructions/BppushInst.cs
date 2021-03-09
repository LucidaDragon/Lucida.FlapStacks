namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class BppushInst : Instruction
	{
		public override string Keyword => "bppush";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.PushBasePointer();
		}

		protected override Instruction CreateNew()
		{
			return new BppushInst();
		}
	}
}

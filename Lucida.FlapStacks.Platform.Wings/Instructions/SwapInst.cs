namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class SwapInst : Instruction
	{
		public override string Keyword => "swap";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Swap();
		}

		protected override Instruction CreateNew()
		{
			return new SwapInst();
		}
	}
}

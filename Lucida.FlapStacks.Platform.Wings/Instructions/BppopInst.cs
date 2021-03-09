namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class BppopInst : Instruction
	{
		public override string Keyword => "bppop";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.PopBasePointer();
		}

		protected override Instruction CreateNew()
		{
			return new BppopInst();
		}
	}
}

namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class StrInst : Instruction
	{
		public override string Keyword => "str";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.StoreHeap();
		}

		protected override Instruction CreateNew()
		{
			return new StrInst();
		}
	}
}

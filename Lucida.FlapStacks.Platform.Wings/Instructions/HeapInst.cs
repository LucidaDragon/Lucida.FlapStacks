namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class HeapInst : Instruction
	{
		public override string Keyword => "heap";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.MaxHeap();
		}

		protected override Instruction CreateNew()
		{
			return new HeapInst();
		}
	}
}

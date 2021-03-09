namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	class LodInst : Instruction
	{
		public override string Keyword => "lod";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.LoadHeap();
		}

		protected override Instruction CreateNew()
		{
			return new LodInst();
		}
	}
}

namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Lod : Instruction
	{
		public override string Keyword => "lod";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Lod();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Push(config.HeapOffset);
			e.Add();
			e.LoadHeap();
		}
	}
}

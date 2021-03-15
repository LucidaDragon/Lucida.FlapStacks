namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Str : Instruction
	{
		public override string Keyword => "str";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Str();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[1].Push(e);
			Operands[0].Push(e);
			e.Push(config.HeapOffset);
			e.Add();
			e.StoreHeap();
		}
	}
}

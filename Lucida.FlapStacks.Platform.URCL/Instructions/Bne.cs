namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Bne : Instruction
	{
		public override string Keyword => "bne";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Bne();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[1].Push(e);
			Operands[2].Push(e);
			Operands[0].Push(e);

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchNotEqual();

			e.MarkLabel(onFalse);
		}
	}
}

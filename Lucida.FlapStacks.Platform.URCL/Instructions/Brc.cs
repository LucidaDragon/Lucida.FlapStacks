namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Brc : Instruction
	{
		public override string Keyword => "brc";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Brc();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			LoadCarry(e);

			Operands[0].Push(e);

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchNotZero();

			e.MarkLabel(onFalse);
		}
	}
}

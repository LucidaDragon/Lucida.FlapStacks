namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Bnc : Instruction
	{
		public override string Keyword => "bnc";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Bnc();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			LoadCarry(e);

			Operands[0].Push(e);

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchZero();

			e.MarkLabel(onFalse);
		}
	}
}

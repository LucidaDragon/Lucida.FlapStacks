namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Brz : Instruction
	{
		public override string Keyword => "brz";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Brz();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			LoadResult(e);
			e.Swap();

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchZero();

			e.MarkLabel(onFalse);
		}

		public override void Emit(UrclConfig config, Emitter e)
		{
			LoadResult(e);

			Operands[0].Push(e);

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchZero();

			e.MarkLabel(onFalse);
		}
	}
}

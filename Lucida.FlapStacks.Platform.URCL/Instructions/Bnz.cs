namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Bnz : Instruction
	{
		public override string Keyword => "bnz";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Bnz();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			LoadResult(e);

			Operands[0].Push(e);

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchNotZero();

			e.MarkLabel(onFalse);
		}
	}
}

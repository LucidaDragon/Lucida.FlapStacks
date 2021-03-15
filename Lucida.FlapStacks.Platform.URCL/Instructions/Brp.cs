namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Brp : Instruction
	{
		public override string Keyword => "brp";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Brp();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			LoadResult(e);
			e.PushZero();

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			Operands[0].Push(e);

			e.BranchSignedLess();

			e.MarkLabel(onFalse);
		}
	}
}

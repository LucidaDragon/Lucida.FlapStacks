namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Brn : Instruction
	{
		public override string Keyword => "brn";

		protected override int OperandCount => 1;

		protected override Instruction CreateNew(string keyword)
		{
			return new Brn();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			LoadResult(e);
			e.PushZero();

			Operands[0].Push(e);

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchSignedLess();

			e.MarkLabel(onFalse);
		}
	}
}

namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Bod : Instruction
	{
		public override string Keyword => "bod";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Bod();
		}

		protected override void EmitCore(UrclConfig config, Emitter e) { }

		public override void Emit(UrclConfig config, Emitter e)
		{
			Operands[1].Push(e);
			e.Push(1);
			e.And();

			Operands[0].Push(e);

			var onFalse = e.CreateLabel();
			e.Push(onFalse);

			e.BranchNotZero();

			e.MarkLabel(onFalse);
		}
	}
}

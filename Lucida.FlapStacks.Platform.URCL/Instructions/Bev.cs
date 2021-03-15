namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Bev : Instruction
	{
		public override string Keyword => "bev";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Bev();
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

			e.BranchZero();

			e.MarkLabel(onFalse);
		}
	}
}

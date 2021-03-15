namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Sbb : Instruction
	{
		public override string Keyword => "sbb";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Sbb();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			var onPrevBorrow = e.CreateLabel();
			var onPrevNotBorrow = e.CreateLabel();

			var onBorrow = e.CreateLabel();
			var onNotBorrow = e.CreateLabel();
			var done = e.CreateLabel();

			e.Push(onBorrow);
			e.Push(onNotBorrow);

			LoadCarry(e);
			e.Push(onPrevBorrow);
			e.Push(onPrevNotBorrow);
			e.BranchNotZero();

			e.MarkLabel(onPrevNotBorrow);
			e.SubtractBorrow();

			e.MarkLabel(onPrevBorrow);
			e.SubtractWithBorrow();

			e.MarkLabel(onBorrow);
			SetCarry(e);
			e.Push(done);
			e.Goto();

			e.MarkLabel(onNotBorrow);
			ClearCarry(e);

			e.MarkLabel(done);

			SaveResult(e);
		}
	}
}

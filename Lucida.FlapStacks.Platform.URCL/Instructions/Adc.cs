namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Adc : Instruction
	{
		public override string Keyword => "adc";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Adc();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			var onPrevCarry = e.CreateLabel();
			var onPrevNotCarry = e.CreateLabel();

			var onCarry = e.CreateLabel();
			var onNotCarry = e.CreateLabel();
			var done = e.CreateLabel();

			e.Push(onCarry);
			e.Push(onNotCarry);

			LoadCarry(e);
			e.Push(onPrevCarry);
			e.Push(onPrevNotCarry);
			e.BranchNotZero();

			e.MarkLabel(onPrevNotCarry);
			e.AddCarry();

			e.MarkLabel(onPrevCarry);
			e.AddWithCarry();

			e.MarkLabel(onCarry);
			SetCarry(e);
			e.Push(done);
			e.Goto();

			e.MarkLabel(onNotCarry);
			ClearCarry(e);

			e.MarkLabel(done);

			SaveResult(e);
		}
	}
}

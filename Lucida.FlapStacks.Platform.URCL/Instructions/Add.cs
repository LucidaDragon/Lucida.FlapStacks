namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Add : Instruction
	{
		public override string Keyword => "add";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Add();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			var onCarry = e.CreateLabel();
			var onNotCarry = e.CreateLabel();
			var done = e.CreateLabel();

			e.Push(onCarry);
			e.Push(onNotCarry);
			e.AddCarry();

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

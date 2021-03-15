namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Inc : Instruction
	{
		public override string Keyword => "inc";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Inc();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			var onCarry = e.CreateLabel();
			var onNotCarry = e.CreateLabel();
			var done = e.CreateLabel();

			e.Push(new Constant(1));
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

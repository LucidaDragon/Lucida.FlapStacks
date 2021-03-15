namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Sub : Instruction
	{
		public override string Keyword => "sub";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Sub();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			var onBorrow = e.CreateLabel();
			var onNotBorrow = e.CreateLabel();
			var done = e.CreateLabel();

			e.Push(onBorrow);
			e.Push(onNotBorrow);
			e.SubtractBorrow();

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

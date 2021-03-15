namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Setl : Instruction
	{
		public override string Keyword => "setl";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Setl();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			var onTrue = e.CreateLabel();
			var onFalse = e.CreateLabel();
			var done = e.CreateLabel();

			e.Push(onTrue);
			e.Push(onFalse);
			e.BranchUnsignedLess();

			e.MarkLabel(onTrue);
			e.Push(1);
			e.Push(done);
			e.Goto();

			e.MarkLabel(onFalse);
			e.PushZero();

			e.MarkLabel(done);
		}
	}
}

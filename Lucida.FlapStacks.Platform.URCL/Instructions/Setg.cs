namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Setg : Instruction
	{
		public override string Keyword => "setg";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Setg();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			var onTrue = e.CreateLabel();
			var onFalse = e.CreateLabel();
			var done = e.CreateLabel();

			e.Push(onTrue);
			e.Push(onFalse);
			e.BranchUnsignedGreater();

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

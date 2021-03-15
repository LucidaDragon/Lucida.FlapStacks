namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Or : Instruction
	{
		public override string Keyword => "or";

		protected override int OperandCount => 3;

		protected override Instruction CreateNew(string keyword)
		{
			return new Or();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Or();
			SaveResult(e);
		}
	}
}

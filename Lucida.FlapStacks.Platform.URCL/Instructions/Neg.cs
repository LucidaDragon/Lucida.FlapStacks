namespace Lucida.FlapStacks.Platform.URCL.Instructions
{
	public class Neg : Instruction
	{
		public override string Keyword => "neg";

		protected override int OperandCount => 2;

		protected override Instruction CreateNew(string keyword)
		{
			return new Neg();
		}

		protected override void EmitCore(UrclConfig config, Emitter e)
		{
			e.Negate();
			SaveResult(e);
		}
	}
}

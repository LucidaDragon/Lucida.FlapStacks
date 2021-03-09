namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class MulInst : Instruction
	{
		public override string Keyword => "mul";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Multiply();
		}

		protected override Instruction CreateNew()
		{
			return new MulInst();
		}
	}
}

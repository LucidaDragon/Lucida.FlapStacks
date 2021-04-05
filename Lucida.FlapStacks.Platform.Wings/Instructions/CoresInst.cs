namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class CoresInst : Instruction
	{
		public override string Keyword => "cores";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Cores();
		}

		protected override Instruction CreateNew()
		{
			return new CoresInst();
		}
	}
}

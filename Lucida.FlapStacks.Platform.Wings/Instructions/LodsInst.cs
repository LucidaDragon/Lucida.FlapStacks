namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class LodsInst : Instruction
	{
		public override string Keyword => "lods";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.LoadStack();
		}

		protected override Instruction CreateNew()
		{
			return new LodsInst();
		}
	}
}

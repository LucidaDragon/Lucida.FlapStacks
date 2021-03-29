namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class BitsInst : Instruction
	{
		public override string Keyword => "bits";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.Bits();
		}

		protected override Instruction CreateNew()
		{
			return new BitsInst();
		}
	}
}

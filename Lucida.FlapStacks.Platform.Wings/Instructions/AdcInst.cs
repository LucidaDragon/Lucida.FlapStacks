namespace Lucida.FlapStacks.Platform.Wings.Instructions
{
	public class AdcInst : Instruction
	{
		public override string Keyword => "adc";

		protected override int ArgumentCount => 0;

		public override void Emit(Emitter emitter)
		{
			emitter.AddWithCarry();
		}

		protected override Instruction CreateNew()
		{
			return new AdcInst();
		}
	}
}

using Lucida.FlapStacks.Platform.x86_16.Ops;

namespace Lucida.FlapStacks.Platform.x86_16
{
	public class Label : Value
	{
		public LabelOp Location { get; private set; } = null;

		private Emitter8086 Target;

		public void Mark(Emitter8086 target)
		{
			Location = new LabelOp();
			Target = target;
			Target.Emit(Location);
		}

		public override ulong Get()
		{
			return Target.GetAddress(Location) + Target.StartOffset;
		}
	}
}

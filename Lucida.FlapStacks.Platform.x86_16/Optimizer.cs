namespace Lucida.FlapStacks.Platform.x86_16
{
	public abstract class Optimizer
	{
		public abstract int MinLength { get; }

		public abstract bool Apply(List<Op> ops, int index);
	}
}

using Lucida.FlapStacks.Platform.x86_16.Ops;

namespace Lucida.FlapStacks.Platform.x86_16.Optimizers
{
	public class MoveToSelf : Optimizer
	{
		public override int MinLength => 1;

		public override bool Apply(List<Op> ops, int index)
		{
			if (ops[index] is MovOp mov && mov.Source == mov.Target)
			{
				ops.RemoveAt(index);
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}

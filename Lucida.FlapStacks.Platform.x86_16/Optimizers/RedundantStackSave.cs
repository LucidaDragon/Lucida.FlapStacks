using Lucida.FlapStacks.Platform.x86_16.Ops;

namespace Lucida.FlapStacks.Platform.x86_16.Optimizers
{
	public class RedundantStackSave : Optimizer
	{
		public override int MinLength => 3;

		public override bool Apply(List<Op> ops, int index)
		{
			if (ops[index] is PushOp push && ops[index + 2] is PopOp pop && push.Source == pop.Target)
			{
				var op = ops[index + 1];

				if (op is StoreOp || op is StoreAddrAxOp)
				{
					ops.RemoveAt(index + 2);
					ops.RemoveAt(index);
					return true;
				}
			}

			return false;
		}
	}
}

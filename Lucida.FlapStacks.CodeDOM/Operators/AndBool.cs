using Lucida.FlapStacks.CodeDOM.Types;

namespace Lucida.FlapStacks.CodeDOM.Operators
{
	public class AndBool : BinaryOperator
	{
		public override string Name => "&&";

		public override Type AType => new BoolType();
		public override Type BType => new BoolType();

		public override Type ResultType => new BoolType();

		public override void Emit(Emitter emitter)
		{
			emitter.And();
		}

		public override Value Compute(Value a, Value b)
		{
			return new Constant((a.Get() != 0 && b.Get() != 0) ? 1 : 0);
		}
	}
}

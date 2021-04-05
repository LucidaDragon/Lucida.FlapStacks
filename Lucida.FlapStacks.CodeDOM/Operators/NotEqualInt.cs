using Lucida.FlapStacks.CodeDOM.Types;

namespace Lucida.FlapStacks.CodeDOM.Operators
{
	public class NotEqualInt : BinaryOperator
	{
		public override string Name => "!=";

		public override Type AType => new NativeIntType();
		public override Type BType => new NativeIntType();

		public override Type ResultType => new BoolType();

		public override void Emit(Emitter emitter)
		{
			emitter.Subtract();
			emitter.Bool();
		}

		public override Value Compute(Value a, Value b)
		{
			return new Constant(a.Get() != b.Get() ? 1 : 0);
		}
	}
}
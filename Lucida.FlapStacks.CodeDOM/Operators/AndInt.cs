using Lucida.FlapStacks.CodeDOM.Types;

namespace Lucida.FlapStacks.CodeDOM.Operators
{
	public class AndInt : BinaryOperator
	{
		public override string Name => "&";

		public override Type AType => new NativeIntType();
		public override Type BType => new NativeIntType();

		public override Type ResultType => new NativeIntType();

		public override void Emit(Emitter emitter)
		{
			emitter.And();
		}

		public override Value Compute(Value a, Value b)
		{
			return new Constant(a.Get() & b.Get());
		}
	}
}

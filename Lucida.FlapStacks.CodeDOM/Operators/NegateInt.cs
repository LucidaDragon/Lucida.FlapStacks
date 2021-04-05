using Lucida.FlapStacks.CodeDOM.Types;

namespace Lucida.FlapStacks.CodeDOM.Operators
{
	public class NegateInt : UnaryOperator
	{
		public override string Name => "-";

		public override Type SourceType => new NativeIntType();

		public override Type ResultType => new NativeIntType();

		public override void Emit(Emitter emitter)
		{
			emitter.Negate();
		}

		public override Value Compute(Value value)
		{
			return new Constant(-(long)value.Get());
		}
	}
}

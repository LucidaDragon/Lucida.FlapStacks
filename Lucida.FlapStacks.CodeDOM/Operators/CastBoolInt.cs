using Lucida.FlapStacks.CodeDOM.Types;

namespace Lucida.FlapStacks.CodeDOM.Operators
{
	public class CastBoolInt : UnaryOperator
	{
		public override string Name => "(int)";

		public override Type SourceType => new BoolType();

		public override Type ResultType => new NativeIntType();

		public override void Emit(Emitter emitter) { }

		public override Value Compute(Value value)
		{
			return new Constant(value.Get() != 0 ? 1 : 0);
		}
	}
}

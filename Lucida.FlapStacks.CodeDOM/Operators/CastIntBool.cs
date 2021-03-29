using Lucida.FlapStacks.CodeDOM.Types;

namespace Lucida.FlapStacks.CodeDOM.Operators
{
	public class CastIntBool : UnaryOperator
	{
		public override string Name => "(bool)";

		public override Type SourceType => new NativeIntType();

		public override Type ResultType => new BoolType();

		public override void Emit(Emitter emitter)
		{
			emitter.Bool();
		}

		public override Value Compute(Value value)
		{
			return new Constant(value.Get() != 0 ? 1 : 0);
		}
	}
}

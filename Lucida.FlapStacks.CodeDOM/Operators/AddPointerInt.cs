using Lucida.FlapStacks.CodeDOM.Types;

namespace Lucida.FlapStacks.CodeDOM.Operators
{
	public class AddPointerInt : BinaryOperator
	{
		public override string Name => "+";

		public override Type AType { get; }
		public override Type BType => new NativeIntType();

		public override Type ResultType { get; }

		public AddPointerInt(Type pointerType)
		{
			AType = new NativePointerType(pointerType);
			ResultType = new NativePointerType(pointerType);
		}

		public override void Emit(Emitter emitter)
		{
			emitter.Push(new Constant(AType.PointerType.Size));
			emitter.Multiply();
			emitter.Add();
		}

		public override Value Compute(Value a, Value b)
		{
			return new Constant(a.Get() + (b.Get() * AType.PointerType.Size));
		}
	}
}

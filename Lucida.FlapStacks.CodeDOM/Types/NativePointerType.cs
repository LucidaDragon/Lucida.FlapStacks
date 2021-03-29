namespace Lucida.FlapStacks.CodeDOM.Types
{
	public class NativePointerType : Type
	{
		public override ulong Size => 1;
		public override Type[] Fields { get => new Type[0]; set { } }
		public override Type PointerType { get; }

		public NativePointerType(Type type)
		{
			PointerType = type;
		}
	}
}

namespace Lucida.FlapStacks.CodeDOM.Types
{
	public class NativeIntType : Type
	{
		public override ulong Size => 1;
		public override Type[] Fields { get => new Type[0]; set { } }
	}
}

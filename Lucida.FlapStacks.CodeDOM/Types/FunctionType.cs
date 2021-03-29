namespace Lucida.FlapStacks.CodeDOM.Types
{
	public class FunctionType : Type
	{
		public override ulong Size => 0;

		public Type ReturnType { get; set; }
	}
}

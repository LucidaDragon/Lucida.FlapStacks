using Lucida.FlapStacks.CodeDOM.Types;

namespace Lucida.FlapStacks.CodeDOM
{
	public abstract class Type
	{
		public static readonly Type Void = new VoidType();

		public string Name { get; set; }
		public abstract ulong Size { get; }
		public virtual Type PointerType => Void;
		public virtual Type[] Fields { get; set; } = new Type[0];
	}
}

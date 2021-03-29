namespace Lucida.FlapStacks.CodeDOM
{
	public abstract class Expression : Emittable
	{
		public abstract Type ResultType { get; }
		
		public virtual bool IsConstant { get; } = false;
		public virtual Value Constant { get; } = null;
	}
}

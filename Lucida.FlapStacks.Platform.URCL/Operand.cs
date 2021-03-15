namespace Lucida.FlapStacks.Platform.URCL
{
	public abstract class Operand
	{
		public virtual ulong MaxRegister => 0;

		public abstract Value Value { get; }

		public abstract bool TryParse(string str, out Operand operand);
		public abstract void Push(Emitter e);
		public abstract void Pop(Emitter e);
		public abstract string GetString();
	}
}

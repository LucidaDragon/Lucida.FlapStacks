namespace Lucida.FlapStacks.CodeDOM
{
	public abstract class Operator
	{
		public abstract string Name { get; }
		public abstract Type ResultType { get; }

		public abstract void Emit(Emitter emitter);
	}
}

namespace Lucida.FlapStacks.CodeDOM
{
	public abstract class Emittable
	{
		public abstract string GetString();
		public abstract bool TryParse(string str, out Emittable result, out string message);
		public abstract void Emit(Emitter emitter);
	}
}

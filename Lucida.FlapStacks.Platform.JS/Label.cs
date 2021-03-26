namespace Lucida.FlapStacks.Platform.JS
{
	public class Label : Value
	{
		public ulong Index { get; private set; } = ulong.MaxValue;

		public void Mark(ulong index)
		{
			Index = index;
		}

		public override ulong Get()
		{
			return Index;
		}
	}
}

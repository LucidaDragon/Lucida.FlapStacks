namespace Lucida.FlapStacks.Platform.Wings
{
	public class Label : Value
	{
		private static ulong NextID = 0;

		public ulong Id { get; } = NextID++;
		public string Name { get; set; }

		public override ulong Get()
		{
			return Id;
		}
	}
}

namespace Lucida.FlapStacks.Platform.URCL
{
	public class UrclConfig
	{
		public LateinitValue MaxRegisters { get; } = new LateinitValue();
		public Value HeapOffset { get; }

		public UrclConfig()
		{
			HeapOffset = new HeapOffsetValue(this);
		}

		private class HeapOffsetValue : Value
		{
			private readonly UrclConfig Config;

			public HeapOffsetValue(UrclConfig config)
			{
				Config = config;
			}

			public override ulong Get()
			{
				return Config.MaxRegisters.Get() + 2;
			}
		}
	}
}

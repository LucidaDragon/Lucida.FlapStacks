namespace Lucida.FlapStacks.Platform.URCL
{
	public class Label : Value
	{
		public string Name { get; }
		public Value Marker { get; }

		public Label(string name, Emitter e)
		{
			Name = name;
			Marker = e.CreateLabel();
		}

		public void Mark(Emitter e)
		{
			e.MarkLabel(Marker);
		}

		public override ulong Get()
		{
			return Marker.Get();
		}

		public static bool TryParse(string str, out string result)
		{
			if (str.StartsWith(".") && str.Length > 1)
			{
				result = str.Substring(1);
				return true;
			}
			else
			{
				result = null;
				return false;
			}
		}
	}
}

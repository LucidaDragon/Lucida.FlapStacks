namespace Lucida.FlapStacks.Platform.Wings
{
	public class StringEmitter : InstructionEmitter
	{
		public override string Name => "wings";

		public override void Save(Stream stream)
		{
			for (int i = 0; i < Instructions.Count; i++)
			{
				var str = Instructions[i].GetString();

				for (int j = 0; j < str.Length; j++)
				{
					stream.WriteByte((byte)str[j]);
					stream.WriteByte((byte)'\n');
				}
			}
		}
	}
}

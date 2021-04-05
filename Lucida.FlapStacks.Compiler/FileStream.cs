using System;

namespace Lucida.FlapStacks.Compiler
{
	public class FileStream : Stream, IDisposable
	{
		public override bool CanRead => Stream.CanRead && Stream.Position < Stream.Length;
		public override bool CanWrite => Stream.CanWrite;

		private readonly System.IO.FileStream Stream;

		public FileStream(string path, bool create, bool write)
		{
			Stream = new System.IO.FileStream(
				path,
				create ? System.IO.FileMode.Create : System.IO.FileMode.Open,
				write ? System.IO.FileAccess.ReadWrite : System.IO.FileAccess.Read
			);
		}

		public override byte ReadByte()
		{
			return (byte)Stream.ReadByte();
		}

		public override void WriteByte(byte value)
		{
			Stream.WriteByte(value);
		}

		public void Dispose()
		{
			Stream.Flush();
			if (Stream != null) Stream.Dispose();
		}
	}
}

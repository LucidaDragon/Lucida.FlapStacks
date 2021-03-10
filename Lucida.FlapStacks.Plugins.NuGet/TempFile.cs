using System.IO;

namespace Lucida.FlapStacks.Plugins.NuGet
{
	public class TempFile : Stream
	{
		public string FullName { get; }

		private readonly FileStream Stream;

		public TempFile() : this(Path.GetTempFileName()) { }

		public TempFile(string path)
		{
			FullName = Path.GetFullPath(path);
			Stream = new FileStream(FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete, 4096, FileOptions.DeleteOnClose);
		}

		public override bool CanRead => Stream.CanRead;

		public override bool CanSeek => Stream.CanSeek;

		public override bool CanWrite => Stream.CanWrite;

		public override long Length => Stream.Length;

		public override long Position { get => Stream.Position; set => Stream.Position = value; }

		public override void Flush()
		{
			Stream.Flush();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return Stream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			return Stream.Seek(offset, origin);
		}

		public override void SetLength(long value)
		{
			Stream.SetLength(value);
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			Stream.Write(buffer, offset, count);
		}
	}
}

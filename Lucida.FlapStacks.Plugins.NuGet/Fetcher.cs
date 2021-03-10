using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace Lucida.FlapStacks.Plugins.NuGet
{
	public static class Fetcher
	{
		private const string URL = "https://www.nuget.org/api/v2/package/";

		public static string[] Install(string packageName, string version, Action<string> output = null, string nugetURL = URL)
		{
			if (output is null) output = (str) => { };

			var path = Path.GetTempFileName();

			Download(packageName, version, path, nugetURL, output);

			using (var file = new TempFile(path))
			{
				return Extract(file, output);
			}
		}

		private static string[] Extract(Stream source, Action<string> output)
		{
			var file = new ZipArchive(source);
			var result = new List<string>();

			foreach (var entry in file.Entries)
			{
				if (entry.Name.EndsWith(".dll"))
				{
					output($"Extracting {entry.Name}...");
					entry.ExtractToFile(entry.Name, true);
					result.Add(entry.Name);
				}
			}

			return result.ToArray();
		}

		private static void Download(string packageName, string version, string filename, string nugetURL, Action<string> output)
		{
			var url = GetDownloadURL(nugetURL, packageName, version);
			output($"Downloading {packageName}@{version} from \"{url}\"...");

			var client = new WebClient();
			client.DownloadFile(url, filename);
			client.Dispose();
		}

		private static string GetDownloadURL(string nugetURL, string packageName, string version)
		{
			return $"{nugetURL}{packageName}/{version}";
		}
	}
}

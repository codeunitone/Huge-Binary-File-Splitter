using System;
using System.IO;

namespace source
{
	class Program
	{
		static void Main(string[] args)
		{
			string srcFileName = "";
			string destFileName = "";
			long chunckSize = 100000000;
			SplitFile(srcFileName, destFileName, chunckSize);
		}

		static void SplitFile(string srcFileName, string destFileName, long chunckSize) {
			long noOfChunks = DetermineNoOfChunks(srcFileName,chunckSize);

			Console.WriteLine("No of chunks: {0}",noOfChunks);
			var srcFile = OpenFile(srcFileName);
			var reader = new BinaryReader(srcFile);

			for (long i = 0; i < noOfChunks; i++)
			{
				Console.WriteLine(destFileName + ".part" + i);
				byte[] chunk  = reader.ReadBytes((int)chunckSize);
				var destFileStream = new FileStream(destFileName + ".part" + i, FileMode.Create, FileAccess.Write);
				destFileStream.Write(chunk, 0, chunk.Length);
			}
		}

		static int DetermineNoOfChunks(string srcFileName, long chunckSize) {
			FileInfo fi = new FileInfo(srcFileName);
			// file lenght => size of the file in bytes
			// definig it as deciaml (even if it is not possible) because the implicite casting of the devision
			decimal fileLength = fi.Length;
			decimal noOfChunks = fileLength / chunckSize;

			return Decimal.ToInt32(Math.Ceiling(noOfChunks));
		}

		static FileStream OpenFile(string srcFileName) {
			var fileStream = new FileStream(srcFileName,FileMode.Open,FileAccess.Read);
			return fileStream;
		}
	} 
}

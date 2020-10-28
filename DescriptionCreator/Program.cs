using System;
using System.IO;
using System.IO.Compression;

namespace DescriptionCreator {
    class Program {
        static readonly Emgu.CV.Features2D.SIFT sift = new Emgu.CV.Features2D.SIFT(edgeThreshold: 25, sigma: 1.2);

        static void CreateDescriptions(string dirname, string message) {
            Console.Write("Creating descriptions for the {0:s}...", message);
            using (var stream = new MemoryStream()) {
                using (var archive = new ZipArchive(stream, ZipArchiveMode.Create, true)) {
                    foreach (var filename in Directory.GetFiles(dirname, "*.png")) {
                        var heroname = Path.GetFileNameWithoutExtension(filename);
                        using (var portrait = new Emgu.CV.Image<Emgu.CV.Structure.Bgra, byte>(filename))
                        using (var kp = new Emgu.CV.Util.VectorOfKeyPoint())
                        using (var des = new Emgu.CV.Mat()) {
                            sift.DetectAndCompute(portrait, null, kp, des, false);

                            var file = archive.CreateEntry(heroname, CompressionLevel.Optimal);
                            using (var fstream = file.Open())
                            using (var writer = new BinaryWriter(fstream)) {
                                var bytes = new byte[des.Cols * des.Rows * des.ElementSize];
                                unsafe {
                                    fixed (byte* ptr = bytes) {
                                        Buffer.MemoryCopy(des.DataPointer.ToPointer(), ptr, bytes.Length, bytes.Length);
                                    }
                                }
                                writer.Write(bytes);
                            }
                        }
                    }
                }

                using (var fstream = new FileStream(dirname + ".zip", FileMode.Create)) {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fstream);
                }
            }
            Console.WriteLine("Done.");
        }
        static void Main() {
            CreateDescriptions("portraits", "hero portraits");
            CreateDescriptions("bgnames", "battleground names");
        }
    }
}

using ERC.Framework.Bootstrapper;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Drawing;

namespace Service.Upload {
    public class UploadService {

        public string Upload(string file) {

            var appSettings     = ConfigurationManager.AppSettings;
            var serverDirectory = appSettings["ImagePath"];
            var filename        = string.Format(appSettings["JPGFilenameFormat"], Guid.NewGuid());
            var path            = Path.Combine(serverDirectory, filename);


             using (FileStream fs = new FileStream(path, FileMode.Create)) {
                using (BinaryWriter bw = new BinaryWriter(fs)) {
                    byte[] data = Convert.FromBase64String(file);
                    bw.Write(data);
                    bw.Close();
                }
            }

            return path;
        }



        public object Upload(string image, Guid residentID) {

            var appSettings            = ConfigurationManager.AppSettings;
            var serverDirectory        = appSettings["ImagePath"];
            var filename               = string.Format(appSettings["JPGFilenameFormat"], Guid.NewGuid().ToString());
            var path                   = Path.Combine(serverDirectory, filename);
            var tPath                  = Path.Combine(serverDirectory, "t-" + filename);

            var oStream                = new MemoryStream();
            var tStream                = new MemoryStream();

            using (FileStream fs       = new FileStream(path, FileMode.Create)) {

                oStream = GenerateActualImage(image, oStream, fs);
            }

            using (FileStream fst = new FileStream(tPath, FileMode.Create)) {

                tStream = GenerateThumbnailImage(image, tStream, fst);
            }

            return path;

        }

        private MemoryStream GenerateThumbnailImage(string image, MemoryStream tStream, FileStream fs) {
            using (BinaryWriter bw = new BinaryWriter(fs)) {

                byte[] data                         = Convert.FromBase64String(image);

                ISupportedImageFormat format        = new JpegFormat { Quality = 200 };
                Size size                           = new Size(145, 145);
                Rectangle rectange                  = new Rectangle(new Point(100, 15), new Size(450, 624));

                using (MemoryStream inStream        = new MemoryStream(data)) {

                    using (MemoryStream outStream   = new MemoryStream()) {

                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true)) {
                            imageFactory.Load(inStream)
                                        .Crop(rectange)
                                        .Resize(size)
                                        .Format(format)
                                        .BackgroundColor(Color.White)
                                        .Save(outStream);

                            tStream = outStream;
                        }
                    }
                }

                bw.Write(tStream.ToArray());
                bw.Close();
            }
            return tStream;
        }

        private static MemoryStream GenerateActualImage(string image, MemoryStream oStream, FileStream fs) {
            using (BinaryWriter bw = new BinaryWriter(fs)) {

                byte[] data = Convert.FromBase64String(image);

                ISupportedImageFormat format = new JpegFormat { Quality = 200 };
                Size size                    = new Size(450, 624);
                Rectangle rectange           = new Rectangle(new Point(100, 15), size);

                using (MemoryStream inStream        = new MemoryStream(data)) {

                    using (MemoryStream outStream   = new MemoryStream()) {

                        using (ImageFactory imageFactory = new ImageFactory(preserveExifData: true)) {
                            imageFactory.Load(inStream)
                                        .Crop(rectange)
                                        .Format(format)
                                        .Watermark(new ImageProcessor.Imaging.TextLayer {
                                            FontColor   = Color.White,
                                            DropShadow  = true,
                                            FontSize    = 15,
                                            FontFamily  = FontFamily.Families.ToList().Where(a => a.Name.Contains("Tahoma")).FirstOrDefault(),
                                            Opacity     = 90,
                                            Style       = FontStyle.Underline,
                                            Text        = "BARRIO.PH",
                                            Position    = new Point(30, 420)
                                        })
                                        .Save(outStream);

                            oStream = outStream;
                        }
                    }
                }

                bw.Write(oStream.ToArray());
                bw.Close();
            }
            return oStream;
        }

    }
}

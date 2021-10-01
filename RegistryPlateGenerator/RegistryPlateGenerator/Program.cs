using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace RegistryPlateGenerator
{
    class Program
    {
        static void Main(string[] args)
        {

            //TextGenerator.Generate();
                        
            Bitmap bitmap = new Bitmap(400, 200, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics gfx = Graphics.FromImage(bitmap);

            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gfx.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Font charlesFont = new("Charles Wright", 40);

            SolidBrush brush = new SolidBrush(Color.FromArgb(255, 255, 255));

            gfx.FillRectangle(brush, 0, 0, 300, 100);
            gfx.DrawString("ABC - 123", charlesFont, Brushes.Black, 30, 10);


            //Console.WriteLine(TextGenerator.Text());
            

            bitmap.Save(@"poo.png");


            var ser = new Server();
            ser.Configure();
            ser.Listen();
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}

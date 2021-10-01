using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RegistryPlateGenerator
{
    public static class TextGenerator
    {
        public static string Text()
        {
            string[] chars = CharArray.chars;
            StringBuilder registryBuilder = new StringBuilder();


            for (int i = 0; i < 6; i++)
            {
                registryBuilder.Append(chars[new Random().Next(0, chars.Length)]);
            }

            string text = registryBuilder.ToString();

            return text;
        }

        public static void Generate()
        {
            for (int i = 0; i < Math.Pow(36, 6); i++)
            {
                string text = Text();

                if (text == "APPLES")
                {            
                    Thread.Sleep(1000000);
                }
                Console.WriteLine(text);
            }
        }
    }
}

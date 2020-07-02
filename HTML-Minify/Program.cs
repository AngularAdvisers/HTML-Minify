using System;
using System.IO;

namespace HTML_Minify
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("A file name must be specified");
            }
            else
            {
                Console.WriteLine("This has {0} arguments, the arguments are: ", args.Length);
                for (int i = 0; i < args.Length; ++i) Console.WriteLine(args[i]);
                var filename = args[0];
                string line = File.ReadAllText(filename);
                var minline = line.Replace("\n", "").Replace("\r", "").Replace(" ", "");
                File.WriteAllText("min-" + filename, minline);
            }
        }
    }
}

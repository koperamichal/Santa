using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Santa
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            //day01();
            day02();
        }

        static void day01()
        {
            var lines = System.IO.File.ReadAllLines("01a");
            var numbers = new long[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
                numbers[i] = long.Parse(lines[i]);
            foreach (var x in numbers)
                foreach (var y in numbers)
                    foreach (var z in numbers)
                        if (x + y + z == 2020)
                        {
                            Console.WriteLine(x * y * z);
                            Console.ReadLine();
                            System.Windows.Forms.Clipboard.SetText((x * y * z).ToString());
                            return;
                        }
        }

        static void day02()
        {
            var lines = System.IO.File.ReadAllLines("day02a");
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)");
            int totalcount = 0;
            int totalcount2 = 0;
            foreach (var s in lines)
            {
                //N-N c: ccccccccccccc
                if (!r.IsMatch(s))
                    continue;
                var m = r.Match(s);
                var from = int.Parse(m.Groups[1].Value);
                var to = int.Parse(m.Groups[2].Value);
                var c = m.Groups[3].Value[0];
                var ss = m.Groups[4].Value;
                int count = 0;
                foreach (var x in ss)
                    if (x == c)
                        ++count;
                if (from <= count && count <= to)
                    ++totalcount;
                if (ss[from - 1] == c ^ ss[to - 1] == c)
                    ++totalcount2;
            }
            Console.WriteLine(totalcount);
            Console.WriteLine(totalcount2);
            //

            //part one
            Console.ReadLine();
            System.Windows.Forms.Clipboard.SetText(totalcount.ToString());
            System.Windows.Forms.Clipboard.SetText(totalcount2.ToString());
        }

    }

}

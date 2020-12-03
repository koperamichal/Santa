using System;
using System.Collections.Generic;
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
            //day02();
            day03();
            Console.ReadLine();
        }

        static void day01()
        {
            Console.WriteLine();
            Console.WriteLine("Day 01");
            var lines = System.IO.File.ReadAllLines("01a");
            var numbers = new long[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
                numbers[i] = long.Parse(lines[i]);
            foreach (var x in numbers)
                foreach (var y in numbers)
                    if (x + y == 2020)
                    {
                        Console.WriteLine(x * y);
                        System.Windows.Forms.Clipboard.SetText((x * y).ToString());
                        goto exit1;
                    }
                exit1:
            foreach (var x in numbers)
                foreach (var y in numbers)
                    foreach (var z in numbers)
                        if (x + y + z == 2020)
                        {
                            Console.WriteLine(x * y * z);
                            System.Windows.Forms.Clipboard.SetText((x * y * z).ToString());
                            goto exit2;
                        }
                    exit2:;
        }

        static void day02()
        {
            Console.WriteLine();
            Console.WriteLine("Day 02");
            var lines = System.IO.File.ReadAllLines("day02a");
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("([0-9]+)-([0-9]+) ([a-z]): ([a-z]+)");
            int totalcount = 0;
            int totalcount2 = 0;
            foreach (var s in lines)
            {
                //N-N c: ccccccccccccc
                var m = r.Match(s);
                if (!m.Success)
                    continue;
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

            System.Windows.Forms.Clipboard.SetText(totalcount.ToString());
            System.Windows.Forms.Clipboard.SetText(totalcount2.ToString());
        }

        static void day03()
        {
            Console.WriteLine();
            Console.WriteLine("Day 03");
            var lines = System.IO.File.ReadAllLines("day03a");
            var len = lines[0].Length;
            {
                var posx = 0;
                var posy = 0;
                int count = 0;
                for (int i = 0; i < lines.Length; ++i)
                {
                    posx = (posx + 3) % len;
                    ++posy;
                    if (posy >= lines.Length)
                        break;
                    if (lines[posy][posx] == '#')
                        ++count;
                }
                Console.WriteLine(count);
            }
            {
                long total = 1L;
                (int, int)[] data = { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
                foreach (var x in data)
                {
                    var posx = 0;
                    var posy = 0;
                    int count = 0;
                    for (int i = 0; i < lines.Length; ++i)
                    {
                        posx = (posx + x.Item1) % len;
                        posy = posy + x.Item2;
                        if (posy >= lines.Length)
                            break;
                        if (lines[posy][posx] == '#')
                            ++count;
                    }
                    total *= count;
                }
                Console.WriteLine(total);
                System.Windows.Forms.Clipboard.SetText(total.ToString());
            }
        }

    }

}

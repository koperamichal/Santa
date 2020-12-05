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
            day01();
            day02();
            day03();
            day04();
            day05();
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

        static void day04()
        {
            Console.WriteLine();
            Console.WriteLine("Day 04");
            var lines = System.IO.File.ReadAllLines("day04a");
            {
                StringBuilder sb = new StringBuilder();
                int k = -1;
                string[] keys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
                string[] colors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                int totalcount = 0;
                int totalcount2 = 0;
                while (true)
                {
                    ++k;
                    if (k == lines.Length || string.IsNullOrEmpty(lines[k]))
                    {
                        var s = sb.ToString();
                        var ss = s.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
                        Dictionary<string, string> list = new Dictionary<string, string>();
                        foreach (var x in ss)
                        {
                            var a = x.IndexOf(':');
                            if (a > 0)
                                list[x.Substring(0, a)] = x.Substring(a + 1);
                        }
                        int count = 0;
                        foreach (var key in keys)
                            if (list.ContainsKey(key))
                                ++count;
                        if (count == keys.Length)
                        {
                            ++totalcount;
                            count = 0;
                            int a;
                            foreach (var key in keys)
                            {
                                s = list[key];
                                switch (key)
                                {
                                    case "byr": if (int.TryParse(s, out a) && a >= 1920 && a <= 2002) ++count; break;
                                    case "iyr": if (int.TryParse(s, out a) && a >= 2010 && a <= 2020) ++count; break;
                                    case "eyr": if (int.TryParse(s, out a) && a >= 2020 && a <= 2030) ++count; break;
                                    case "hgt":
                                        if (s.EndsWith("cm"))
                                        {
                                            if (int.TryParse(s.Substring(0, s.Length - 2), out a) && a >= 150 && a <= 193) ++count; break;
                                        }
                                        if (s.EndsWith("in"))
                                        {
                                            if (int.TryParse(s.Substring(0, s.Length - 2), out a) && a >= 59 && a <= 76) ++count; break;
                                        }
                                        break;
                                    case "hcl":
                                        if (s.Length == 7 && s[0] == '#')
                                        {
                                            bool ok = true;
                                            for (int i = 1; i < s.Length; ++i)
                                            {
                                                if (s[i] >= '0' && s[i] <= '9')
                                                    continue;
                                                if (s[i] >= 'a' && s[i] <= 'f')
                                                    continue;
                                                ok = false;
                                            }
                                            if (ok)
                                                ++count;
                                        }
                                        break;
                                    case "ecl":
                                        foreach (var c in colors)
                                            if (s == c)
                                                ++count;
                                        break;
                                    case "pid":
                                        if (s.Length == 9)
                                        {
                                            bool ok = true;
                                            for (int i = 1; i < s.Length; ++i)
                                            {
                                                if (s[i] >= '0' && s[i] <= '9')
                                                    continue;
                                                ok = false;
                                            }
                                            if (ok)
                                                ++count;
                                        }
                                        break;
                                }
                            }
                            if (count == keys.Length)
                                ++totalcount2;
                        }
                        sb.Clear();
                    }
                    if (k == lines.Length)
                        break;
                    sb.AppendLine(lines[k]);
                }
                Console.WriteLine(totalcount);
                System.Windows.Forms.Clipboard.SetText(totalcount.ToString());
                Console.WriteLine(totalcount2);
                System.Windows.Forms.Clipboard.SetText(totalcount2.ToString());
            }
        }

        static void day05()
        {
            Console.WriteLine();
            Console.WriteLine("Day 05");
            var lines = System.IO.File.ReadAllLines("day5a");
            int max = 0;
            HashSet<int> list = new HashSet<int>();
            foreach (var s in lines)
            {
                //var s = "FBFBBFFRLR";
                int pos1 = 0;
                int pos2 = 127;
                int pos3 = 0;
                int pos4 = 7;
                foreach (var c in s)
                {
                    var diff1 = (pos2 - pos1 + 1) / 2;
                    var diff2 = (pos4 - pos3 + 1) / 2;
                    switch (c)
                    {
                        case 'B': pos1 = pos1 + diff1; break;
                        case 'F': pos2 = pos2 - diff1; break;
                        case 'R': pos3 = pos3 + diff2; break;
                        case 'L': pos4 = pos4 - diff2; break;
                    }
                }
                var id = pos1 * 8 + pos3;
                list.Add(id);
                if (id > max)
                    max = id;
            }
            Console.WriteLine(max);
            for (int i = 0; i < max; ++i)
                if (list.Contains(i - 1) && list.Contains(i + 1) && !list.Contains(i))
                    Console.WriteLine(i);
        }
    }

}
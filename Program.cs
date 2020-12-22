using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

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
            //day03();
            //day04();
            //day05();
            //day06();
            //day07();
            //day08();
            //day09();
            //day10();
            //day11a();
            //day11b();
            //day12();
            //day13();
            //day14();
            //day15();
            //day16();
            //day17();
            //day18();
            //day19();
            //day20();
            //day21();
            day22();

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

        static void day06()
        {
            Console.WriteLine();
            Console.WriteLine("Day 06");
            var lines = System.IO.File.ReadAllLines("day6a");
            int count = 0;
            int count2 = 0;
            int i = -1;
            int g = 0;
            HashSet<char> list = new HashSet<char>();
            Dictionary<char, int> dict = new Dictionary<char, int>();
            while (true)
            {
                ++i;
                if (i >= lines.Length || string.IsNullOrEmpty(lines[i]))
                {
                    count += list.Count;
                    foreach (var x in dict)
                        if (x.Value == g)
                            ++count2;
                    list.Clear();
                    dict.Clear();
                    g = -1;
                }
                if (i >= lines.Length)
                    break;
                ++g;
                foreach (var x in lines[i])
                {
                    if (list.Add(x))
                        dict[x] = 1;
                    else
                        ++dict[x];
                }
            }
            Console.WriteLine(count);
            Console.WriteLine(count2);
        }

        class bag_class
        {
            public string Text;
            public long Count;
            public HashSet<string> items = new HashSet<string>();
            public List<(string name, int count)> list = new List<(string, int)>();
            public bool finished;
        }

        static void day07()
        {
            Console.WriteLine();
            Console.WriteLine("Day 07");
            var lines = System.IO.File.ReadAllLines("day7a");

            Dictionary<string, bag_class> dict = new Dictionary<string, bag_class>();
            dict["no other bag"] = new bag_class() { Text = "no other bag" };
            foreach (var line in lines)
            {
                int a = line.IndexOf(" contain ");
                var bag = line.Substring(0, a).TrimEnd('s');
                var s = line.Substring(a + " contain ".Length).TrimEnd('.');
                var parts = s.Split(',');   //N S
                for (int i = 0; i < parts.Length; ++i)
                    parts[i] = parts[i].Trim().TrimEnd('s');
                var n = new bag_class() { Text = bag };
                foreach (var x in parts)
                    if (x != "no other bag")
                    {
                        n.items.Add(x.Substring(2));
                        n.list.Add((x.Substring(2), int.Parse(x.Substring(0, 1))));
                    }
                if (n.list.Count == 0)
                    n.finished = true;
                dict[bag] = n;
            }
            while (true)
            {
                bool change = false;
                foreach (var n in dict)
                {
                    var bag = n.Value;
                    HashSet<string> toadd = new HashSet<string>();
                    foreach (var x in bag.items)
                        foreach (var xx in dict[x].items)
                            toadd.Add(xx);
                    foreach (var x in toadd)
                        change |= bag.items.Add(x);
                }

                //another way
                //foreach (var n in dict)
                //{
                //    again:
                //    foreach (var x in n.Value.items)
                //    {
                //        var nn = dict[x];
                //        foreach (var xx in nn.items)
                //        {
                //            if (n.Value.items.Add(xx))
                //            {
                //                change = true;
                //                goto again;
                //            }

                //        }
                //    }
                //}
                if (!change)
                    break;
            }
            int count = 0;
            foreach (var n in dict)
                if (n.Value.items.Contains("shiny gold bag"))
                    ++count;
            Console.WriteLine(count.ToString());

            //part2
            while (true)
            {
                bool change = false;
                foreach (var n in dict)
                {
                    var bag = n.Value;
                    if (bag.finished)
                        continue;
                    bool found = false;
                    foreach (var x in bag.list)
                        if (!dict[x.name].finished)
                            found = true;
                    if (!found)
                    {
                        foreach (var x in bag.list)
                            bag.Count += (dict[x.name].Count + 1) * x.count;
                        bag.finished = true;
                        change = true;
                    }
                }
                if (!change)
                    break;
            }
            Console.WriteLine(dict["shiny gold bag"].Count.ToString());
        }

        static void day08()
        {
            Console.WriteLine();
            Console.WriteLine("Day 08");
            var lines = System.IO.File.ReadAllLines("day08a");

            (int op, int value)[] ops = new (int, int)[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
            {
                switch (lines[i].Substring(0, 3))
                {
                    case "acc":
                        ops[i] = (0, int.Parse(lines[i].Substring(4)));
                        break;
                    case "jmp":
                        ops[i] = (1, int.Parse(lines[i].Substring(4)));
                        break;
                    case "nop":
                        ops[i] = (2, int.Parse(lines[i].Substring(4)));
                        break;
                }
            }
            for (int i = -1; i < ops.Length; ++i)
            {
                int pos = 0;
                long accumulator = 0;
                HashSet<int> list = new HashSet<int>();
                while (true)
                {
                    if (pos == ops.Length)
                    {
                        Console.WriteLine(accumulator);
                        return;
                    }
                    if (!list.Add(pos))
                    {
                        if (i < 0)
                            Console.WriteLine(accumulator);
                        break;
                    }
                    if (pos == i)
                    {
                        switch (ops[pos].op)
                        {
                            case 0: accumulator += ops[pos].value; ++pos; break;
                            case 1: ++pos; break;
                            case 2: pos += ops[pos].value; break;
                        }
                    }
                    else
                    {
                        switch (ops[pos].op)
                        {
                            case 0: accumulator += ops[pos].value; ++pos; break;
                            case 1: pos += ops[pos].value; break;
                            case 2: ++pos; break;
                        }
                    }
                }
            }
        }

        static void day09()
        {
            Console.WriteLine();
            Console.WriteLine("Day 09");
            var lines = System.IO.File.ReadAllLines("day09a");
            Queue<long> numbers = new Queue<long>();
            List<long> list = new List<long>();
            for (int i = 0; i < 25; ++i)
                numbers.Enqueue(long.Parse(lines[i]));
            foreach (var x in numbers)
                list.Add(x);
            long num = 0;
            for (int i = 25; i < lines.Length; ++i)
            {
                HashSet<long> dict = new HashSet<long>();
                foreach (var x in numbers)
                    dict.Add(x);
                numbers.Dequeue();
                num = long.Parse(lines[i]);
                bool found = false;
                foreach (var x in dict)
                    if (dict.Contains(num - x))
                    {
                        found = true;
                        break;
                    }
                if (!found)
                {
                    Console.WriteLine(num);
                    break;
                }
                list.Add(num);
                numbers.Enqueue(num);
            }
            //
            for (int i = 0; i < list.Count; ++i)
            {
                long total = 0;
                long max = long.MinValue;
                long min = long.MaxValue;
                for (int j = i; j < list.Count; ++j)
                {
                    var x = list[j];
                    if (x > max)
                        max = x;
                    if (x < min)
                        min = x;
                    total += x;
                    if (total == num)
                    {
                        Console.WriteLine(min + max);
                        return;
                    }
                    if (total > num)
                        break;
                }
            }

        }

        static void day10()
        {
            Console.WriteLine();
            Console.WriteLine("Day 10");
            var lines = System.IO.File.ReadAllLines("day10a");
            List<int> list = new List<int>();
            foreach (var s in lines)
                list.Add(int.Parse(s));
            list.Sort();
            Dictionary<int, int> diff = new Dictionary<int, int>();
            diff[1] = 0;
            diff[2] = 0;
            diff[3] = 1;
            int now = 0;
            long total = 1;
            int count = 0;
            foreach (var x in list)
            {
                if (x - now == 1)
                {
                    ++count;
                }
                else
                {
                    switch (count)
                    {
                        case 0: break;
                        case 1: break;
                        case 2: total *= 2; break;
                        case 3: total *= 4; break;
                        case 4: total *= 7; break;
                        default: break;
                    }
                    count = 0;
                }
                ++diff[x - now];
                now = x;
            }
            switch (count)
            {
                case 0: break;
                case 1: break;
                case 2: total *= 2; break;
                case 3: total *= 4; break;
                case 4: total *= 7; break;
                default: break;
            }
            Console.WriteLine(diff[1] * diff[3]);
            Console.WriteLine(total);
            //0125: 012, 02                                     *2
            //01236: 013, 023, 0123, 03                         *4
            //012347: 014, 024, 034, 0124, 0134, 0234, 01234    *7
            //0123458: *13
            //1 1 2 4 7 13 24 ...
        }

        static void day11a()
        {
            Console.WriteLine();
            Console.WriteLine("Day 11");
            var lines = System.IO.File.ReadAllLines("day11a");
            var len = lines[0].Length;
            bool change = true;
            int count = 0;
            while (change)
            {
                change = false;
                var lines2 = new string[lines.Length];
                for (int j = 0; j < lines.Length; ++j)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < len; ++i)
                    {
                        switch (lines[j][i])
                        {
                            case '.':
                                sb.Append('.');
                                break;
                            case 'L':
                                count = 0;
                                if (j >= lines.Length - 1)
                                    ++count;
                                else if (lines[j + 1][i] != '#')
                                    ++count;

                                if (i >= len - 1)
                                    ++count;
                                else if (lines[j][i + 1] != '#')
                                    ++count;

                                if (j <= 0)
                                    ++count;
                                else if (lines[j - 1][i] != '#')
                                    ++count;

                                if (i <= 0)
                                    ++count;
                                else if (lines[j][i - 1] != '#')
                                    ++count;

                                if (j >= lines.Length - 1 || i >= len - 1)
                                    ++count;
                                else if (lines[j + 1][i + 1] != '#')
                                    ++count;

                                if (j >= lines.Length - 1 || i <= 0)
                                    ++count;
                                else if (lines[j + 1][i - 1] != '#')
                                    ++count;

                                if (j <= 0 || i >= len - 1)
                                    ++count;
                                else if (lines[j - 1][i + 1] != '#')
                                    ++count;

                                if (j <= 0 || i <= 0)
                                    ++count;
                                else if (lines[j - 1][i - 1] != '#')
                                    ++count;

                                if (count == 8)
                                {
                                    sb.Append('#');
                                    if (lines[j][i] != '#')
                                        change = true;
                                }
                                else
                                {
                                    sb.Append('L');
                                    if (lines[j][i] != 'L')
                                        change = true;
                                }
                                break;
                            case '#':
                                count = 0;
                                if (j >= lines.Length - 1)
                                    ;
                                else if (lines[j + 1][i] == '#')
                                    ++count;

                                if (i >= len - 1)
                                    ;
                                else if (lines[j][i + 1] == '#')
                                    ++count;

                                if (j <= 0)
                                    ;
                                else if (lines[j - 1][i] == '#')
                                    ++count;

                                if (i <= 0)
                                    ;
                                else if (lines[j][i - 1] == '#')
                                    ++count;

                                if (j >= lines.Length - 1 || i >= len - 1)
                                    ;
                                else if (lines[j + 1][i + 1] == '#')
                                    ++count;

                                if (j >= lines.Length - 1 || i <= 0)
                                    ;
                                else if (lines[j + 1][i - 1] == '#')
                                    ++count;

                                if (j <= 0 || i >= len - 1)
                                    ;
                                else if (lines[j - 1][i + 1] == '#')
                                    ++count;

                                if (j <= 0 || i <= 0)
                                    ;
                                else if (lines[j - 1][i - 1] == '#')
                                    ++count;
                                if (count >= 4)
                                {
                                    sb.Append('L');
                                    if (lines[j][i] != 'L')
                                        change = true;
                                }
                                else
                                {
                                    sb.Append('#');
                                    if (lines[j][i] != '#')
                                        change = true;
                                }
                                break;
                        }
                    }
                    lines2[j] = sb.ToString();
                }
                lines = lines2;
            }
            count = 0;
            foreach (var s in lines)
                foreach (var c in s)
                    if (c == '#')
                        ++count;
            Console.WriteLine(count);
        }

        static void day11b()
        {
            Console.WriteLine();
            Console.WriteLine("Day 11");
            var lines = System.IO.File.ReadAllLines("day11a");
            var len = lines[0].Length;
            bool change = true;
            int count = 0;
            (int x, int y)[] dirs = { (1, 0), (-1, 0), (0, 1), (0, -1), (1, 1), (1, -1), (-1, 1), (-1, -1) };
            while (change)
            {
                change = false;
                var lines2 = new string[lines.Length];
                for (int j = 0; j < lines.Length; ++j)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < len; ++i)
                    {
                        switch (lines[j][i])
                        {
                            case '.':
                                sb.Append('.');
                                break;
                            case 'L':
                                count = 0;
                                foreach (var dir in dirs)
                                {
                                    for (int k = 1; k <= len; ++k)
                                    {
                                        if (j + dir.x * k < 0 || j + dir.x * k > lines.Length - 1 || i + dir.y * k < 0 || i + dir.y * k > len - 1)
                                        {
                                            ++count;
                                            break;
                                        }
                                        else if (lines[j + dir.x * k][i + dir.y * k] == '#')
                                        {
                                            break;
                                        }
                                        else if (lines[j + dir.x * k][i + dir.y * k] == '.')
                                        {
                                            //continue searching
                                        }
                                        else if (lines[j + dir.x * k][i + dir.y * k] == 'L')
                                        {
                                            ++count;
                                            break;
                                        }
                                    }
                                }
                                if (count == 8)
                                {
                                    sb.Append('#');
                                    if (lines[j][i] != '#')
                                        change = true;
                                }
                                else
                                {
                                    sb.Append('L');
                                    if (lines[j][i] != 'L')
                                        change = true;
                                }
                                break;
                            case '#':
                                count = 0;
                                foreach (var dir in dirs)
                                {
                                    for (int k = 1; k <= len; ++k)
                                    {
                                        if (j + dir.x * k < 0 || j + dir.x * k > lines.Length - 1 || i + dir.y * k < 0 || i + dir.y * k > len - 1)
                                        {
                                            break;
                                        }
                                        else if (lines[j + dir.x * k][i + dir.y * k] == '#')
                                        {
                                            ++count;
                                            break;
                                        }
                                        else if (lines[j + dir.x * k][i + dir.y * k] == '.')
                                        {
                                            //continue searching
                                        }
                                        else if (lines[j + dir.x * k][i + dir.y * k] == 'L')
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (count >= 5)
                                {
                                    sb.Append('L');
                                    if (lines[j][i] != 'L')
                                        change = true;
                                }
                                else
                                {
                                    sb.Append('#');
                                    if (lines[j][i] != '#')
                                        change = true;
                                }
                                break;
                        }
                    }
                    lines2[j] = sb.ToString();
                }
                lines = lines2;
            }
            count = 0;
            foreach (var s in lines)
                foreach (var c in s)
                    if (c == '#')
                        ++count;
            Console.WriteLine(count);
        }

        static void day12()
        {
            Console.WriteLine();
            Console.WriteLine("Day 12");
            var lines = System.IO.File.ReadAllLines("day12a");
            (char cmd, int value)[] list = new (char cmd, int value)[lines.Length];
            for (int i = 0; i < list.Length; i++)
                list[i] = (lines[i][0], int.Parse(lines[i].Substring(1)));
            //int x = 0, y = 0, rot = 0;  //E=0,N=1,W=2,S=3,L=+1,R=-1
            (int x, int y, int rot) pos = (0, 0, 0);
            foreach (var cmd in list)
            {
                switch (cmd.cmd)
                {
                    case 'N': pos.y -= cmd.value; break;
                    case 'S': pos.y += cmd.value; break;
                    case 'E': pos.x += cmd.value; break;
                    case 'W': pos.x -= cmd.value; break;
                    case 'L': pos.rot = (pos.rot + cmd.value + 360) % 360; break;
                    case 'R': pos.rot = (pos.rot - cmd.value + 360) % 360; break;
                    case 'F':
                        switch (pos.rot)
                        {
                            case 0: pos.x += cmd.value; break;
                            case 90: pos.y -= cmd.value; break;
                            case 180: pos.x -= cmd.value; break;
                            case 270: pos.y += cmd.value; break;
                            default: return;
                        }
                        break;
                }
            }
            Console.WriteLine(Math.Abs(pos.x) + Math.Abs(pos.y));

            pos = (0, 0, 0);
            (int x, int y) w = (10, -1);
            int a;
            foreach (var cmd in list)
            {
                switch (cmd.cmd)
                {
                    case 'N': w.y -= cmd.value; break;
                    case 'S': w.y += cmd.value; break;
                    case 'E': w.x += cmd.value; break;
                    case 'W': w.x -= cmd.value; break;
                    case 'L':
                        a = cmd.value;
                        while ((a -= 90) >= 0)
                            w = (w.y, -w.x);
                        break;
                    case 'R':
                        a = cmd.value;
                        while ((a -= 90) >= 0)
                            w = (-w.y, w.x);
                        break;
                    case 'F':
                        pos.x += w.x * cmd.value;
                        pos.y += w.y * cmd.value;
                        break;
                }
            }
            Console.WriteLine(Math.Abs(pos.x) + Math.Abs(pos.y));
        }

        static void day13()
        {
            Console.WriteLine();
            Console.WriteLine("Day 13");
            var lines = System.IO.File.ReadAllLines("day13a");
            var t = int.Parse(lines[0]);
            List<(int, int)> bus = new List<(int, int)>();
            int pos = 0;
            foreach (var s in lines[1].Split(','))
            {
                if (int.TryParse(s, out var a))
                    bus.Add((a, pos % a));
                ++pos;
            }
            int k = bus[0].Item1;
            var diff = ((t / bus[0].Item1) + 1) * bus[0].Item1;
            for (int i = 1; i < bus.Count; ++i)
            {
                var a = ((t / bus[i].Item1) + 1) * bus[i].Item1;
                if (a < diff)
                {
                    diff = a;
                    k = bus[i].Item1;
                }
            }
            Console.WriteLine((diff - t) * k);

            int b = 0;
            StringBuilder sb = new StringBuilder();
            //foreach (var x in bus)
            //    sb.Append(x.Item1 + "*" + (char)('a' + (b++)) + "=X+" + x.Item2 + ",");
            foreach (var x in bus)
                sb.Append(x.Item1 + "*" + (char)('a' + (b++)) + "-" + x.Item2 + "=");
            //and use wolframalpha to solve the problem :D

            //based on some reddit solution
            long time = 0;
            long koef = bus[0].Item1;
            for (int i = 1; i < bus.Count; ++i)
            {
                while (true)
                {
                    if ((time + bus[i].Item2) % bus[i].Item1 == 0)
                    {
                        koef *= bus[i].Item1;
                        break;
                    }
                    time += koef;
                }
            }
            Console.WriteLine(time);
        }

        static void day14()
        {
            Console.WriteLine();
            Console.WriteLine("Day 14");
            var lines = System.IO.File.ReadAllLines("day14a");

            {
                Dictionary<int, long> memory = new Dictionary<int, long>();
                string mask = "";
                foreach (var s in lines)
                {
                    if (s.StartsWith("mask = "))
                    {
                        mask = s.Substring(7);
                    }
                    else
                    {
                        var a = s.IndexOf("[");
                        var b = s.IndexOf("]");
                        var c = s.LastIndexOf(" ");
                        var address = int.Parse(s.Substring(a + 1, b - a - 1));
                        var value = long.Parse(s.Substring(c + 1));
                        BitArray ar = new BitArray(BitConverter.GetBytes(value));
                        long total = 0L;
                        long aa = 1L;
                        for (int i = 0; i < 36; ++i)
                        {
                            switch (mask[35 - i])
                            {
                                case '0': break;
                                case '1': total += aa; break;
                                case 'X': if (ar.Get(i)) total += aa; break;
                            }
                            aa *= 2;
                        }
                        memory[address] = total;
                    }
                }
                {
                    long total = 0L;
                    foreach (var x in memory)
                        total += x.Value;
                    Console.WriteLine(total);
                }
            }

            {
                Dictionary<long, long> memory = new Dictionary<long, long>();
                string mask = "";
                foreach (var s in lines)
                {
                    if (s.StartsWith("mask = "))
                    {
                        mask = s.Substring(7);
                    }
                    else
                    {
                        var a = s.IndexOf("[");
                        var b = s.IndexOf("]");
                        var c = s.LastIndexOf(" ");
                        var address = long.Parse(s.Substring(a + 1, b - a - 1));
                        var value = long.Parse(s.Substring(c + 1));
                        BitArray ar = new BitArray(BitConverter.GetBytes((long)address));
                        long total = 0L;
                        long aa = 1L;
                        List<long> mods = new List<long>();
                        for (int i = 0; i < 36; ++i)
                        {
                            switch (mask[35 - i])
                            {
                                case '0': if (ar.Get(i)) total += aa; break;
                                case '1': total += aa; break;
                                case 'X': mods.Add(1L << i); break;
                            }
                            aa *= 2;
                        }
                        var count = 1 << mods.Count;
                        for (int k = 0; k < count; ++k)
                        {
                            long bb = total;
                            ar = new BitArray(BitConverter.GetBytes(k));
                            for (int j = 0; j < mods.Count; ++j)
                            {
                                if (ar.Get(j))
                                    bb += mods[j];
                            }
                            memory[bb] = value;
                        }
                    }
                }
                {
                    long total = 0L;
                    foreach (var x in memory)
                        total += x.Value;
                    Console.WriteLine(total);
                }
            }
        }

        static void day15()
        {
            Console.WriteLine();
            Console.WriteLine("Day 15");
            var line = "15,12,0,14,3,1";
            //line = "0,3,6";
            List<int> list = new List<int>();
            foreach (var x in line.Split(','))
                list.Add(int.Parse(x));
            foreach (var N in new int[] { 2020, 30000000 })
            {
                Dictionary<int, (int, int)> dict2 = new Dictionary<int, (int, int)>();
                for (int i = 0; i < list.Count; ++i)
                {
                    dict2[list[i]] = (-1, i);
                }
                var k = list[list.Count - 1];
                for (int i = list.Count; i < N; ++i)
                {
                    if (dict2.TryGetValue(k, out var x) && x.Item1 != -1)
                    {
                        k = x.Item2 - x.Item1;
                    }
                    else
                        k = 0;
                    if (!dict2.TryGetValue(k, out x))
                        dict2[k] = (-1, i);
                    else
                        dict2[k] = (x.Item2, i);
                }
                Console.WriteLine(k);
            }
        }

        static void day16()
        {
            Console.WriteLine();
            Console.WriteLine("Day 16");
            var lines = System.IO.File.ReadAllLines("day16a");

            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("([0-9]+)-([0-9]+) or ([0-9]+)-([0-9]+)"); List<(int from1, int to1, int from2, int to2)> list_range = new List<(int from1, int to1, int from2, int to2)>();
            int state = 0;
            int total = 0;
            List<int[]> tickets = new List<int[]>();
            foreach (var s in lines)
            {
                if (string.IsNullOrEmpty(s))
                    ++state;
                else if (state == 0)
                {
                    var m = r.Match(s);
                    list_range.Add((int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value), int.Parse(m.Groups[4].Value)));
                }
                else if (state == 1)
                {
                    if (s == "your ticket:")
                        continue;
                    var ss = s.Split(',');
                    List<int> numbers = new List<int>();
                    foreach (var sss in ss)
                        numbers.Add(int.Parse(sss));
                    tickets.Add(numbers.ToArray());
                }
                else if (state == 2)
                {
                    if (s == "nearby tickets:")
                        continue;
                    var ss = s.Split(',');
                    List<int> numbers = new List<int>();
                    foreach (var sss in ss)
                        numbers.Add(int.Parse(sss));
                    bool found2 = true;
                    foreach (var num in numbers)
                    {
                        bool found = false;
                        foreach (var a in list_range)
                        {
                            if (a.from1 <= num && num <= a.to1)
                            {
                                found = true;
                                break;
                            }
                            if (a.from2 <= num && num <= a.to2)
                            {
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            total += num;
                            found2 = false;
                        }
                        else
                        {
                        }
                    }
                    if (found2)
                        tickets.Add(numbers.ToArray());
                }
            }
            Console.WriteLine(total);

            bool[,] isok = new bool[list_range.Count, list_range.Count];
            List<(int, int)> poradie = new List<(int, int)>();
            for (int i = 0; i < list_range.Count; i++)
            {
                int z = 0;
                for (int j = 0; j < list_range.Count; j++)
                {
                    bool found = false;
                    var a = list_range[i];
                    foreach (var t in tickets)
                    {
                        var num = t[j];
                        if (a.from1 <= num && num <= a.to1)
                            continue;
                        if (a.from2 <= num && num <= a.to2)
                            continue;
                        found = true;
                        break;
                    }
                    isok[i, j] = found;
                    if (!found)
                        ++z;
                }
                poradie.Add((z, i));
            }
            poradie.Sort();
            //najskor treba sort podla stupna volnosti, tie co maju najmenej volnych pozicii tie sa preskumaju najskor a tie co mozu byt skoro vsade, tak tie preskumame az na konci
            //a zhodou okolnosti, stupne volnosti su presne 1 .. N, takze sa vobec nemusime vraciat a staci to prejst raz, resp. ani raz, cize premennu "dict" mozeme zrusit

            //brute force = backtracking
            HashSet<int> dict = new HashSet<int>();
            int[] matrix = new int[list_range.Count];
            int k = 0;
            while (k < list_range.Count)
            {
                var kk = poradie[k].Item2;  //greedy implementation
                if (isok[kk, matrix[kk]] || dict.Contains(matrix[kk]))
                {
                    while ((++matrix[kk]) == list_range.Count)
                    {
                        matrix[kk] = 0;
                        --k;
                        dict.Remove(matrix[kk]);
                    }
                }
                else
                {
                    dict.Add(matrix[kk]);
                    ++k;
                }
            }
            long result = 1L;
            for (int i = 0; i < 6; ++i)
                result *= tickets[0][matrix[i]];
            Console.WriteLine(result);

            ////or you can use DP (or what is name for it) :) which should be faster but a lot more of memory and too slow
            //Queue<Dictionary<int, int>> q = new Queue<Dictionary<int, int>>();
            //q.Enqueue(new Dictionary<int, int>());
            //int kk = 0;
            //while (q.Count > 0)
            //{
            //    ++kk;
            //    var d = q.Dequeue();
            //    var k = d.Count;
            //    if (k == list_range.Count)
            //    {
            //        long result = 1L;
            //        foreach (var x in d)
            //            if (x.Value < 6)
            //                result *= tickets[0][x.Key];
            //        Console.WriteLine(result);
            //        break;
            //    }
            //    for (int i = 0; i < list_range.Count; ++i)
            //    {
            //        if (!isok[k, i] && !d.ContainsKey(i))
            //        {
            //            var dd = new Dictionary<int, int>(d);
            //            dd[i] = k;
            //            q.Enqueue(dd);
            //        }
            //    }
            //}
        }

        static string print17(HashSet<(int, int, int)> cubes)
        {
            var min = int.MinValue;
            foreach (var x in cubes)
            {
                if (x.Item1 > min)
                    min = x.Item1;
                if (x.Item2 > min)
                    min = x.Item2;
                if (x.Item3 > min)
                    min = x.Item3;
            }
            StringBuilder sb = new StringBuilder();
            for (int i = -min; i <= min; ++i)
            {
                for (int j = -min; j <= min; ++j)
                {
                    for (int k = -min; k <= min; ++k)
                    {
                        if (cubes.Contains((j, k, i)))
                            sb.Append('#');
                        else
                            sb.Append('.');
                    }
                    sb.AppendLine();
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        static void day17()
        {
            Console.WriteLine();
            Console.WriteLine("Day 17");
            var lines = System.IO.File.ReadAllLines("day17a");
            var n = lines.Length;
            {
                var cubes = new HashSet<(int, int, int)>();
                var cubes2 = new HashSet<(int, int, int)>();
                for (int i = 0; i < n; ++i)
                    for (int j = 0; j < n; ++j)
                        if (lines[i][j] == '#')
                            cubes.Add((i, j, 0));
                List<(int, int, int)> dirs = new List<(int, int, int)>();
                for (int i = -1; i < 2; ++i)
                    for (int j = -1; j < 2; ++j)
                        for (int k = -1; k < 2; ++k)
                        {
                            if (i == 0 && j == 0 && k == 0)
                                continue;
                            dirs.Add((i, j, k));
                        }
                for (int cycle = 0; cycle < 6; ++cycle)
                {
                    //var s = print17(cubes);
                    cubes2.Clear();
                    for (int i = 0 - cycle - 1; i < n + cycle + 1; ++i)
                        for (int j = 0 - cycle - 1; j < n + cycle + 1; ++j)
                            for (int k = -cycle - 1; k <= cycle + 1; ++k)
                            {
                                var c = (i, j, k);
                                int count = 0;
                                foreach (var d in dirs)
                                    if (cubes.Contains((d.Item1 + c.Item1, d.Item2 + c.Item2, d.Item3 + c.Item3)))
                                        ++count;
                                if (cubes.Contains(c))
                                {
                                    if (count == 2 || count == 3)
                                        cubes2.Add(c);
                                }
                                else
                                {
                                    if (count == 3)
                                        cubes2.Add(c);
                                }
                            }

                    //transfer
                    cubes.Clear();
                    foreach (var c in cubes2)
                        cubes.Add(c);
                }
                Console.WriteLine(cubes.Count);
            }
            //part2
            {
                var cubes = new HashSet<(int, int, int, int)>();
                var cubes2 = new HashSet<(int, int, int, int)>();
                for (int i = 0; i < n; ++i)
                    for (int j = 0; j < n; ++j)
                        if (lines[i][j] == '#')
                            cubes.Add((i, j, 0, 0));
                List<(int, int, int, int)> dirs = new List<(int, int, int, int)>();
                for (int i = -1; i < 2; ++i)
                    for (int j = -1; j < 2; ++j)
                        for (int k = -1; k < 2; ++k)
                            for (int z = -1; z < 2; ++z)
                            {
                                if (i == 0 && j == 0 && k == 0 && z == 0)
                                    continue;
                                dirs.Add((i, j, k, z));
                            }
                for (int cycle = 0; cycle < 6; ++cycle)
                {
                    cubes2.Clear();
                    for (int i = 0 - cycle - 1; i < n + cycle + 1; ++i)
                        for (int j = 0 - cycle - 1; j < n + cycle + 1; ++j)
                            for (int k = -cycle - 1; k <= cycle + 1; ++k)
                                for (int z = -cycle - 1; z <= cycle + 1; ++z)
                                {
                                    var c = (i, j, k, z);
                                    int count = 0;
                                    foreach (var d in dirs)
                                        if (cubes.Contains((d.Item1 + c.Item1, d.Item2 + c.Item2, d.Item3 + c.Item3, d.Item4 + c.Item4)))
                                            ++count;
                                    if (cubes.Contains(c))
                                    {
                                        if (count == 2 || count == 3)
                                            cubes2.Add(c);
                                    }
                                    else
                                    {
                                        if (count == 3)
                                            cubes2.Add(c);
                                    }
                                }

                    //transfer
                    cubes.Clear();
                    foreach (var c in cubes2)
                        cubes.Add(c);
                }
                Console.WriteLine(cubes.Count);
            }
        }

        static string evalute18(string s)
        {
            List<long> numbers = new List<long>();
            List<long> operations = new List<long>();
            long num = 0;
            bool any = false;
            foreach (var c in s)
            {
                if (c >= '0' && c <= '9')
                {
                    any = true;
                    num = num * 10 + (int)(c - '0');
                }
                else if (any)
                {
                    numbers.Add(num);
                    num = 0;
                    any = false;
                }
                if (c == '+')
                {
                    operations.Add(1);
                }
                else if (c == '*')
                {
                    operations.Add(2);
                }
            }
            num = numbers[0];
            for (int i = 0; i < operations.Count; ++i)
            {
                switch (operations[i])
                {
                    case 1: num += numbers[i + 1]; break;
                    case 2: num *= numbers[i + 1]; break;
                }
            }
            return num.ToString();
        }

        static string evalute18b(string s)
        {
            List<long> numbers = new List<long>();
            List<long> operations = new List<long>();
            long num = 0;
            bool any = false;
            foreach (var c in s)
            {
                if (c >= '0' && c <= '9')
                {
                    any = true;
                    num = num * 10 + (int)(c - '0');
                }
                else if (any)
                {
                    numbers.Add(num);
                    num = 0;
                    any = false;
                }
                if (c == '+')
                {
                    operations.Add(1);
                }
                else if (c == '*')
                {
                    operations.Add(2);
                }
            }

            for (int i = 0; i < operations.Count; ++i)
            {
                if (operations[i] == 1)
                {
                    numbers[i] = numbers[i] + numbers[i + 1];
                    numbers.RemoveAt(i + 1);
                    operations.RemoveAt(i--);
                }
            }
            if (operations.Count == 0)
                return numbers[0].ToString();

            num = 1L;
            foreach (var x in numbers)
                num *= x;
            return num.ToString();
        }

        static void day18()
        {
            Console.WriteLine();
            Console.WriteLine("Day 18");
            var lines = System.IO.File.ReadAllLines("day18a");
            long total = 0L;
            foreach (var line in lines)
            {
                var s = line;
                while (true)
                {
                    var a = s.IndexOf(')');
                    if (a < 0)
                        break;
                    var b = s.LastIndexOf('(', a);
                    var ss = s.Substring(b + 1, a - b - 1) + " ";
                    ss = evalute18(ss);
                    s = s.Remove(b, a - b + 1).Insert(b, ss);
                }
                s = evalute18(s + " ");
                total += long.Parse(s);
            }
            Console.WriteLine(total);

            total = 0L;
            foreach (var line in lines)
            {
                var s = line;
                while (true)
                {
                    var a = s.IndexOf(')');
                    if (a < 0)
                        break;
                    var b = s.LastIndexOf('(', a);
                    var ss = s.Substring(b + 1, a - b - 1) + " ";
                    ss = evalute18b(ss);
                    s = s.Remove(b, a - b + 1).Insert(b, ss);
                }
                s = evalute18b(s + " ");
                total += long.Parse(s);
            }
            Console.WriteLine(total);
        }

        static IEnumerable<string> getWord19(string s, Dictionary<int, string> dict)
        {

            if (s == "\"a\"")
            {
                yield return "a";
            }
            else if (s == "\"b\"")
            {
                yield return "b";
            }
            else if (s.Contains("|"))
            {
                var a = s.IndexOf('|');
                var s1 = s.Substring(0, a - 1);
                var s2 = s.Substring(a + 2);
                foreach (var x in getWord19(s1, dict))
                    yield return x;
                foreach (var x in getWord19(s2, dict))
                    yield return x;
            }
            else
            {
                var ss = s.Split();
                bool loop = false;
                if (ss.Length == 1)
                {
                    foreach (var x in getWord19(dict[int.Parse(ss[0])], dict))
                        yield return x;
                }
                else if (ss.Length == 2)
                {
                    foreach (var x in getWord19(dict[int.Parse(ss[0])], dict))
                        foreach (var y in getWord19(dict[int.Parse(ss[1])], dict))
                            yield return x + y;

                    //getWord(dict[int.Parse(ss[0])], dict, allwords);
                    //getWord(dict[int.Parse(ss[1])], dict, allwords);
                }
                else if (ss.Length == 3)
                {
                    foreach (var x in getWord19(dict[int.Parse(ss[0])], dict))
                        foreach (var y in getWord19(dict[int.Parse(ss[1])], dict))
                            foreach (var z in getWord19(dict[int.Parse(ss[2])], dict))
                                yield return x + y + z;
                }
                else
                {
                    //return null;
                }
                //StringBuilder sb = new StringBuilder();
                //foreach (var x in s.Split())
                //    sb.Append(getWord(x, dict, allwords));
                //return sb.ToString();
            }
            //return null;
        }

        static void day19()
        {
            Console.WriteLine();
            Console.WriteLine("Day 19");
            var lines = System.IO.File.ReadAllLines("day19a");

            bool found = false;
            Dictionary<int, string> dict = new Dictionary<int, string>();
            List<string> messeges = new List<string>();
            int max = 0;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    found = true;
                    continue;
                }
                if (found)
                {
                    messeges.Add(line);
                    if (line.Length > max)
                        max = line.Length;
                }
                else
                {
                    var a = line.IndexOf(':');
                    dict[int.Parse(line.Substring(0, a))] = line.Substring(a + 2);
                }
            }
            //part1
            //8: 42
            //11: 42 31

            ////part2
            //dict[8] = "42 | 42 8";
            //dict[11] = "42 31 | 42 11 31";
            //42 31 alebo 42 42 31 31 alebo 42 42 42 31 31 31

            ////part1 bruteforce
            //int count = 0;
            //int total = 0;
            //foreach (var x in getWord(dict[0], dict, 12, 0))
            //{
            //    ++total;
            //    if (messeges.Contains(x))
            //    {
            //        ++count;
            //        messeges.Remove(x);
            //    }
            //}
            //Console.WriteLine(count);
            //part2
            //2097152
            //kazda sprava ma iba 24 znakov, cize [8] (128 moznosti) + [11] (16384 moznosti)

            //len = 8
            List<string> list_8 = new List<string>();
            foreach (var x in getWord19(dict[8], dict))
                list_8.Add(x);

            //len = 16
            List<string> list_11 = new List<string>();
            foreach (var x in getWord19(dict[11], dict))
                list_11.Add(x);

            List<string> list_31 = new List<string>();
            foreach (var x in getWord19(dict[31], dict))
                list_31.Add(x);
            //takze pre spravy staci overit, ci prvych 8 znakov je z list_8 a dalsich 16 znakov je z list_11

            ////part1 slow
            //int count = 0;
            //foreach (var m in messeges)
            //{
            //    if (m.Length != 24)
            //        continue;
            //    foreach (var s1 in list_8)
            //    {
            //        if (string.Compare(s1, 0, m, 0, 8) == 0)
            //        {
            //            foreach (var s2 in list_11)
            //            {
            //                if (string.Compare(s2, 0, m, 8, 16) == 0)
            //                {
            //                    ++count;
            //                    break;
            //                }
            //            }
            //            break;
            //        }
            //    }
            //}
            //Console.WriteLine(count);

            //part1 fast
            int count = 0;
            foreach (var m in messeges)
            {
                if (m.Length != 24)
                    continue;
                int k0 = 0;
                found = true;
                while (found)
                {
                    found = false;
                    foreach (var s1 in list_8)
                    {
                        if (string.Compare(s1, 0, m, k0, 8) == 0)
                        {
                            found = true;
                            k0 += 8;
                            break;
                        }
                    }
                }
                int k1 = 0;
                found = true;
                while (found && k0 + k1 < m.Length)
                {
                    found = false;
                    foreach (var s1 in list_31)
                    {
                        if (string.Compare(s1, 0, m, k0 + k1, 8) == 0)
                        {
                            found = true;
                            k1 += 8;
                            break;
                        }
                    }
                }
                if (m.Length == k0 + k1 && k1 > 0 && k1 - k0 < 0)
                    ++count;
            }
            Console.WriteLine(count);

            //part2  
            count = 0;
            foreach (var m in messeges)
            {
                int k0 = 0;
                found = true;
                while (found)
                {
                    found = false;
                    foreach (var s1 in list_8)
                    {
                        if (string.Compare(s1, 0, m, k0, 8) == 0)
                        {
                            found = true;
                            k0 += 8;
                            break;
                        }
                    }
                }
                int k1 = 0;
                found = true;
                while (found && k0 + k1 < m.Length)
                {
                    found = false;
                    foreach (var s1 in list_31)
                    {
                        if (string.Compare(s1, 0, m, k0 + k1, 8) == 0)
                        {
                            found = true;
                            k1 += 8;
                            break;
                        }
                    }
                }
                if (m.Length == k0 + k1 && k1 > 0 && k1 - k0 < 0)
                    ++count;
            }
            Console.WriteLine(count);
        }

        static bool near20(int a, int b, Dictionary<int, (int, int, int, int)> dict, Dictionary<int, int> reverse)
        {
            var x = dict[a];
            var y = dict[b];

            if (x.Item1 == y.Item1)
                return true;
            if (x.Item1 == y.Item2)
                return true;
            if (x.Item1 == y.Item3)
                return true;
            if (x.Item1 == y.Item4)
                return true;

            if (x.Item2 == y.Item1)
                return true;
            if (x.Item2 == y.Item2)
                return true;
            if (x.Item2 == y.Item3)
                return true;
            if (x.Item2 == y.Item4)
                return true;

            if (x.Item3 == y.Item1)
                return true;
            if (x.Item3 == y.Item2)
                return true;
            if (x.Item3 == y.Item3)
                return true;
            if (x.Item3 == y.Item4)
                return true;

            if (x.Item4 == y.Item1)
                return true;
            if (x.Item4 == y.Item2)
                return true;
            if (x.Item4 == y.Item3)
                return true;
            if (x.Item4 == y.Item4)
                return true;


            if (x.Item1 == reverse[y.Item1])
                return true;
            if (x.Item1 == reverse[y.Item2])
                return true;
            if (x.Item1 == reverse[y.Item3])
                return true;
            if (x.Item1 == reverse[y.Item4])
                return true;

            if (x.Item2 == reverse[y.Item1])
                return true;
            if (x.Item2 == reverse[y.Item2])
                return true;
            if (x.Item2 == reverse[y.Item3])
                return true;
            if (x.Item2 == reverse[y.Item4])
                return true;

            if (x.Item3 == reverse[y.Item1])
                return true;
            if (x.Item3 == reverse[y.Item2])
                return true;
            if (x.Item3 == reverse[y.Item3])
                return true;
            if (x.Item3 == reverse[y.Item4])
                return true;

            if (x.Item4 == reverse[y.Item1])
                return true;
            if (x.Item4 == reverse[y.Item2])
                return true;
            if (x.Item4 == reverse[y.Item3])
                return true;
            if (x.Item4 == reverse[y.Item4])
                return true;
            return false;
        }

        static bool near20b(int x, int b, Dictionary<int, (int, int, int, int)> dict, Dictionary<int, int> reverse)
        {
            var y = dict[b];

            if (x == y.Item1)
                return true;
            if (x == y.Item2)
                return true;
            if (x == y.Item3)
                return true;
            if (x == y.Item4)
                return true;

            if (x == reverse[y.Item1])
                return true;
            if (x == reverse[y.Item2])
                return true;
            if (x == reverse[y.Item3])
                return true;
            if (x == reverse[y.Item4])
                return true;
            return false;
        }

        static IEnumerable<(int, int, int, int)> rotation1(int a, Dictionary<int, (int, int, int, int)> dict, Dictionary<int, int> reverse)
        {
            var x = dict[a];    //top, left, bottom, right
            var y = x;
            yield return y;
            yield return (y.Item1, y.Item4, y.Item3, y.Item2);
            //yield return (y.Item3, y.Item2, y.Item1, y.Item4);
            //yield return (y.Item3, y.Item4, y.Item1, y.Item2);

            y = (y.Item4, reverse[y.Item1], y.Item2, reverse[y.Item3]);
            yield return y;
            yield return (y.Item1, y.Item4, y.Item3, y.Item2);
            //yield return (y.Item3, y.Item2, y.Item1, y.Item4);
            //yield return (y.Item3, y.Item4, y.Item1, y.Item2);

            y = (y.Item4, reverse[y.Item1], y.Item2, reverse[y.Item3]);
            yield return y;
            yield return (y.Item1, y.Item4, y.Item3, y.Item2);
            //yield return (y.Item3, y.Item2, y.Item1, y.Item4);
            //yield return (y.Item3, y.Item4, y.Item1, y.Item2);

            y = (y.Item4, reverse[y.Item1], y.Item2, reverse[y.Item3]);
            yield return y;
            yield return (y.Item1, y.Item4, y.Item3, y.Item2);
            //yield return (y.Item3, y.Item2, y.Item1, y.Item4);
            //yield return (y.Item3, y.Item4, y.Item1, y.Item2);
        }

        static void day20()
        {
            Console.WriteLine();
            Console.WriteLine("Day 20");
            var lines = System.IO.File.ReadAllLines("day20a");

            bool newtile = true;
            int id = 0;
            var N = 10; //1023 je max
            Dictionary<int, (int, int, int, int)> dict = new Dictionary<int, (int, int, int, int)>();
            Dictionary<int, int> all = new Dictionary<int, int>();
            List<string> tile = new List<string>();
            Dictionary<int, int> reverse = new Dictionary<int, int>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    newtile = true;
                    int a1 = 0, aa1 = 0;                         //top line
                    int k = 1;
                    int kk = 1024 / 2;
                    for (int i = 0; i < N; ++i)
                    {
                        if (tile[0][i] == '#')
                        {
                            a1 += k;
                            aa1 += kk;
                        }
                        k *= 2;
                        kk /= 2;
                    }
                    int a2 = 0, aa2 = 0;
                    k = 1;
                    kk = 1024 / 2;
                    for (int i = 0; i < N; ++i)
                    {
                        if (tile[i][0] == '#')          //left line
                        {
                            a2 += k;
                            aa2 += kk;
                        }
                        k *= 2;
                        kk /= 2;
                    }
                    int a3 = 0, aa3 = 0;
                    k = 1;
                    kk = 1024 / 2;
                    for (int i = 0; i < N; ++i)
                    {
                        if (tile[N - 1][i] == '#')      //bottom line
                        {
                            a3 += k;
                            aa3 += kk;
                        }
                        k *= 2;
                        kk /= 2;
                    }
                    int a4 = 0, aa4 = 0;
                    k = 1;
                    kk = 1024 / 2;
                    for (int i = 0; i < N; ++i)         //right line
                    {
                        if (tile[i][N - 1] == '#')
                        {
                            a4 += k;
                            aa4 += kk;
                        }
                        k *= 2;
                        kk /= 2;
                    }
                    tile.Clear();
                    dict[id] = (a1, a2, a3, a4);
                    reverse[a1] = aa1;
                    reverse[a2] = aa2;
                    reverse[a3] = aa3;
                    reverse[a4] = aa4;

                    reverse[aa1] = a1;
                    reverse[aa2] = a2;
                    reverse[aa3] = a3;
                    reverse[aa4] = a4;

                    if (!all.ContainsKey(a1))
                        all[a1] = 1;
                    else
                        ++all[a1];

                    if (!all.ContainsKey(a2))
                        all[a2] = 1;
                    else
                        ++all[a2];

                    if (!all.ContainsKey(a3))
                        all[a3] = 1;
                    else
                        ++all[a3];

                    if (!all.ContainsKey(a4))
                        all[a4] = 1;
                    else
                        ++all[a4];

                    if (!all.ContainsKey(a1))
                        all[a1] = 1;
                    else
                        ++all[a1];

                    if (!all.ContainsKey(a2))
                        all[a2] = 1;
                    else
                        ++all[a2];

                    if (!all.ContainsKey(a3))
                        all[a3] = 1;
                    else
                        ++all[a3];

                    if (!all.ContainsKey(a4))
                        all[a4] = 1;
                    else
                        ++all[a4];

                    continue;
                }
                if (newtile)
                {
                    newtile = false;
                    id = int.Parse(line.Substring(5, line.Length - 6));
                    continue;
                }
                tile.Add(line);
            }
            var len = (int)Math.Sqrt(dict.Count);
            int[,] map = new int[len, len];
            long total = 1L;
            List<int> list_2 = new List<int>();
            List<int> list_3 = new List<int>();
            List<int> list_4 = new List<int>();
            foreach (var x in dict)
            {
                int count = 0;
                bool found = false;
                foreach (var y in dict)
                {
                    if (x.Key == y.Key)
                        continue;
                    if (x.Value.Item1 == y.Value.Item1)
                        found = true;
                    if (x.Value.Item1 == y.Value.Item2)
                        found = true;
                    if (x.Value.Item1 == y.Value.Item3)
                        found = true;
                    if (x.Value.Item1 == y.Value.Item4)
                        found = true;

                    if (x.Value.Item1 == reverse[y.Value.Item1])
                        found = true;
                    if (x.Value.Item1 == reverse[y.Value.Item2])
                        found = true;
                    if (x.Value.Item1 == reverse[y.Value.Item3])
                        found = true;
                    if (x.Value.Item1 == reverse[y.Value.Item4])
                        found = true;
                }
                if (found)
                    ++count;

                found = false;
                foreach (var y in dict)
                {
                    if (x.Key == y.Key)
                        continue;
                    if (x.Value.Item2 == y.Value.Item1)
                        found = true;
                    if (x.Value.Item2 == y.Value.Item2)
                        found = true;
                    if (x.Value.Item2 == y.Value.Item3)
                        found = true;
                    if (x.Value.Item2 == y.Value.Item4)
                        found = true;

                    if (x.Value.Item2 == reverse[y.Value.Item1])
                        found = true;
                    if (x.Value.Item2 == reverse[y.Value.Item2])
                        found = true;
                    if (x.Value.Item2 == reverse[y.Value.Item3])
                        found = true;
                    if (x.Value.Item2 == reverse[y.Value.Item4])
                        found = true;
                }
                if (found)
                    ++count;

                found = false;
                foreach (var y in dict)
                {
                    if (x.Key == y.Key)
                        continue;
                    if (x.Value.Item3 == y.Value.Item1)
                        found = true;
                    if (x.Value.Item3 == y.Value.Item2)
                        found = true;
                    if (x.Value.Item3 == y.Value.Item3)
                        found = true;
                    if (x.Value.Item3 == y.Value.Item4)
                        found = true;

                    if (x.Value.Item3 == reverse[y.Value.Item1])
                        found = true;
                    if (x.Value.Item3 == reverse[y.Value.Item2])
                        found = true;
                    if (x.Value.Item3 == reverse[y.Value.Item3])
                        found = true;
                    if (x.Value.Item3 == reverse[y.Value.Item4])
                        found = true;
                }
                if (found)
                    ++count;

                found = false;
                foreach (var y in dict)
                {
                    if (x.Key == y.Key)
                        continue;
                    if (x.Value.Item4 == y.Value.Item1)
                        found = true;
                    if (x.Value.Item4 == y.Value.Item2)
                        found = true;
                    if (x.Value.Item4 == y.Value.Item3)
                        found = true;
                    if (x.Value.Item4 == y.Value.Item4)
                        found = true;

                    if (x.Value.Item4 == reverse[y.Value.Item1])
                        found = true;
                    if (x.Value.Item4 == reverse[y.Value.Item2])
                        found = true;
                    if (x.Value.Item4 == reverse[y.Value.Item3])
                        found = true;
                    if (x.Value.Item4 == reverse[y.Value.Item4])
                        found = true;
                }
                if (found)
                    ++count;
                if (count == 2)
                {
                    //corner
                    total *= x.Key;
                    list_2.Add(x.Key);
                }
                else if (count == 3)
                {
                    //border  
                    list_3.Add(x.Key);
                }
                else
                {
                    //inside
                    list_4.Add(x.Key);
                }
            }
            Console.WriteLine(total);

            map[0, 0] = list_2[0];
            list_2.RemoveAt(0);
            for (int i = 1; i < len - 1; ++i)
            {
                var x = map[i - 1, 0];
                foreach (var y in list_3)
                {
                    if (near20(x, y, dict, reverse))
                    {
                        map[i, 0] = y;
                        list_3.Remove(y);
                        break;
                    }
                }
            }
            for (int i = 1; i < len - 1; ++i)
            {
                var x = map[0, i - 1];
                foreach (var y in list_3)
                {
                    if (near20(x, y, dict, reverse))
                    {
                        map[0, i] = y;
                        list_3.Remove(y);
                        break;
                    }
                }
            }
            if (near20(list_2[0], map[0, len - 2], dict, reverse))
            {
                map[0, len - 1] = list_2[0];
                list_2.RemoveAt(0);
            }
            else if (near20(list_2[1], map[0, len - 2], dict, reverse))
            {
                map[0, len - 1] = list_2[1];
                list_2.RemoveAt(1);
            }
            else if (near20(list_2[2], map[0, len - 2], dict, reverse))
            {
                map[0, len - 1] = list_2[2];
                list_2.RemoveAt(2);
            }

            if (near20(list_2[0], map[len - 2, 0], dict, reverse))
            {
                map[len - 1, 0] = list_2[0];
                list_2.RemoveAt(0);
            }
            else if (near20(list_2[1], map[len - 2, 0], dict, reverse))
            {
                map[len - 1, 0] = list_2[1];
                list_2.RemoveAt(1);
            }
            map[len - 1, len - 1] = list_2[0];
            list_2.RemoveAt(0);

            //zvysny border
            for (int i = 1; i < len - 1; ++i)
            {
                var x = map[i - 1, len - 1];
                foreach (var y in list_3)
                {
                    if (near20(x, y, dict, reverse))
                    {
                        map[i, len - 1] = y;
                        list_3.Remove(y);
                        break;
                    }
                }
            }
            for (int i = 1; i < len - 1; ++i)
            {
                var x = map[len - 1, i - 1];
                foreach (var y in list_3)
                {
                    if (near20(x, y, dict, reverse))
                    {
                        map[len - 1, i] = y;
                        list_3.Remove(y);
                        break;
                    }
                }
            }
            //inside
            for (int i = 1; i < len - 1; ++i)
                for (int j = 1; j < len - 1; ++j)
                {
                    if (map[i, j] != 0)
                        continue;
                    (int, int)[] dirs = { (-1, 0), (0, -1), (1, 0), (0, 1) };
                    HashSet<int> h = new HashSet<int>();
                    foreach (var d in dirs)
                    {
                        var x = map[i + d.Item1, j + d.Item2];
                        if (x == 0)
                            continue;
                        foreach (var y in list_4)
                        {
                            if (near20(x, y, dict, reverse))
                            {
                                h.Add(y);
                            }
                        }
                    }
                    if (h.Count == 1)
                    {
                        foreach (var y in h)
                        {
                            map[i, j] = y;
                            list_4.Remove(y);
                        }
                    }
                }

            for (int k = 4; k > 0; --k)
            {
                bool change = true;
                while (change)
                {
                    change = false;
                    for (int i = 1; i < len - 1; ++i)
                        for (int j = 1; j < len - 1; ++j)
                        {
                            if (map[i, j] != 0)
                                continue;
                            (int, int)[] dirs = { (-1, 0), (0, -1), (1, 0), (0, 1) };
                            Dictionary<int, int> h = new Dictionary<int, int>();
                            foreach (var d in dirs)
                            {
                                var x = map[i + d.Item1, j + d.Item2];
                                if (x == 0)
                                    continue;
                                foreach (var y in list_4)
                                {
                                    if (near20(x, y, dict, reverse))
                                    {
                                        if (!h.ContainsKey(y))
                                            h[y] = 1;
                                        else
                                            ++h[y];
                                    }
                                }
                            }
                            foreach (var x in h)
                            {
                                if (x.Value == k)
                                {
                                    change = true;
                                    map[i, j] = x.Key;
                                    list_4.Remove(x.Key);
                                    break;
                                }
                            }
                        }
                    if (change)
                        k = 4;
                }
            }
            //map filled
            //Dictionary<int, int> test1 = new Dictionary<int, int>();
            //for (int i = 0; i < len; ++i)
            //    for (int j = 0; j < len; ++j)
            //    {
            //        if (i == 0 || j == 0 || i == len - 1 || j == len - 1)
            //        {
            //            var x = dict[map[i, j]];
            //            if (!test1.ContainsKey(x.Item1))
            //                test1[x.Item1] = 1;
            //            else
            //                ++test1[x.Item1];

            //            if (!test1.ContainsKey(x.Item2))
            //                test1[x.Item2] = 1;
            //            else
            //                ++test1[x.Item2];

            //            if (!test1.ContainsKey(x.Item3))
            //                test1[x.Item3] = 1;
            //            else
            //                ++test1[x.Item3];

            //            if (!test1.ContainsKey(x.Item4))
            //                test1[x.Item4] = 1;
            //            else
            //                ++test1[x.Item4];

            //            x = (reverse[x.Item1], reverse[x.Item2], reverse[x.Item3], reverse[x.Item4]);

            //            if (!test1.ContainsKey(x.Item1))
            //                test1[x.Item1] = 1;
            //            else
            //                ++test1[x.Item1];

            //            if (!test1.ContainsKey(x.Item2))
            //                test1[x.Item2] = 1;
            //            else
            //                ++test1[x.Item2];

            //            if (!test1.ContainsKey(x.Item3))
            //                test1[x.Item3] = 1;
            //            else
            //                ++test1[x.Item3];

            //            if (!test1.ContainsKey(x.Item4))
            //                test1[x.Item4] = 1;
            //            else
            //                ++test1[x.Item4];
            //        }
            //    }
            //int count1 = 0;
            //foreach (var x in test1)
            //    if (x.Value == 2)
            //        ++count1;

            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < len; ++i)
            //{
            //    for (int j = 0; j < len; ++j)
            //    {
            //        sb.Append(map[j, i]);
            //        sb.Append(',');
            //    }
            //    sb.AppendLine();
            //}
            //count1 = 0;


            //---map is ok, need to found the right rotation

            int count2 = 0;
            (int, int, int, int, int)[,] rot = new (int, int, int, int, int)[len, len];
            for (int i = 0; i < len; ++i)
                for (int j = 0; j < len; ++j)
                {
                    if (rot[i, j].Item5 != 0)
                        continue;
                    int r = -1;
                    foreach (var x in rotation1(map[i, j], dict, reverse))
                    {
                        ++r;
                        int[] a = { x.Item2, x.Item1, x.Item4, x.Item3 };
                        int count = 0;
                        (int, int)[] dirs = { (-1, 0), (0, -1), (1, 0), (0, 1) };
                        int k = -1;
                        foreach (var d in dirs)
                        {
                            ++k;
                            if (i + d.Item1 < 0)
                            {
                                ++count;
                                continue;
                            }
                            if (j + d.Item2 < 0)
                            {
                                ++count;
                                continue;
                            }
                            if (i + d.Item1 >= len)
                            {
                                ++count;
                                continue;
                            }
                            if (j + d.Item2 >= len)
                            {
                                ++count;
                                continue;
                            }
                            //if (rot[i + d.Item1, j + d.Item2].Item5 != 0)
                            //{
                            //    switch (k)
                            //    {
                            //        case 0: if (rot[i + d.Item1, j + d.Item2].Item4 == x.Item2) ++count; break;
                            //        case 1: if (rot[i + d.Item1, j + d.Item2].Item3 == x.Item1) ++count; break;
                            //        case 2: if (rot[i + d.Item1, j + d.Item2].Item2 == x.Item4) ++count; break;
                            //        case 3: if (rot[i + d.Item1, j + d.Item2].Item1 == x.Item3) ++count; break;
                            //    }
                            //}
                            //else
                            if (near20b(a[k], map[i + d.Item1, j + d.Item2], dict, reverse))
                            {
                                ++count;
                            }
                        }
                        if (count == 4)
                        {
                            rot[i, j] = (x.Item1, x.Item2, x.Item3, x.Item4, r);
                            ++count2;
                            //break;
                        }
                    }
                    //if (count2 == 0)
                    //{
                    //    (int, int)[] dirs = { (-1, 0), (0, -1), (1, 0), (0, 1) };
                    //    foreach (var d in dirs)
                    //        rot[i + d.Item1, j + d.Item2] = (0, 0, 0, 0, 0);
                    //    i = 0;
                    //    j = 0;
                    //}
                }

            //Console.WriteLine(count2);

        }

        static void day21()
        {
            Console.WriteLine();
            Console.WriteLine("Day 21");
            var lines = System.IO.File.ReadAllLines("day21a");

            HashSet<string> all_parts = new HashSet<string>();
            HashSet<string> all_alergens = new HashSet<string>();

            List<(HashSet<string> part, HashSet<string> alergen)> foods = new List<(HashSet<string>, HashSet<string>)>();
            foreach (var line in lines)
            {
                var a = line.IndexOf(" (contains ");
                var s = line.Substring(a + 11);
                s = s.Remove(s.Length - 1);
                var alergens = s.Split();
                for (int i = 0; i < alergens.Length; ++i)
                    alergens[i] = alergens[i].Trim().Trim(',').Trim().Trim(',').Trim();

                s = line.Substring(0, a);
                var parts = s.Split();
                for (int i = 0; i < parts.Length; ++i)
                    parts[i] = parts[i].Trim().Trim(',').Trim().Trim(',').Trim();

                foods.Add((new HashSet<string>(parts), new HashSet<string>(alergens)));

                foreach (var x in parts)
                    all_parts.Add(x);
                foreach (var x in alergens)
                    all_alergens.Add(x);
            }

            Dictionary<string, HashSet<string>> dict = new Dictionary<string, HashSet<string>>();
            foreach (var x in all_alergens)
            {
                HashSet<string> list = new HashSet<string>(all_parts);
                dict[x] = list;
            }

            Dictionary<string, string> mask = new Dictionary<string, string>();
            List<(string, string)> tosort = new List<(string, string)>();
            while (all_alergens.Count > 0)
            {
                foreach (var x in dict)
                {
                    if (x.Value.Count == 1)
                    {
                        string a = null;
                        foreach (var aa in x.Value)
                            a = aa;
                        foreach (var z in dict)
                            z.Value.Remove(a);
                        all_alergens.Remove(x.Key);
                        mask[a] = x.Key;
                        tosort.Add((x.Key, a));
                        continue;
                    }
                    foreach (var f in foods)
                    {
                        if (!f.alergen.Contains(x.Key))
                            continue;
                        x.Value.IntersectWith(f.part);
                    }
                }
            }
            int count = 0;
            foreach (var f in foods)
            {
                foreach (var x in f.part)
                    if (!mask.ContainsKey(x))
                        ++count;
            }
            Console.WriteLine(count);

            tosort.Sort();
            StringBuilder sb = new StringBuilder();
            foreach (var x in tosort)
                sb.Append(x.Item2 + ",");
            Console.WriteLine(sb.ToString(0, sb.Length - 1));
        }

        static System.Security.Cryptography.SHA256Managed sha = new System.Security.Cryptography.SHA256Managed();

        static string getHash(Queue<int> p1, Queue<int> p2)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            using (System.IO.BinaryWriter writer = new System.IO.BinaryWriter(ms))
            {
                foreach (var x in p1)
                    writer.Write(x);
                writer.Write(-1);
                foreach (var x in p2)
                    writer.Write(x);
                return Convert.ToBase64String(sha.ComputeHash(ms.ToArray()));
            }
        }

        static void day22()
        {
            Console.WriteLine();
            Console.WriteLine("Day 22");
            var lines = System.IO.File.ReadAllLines("day22a");

            //part1
            {
                Queue<int> p1 = new Queue<int>();
                Queue<int> p2 = new Queue<int>();
                bool first = true;
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        first = false;
                        continue;
                    }
                    int a;
                    if (int.TryParse(line, out a))
                    {
                        if (first)
                            p1.Enqueue(a);
                        else
                            p2.Enqueue(a);
                    }
                }

                int r = 0;
                while (p1.Count > 0 && p2.Count > 0)
                {
                    ++r;
                    var a = p1.Dequeue();
                    var b = p2.Dequeue();
                    if (a > b)
                    {
                        p1.Enqueue(a);
                        p1.Enqueue(b);
                    }
                    else
                    {
                        p2.Enqueue(b);
                        p2.Enqueue(a);
                    }
                }
                long total = 0;
                var p = p1.Count > 0 ? p1 : p2;
                long k = p.Count;
                while (k > 0)
                {
                    total += (k--) * p.Dequeue();
                }
                Console.WriteLine(total);
            }

            //part2
            {
                lines = System.IO.File.ReadAllLines("day22a");
                Queue<int> p1 = new Queue<int>();
                Queue<int> p2 = new Queue<int>();
                bool first = true;
                foreach (var line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        first = false;
                        continue;
                    }
                    int a;
                    if (int.TryParse(line, out a))
                    {
                        if (first)
                            p1.Enqueue(a);
                        else
                            p2.Enqueue(a);
                    }
                }

                int r = 0;
                while (p1.Count > 0 && p2.Count > 0)
                {
                    ++r;
                    var a = p1.Dequeue();
                    var b = p2.Dequeue();

                    if (a <= p1.Count && b <= p2.Count)
                    {
                        int k = 0;
                        Queue<int> pp1 = new Queue<int>();
                        foreach (var x in p1)
                        {
                            pp1.Enqueue(x);
                            if ((++k) == a)
                                break;
                        }
                        k = 0;
                        Queue<int> pp2 = new Queue<int>();
                        foreach (var x in p2)
                        {
                            pp2.Enqueue(x);
                            if ((++k) == b)
                                break;
                        }

                        HashSet<string> hash = new HashSet<string>();
                        while (pp1.Count > 0 && pp2.Count > 0 && hash.Add(getHash(pp1, pp2)))
                        {
                            ++r;
                            var aa = pp1.Dequeue();
                            var bb = pp2.Dequeue();
                            if (aa > bb)
                            {
                                pp1.Enqueue(aa);
                                pp1.Enqueue(bb);
                            }
                            else
                            {
                                pp2.Enqueue(bb);
                                pp2.Enqueue(aa);
                            }
                        }
                        if (pp1.Count > 0)
                        {
                            p1.Enqueue(a);
                            p1.Enqueue(b);
                        }
                        else
                        {
                            p2.Enqueue(b);
                            p2.Enqueue(a);
                        }
                    }
                    else if (a > b)
                    {
                        p1.Enqueue(a);
                        p1.Enqueue(b);
                    }
                    else
                    {
                        p2.Enqueue(b);
                        p2.Enqueue(a);
                    }
                }
                {
                    long total = 0;
                    var p = p1.Count > 0 ? p1 : p2;
                    long k = p.Count;
                    while (k > 0)
                    {
                        total += (k--) * p.Dequeue();
                    }
                    Console.WriteLine(total);
                }
            }

        }

    }

}

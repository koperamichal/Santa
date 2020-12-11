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
            //day03();
            //day04();
            //day05();
            //day06();
            //day07();
            day08();
            //day09();
            //day10();
            //day11a();
            //day11b();
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

    }

}

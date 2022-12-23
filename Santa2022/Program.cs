using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;

namespace Santa2022
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
            //day05a();
            //day05b();
            //day06();
            day07();

            Console.ReadLine();
        }

        static void day01()
        {
            Console.WriteLine();
            Console.WriteLine("Day 01");
            var lines = System.IO.File.ReadAllLines("01a");
            long now = 0L;
            long max = 0L;
            List<long> list = new List<long>();
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (now > max)
                        max = now;
                    list.Add(now);
                    now = 0L;
                    continue;
                }
                now += long.Parse(line);
            }
            if (now > max)
                max = now;
            list.Add(now);
            now = 0L;
            var s = max.ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);
            list.Sort();
            s = (list[list.Count - 1] + list[list.Count - 2] + list[list.Count - 3]).ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);

        }

        static void day02()
        {
            Console.WriteLine();
            Console.WriteLine("Day 02");
            var lines = System.IO.File.ReadAllLines("02a");
            long score = 0;
            foreach (var line in lines)
            {
                switch (line[0])
                {
                    case 'A':   //rock
                        switch (line[2])
                        {
                            case 'X': //rock +1
                                score += 1;
                                score += 3;
                                break;
                            case 'Y':   //paper +2
                                score += 2;
                                score += 6;
                                break;
                            case 'Z':   //scissors +3
                                score += 3;
                                break;
                        }
                        break;
                    case 'B':   //paper
                        switch (line[2])
                        {
                            case 'X': //rock +1
                                score += 1;
                                break;
                            case 'Y':   //paper +2
                                score += 2;
                                score += 3;
                                break;
                            case 'Z':   //scissors +3
                                score += 3;
                                score += 6;
                                break;
                        }
                        break;
                    case 'C':   //scissors
                        switch (line[2])
                        {
                            case 'X': //rock +1
                                score += 1;
                                score += 6;
                                break;
                            case 'Y':   //paper +2
                                score += 2;
                                break;
                            case 'Z':   //scissors +3
                                score += 3;
                                score += 3;
                                break;
                        }
                        break;
                }
            }
            var s = score.ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);

            //part2
            score = 0;
            foreach (var line in lines)
            {
                //X = lose
                //Y = draw
                //Z = win
                switch (line[0])
                {
                    case 'A':   //rock
                        switch (line[2])
                        {
                            case 'X': //rock +1
                                goto case 'z';
                            case 'Y':   //paper +2
                                goto case 'x';
                            case 'Z':   //scissors +3
                                goto case 'y';

                            case 'x': //rock +1
                                score += 1;
                                score += 3;
                                break;
                            case 'y':   //paper +2
                                score += 2;
                                score += 6;
                                break;
                            case 'z':   //scissors +3
                                score += 3;
                                break;
                        }
                        break;
                    case 'B':   //paper
                        switch (line[2])
                        {
                            case 'X': //rock +1
                                goto case 'x';
                            case 'Y':   //paper +2
                                goto case 'y';
                            case 'Z':   //scissors +3
                                goto case 'z';

                            case 'x': //rock +1
                                score += 1;
                                break;
                            case 'y':   //paper +2
                                score += 2;
                                score += 3;
                                break;
                            case 'z':   //scissors +3
                                score += 3;
                                score += 6;
                                break;
                        }
                        break;
                    case 'C':   //scissors
                        switch (line[2])
                        {
                            case 'X': //rock +1
                                goto case 'y';
                            case 'Y':   //paper +2
                                goto case 'z';
                            case 'Z':   //scissors +3
                                goto case 'x';

                            case 'x': //rock +1
                                score += 1;
                                score += 6;
                                break;
                            case 'y':   //paper +2
                                score += 2;
                                break;
                            case 'z':   //scissors +3
                                score += 3;
                                score += 3;
                                break;
                        }
                        break;
                }
            }
            s = score.ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);
        }

        static void day03()
        {
            Console.WriteLine();
            Console.WriteLine("Day 03");
            var lines = System.IO.File.ReadAllLines("03a");
            long score = 0L;
            foreach (var line in lines)
            {
                if (line.Length % 2 != 0)
                    return;
                HashSet<char> list1 = new HashSet<char>();
                HashSet<char> list2 = new HashSet<char>();
                for (int i = 0; i < line.Length / 2; ++i)
                    list1.Add(line[i]);
                for (int i = line.Length / 2; i < line.Length; ++i)
                    list2.Add(line[i]);
                list1.IntersectWith(list2);
                if (list1.Count != 1)
                    return;
                foreach (var c in list1)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        score += 1 + (c - 'a');
                    }
                    else if (c >= 'A' && c <= 'Z')
                    {
                        score += 27 + (c - 'A');
                    }

                    break;
                }
            }
            var s = score.ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);

            score = 0L;
            for (int i = 0; i < lines.Length;)
            {
                HashSet<char> list1 = new HashSet<char>();
                HashSet<char> list2 = new HashSet<char>();
                HashSet<char> list3 = new HashSet<char>();

                var line = lines[i++];
                foreach (var c in line)
                    list1.Add(c);
                line = lines[i++];
                foreach (var c in line)
                    list2.Add(c);
                line = lines[i++];
                foreach (var c in line)
                    list3.Add(c);

                list1.IntersectWith(list2);
                list1.IntersectWith(list3);
                if (list1.Count != 1)
                    return;
                foreach (var c in list1)
                {
                    if (c >= 'a' && c <= 'z')
                    {
                        score += 1 + (c - 'a');
                    }
                    else if (c >= 'A' && c <= 'Z')
                    {
                        score += 27 + (c - 'A');
                    }

                    break;
                }
            }
            s = score.ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);
        }

        static void day04()
        {
            Console.WriteLine();
            Console.WriteLine("Day 04");
            var lines = System.IO.File.ReadAllLines("04a");
            long count = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { '-', ',' });
                var a1 = long.Parse(parts[0]);
                var a2 = long.Parse(parts[1]);
                var b1 = long.Parse(parts[2]);
                var b2 = long.Parse(parts[3]);

                if (a1 <= b1 && b1 <= a2 && a1 <= b2 && b2 <= a2)
                {
                    ++count;
                }
                else if (b1 <= a1 && a1 <= b2 && b1 <= a2 && a2 <= b2)
                {
                    ++count;
                }
            }

            var s = count.ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);

            count = 0;
            foreach (var line in lines)
            {
                var parts = line.Split(new char[] { '-', ',' });
                var a1 = long.Parse(parts[0]);
                var a2 = long.Parse(parts[1]);
                var b1 = long.Parse(parts[2]);
                var b2 = long.Parse(parts[3]);

                if (a1 <= b1 && b1 <= a2 || a1 <= b2 && b2 <= a2)
                {
                    ++count;
                }
                else if (b1 <= a1 && a1 <= b2 || b1 <= a2 && a2 <= b2)
                {
                    ++count;
                }
            }

            s = count.ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);
        }

        static void day05a()
        {
            Console.WriteLine();
            Console.WriteLine("Day 05");
            var lines = System.IO.File.ReadAllLines("05a");
            int count = 0;
            Stack<char>[] list = null;
            for (int i = 0; i < lines.Length; ++i)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    var parts = lines[i - 1].Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
                    count = parts.Length;
                    list = new Stack<char>[count];
                    for (int j = 0; j < count; ++j)
                        list[j] = new Stack<char>();
                    --i;
                    while ((--i) >= 0)
                    {
                        //012345
                        //[F] [Q]     [T] [G] [C] [T] [T] [W]
                        //1 5 9
                        for (int j = 0; j < count; ++j)
                        {
                            var c = lines[i][1 + j * 4];
                            if (c != ' ')
                                list[j].Push(c);
                        }
                    }
                    break;
                }
            }
            for (int i = 0; i < lines.Length; ++i)
            {
                if (lines[i].StartsWith("move "))
                {
                    var parts = lines[i].Split();
                    var n = int.Parse(parts[1]);
                    var from = int.Parse(parts[3]) - 1;
                    var to = int.Parse(parts[5]) - 1;
                    for (int k = 0; k < n; ++k)
                    {
                        list[to].Push(list[from].Pop());
                    }
                }
            }
            var s = string.Empty;
            foreach (var x in list)
                s += x.Peek();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);

        }

        static void day05b()
        {
            Console.WriteLine();
            Console.WriteLine("Day 05");
            var lines = System.IO.File.ReadAllLines("05a");
            int count = 0;
            Stack<char>[] list = null;
            for (int i = 0; i < lines.Length; ++i)
            {
                if (string.IsNullOrEmpty(lines[i]))
                {
                    var parts = lines[i - 1].Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
                    count = parts.Length;
                    list = new Stack<char>[count];
                    for (int j = 0; j < count; ++j)
                        list[j] = new Stack<char>();
                    --i;
                    while ((--i) >= 0)
                    {
                        //012345
                        //[F] [Q]     [T] [G] [C] [T] [T] [W]
                        //1 5 9
                        for (int j = 0; j < count; ++j)
                        {
                            var c = lines[i][1 + j * 4];
                            if (c != ' ')
                                list[j].Push(c);
                        }
                    }
                    break;
                }
            }
            for (int i = 0; i < lines.Length; ++i)
            {
                if (lines[i].StartsWith("move "))
                {
                    var parts = lines[i].Split();
                    var n = int.Parse(parts[1]);
                    var from = int.Parse(parts[3]) - 1;
                    var to = int.Parse(parts[5]) - 1;
                    List<char> tmp = new List<char>();
                    for (int k = 0; k < n; ++k)
                        tmp.Add(list[from].Pop());
                    tmp.Reverse();
                    foreach (var x in tmp)
                        list[to].Push(x);
                }
            }
            var s = string.Empty;
            foreach (var x in list)
                s += x.Peek();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);
        }

        static void day06()
        {
            Console.WriteLine();
            Console.WriteLine("Day 06");
            var line = System.IO.File.ReadAllText("06a");
            for (int i = 3; i < line.Length; ++i)
            {
                var c1 = line[i];
                var c2 = line[i - 1];
                var c3 = line[i - 2];
                var c4 = line[i - 3];
                if (c1 != c2 && c1 != c3 && c1 != c4 && c2 != c3 && c2 != c4 && c3 != c4)
                {
                    var s = (i + 1).ToString();
                    Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);
                    break;
                }
            }

            var N = 14;
            for (int i = N - 1; i < line.Length; ++i)
            {
                char[] cc = new char[N];
                for (int j = 0; j < N; ++j)
                    cc[j] = line[i - j];
                HashSet<char> list = new HashSet<char>();
                foreach (var c in cc)
                    if (!list.Add(c))
                        break;
                if (list.Count == N)
                {
                    var s = (i + 1).ToString();
                    Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);
                    break;
                }
            }

        }

        class node
        {
            public string name;
            public List<node> children = new List<node>();
            public long size;
            public bool isdir;
            public node parent;
            public long size_total;

            public long GetSize()
            {
                long total = 0;
                foreach (var x in this.children)
                    if (!x.isdir)
                        total += x.size;
                    else
                        total += x.GetSize();
                return total;
            }
        }

        static void day07()
        {
            Console.WriteLine();
            Console.WriteLine("Day 07");
            var lines = System.IO.File.ReadAllLines("07a");

            node root = new node() { isdir = true, name = @"/" };
            //Dictionary<string, node> dict = new Dictionary<string, node>();
            //dict[root.name] = root;
            node now = root;
            List<node> list = new List<node>();
            //bool islist = false;
            foreach (var line in lines)
            {
                if (line == "$ cd ..")
                {
                    now = now.parent;
                    continue;
                }
                if (line == "$ cd /")
                {
                    now = root;
                    continue;
                }
                if (line.StartsWith("$ cd "))
                {
                    var name = line.Substring(5);
                    foreach (var x in now.children)
                    {
                        if (x.name == name)
                        {
                            now = x;
                            break;
                        }
                    }
                    continue;
                }
                if (line == "$ ls")
                {
                    //islist = true;
                    continue;
                }
                if (line.StartsWith("dir "))
                {
                    var name = line.Substring(4);
                    node found = null;
                    foreach (var x in now.children)
                    {
                        if (x.name == name)
                        {
                            found = x;
                            break;
                        }
                    }
                    if (found == null)
                    {
                        found = new node() { name = name, parent = now, isdir = true };
                        now.children.Add(found);
                        list.Add(found);
                    }
                    continue;
                }
                {
                    var a = line.IndexOf(' ');
                    var n = new node() { name = line.Substring(a + 1), parent = now, size = long.Parse(line.Substring(0, a)) };
                    now.children.Add(n);
                }
            }
            long total = 0;
            foreach (var x in list)
            {
                x.size_total = x.GetSize();
                if (x.size_total <= 100000)
                    total += x.size_total;
            }
            root.size_total = root.GetSize();

            var s = total.ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);

            total = 70000000;
            var free = total - root.size_total;
            var need = 30000000 - free;

            long max = long.MaxValue;
            foreach (var x in list)
            {
                if (x.size_total >= need && x.size_total < max)
                {
                    max = x.size_total;
                }
            }

            s = max.ToString();
            Console.WriteLine(s); TextCopy.ClipboardService.SetText(s);

        }

    }

}

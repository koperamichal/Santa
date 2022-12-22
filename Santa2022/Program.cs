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
            day04();

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

    }

}

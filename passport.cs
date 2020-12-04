using System;
using System.Collections.Generic;
using System.Text;

namespace Santa
{
    class passport
    {
        public passport(string s)
        {
            string[] keys = new string[] { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };
            string[] colors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            {
                var ss = s.Split(new char[] { }, StringSplitOptions.RemoveEmptyEntries);
                Dictionary<string, string> dict = new Dictionary<string, string>();
                foreach (var x in ss)
                {
                    var a = x.IndexOf(':');
                    if (a > 0)
                        dict[x.Substring(0, a)] = x.Substring(a + 1);
                }
                int count = 0;
                foreach (var key in keys)
                    if (dict.ContainsKey(key))
                        ++count;
                if (count == keys.Length)
                {
                    count = 0;
                    int a;
                    foreach (var key in keys)
                    {
                        s = dict[key];
                        switch (key)
                        {
                            case "byr": if (int.TryParse(s, out a) && a >= 1920 && a <= 2002) ++count; BirthYear = a; break;
                            case "iyr": if (int.TryParse(s, out a) && a >= 2010 && a <= 2020) ++count; IssueYear = a; break;
                            case "eyr": if (int.TryParse(s, out a) && a >= 2020 && a <= 2030) ++count; ExpirationYear = a; break;
                            case "hgt":
                                if (s.EndsWith("cm"))
                                {
                                    if (int.TryParse(s.Substring(0, s.Length - 2), out a) && a >= 150 && a <= 193) ++count; Height = a; HeightInCm = true; break;
                                }
                                if (s.EndsWith("in"))
                                {
                                    if (int.TryParse(s.Substring(0, s.Length - 2), out a) && a >= 59 && a <= 76) ++count; Height = a; HeightInCm = false; break;
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
                                    {
                                        HairColor = s.Substring(1);
                                        HairColorRGB = (int.Parse(s.Substring(1, 2), System.Globalization.NumberStyles.HexNumber), 
                                            int.Parse(s.Substring(3, 2), System.Globalization.NumberStyles.HexNumber), 
                                            int.Parse(s.Substring(5, 2), System.Globalization.NumberStyles.HexNumber));
                                        ++count;
                                    }
                                }
                                break;
                            case "ecl":
                                foreach (var c in colors)
                                    if (s == c)
                                        ++count;
                                EyeColor = s;
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
                                    {
                                        ++count;
                                        ID = long.Parse(s);
                                    }
                                }
                                break;
                        }
                    }
                    if (count == keys.Length)
                        IsValid = true;
                }
                if (dict.TryGetValue("cid", out var sss) && long.TryParse(sss, out var aa))
                    CountryID = aa;
            }
        }

        public bool IsValid { get; private set; }
        public long ID { get; private set; }
        public long CountryID { get; private set; }
        public int BirthYear { get; private set; }
        public int IssueYear { get; private set; }
        public int ExpirationYear { get; private set; }
        public int Height { get; private set; }
        public bool HeightInCm { get; private set; }
        public string HairColor { get; private set; }
        public (int R, int G, int B) HairColorRGB { get; private set; }
        public string EyeColor { get; private set; }

    }

}

using System;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(new[] {"12:12:12"}, new [] { "few seconds ago" }, "12:12:12");
            Test(new[] { "23:23:23", "23:23:23" }, new[] { "59 minutes ago", "59 minutes ago" }, "00:22:23");
            Test(new[] { "00:10:10", "00:10:10" }, new[] { "59 minutes ago", "1 hours ago" }, "impossible");
            Test(new[] { "11:59:13", "11:13:23", "12:25:15" }, new[] { "few seconds ago", "46 minutes ago", "23 hours ago" }, "11:59:23");
            Console.ReadKey(true);
        }

        private static void Test(string[] postTimes, string[] showTimes, string expected)
        {
            var result = GetCurrentTime(postTimes, showTimes).Equals(expected) ? "PASS" : "FAIL";
            var postTimesCsv = string.Join(", ", postTimes);
            var showTimesCsv = string.Join(", ", showTimes);
            Console.WriteLine($"[{postTimesCsv}], [{showTimesCsv}] => {result}");
        }

        public static string GetCurrentTime(string[] exactPostTime, string[] showPostTime)
        {
            // Add your code here.
            string ans;
            DateTime[] dateValue = new DateTime[exactPostTime.Length];
            int h=0, m=0, s=0;
            bool f = true;
            DateTime d;
            for (int i = 0; i < exactPostTime.Length; i++)
            {
                if (DateTime.TryParse(exactPostTime[i], out dateValue[i]))
                {
                    string[] words = showPostTime[i].Split(' ');
                    if (f)
                    {
                        //will be executed only once.
                        h = dateValue[i].Hour;
                        m = dateValue[i].Minute;
                        s = dateValue[i].Second;
                        f = false;
                    }
                    else
                    {
                        //we will check wheather repeated time is given.
                        if (dateValue[i-1].ToString("HH:mm:ss").Equals(dateValue[i].ToString("HH:mm:ss")) && (!(showPostTime[i-1].Split(' ')[0].Equals(words[0])) || !(showPostTime[i-1].Split(' ')[1].Equals(words[1]))))
                        {
                            return "impossible";
                        }
                    }
                    if (words[1].Equals("hours"))
                    {
                        h = dateValue[i].AddHours(int.Parse(words[0])).Hour;
                    }
                    else if (words[1].Equals("minutes"))
                    {
                        d = dateValue[i].AddMinutes(int.Parse(words[0]));
                        h = d.Hour;
                        m = d.Minute;
                        s = dateValue[i].Second;
                    }
                }
                else
                {
                    Console.WriteLine("Unable to convert '{0}' to a date.", exactPostTime[i]);
                }
            }
            string h1 = ""+h;
            if (h == 0) h1 = "00";
            ans = h1 + ":" + m;
            ans += ":" + s;
            return ans;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace task_1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Tuple<int, int> from, to;
            using (var sr = new StreamReader("in.txt"))
            {
                var s = sr.ReadLine();
                from = Tuple.Create(s[0] - 'a', s[1] - '1');
                s = sr.ReadLine();
                to = Tuple.Create(s[0] - 'a', s[1] - '1');
            }

            var moves = new Tuple<int, int>[]
            {
                Tuple.Create(-1, 2),
                Tuple.Create(1, 2),
                Tuple.Create(2, 1),
                Tuple.Create(2, -1),
                Tuple.Create(1, -2),
                Tuple.Create(-1, -2),
                Tuple.Create(-2, -1),
                Tuple.Create(-2, 1),
            };

            var stack = new Stack<Tuple<int, int>>();
            var parents = new Dictionary<Tuple<int, int>, Tuple<int, int>>();
            stack.Push(from);
            parents[from] = null;
            while (stack.Count != 0)
            {
                var cur = stack.Pop();
                foreach (var move in moves)
                {
                    var t = Tuple.Create(cur.Item1 + move.Item1, cur.Item2 + move.Item2);
                    if (t.Item1 < 0 || t.Item1 > 7 || t.Item2 < 0 || t.Item2 > 7)
                        continue;
                    if (!parents.ContainsKey(t))
                    {
                        parents[t] = cur;
                        stack.Push(t);
                    }
                }
            }

            var res = new List<Tuple<int, int>>();
            for (var t = to; t != null; t = parents[t])
                res.Add(t);

            res.Reverse();
            using (var sw = new StreamWriter("out.txt"))
                foreach (var t in res)
                {
                    sw.WriteLine(new char[] {(char) (t.Item1 + 'a'), (char) (t.Item2 + '1')});
                }
        }
    }
}
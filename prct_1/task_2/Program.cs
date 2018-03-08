using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace task_2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            int n;
            List<int>[] graph;
            using (var sr = new StreamReader("in.txt"))
            {
                n = Convert.ToInt32(sr.ReadLine());
                graph = new List<int>[n];
                for (int i = 0; i < n; i++)
                    graph[i] = sr.ReadLine()
                        .Split(' ')
                        .Where(s => s.Length != 0)
                        .Select(s => Convert.ToInt32(s) - 1)
                        .Where(x => x != -1)
                        .ToList();
            }

            var color = new int[n];
            for (int i = 0; i < n; i++)
            {
                color[i] = 0;
            }

            var resn = 0;
            var stack = new Stack<int>();

            for (int i = 0; i < n; i++)
            {
                if (color[i] != 0) continue;
                color[i] = ++resn;
                stack.Push(i);
                while (stack.Count != 0)
                {
                    var cur = stack.Pop();
                    foreach (var t in graph[cur])
                    {
                        if (color[t] != 0) continue;
                        color[t] = color[cur];
                        stack.Push(t);
                    }
                }
            }

            var res = Enumerable.Range(0, n)
                .GroupBy(i => color[i])
                .OrderBy(g => g.First());
            using (var sw = new StreamWriter("out.txt"))
            {
                sw.WriteLine(resn);
                foreach (var group in res)
                {
                    foreach (var i in group.OrderBy(i => i))
                    {
                        sw.Write((i + 1) + " ");
                    }
                    sw.WriteLine("0");
                }
            }
        }
    }
}
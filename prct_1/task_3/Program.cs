using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace task_3
{
    internal class Program
    {
        private static int INF = -32768;

        private static void TopSort(int[,] matrix, int v, int[] time, int[] res)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[v, i] != INF)
                    TopSort(matrix, i, time, res);
            }

            res[v] = time[0];
            time[0]++;
        }

        public static void Main(string[] args)
        {
            int n;
            int[,] matrix;
            int from, to;
            using (var sr = new StreamReader("in.txt"))
            {
                n = Convert.ToInt32(sr.ReadLine());
                matrix = new int[n, n];
                for (int i = 0; i < n; i++)
                {
                    foreach (var t in Enumerable.Range(0, n)
                        .Zip(sr.ReadLine().Split(' ')
                                .Where(s => s.Length != 0)
                                .Select(s => Convert.ToInt32(s)),
                            Tuple.Create))
                        matrix[i, t.Item1] = t.Item2;
                }

                from = Convert.ToInt32(sr.ReadLine()) - 1;
                to = Convert.ToInt32(sr.ReadLine()) - 1;
            }

            var r = new int[n];
            for (int i = 0; i < n; i++)
            {
                r[i] = -1;
            }
            TopSort(matrix, from, new[]{0}, r);

            var weight = new int[n];
            var parant = new int[n];
            for (int i = 0; i < n; i++)
            {
                weight[i] = INF;
            }

            weight[from] = 0;
            parant[from] = -1;
            foreach (var cur in Enumerable.Range(0, n)
            .Where(i => r[i] != -1)
            .OrderByDescending(i => r[i]))
            {
                foreach (var t in Enumerable.Range(0, n)
                    .Where(v => matrix[cur, v] != INF))
                {
                    if (weight[t] != INF && weight[t] >= weight[cur] + matrix[cur, t]) continue;
                    parant[t] = cur;
                    weight[t] = weight[cur] + matrix[cur, t];
                }
            }

            using (var sw = new StreamWriter("out.txt"))
            {
                if (weight[to] == INF)
                {
                    sw.WriteLine("N");
                    return;
                }
                sw.WriteLine("Y");
                var res = new List<int>();
                for (int i = to; i != -1; i = parant[i])
                    res.Add(i);
                res.Reverse();
                foreach (var i in res)
                {
                    sw.Write((i + 1) + " ");
                }
                sw.WriteLine();
                sw.WriteLine(weight[to]);
            }
        }
    }
}
using System;
using System.IO;
using BaiTap_01;

namespace BaiTap_01
{
    class Program
    {
        static void Main(string[] args)
        {
            Ketqua("input.txt");        
        }
        static void Ketqua(string filename)
        {
            AdjacencyMatrix g = new AdjacencyMatrix();
            Console.WriteLine("Cau 1");
            if (!g.readAdjacencyMatrix(filename))
            {
                return;
            }
            else
            {
                g.showAdjacencyMatrix();
                if (g.isUndirectedGraph() == true)
                {
                    Console.WriteLine("Do thi vo huong");
                    Console.WriteLine("So dinh cua do thi: " + g.n);
                    Console.WriteLine("So canh cua do thi: {0}", g.countEdge());
                    Console.WriteLine("So cap dinh xuat hien canh boi: {0}", g.countMultiple());
                    Console.WriteLine("So canh khuyen: {0}", g.countLoops());
                    Console.WriteLine("So dinh treo: {0}", g.countPendantVertex());
                    Console.WriteLine("So dinh co lap: {0}", g.countIsolatedVertex());
                    g.showDegree();
                }
                else
                {
                    Console.WriteLine("Do thi co huong");
                    Console.WriteLine("So dinh cua do thi: " + g.n);
                    Console.WriteLine("So canh cua do thi: {0}", g.countEdgeDigraph());
                    Console.WriteLine("So cap dinh xuat hien canh boi: {0}", g.countMultipleDigraph());
                    Console.WriteLine("So canh khuyen: {0}", g.countLoopsDigraph());
                    Console.WriteLine("So dinh treo: {0}", g.countPendantVertexDigraph());
                    Console.WriteLine("So dinh co lap: {0}", g.countIsolatedVertexDigraph());
                    g.showDegreeDigraph();
                }            
                g.GraphType();
                Console.WriteLine("========================\nCau 2");
                Console.WriteLine(g.CompleteGraph() ? $"Day la do thi day du K{g.n}" : "Day khong phai la do thi day du");
                Console.WriteLine(g.RegularGraph() ? $"Day la do thi {g.countDegrees()[1]}-chinh quy" : "Day khong phai la do thi chinh quy");
                Console.WriteLine(g.CycleGraph() ? $"Day la do thi vong C{g.n}" : "Day khong phai la do thi vong");
            }
            Console.ReadLine();
        }
    }
}

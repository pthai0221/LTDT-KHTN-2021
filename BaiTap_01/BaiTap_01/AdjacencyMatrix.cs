using System;
using System.Collections;
using System.IO;

namespace BaiTap_01
{
    class AdjacencyMatrix
    {
        public int n;
        public int[,] a;     
        public bool readAdjacencyMatrix(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine("Tap tin khong ton tai");
                return false;
            }
            string[] lines = File.ReadAllLines(filename);
            n = int.Parse(lines[0]);
            a = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] tokens = lines[i + 1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < n; j++)
                    a[i, j] = int.Parse(tokens[j]);
            }
            return true;
        }
        public void showAdjacencyMatrix()
        {
            Console.WriteLine(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(a[i, j]+" ");
                }
                Console.WriteLine();
            }
        }
        //kiểm tra đồ thị đối xứng
        public bool isUndirectedGraph()
        {
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (a[i, j] != a[j, i])
                        return false;
            return true;
        }
        //tính bậc của đỉnh
        public int[] countDegrees()
        {
            int[] degrees = new int[n];
            for (int i = 0; i < n; i++)
            {
                int count = 0;
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] != 0)
                    {
                        count += a[i, j];
                        if (i == j)
                            count += a[i, j];
                    }
                }
                degrees[i] = count;
            }
            return degrees;
        }
        //tính số cạnh đồ thị, dùng định lý bắt tay
        public int countEdge()
        {
            int sum = 0;
            int canh = 0;
            foreach (int item in countDegrees())
                sum += item;
            canh = sum / 2;
            return canh;
        }
        //tính số cặp đỉnh có bội cạnh bội
        public int countMultiple()
        {
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i+1; j < n; j++)
                    if (a[i, j] > 1)
                        count++;
            }
            return count;
        }
        //tính cạnh khuyên
        public int countLoops()
        {
            int count = 0;
            for (int i = 0; i < n; i++)
                if (a[i, i] != 0)
                    count+=a[i,i];
            return count;
        }
        // tính đỉnh treo
        public int countPendantVertex()
        {
            int count = 0;
            foreach (int items in countDegrees())
                if (items == 1)
                    count++;
            return count;
        }
        //tính đỉnh cô lập
        public int countIsolatedVertex()
        {
            int count = 0;
            foreach (int items in countDegrees())
                if (items == 0)
                    count++;
            return count;
        }
        //xuất bậc của đỉnh dt vô hướng
        public void showDegree()
        {
            int count = 0;
            Console.WriteLine("Bac cua tung dinh: ");
            foreach (int items in countDegrees())
            {
                Console.Write(count + "(" + items + ") ");
                count++;
            }
        }
        //xét loại đồ thị
        public void GraphType()
        {
            if (isUndirectedGraph() == true)
            {
                if ( countMultiple() == 0 && countLoops() == 0)
                    Console.WriteLine("\nDon do thi");
                if ( countMultiple() > 0 && countLoops() == 0)
                    Console.WriteLine("\nDa do thi");
                if (countLoops() > 0)
                    Console.WriteLine("\nGia do thi");
            }
            else
            {
                if (isUndirectedGraph() == false && countMultipleDigraph() == 0)
                    Console.WriteLine("\nDo thi co huong");
                if (isUndirectedGraph() == false && countMultipleDigraph() > 0)
                    Console.WriteLine("\nDa do thi co huong");
            }
        }
        public int[] countDegreesDigraph_in()//tính bậc vào đồ thị có hướng
        {
            int[] degrees_v = new int[n];
            for (int i = 0; i < n; i++)
            {
                int count_v = 0;
                for (int j = 0; j < n; j++)
                    if (a[j, i] != 0)
                        count_v += a[j, i];
                degrees_v[i] = count_v;
            }
            return degrees_v;
        }
        //bậc ra đồ thị có hướng
        public int[] countDegreesDigraph_out()
        {
            int[] degrees_r = new int[n];
            for (int i = 0; i < n; i++)
            {
                int count_r = 0;
                for (int j = 0; j < n; j++)
                    if (a[i, j] != 0)
                        count_r += a[i, j];
                degrees_r[i] = count_r;
            }
            return degrees_r;
        }
        //tính cạnh đồ thị có hướng
        public int countEdgeDigraph()
        {
            countDegreesDigraph_in();
            int canh = 0;
            int sum = 0;
            foreach (int items in countDegreesDigraph_in())
                sum += items;
            canh = sum;
            return canh;//cạnh đồ thị vô hướng = tổng bậc ra = tổng bậc vào
        }
        //tính cạnh bội đồ thị có hướng
        public int countMultipleDigraph()
        {
            int count = 0;
            int flag = 0;
            int boi = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (a[i, j] > 1)//nếu có cạnh bội count++
                        count++;
                    if (a[i,j]>1&& a[j,i]>1 && a[i,j]>1)//nếu đỉnh đối xứng có cạnh bội i->j và ngược lại, flag++
                        flag++;
                }                                 
            }
            boi = count-(flag/2);//tính cặp đỉnh có bội
            return boi;
        }
        //đếm khuyên đồ thị có hướng
        public int countLoopsDigraph()
        {
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (a[i, i] != 0)
                    count+=a[i,i];
            }
            return count;
        }
        //tính đỉnh treo đồ thị có hướng
        public int countPendantVertexDigraph()
        {
            int[] Deg_v = countDegreesDigraph_in();
            int[] Deg_r = countDegreesDigraph_out();
            int count = 0;
            int Deg = 0;
            for (int i = 0; i < n; i++)
            {
                Deg = Deg_v[i] + Deg_r[i];
                if (Deg == 1)
                    count++;
            }
            return count;
        }
        //tính đỉnh cô lập đồ thị có hướng
        public int countIsolatedVertexDigraph()
        {
            int[] Deg_v = countDegreesDigraph_in();
            int[] Deg_r = countDegreesDigraph_out();
            int count = 0;
            int Deg = 0;
            for (int i = 0; i < n; i++)
            {
                Deg = Deg_v[i] + Deg_r[i];
                if (Deg == 0)
                    count++;
            }
            return count;
        }
        //xuất bậc của đồ thị có hướng
        public void showDegreeDigraph()
        {
            int[] Deg_v = countDegreesDigraph_in();
            int[] Deg_r = countDegreesDigraph_out();
            Console.WriteLine("(Bac vao - Bac ra) cua tung dinh: ");
            for (int i = 0; i < n; i++)
            {
                Console.Write(i + "(" + Deg_v[i] + "-" + Deg_r[i] + ") ");
            }
        }
        //xét đồ thị đầy đủ
        public bool CompleteGraph()
        {
            int count = 0;
            int flag = 0;
            if (isUndirectedGraph() == true && countMultiple()==0 && countLoops()==0)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = i + 1; j < n; j++)
                        if (a[i, j] != 0 && a[i, i] == 0)
                            count++;
                    if (count == (n - (i + 1)))
                        flag++;
                    count = 0;
                }
                if (flag == n)
                    return true;
            }
            return false;
        }
        //Xét đồ thị chính quy
        public bool RegularGraph()
        {
            bool kq = false;
            if (isUndirectedGraph() == true && countMultiple()==0 && countLoops()==0)
            {
                int[] degrees = countDegrees();
                for (int i = 0; i < n - 1; i++)
                {
                    if (degrees[i] == degrees[i + 1])
                        kq = true;
                    else
                        kq = false;
                }
            }
            return kq;
        }
        //xét đồ thị vòng, duyệt từ đỉnh 0.
        //dùng stack nếu đã duyệt cho vào stack, nếu tạo thành chu trình => đt vòng, nếu ko tạo thành chu trình break dừng vòng lập => ko phai dt vong
        public bool CycleGraph()
        {
            int[] degrees = countDegrees();
            Stack mystack = new Stack();
            int batdau = 0;
            mystack.Push(batdau);//add dinh bắt đầu ( thường chọn đỉnh nhỏ nhất là đỉnh 0)
            if (isUndirectedGraph() == true && n >= 3 && countMultiple() == 0 && countLoops() == 0)
                for (int i = batdau; mystack.Count < n; i = (int)mystack.Peek())
                {
                    if (degrees[i] == 2)
                        for (int j = 0; j < n; j++)
                        {
                            if (a[i, j] == 1 && !mystack.Contains(j))
                            {
                                mystack.Push(j);
                                break;                              
                            }
                        }
                    if ((int)mystack.Peek() == i)
                        break;
                }
            if (mystack.Count == n && a[(int)mystack.Peek(), batdau] != 0)
                return true;
            return false;
        }
    }
}

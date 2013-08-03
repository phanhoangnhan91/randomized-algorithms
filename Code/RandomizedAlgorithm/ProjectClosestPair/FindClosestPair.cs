using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ProjectClosestPair
{
    public partial class FindClosestPair : Form
    {
       Random random;
        public FindClosestPair()
        {
            InitializeComponent();
            random = new Random();
            lbresult.Text = "";

            //first running
            IPoint[] testP = new IPoint[6] { new IPoint(2, 2), new IPoint(5, 5),new IPoint(4,2),new IPoint(3,4),new IPoint(2,6),new IPoint(10,11) };
            double testdistanceBF = double.MaxValue,testdistanceDC,testdistanceRa;
            IPoint[] resultBF = bruteForce(testP, ref testdistanceBF);
            IPoint[] resultDC = closestDC(testP, out testdistanceDC);
            IPoint[] resultRA = RandomizedAl(testP, out testdistanceRa);

            listViewDuLieu.ListViewItemSorter = new IntegerComparer(1);
            listViewDuLieu.Sort();
        }


        private void FindClosestPair_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        //Nút sinh mảng điểm ngẫu nhiên
        private void btInput_Click(object sender, EventArgs e)
        {
            try
            {
                int countP;

                string inputK = Microsoft.VisualBasic.Interaction.InputBox("The size of input array:", "Random input", "2", 200, 200);
                if (!int.TryParse(inputK, out countP)&& inputK!="")
                {
                    throw new Exception("Invalid value");
                }

                //refresh 
                tbInput.Clear();
                lbresult.Text = "";
                
                StringBuilder strInput = new StringBuilder();
                IPoint[] P = new IPoint[countP];

                for (int i = 0; i < P.Length; i++)
                {
                    P[i] = new IPoint(random.Next(0,P.Length*10), random.Next(0,P.Length*10));
                    strInput.Append(string.Format("({0},{1})\t", P[i]._X, P[i]._Y));
                }
                tbInput.Text = strInput.ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private IPoint[] ReadArrP()
        {
            string[] chuoi_cat =tbInput.Text.Trim().Split('\t','\t');
            IPoint[] dsdiem = new IPoint[chuoi_cat.Length];
            try
            {
                for (int i = 0; i < chuoi_cat.Length; i++)
                {
                    chuoi_cat[i] = chuoi_cat[i].Replace("(", "");
                    chuoi_cat[i] = chuoi_cat[i].Replace(")", "");
                    string[] xy = chuoi_cat[i].Split(',');
                    dsdiem[i] = new IPoint(Convert.ToInt32(xy[0]), Convert.ToInt32(xy[1]));
                    
                }
                return dsdiem;
            }
            catch
            {
                MessageBox.Show("The input has a invalid point");
                return null;
            }
         
        }
        #region Brute Force
        private void btBF_Click(object sender, EventArgs e)
        {
            try
            {
                //Đọc mảng từ input
                IPoint[] inputP = ReadArrP();

                //có lỗi xảy ra
                if (inputP == null)
                    return;
                Stopwatch swBF = new Stopwatch();
                swBF.Start();
                double distance = double.MaxValue;
                IPoint[] result = bruteForce(inputP, ref distance);
                swBF.Stop();
                long microsecondsBF = swBF.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                ThemDuLieuVaoList(inputP.Length, "Brute Force", microsecondsBF.ToString());
                lbresult.Text = string.Format("[Brute Force]\nThe closest pair of points:({0},{1})---({2},{3}). Distance: {4}\nThe running time: {5} microseconds", result[0]._X, result[0]._Y, result[1]._X, result[1]._Y,Math.Round(distance,3),microsecondsBF.ToString());
            }
            catch
            {
            }
        }
         // A Brute Force method to return the smallest distance between two points
        //in array P[]
        private IPoint[] bruteForce(IPoint[] P,ref double minDist)
        {
            double distTmp = minDist;
            IPoint[] result = new IPoint[2];
            for (int i = 0; i < P.Length; ++i)
            {
                for (int j = i + 1; j < P.Length; ++j)
                {
                    double distXY = distanceXY(P[i], P[j]);
                    if (distXY < minDist)
                    {
                        minDist = distXY;
                        result[0] = P[i];
                        result[1] = P[j];
                    }
                }
            }
            if (distTmp == minDist)
                return null;
            return result;
        }
        #endregion

        #region Divide and Conquer
        private void btDC_Click(object sender, EventArgs e)
        {
            try
            {
                //Đọc mảng từ textbox input
                IPoint[] inputP = ReadArrP();

                //có lỗi xảy ra
                if (inputP == null)
                    return;

                
                double distance;
                Stopwatch swDC = new Stopwatch();
                swDC.Start();
                //Sắp xếp mảng điểm theo hoành độ
                var sortedX = (from arr in inputP
                               orderby arr._X
                               select arr).ToArray();
                IPoint[] result = closestDC(sortedX, out distance);
                swDC.Stop();
                long microsecondsDC = swDC.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                lbresult.Text = string.Format("[Divide and Conquer]\nThe closest pair of points:({0},{1})---({2},{3}). Distance: {4}\nThe running time: {5} microseconds", result[0]._X, result[0]._Y, result[1]._X, result[1]._Y, Math.Round(distance, 3), microsecondsDC.ToString());
                ThemDuLieuVaoList(inputP.Length, "Divide-Conquer", microsecondsDC.ToString());
            }
            catch
            {
            }
        }
        private IPoint[] closestDC(IPoint[] P,out double distance)
        {
             distance = double.MaxValue;

            //Nếu đầu vào có 2 hoặc 3 điểm sử dụng chiến lược trực tiếp
            if (P.Length <= 3)
            {             
                return bruteForce(P,ref distance);
            }
            IPoint[] result = new IPoint[2];

            //Chia mảng làm hai phần
            int mid = (int)P.Length / 2;
            IPoint[] pointLeft = new IPoint[mid];
            IPoint[] pointRight = new IPoint[P.Length - mid];
            for (int i = 0; i < mid; i++)
                pointLeft[i] = P[i];
            for (int i = 0; i < P.Length - mid; i++)
                pointRight[i] = P[i + mid];

            //Tiến hành đệ qui để tìm khoảng cách ngắn nhất của mỗi mảnh
            // Tìm khoảng cách ngắn nhất ở mảng trái và mảng phải
            IPoint[] pairLeft = new IPoint[2];
            IPoint[] pairRight = new IPoint[2];
            double distanceL , distanceR ;

            pairLeft = closestDC(pointLeft,out distanceL);
            pairRight = closestDC(pointRight,out distanceR);
            //Chọn khoảng cách ngắn nhất giữa 2 kết quả
            if (distanceL > distanceR)
            {
                result = pairRight;
                distance = distanceR;
            }
            else
            {
                result = pairLeft;
                distance = distanceL;
            }

            //Tạo mảng gồm các điểm có hoành độ đến điểm giữa mảng nhỏ hơn khoảng cách được chọn ở trên
            //Tạo tập điểm midPoints
            int midCoordinate = P[mid]._X;
            double xDown = midCoordinate - distance;
            double xUpper = midCoordinate + distance;
            var midPoints = (from arr in P
                             where arr._X > xDown && arr._X < xUpper
                             orderby arr._Y
                             select arr).ToArray();

            // Khoảng cách ngắn nhất là khoảng cách ngắn nhất được tìm thấy trong tập hợp điểm midPoints
            if (midPoints.Length > 1)
            {
                IPoint[] resultTmp = bruteForce(midPoints,ref distance);
                if (resultTmp != null)
                    return resultTmp;
            }
            return result;
        }
        #endregion

        #region Randomize Algorithm
        private void btRA_Click(object sender, EventArgs e)
        {
            try
            {
                //Đọc mảng từ textbox input
                IPoint[] inputP = ReadArrP();

                //có lỗi xảy ra
                if (inputP == null)
                    return;
                recursive = true;
                inrecurisve = false;

                Stopwatch swRa = new Stopwatch();
                swRa.Start();
                double distance;
                IPoint[] result = RandomizedAl(inputP, out distance);
                swRa.Stop();
                long microsecondsRa = swRa.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                lbresult.Text = string.Format("[Randomized Algorithm]\nThe closest pair of points:({0},{1})---({2},{3}). Distance: {4}\nThe running time: {5} microseconds", result[0]._X, result[0]._Y, result[1]._X, result[1]._Y, Math.Round(distance, 3), microsecondsRa.ToString());
                ThemDuLieuVaoList(inputP.Length, "Randomized", microsecondsRa.ToString());
            }
            catch
            {
            }
           
        }

        bool recursive = true,inrecurisve=false;
        private IPoint[] RandomizedAl(IPoint[] P, out double distantRA)
        {
            distantRA = double.MaxValue;

            if (P.Length <= 3)
            {
                return bruteForce(P, ref distantRA);
            }

            //Chọn ngẫu nhiên n^2/3 điểm từ input
            //Tạo thành mảng S[]
            int rp = (int)Math.Round(Math.Pow(P.Length, (2 * 1.0 / 3)), 0);
            IPoint[] S = new IPoint[rp];

            for (int i = 0; i < S.Length; i++)
            {
                bool isSame = false;
                int r = random.Next(0, P.Length - 1);
                for (int j = 0; j < i; j++)
                {
                    if (S[j] == P[r])
                        isSame = true;
                }
                if (!isSame)
                    S[i] = P[r];
                else i--;
            }

            //Tính khoảng cách ngắn nhất trong mảng S[], bằng cách đệ qui gọi hàm RandomizedAl 1 lần
            //Đặt làm denta
            double denta=double.MaxValue;
            //Trong lần đệ qui đó dùng chiến lược trực tiếp để tìm khoảng cách denta
            if (inrecurisve)
            {
                inrecurisve = false;
                IPoint[] tmp = bruteForce(S, ref denta);
            }
            if (recursive)
            {
                recursive = false;
                inrecurisve = true;
                IPoint[] resultS = RandomizedAl(S, out denta);
            }

            //Xác định số lượng ô vuông có cạnh bằng denta:
            //Xác định hoành độ lớn nhất và tung độ lớn nhất trong các điểm 
            //Chọn giá trị lớn và đem chia cho denta
            int maxX, maxY;
            int SquareInRow = 0;
            FindMaxXY(P, out maxX, out maxY);

            int maxValue = maxX >= maxY ? maxX : maxY;
            SquareInRow = (int)Math.Round(maxValue / denta); 
    
            if (SquareInRow * denta <= maxValue)
                SquareInRow++;

            //Tạo bộ T gồm các ô vuông có cạnh= denta
            Square[] T = new Square[SquareInRow*SquareInRow];

            for (int i = 0; i < SquareInRow*SquareInRow; i++)
                T[i] = new Square();
            for (int i = 0; i < P.Length; i++)
            {
                double a =P[i]._X / denta;
                double b = P[i]._Y / denta;
                //Xác định ô vuông chứa điểm đang xét
                T[(int)a * SquareInRow + (int)b].lstPoints.Add(P[i]);
            }

            //Tạo các bộ T1, T2, T3, T4
            List<Square> T1, T2;
            List<Square> T3, T4;

            createAllT(T, SquareInRow, out T1, out T2, out T3, out T4);
            IPoint[] resultP = new IPoint[2];

            //Tìm khoảng cách ngắn nhất ở từng ô vuông ở tập T1, T2, T3, T4
            foreach (var item in T1)
            {
                if (item.lstPoints.Count > 1)
                {
                   IPoint[] resultTmp = bruteForce(item.lstPoints.ToArray(), ref distantRA);
                   if (resultTmp != null)
                   {
                       resultP = resultTmp;                      
                   }
                }
            }
            foreach (var item in T2)
            {
                if (item.lstPoints.Count > 1)
                {
                    IPoint[] resultTmp = bruteForce(item.lstPoints.ToArray(), ref distantRA);
                    if (resultTmp != null)
                        resultP = resultTmp;
                }
            }
            foreach (var item in T3)
            {
                if (item.lstPoints.Count > 1)
                {
                    IPoint[] resultTmp = bruteForce(item.lstPoints.ToArray(), ref distantRA);
                    if (resultTmp != null)
                        resultP = resultTmp;
                }
            }
            foreach (var item in T4)
            {
                if (item.lstPoints.Count > 1)
                {
                    IPoint[] resultTmp = bruteForce(item.lstPoints.ToArray(), ref distantRA);
                    if (resultTmp != null)
                        resultP = resultTmp;
                }
            }

            return resultP;
        }

        private List<IPoint> HashP(Square[] T, int SquareRow, int _index)
        {
            List<IPoint> result = new List<IPoint>();
            result.AddRange(T[_index].lstPoints);
            result.AddRange(T[_index + SquareRow].lstPoints);
            return result;
        }

        private void createAllT(Square[] T, int SquareRow, out List<Square> T1, out List<Square> T2, out List<Square> T3, out List<Square> T4)
        {
            T1 = new List<Square>();
            T2 = new List<Square>();
            T3 = new List<Square>();
            T4 = new List<Square>();
            bool isAddT4 = true;
            List<IPoint> tmp = new List<IPoint>();
            int h = 0;
            while(h<SquareRow*(SquareRow-1))
            {
                //h thuộc hàng chẵn thực hiện trên T1 và T2
                if ((h / SquareRow) % 2 == 0)
                {
                    if (h % SquareRow == 0)
                    {
                        List<IPoint> l1 = HashP(T, SquareRow, h);
                        List<IPoint> l2 = HashP(T, SquareRow, h + 1);
                        Square sG1 = new Square(l1, l2);
                        T1.Add(sG1);
                        tmp = l2;
                        h++;
                        continue;
                    }
                    //chưa tới biên phải
                    if (h + 1 < SquareRow * (h / SquareRow + 1))
                    {
                        List<IPoint> l2 = HashP(T, SquareRow, h + 1);
                        Square s = new Square(tmp, l2);

                        //h lẻ thì thêm vào T2
                        if (h % 2 != 0 && s.lstPoints.Count != 0)
                            T2.Add(s);
                        else if (h % 2 == 0 && s.lstPoints.Count != 0)
                            T1.Add(s);
                        tmp = l2;
                    }
                }
                //h thuộc hàng lẻ thực hiện trên T3 T4
                else
                {
                    if (h % SquareRow == 0)
                    {
                        List<IPoint> l1 = HashP(T, SquareRow, h);
                        List<IPoint> l2 = HashP(T, SquareRow, h + 1);
                        Square sG1 = new Square(l1, l2);
                        T3.Add(sG1);
                        tmp = l2;
                        h++;
                        isAddT4 = true;
                        continue;
                    }
                    //chưa tới biên phải dòng hiện tại
                    if (h + 1 < SquareRow * (h / SquareRow + 1))
                    {
                        List<IPoint> l = HashP(T, SquareRow, h + 1);
                        Square s = new Square(tmp, l);
                        //Thay phiên thêm cho T3 và T4
                        if (isAddT4)
                        {
                            isAddT4 = false;
                            if (s.lstPoints.Count != 0)
                                T4.Add(s);
                        }
                        else
                        {
                            isAddT4 = true;
                            if (s.lstPoints.Count != 0)
                                T3.Add(s);
                        }
                        tmp = l;
                    }
                }
                h++;
            }
        }
 
        private void FindMaxXY(IPoint[] points, out int maxX, out int maxY)
        {
            maxX = points[0]._X;
            maxY = points[0]._Y;
            for (int i = 1; i < points.Length; i++)
            {
                if (points[i]._X > maxX)
                    maxX = points[i]._X;
                if (points[i]._Y > maxY)
                    maxY = points[i]._Y;
            }
        }

        private double distanceXY(IPoint p, IPoint q)
        {
            return Math.Sqrt(Math.Pow(p._X - q._X, 2) + Math.Pow(p._Y - q._Y, 2));
        }

        #endregion

        private void ThemDuLieuVaoList(int N, string name, string time)
        {
            ListViewItem i = new ListViewItem(name);
            i.SubItems.Add(N.ToString());
            i.SubItems.Add(time);
            this.listViewDuLieu.Items.Add(i);
            DrawChart();
        }

        private void DrawChart()
        {
            chart1.Series.Clear();
            chart1.Visible = true;
            Series sBF = new Series("Brute Force");
            Series sDC = new Series("Divide-Conquer");
            Series sRa = new Series("Randomized");
            for (int i = 0; i < listViewDuLieu.Items.Count; i++)
            {
                int x = Convert.ToInt32(listViewDuLieu.Items[i].SubItems[1].Text);
                int y = Convert.ToInt32(listViewDuLieu.Items[i].SubItems[2].Text);
                if (listViewDuLieu.Items[i].SubItems[0].Text == "Brute Force")
                    sBF.Points.AddXY(x, y);
                else if (listViewDuLieu.Items[i].SubItems[0].Text == "Divide-Conquer")
                    sDC.Points.AddXY(x, y);
                else
                    sRa.Points.AddXY(x, y);

            }
            chart1.Series.Add(sBF);
            chart1.Series.Add(sDC);
            chart1.Series.Add(sRa);
            chart1.Series[0].Color = Color.Blue;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].BorderWidth = 2;
            chart1.Series[1].Color = Color.Orange;
            chart1.Series[1].ChartType = SeriesChartType.Line;
            chart1.Series[1].BorderWidth = 2;
            chart1.Series[2].Color = Color.Red;
            chart1.Series[2].ChartType = SeriesChartType.Line;
            chart1.Series[2].BorderWidth = 2;
        }


        private void btDel_Click(object sender, EventArgs e)
        {
            this.listViewDuLieu.Items.Clear();
            chart1.Series.Clear();
        } 
    }
    public class Square
    {
        public List<IPoint> lstPoints { get; set; }
        public Square()
        {
            this.lstPoints = new List<IPoint>();
        }
        public Square(List<IPoint> l1, List<IPoint> l2)
        {
            this.lstPoints = new List<IPoint>();
            lstPoints.AddRange(l1);
            lstPoints.AddRange(l2);
        }
    }    
}

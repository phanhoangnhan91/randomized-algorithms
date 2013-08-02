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

namespace ProjectQuicksort
{
    
    public partial class QuicksortForm : Form
    {
        Random random=new Random();
        int[] qsArray;
        public QuicksortForm()
        {
            InitializeComponent();
            listViewDuLieu.ListViewItemSorter = new IntegerComparer(1);
            listViewDuLieu.Sort();
            int[] startArr = new int[2] { 1, 2 };
            quickSort(startArr, 0, startArr.Length - 1);
            randomizedQuicksort(startArr, 0, startArr.Length - 1);
        }

       
        #region Random Array button
        private void bt_RandomArray_Click(object sender, EventArgs e)
        {
            try
            {
                tb_Input.Clear();
                tb_OutputQS.Clear();
                tb_OutputRQS.Clear();
                lbQS.Text = "";
                lbRQS.Text = "";
                int so_pt;
                string inputK = Microsoft.VisualBasic.Interaction.InputBox("The size of input array:", "Random input", "2", 200, 200);
                if (!int.TryParse(inputK, out so_pt))
                {
                    throw new Exception("Invalid value");
                }
                qsArray = new int[so_pt];
                StringBuilder str = new StringBuilder();
                for (int i = 0; i < so_pt; i++)
                {
                    qsArray[i] = random.Next(-5000000, 5000000);

                }
                Array.Sort(qsArray);
                Array.Reverse(qsArray);
                foreach (int i in qsArray)
                {
                    str.Append(i.ToString() + " ");
                }

                tb_Input.Text = str.ToString().TrimEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Read write array 
        private void createArray()
        {
            string[] chuoi_cat = tb_Input.Text.Trim().Split(' ');
            qsArray = new int[chuoi_cat.Length];
            try
            {
                for (int i = 0; i < chuoi_cat.Length; i++)
                {
                    int so = 0;
                    if (!int.TryParse(chuoi_cat[i], out so))
                        throw new Exception("mảng nhập không hợp lệ");
                    qsArray[i] = so;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private string readArray(int[] array)
        {
            StringBuilder content = new StringBuilder();
            for (int i = 0; i < array.Length; i++)
                content.Append(array[i] + " ");
            return content.ToString().TrimEnd();
        }
        #endregion
       
        #region Quicksort (classic)
        private void bt_QuickSort_Click(object sender, EventArgs e)
        {
            tb_OutputQS.Clear();
            lbQS.Text = "";
            createArray();
            Stopwatch sw = Stopwatch.StartNew();
            quickSort(qsArray, 0, qsArray.Length - 1);           
            sw.Stop();            
            long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
            ThemDuLieuVaoList(qsArray.Length,"Quicksort", microseconds.ToString());
            tb_OutputQS.Text = readArray(qsArray);
            lbQS.Text = "Running time: " + microseconds.ToString() + " microseconds";
        }
        void quickSort(int[] a, int l, int r)
        {
            if (l < r)
            {
                int q = Parttition(a, l, r);
                quickSort(a, l, q - 1);
                quickSort(a, q + 1, r);
            }
          
        }
        #endregion

        #region Quicksort (randomized)
        private void bt_RAQuicksort_Click(object sender, EventArgs e)
        {
            tb_OutputRQS.Clear();
            lbRQS.Text = "";
            createArray();
            Stopwatch sw = Stopwatch.StartNew();
            randomizedQuicksort(qsArray, 0, qsArray.Length-1);
            sw.Stop();
            long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
            ThemDuLieuVaoList(qsArray.Length,"Randomized quicksort", microseconds.ToString());
            tb_OutputRQS.Text = readArray(qsArray);
            lbRQS.Text = "Running time: " + microseconds.ToString() + " microseconds";
        }

        private void randomizedQuicksort(int[] a, int p, int r)
        {
            if (p < r)
            {
                int q=randomizedPartition(a,p,r);
                randomizedQuicksort(a, p, q - 1);
                randomizedQuicksort(a, q + 1, r);
            }
        }
        //Chọn chốt
        private int randomizedPartition(int[] a, int p, int r)
        {
            //Chọn chỉ mục i ngẫu nhiên trong mảng.
            int i = random.Next(p, r+1);
            //Hoán vị trí của phần tử thứ i với phần tử cuối mảng.
            permutation(ref a[i],ref a[r]);
            return Parttition(a, p, r);
        }
        // Sắp xếp mảng: phần tử nhỏ hơn bằng chốt về bên trái chốt và phần tử lớn hơn chốt về bên phải chốt
        private int Parttition(int[] a, int p, int r)
        {
            int x = a[r];
            int i = p - 1;
            for (int j = p; j < r; j++)
            {
                if (a[j] <= x)
                {
                    i++;
                    permutation(ref a[i],ref a[j]);
                }
            }
            permutation(ref a[i + 1], ref a[r]);
            return i + 1;
        }

        private void permutation(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
        #endregion

        private void ThemDuLieuVaoList(int N,string name, string time)
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
            Series sQS = new Series("Quicksort");
            Series sRQS = new Series("Randomized quicksort");
            for (int i = 0; i < listViewDuLieu.Items.Count; i++)
            {
                int x = Convert.ToInt32(listViewDuLieu.Items[i].SubItems[1].Text);
                int y = Convert.ToInt32(listViewDuLieu.Items[i].SubItems[2].Text);
                if (listViewDuLieu.Items[i].SubItems[0].Text == "Quicksort")
                    sQS.Points.AddXY(x, y);
                else
                    sRQS.Points.AddXY(x, y);

            }
            chart1.Series.Add(sQS);
            chart1.Series.Add(sRQS);
            chart1.Series[0].Color = Color.Blue;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].BorderWidth = 2;
            chart1.Series[1].Color = Color.Red;
            chart1.Series[1].ChartType = SeriesChartType.Line;
            chart1.Series[1].BorderWidth = 2;
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            listViewDuLieu.Items.Clear();
            chart1.Series.Clear();
        }

        private void bt_SapXep_Click(object sender, EventArgs e)
        {
            tb_OutputQS.Clear();
            createArray();
            int[] inputRA =(from inputArr in qsArray
                                select inputArr).ToArray();
            Stopwatch swRQS = Stopwatch.StartNew();
            // randomizedQuicksort(qsArray,0,qsArray.Length-1);
            randomizedQuicksort(qsArray, 0, qsArray.Length - 1);
            swRQS.Stop();
            long microsecondsRQS = swRQS.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
            Stopwatch sw = Stopwatch.StartNew();
            quickSort(qsArray, 0, qsArray.Length - 1);
            sw.Stop();
            long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
            ThemDuLieuVaoList(qsArray.Length, microseconds.ToString(), microsecondsRQS.ToString());
            tb_OutputQS.Text = readArray(qsArray);
        }

        private void QuicksortForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}

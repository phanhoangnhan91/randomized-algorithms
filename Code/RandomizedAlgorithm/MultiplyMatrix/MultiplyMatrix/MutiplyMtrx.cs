﻿using System;
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

namespace MultiplyMatrix
{
    public partial class MutiplyMtrx : Form
    {
        Random random;

        public MutiplyMtrx()
        {
            InitializeComponent();
            random = new Random();
            listViewDuLieu.ListViewItemSorter = new IntegerComparer(1);
            listViewDuLieu.Sort();
            //Chạy nháp
            int[,]tmpA=new int[2,2]{{2,4},{3,4}},tmpB=new int[2,2]{{3,5},{2,5}},tmpC=new int[2,2]{{4,6},{12,13}};
            //Brute Force
            int[,]MTResultBF = MultiplyMT(tmpA, tmpB, tmpA.GetLength(0),tmpA.GetLength(1), tmpB.GetLength(1));
            bool compareBF = CompareMT(MTResultBF, tmpC);

            //Freivalds
            int loop = 2;
            bool compareF = MatrixEqualityTest(tmpA, tmpB, tmpC, ref loop);

        }



        #region Phép tính ma trận
        //SO sánh hai ma trận
        private bool CompareMT(int[,] A, int[,] B)
        {
            for (int i = 0; i < A.GetLength(0); i++)
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (A[i, j] != B[i, j])
                        return false;
                }
            return true;
        }
        //Trừ ma trận
        private int[,] SubtractionMT(int[,] A, int[,] B, int d, int c)
        {
            int[,] Result = new int[d, c];
            for (int i = 0; i < d; i++)
                for (int j = 0; j < c; j++)
                    Result[i, j] = A[i, j] - B[i, j];
            return Result;
        }
        //Nhân ma trận
        private int[,] MultiplyMT(int[,] A, int[,] B, int dA, int cA, int cB)
        {
            int[,] Result = new int[cA, cB];
            for (int i = 0; i < dA; i++)
                for (int j = 0; j < cB; j++)
                {
                    for (int k = 0; k < cA; k++)
                    {
                        Result[i, j] += A[i, k] * B[k, j];
                    }
                }
            return Result;
        }
        #endregion

        #region nhập xuất ma trận

        private string PrintMatrix(int[,] MT)
        {
            StringBuilder str = new StringBuilder();

            for (int i = 0; i < MT.GetLength(0); i++)
            {
                for (int j = 0; j < MT.GetLength(1); j++)
                {
                    str.Append(MT[i, j].ToString() + " ");
                }
                str.AppendLine();
            }
            return str.ToString().TrimEnd();
        }

        private int[,] createMT(string name,string text,int MTsize)
        {
            try
            {
                int[,] error = new int[1, 1];
                int[,] Rs = new int[MTsize, MTsize];
                string[] Rows = text.Split('\n');
                int i = 0, j = 0;
                foreach (string row in Rows)
                {
                    string r = row.TrimEnd();
                    string[] number = r.Split(' ');
                    if (number.Length != MTsize)   // ma trận có kích thước khác với kích thước nhập ban đầu
                    {
                        throw new Exception("The size of matrices must be equal to "+MTsize);
                    }
                    for (j = 0; j < MTsize; j++)
                    {
                        if (!int.TryParse(number[j], out Rs[i, j])) //ép kiểu không được
                        {
                            throw new Exception("Invalid value in matrix " + name);
                        }
                    }
                    i++;
                }
                return Rs;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        #endregion

        private void btRInput_Click(object sender, EventArgs e)
        {
            try
            {
                tbMTA.Clear();
                tbMTB.Clear();
                tbMTC.Clear();
                tbResultBF.Clear();
                tbResultF.Clear();
                lbResult1.Text = "";
                lbResult2.Text = "";

                int MTsize;
                if (!int.TryParse(tbSize.Text, out MTsize)) //ktra nhập số
                {
                    throw new Exception("The size of matrices must be a positive integer");
                }
                if (MTsize < 2)  //kích thước ma trận phải >=2
                {
                    throw new Exception("The size of matrices must be equal or greater than 2");
                }
                int[,] MTA = new int[MTsize, MTsize], MTB = new int[MTsize, MTsize], MTC = new int[MTsize, MTsize];
                for (int i = 0; i < MTsize; i++)
                {
                    for (int j = 0; j < MTsize; j++)
                    {
                        MTA[i, j] = random.Next(0, 10);
                        MTB[i, j] = random.Next(0, 10);
                        MTC[i, j] = random.Next(0, 1000);
                    }
                } 
                tbMTA.Text = PrintMatrix(MTA);
                tbMTB.Text = PrintMatrix(MTB);
                tbMTC.Text = PrintMatrix(MTC);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btMultiply_Click(object sender, EventArgs e)
        {
            try
            {
                int MTsize;
                if (!int.TryParse(tbSize.Text, out MTsize)) //ktra nhập số
                {
                    throw new Exception("The size of matrices must be a positive integer");
                }
                if (MTsize < 2)  //kích thước ma trận phải >=2
                {
                    throw new Exception("The size of matrices must be equal or greater than 2");
                }
                tbResultBF.Clear();
                lbResult1.Text = "";
                int[,] MTResult;
                bool compare = true;

                int[,]MTA = createMT("A",tbMTA.Text,MTsize);
                if (MTA == null)
                    return;
                int[,] MTB = createMT("B",tbMTB.Text,MTsize);
                if (MTB == null)
                    return;
                int[,] MTC = createMT("C",tbMTC.Text,MTsize);
                if (MTC == null)
                    return;

                Stopwatch sw = Stopwatch.StartNew();
                MTResult = MultiplyMT(MTA, MTB, MTsize, MTsize, MTsize);
                compare = CompareMT(MTResult, MTC);
                sw.Stop();

                //Thời gian chạy 
                long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                ThemDuLieuVaoList(MTsize, "Brute Force", microseconds.ToString());

                //Xuất kết quả
                string rs = "Result: ";
                if (compare)
                    rs += "AxB=C\r\n";
                else
                    rs += "AxB!=C\r\n";
                rs += "Running time: " + microseconds.ToString() + " microseconds";
                lbResult1.Text = rs;
                tbResultBF.Text = PrintMatrix(MTResult);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        string strVector = "";
        private void btRA_Click(object sender, EventArgs e)
        {
            try
            {
               
                int MTsize;
                if (!int.TryParse(tbSize.Text, out MTsize)) //ktra nhập số
                {
                    throw new Exception("The size of matrices must be a positive integer");
                }
                if (MTsize < 2)  //kích thước ma trận phải >=2
                {
                    throw new Exception("The size of matrices must be equal or greater than 2");
                }
                dem = 0;
                lbResult2.Text = "";
                tbResultF.Clear();
                string inputK = Microsoft.VisualBasic.Interaction.InputBox("k=", "The number of trials ", "1", 320, 320);
                int k;
                if (!int.TryParse(inputK, out k))
                {
                     throw new Exception("Invalid value");
                }
                int[,] MTA = createMT("A", tbMTA.Text, MTsize);
                if (MTA == null)
                    return;
                int[,] MTB = createMT("B", tbMTB.Text, MTsize);
                if (MTB == null)
                    return;
                int[,] MTC = createMT("C", tbMTC.Text, MTsize);
                if (MTC == null)
                    return;
                //if (startF) //chạy nháp ko tính 
                //{
                //    bool compareDrafts = MatrixEqualityTest(MTA, MTB, MTC,ref k);
                //    startF = false;
                //}
                Stopwatch sw = Stopwatch.StartNew();
                bool compare = MatrixEqualityTest(MTA, MTB, MTC,ref k);
                sw.Stop();

                //Thời gian chạy 
                long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                ThemDuLieuVaoList(MTsize, "Freivalds", microseconds.ToString());

                //Xuất kết quả
                tbResultF.Text = strVector;
                string rs = "The number of trials: " + dem.ToString() + "\r\nResult: ";
                if (compare)
                    rs += "AxB=C\r\n";
                else
                    rs += "AxB!=C\r\n";
                rs += "Running time " + microseconds.ToString() + " microseconds";
                lbResult2.Text = rs;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        int dem;
        private bool MatrixEqualityTest(int[,] MTA, int[,] MTB, int[,] MTC,ref int k)
        {
            strVector = "";
            int MTsize=MTA.GetLength(0);
            for (int j = 0; j < k; j++)
            {               
                //Tạo vector ngẫu nhiên r
                int[,] vectorR = new int[MTsize, 1];
                for (int i = 0; i < MTsize; i++)
                {
                    vectorR[i, 0] = random.Next(0, 2);
                }
                //mỗi lần chạy in vector ngẫu nhiên 
                strVector += "random vector in trial " + (j + 1).ToString() + ":\r\n" + PrintMatrix(vectorR) + "\r\n";
                dem++;
                
                int[,] MTResultL, MTResultR;
                //A x (B x r)
                MTResultL = MultiplyMT(MTB, vectorR, MTsize, MTsize, 1);
                MTResultL = MultiplyMT(MTA, MTResultL, MTsize, MTsize, 1);

                //C x r
                MTResultR = MultiplyMT(MTC, vectorR, MTsize, MTsize, 1);

                //A x (B x r)!=C x r
                if (!CompareMT(MTResultR, MTResultL))
                    return false;
            }
            return true;
        }

        private void ThemDuLieuVaoList(int size, string name, string time)
        {
            ListViewItem i = new ListViewItem(name);
            i.SubItems.Add(size.ToString());
            i.SubItems.Add(time);
            this.listViewDuLieu.Items.Add(i);
            DrawChart();
        }

        private void DrawChart()
        {
            chart1.Series.Clear();
            chart1.Visible = true;
            Series sBF = new Series("Brute Force");
            Series sF = new Series("Freivalds");
            for (int i = 0; i < listViewDuLieu.Items.Count; i++)
            {
                int x = Convert.ToInt32(listViewDuLieu.Items[i].SubItems[1].Text);
                int y = Convert.ToInt32(listViewDuLieu.Items[i].SubItems[2].Text);
                if (listViewDuLieu.Items[i].SubItems[0].Text == "Brute Force")
                    sBF.Points.AddXY(x, y);
                else
                    sF.Points.AddXY(x, y);

            }
            chart1.Series.Add(sBF);
            chart1.Series.Add(sF);
            chart1.Series[0].Color = Color.Blue;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].BorderWidth = 2;
            chart1.Series[1].Color = Color.Red;
            chart1.Series[1].ChartType = SeriesChartType.Line;
            chart1.Series[1].BorderWidth = 2;
        }

        private void btDel_Click(object sender, EventArgs e)
        {
            this.listViewDuLieu.Items.Clear();
            this.chart1.Series.Clear();
        }

        private void MutiplyMtrx_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.A))
            {
                ((TextBox)sender).SelectAll();
                e.Handled = true;
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Client;

namespace Kruskal
{
    public partial class frmMinimuSpanningStree : Form
    {

        #region Member Variables

        const int _radius = 20;
        const int _halfRadius = (_radius / 2);

        Color _vertexColor = Color.Aqua;
        Color _edgeColor = Color.Magenta;

        IList<Vertex> _vertices;
        IList<Edge> _graph, _graphSolved;

        Vertex _firstVertex, _secondVertex;

        bool _drawEdge, _solved;

        #endregion

        public frmMinimuSpanningStree()
        {
            InitializeComponent();
            Clear();
            listViewDuLieu.ListViewItemSorter = new IntegerComparer(1);
            listViewDuLieu.Sort();
        }
        public void drawchart()
        {
            chart1.Series.Clear();
            chart1.Visible = true;
            Series sPrime = new Series("Prim");
            Series sRandomized = new Series("Randomized MST");
            for (int i = 0; i < listViewDuLieu.Items.Count; i++)
            {
                int x = Convert.ToInt32(listViewDuLieu.Items[i].SubItems[1].Text);
                int y = Convert.ToInt32(listViewDuLieu.Items[i].SubItems[2].Text);
                if (listViewDuLieu.Items[i].SubItems[0].Text == "Prim")
                    sPrime.Points.AddXY(x, y);
                else
                    sRandomized.Points.AddXY(x, y);

            }
            chart1.Series.Add(sPrime);
            chart1.Series.Add(sRandomized);
            chart1.Series[0].Color = Color.Blue;
            chart1.Series[0].ChartType = SeriesChartType.Line;
            chart1.Series[0].BorderWidth = 2;
            chart1.Series[1].Color = Color.Red;
            chart1.Series[1].ChartType = SeriesChartType.Line;
            chart1.Series[1].BorderWidth = 2;
        }

        #region Events

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Point clicked = new Point(e.X - _halfRadius, e.Y - _halfRadius);
            if (Control.ModifierKeys == Keys.Control)//if Ctrl is pressed
            {
                if (!_drawEdge)
                {
                    _firstVertex = GetSelectedVertex(clicked);
                    _drawEdge = true;
                }
                else
                {
                    _secondVertex = GetSelectedVertex(clicked);
                    _drawEdge = false;
                    if (_firstVertex != null && _secondVertex != null && _firstVertex.Name != _secondVertex.Name)
                    {
                        frmCost formCost = new frmCost();
                        formCost.ShowDialog();

                        Point stringPoint = GetStringPoint(_firstVertex.Position, _secondVertex.Position);
                        _graph.Add(new Edge(_firstVertex, _secondVertex, formCost._cost, stringPoint));
                        panel1.Invalidate();
                    }
                }
            }
            else
            {
                _vertices.Add(new Vertex(_vertices.Count, clicked));
                panel1.Invalidate();
               
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
           
            DrawEdges(g);
            DrawVertices(g);
            g.Dispose();
        }

        private void btnSolve_Click(object sender, EventArgs e)
        {
            if (_vertices.Count > 2)
            {
                if (_graph.Count < _vertices.Count - 1)
                {
                    MessageBox.Show("Lack of edge!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                  //  btnSolve.Enabled = false;
                    btRA.Enabled = false;
                    IKruskal kruskal = new Kruskal();
                    int totalCost;

                    Stopwatch sw = Stopwatch.StartNew();
                    _graphSolved = kruskal.Solve(_graph, out totalCost);
                    sw.Stop();
                    long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                    ThemDuLieuVaoList("",microseconds.ToString());

                    _solved = true;
                    panel1.Invalidate();
                    //MessageBox.Show("Total Cost: " + totalCost.ToString(), "Solution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbTongchiphi.Text = "Total cost:  " + totalCost.ToString();
                }
            }
        }

        private void ThemDuLieuVaoList(string name,string microS)
        {
            ListViewItem i = new ListViewItem(name);
            i.SubItems.Add(_graph.Count.ToString());
            i.SubItems.Add(microS);
            this.listViewDuLieu.Items.Add(i);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Do you want to clear this graph ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (dr == DialogResult.Yes)
            {
                //btnSolve.Enabled = true;
                btRA.Enabled = true;
                btPrim.Enabled = true;
                Graphics g = panel1.CreateGraphics();
                g.Clear(panel1.BackColor);
                Clear();
                lbTongchiphi.Text = "";
                lbGT.Text = "";
            }
        }

        #endregion

        #region Private Methods

        private void DrawEdges(Graphics g)
        {
            Pen p = new Pen(_edgeColor);
            var edges = _graph;
            float[] dashValues = { 500, 1, 1, 1 };
            Pen blackPen = new Pen(Color.Orange, 5);
            blackPen.DashPattern = dashValues;

            foreach (Edge e in edges)
            {
                Point v1 = new Point(e.V1.Position.X + _halfRadius, e.V1.Position.Y + _halfRadius);
                Point v2 = new Point(e.V2.Position.X + _halfRadius, e.V2.Position.Y + _halfRadius);
                g.DrawLine(p, v1, v2);
                DrawString(e.Cost.ToString(), e.StringPosition, g);
            }
            var edges2 =  _graphSolved;
            Pen p2 = new Pen(Color.Red);
            if (edges2 != null && _solved==true)
            {
                foreach (Edge e in edges2)
                {
                    Point v1 = new Point(e.V1.Position.X + _halfRadius, e.V1.Position.Y + _halfRadius);
                    Point v2 = new Point(e.V2.Position.X + _halfRadius, e.V2.Position.Y + _halfRadius);
                    g.DrawLine(blackPen, v1, v2);
                    DrawString2(e.Cost.ToString(), e.StringPosition, g);
                }
            }

        }

        private void DrawString2(string strText, Point pDrawPoint, Graphics g)
        {
            Font drawFont = new Font("Arial", 15);
            SolidBrush drawBrush = new SolidBrush(Color.Blue);
            g.DrawString(strText, drawFont, drawBrush, pDrawPoint);
        }

        private void DrawString(string strText, Point pDrawPoint, Graphics g)
        {
            Font drawFont = new Font("Arial", 15);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            g.DrawString(strText, drawFont, drawBrush, pDrawPoint);
        }

        private void DrawVertices(Graphics g)
        {
            Pen p = new Pen(_vertexColor);
            Brush b = new SolidBrush(_vertexColor);
            foreach (Vertex v in _vertices)
            {
                g.DrawEllipse(p, v.Position.X, v.Position.Y, _radius, _radius);
                g.FillEllipse(b, v.Position.X, v.Position.Y, _radius, _radius);
                DrawString(v.Name.ToString(), v.Position, g);
            }
        }

        private Vertex GetSelectedVertex(Point pClicked)
        {
            foreach (Vertex v in _vertices)
            {
                var distance = GetDistance(v.Position, pClicked);
                if (distance <= _radius)
                {
                    return v;
                }
            }
            return null;
        }

        private double GetDistance(Point start, Point end)
        {
            return Math.Sqrt(Math.Pow(start.X - end.X, 2) + Math.Pow(start.Y - end.Y, 2));
        }

        private Point GetStringPoint(Point start, Point end)
        {
            int X = (start.X + end.X) / 2;
            int Y = (start.Y + end.Y) / 2;
            return new Point(X, Y);
        }

        private void Clear()
        {
            _vertices = new List<Vertex>();
            _graph = new List<Edge>();
            _solved = false;
            _firstVertex = _secondVertex = null;
        }

        #endregion

        private void btRA_Click(object sender, EventArgs e)
        {
            if (_vertices.Count > 2)
            {
                if (_graph.Count < _vertices.Count - 1)
                {
                    MessageBox.Show("Missing Edges", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //btnSolve.Enabled = false;
                    btRA.Enabled = false;
                    btPrim.Enabled = false;
                    Kruskal kruskal = new Kruskal();
                    int totalCost;

                    Stopwatch sw = Stopwatch.StartNew();
                    _graphSolved = kruskal.RandMST(_graph,  out totalCost);
                    sw.Stop();
                    long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                    ThemDuLieuVaoList("Randomized",microseconds.ToString());

                    _solved = true;
                    panel1.Invalidate();
                    //MessageBox.Show("Total Cost: " + totalCost.ToString(), "Solution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbTongchiphi.Text = "Total cost: " + totalCost.ToString();
                    lbGT.Text = "[RA]";
                    drawchart();
                }
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btDel_Click(object sender, EventArgs e)
        {
            this.listViewDuLieu.Items.Clear();       
        }

        private void btdothi_Click(object sender, EventArgs e)
        {
          
            dothi dt = new dothi(listViewDuLieu);
            dt.Show();
        }

        private void btSave_Click(object sender, EventArgs e)
        {

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text file|*.txt";
            save.Title = "Save a file";


            if (save.ShowDialog() == DialogResult.OK)
            {
                
                string vitriluu = save.FileName;
                
                TextWriter sw = new StreamWriter(vitriluu);
                foreach (Edge ed in _graph)
                {
                    sw.WriteLine("*");
                   // luu dinh v1
                    
                    //luu vị trí
                    sw.WriteLine(ed.V1.Position.X);
                    sw.WriteLine(ed.V1.Position.Y);
                    // luu ten dinh
                    sw.WriteLine(ed.V1.Name);
                    
                    // luu dinh v2
                   
                    //luu vị trí
                    sw.WriteLine(ed.V2.Position.X);
                    sw.WriteLine(ed.V2.Position.Y);
                    // luu ten dinh
                    sw.WriteLine(ed.V2.Name);

                    // luu chi phi
                    sw.WriteLine(ed.Cost);
                }

                
                
                sw.Close();


            } 
        }

        private void btopen_Click(object sender, EventArgs e)
        {
            lbTongchiphi.Text = "";
            lbGT.Text = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            // Show the dialog and get result.
            openFileDialog1.Filter = "Text file|*.txt";
            openFileDialog1.Title = "Open a file";
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {

                try
                {
                    _graph.Clear();
                    _vertices.Clear();
                   // _graphSolved.Clear();
                    _solved = false;
                    //btnSolve.Enabled = true;
                    btRA.Enabled = true;
                    btPrim.Enabled = true;
                    // StreamReader Re = new StreamReader(@"C:\Users\HoangNhan\Desktop\do thi 1.txt");
                    string[] lines = File.ReadAllLines(openFileDialog1.FileName);
                    // dem so canh
                    int soCanh = lines.Length / 8;
                    for (int i = 0; i < soCanh; i++)
                    {
                        int k = i * 8;
                        Point p1 = new Point();
                        p1.X = Convert.ToInt32(lines[k + 1]);
                        p1.Y = Convert.ToInt32(lines[k + 2]);
                        Vertex v1 = new Vertex(Convert.ToInt32(lines[k + 3]), p1);
                        Point p2 = new Point();
                        p2.X = Convert.ToInt32(lines[k + 4]);
                        p2.Y = Convert.ToInt32(lines[k + 5]);
                        Vertex v2 = new Vertex(Convert.ToInt32(lines[k + 6]), p2);
                        Point stringPoint = GetStringPoint(v1.Position, v2.Position);
                       
                        bool kt = false;
                        foreach (Vertex vt in _vertices)
                        {
                            if (vt.Name == v1.Name)
                            {
                                kt = true;
                                break;
                            }
                        }
                        if (kt == false)
                            _vertices.Add(v1);
                        bool kt2 = false;
                        foreach (Vertex vt in _vertices)
                        {
                            if (vt.Name == v2.Name)
                            {
                                kt2 = true;
                                break;
                            }
                        }
                        if (kt2 == false)
                            _vertices.Add(v2);
                        foreach (Vertex vt in _vertices) // vì cac dỉnh dù trùng tên nhưng k trùng vị trí
                        {
                            if (vt.Name == v1.Name)
                                v1 = vt;
                            if (vt.Name == v2.Name)
                                v2 = vt;

                        }
                        _graph.Add(new Edge(v1, v2, Convert.ToInt32(lines[k + 7]), stringPoint));

                    }
                    panel1.Invalidate();
                }
                catch
                {
                    MessageBox.Show("Cannot read this file!");
                }
            }
            

           
        }

        private void btPrim_Click(object sender, EventArgs e)
        {

            if (_vertices.Count > 2)
            {
                if (_graph.Count < _vertices.Count - 1)
                {
                    MessageBox.Show("Missing Edges", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    //btnSolve.Enabled = false;
                    btRA.Enabled = false;
                    btPrim.Enabled = false;
                    IKruskal kruskal = new Kruskal();
                    int totalCost;

                    Stopwatch sw = Stopwatch.StartNew();
                    _graphSolved = kruskal.Prim(_graph, out totalCost);
                    sw.Stop();
                    long microseconds = sw.ElapsedTicks / (Stopwatch.Frequency / (1000L * 1000L));
                    ThemDuLieuVaoList("Prim",microseconds.ToString());

                    _solved = true;
                    panel1.Invalidate();
                    //MessageBox.Show("Total Cost: " + totalCost.ToString(), "Solution", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbTongchiphi.Text = "Total cost: " + totalCost.ToString();
                    lbGT.Text = "[Prim]";
                    drawchart();
                }
            }
        }

        private void btdothi_Click_1(object sender, EventArgs e)
        {
            
        }

        private void btDel_Click_1(object sender, EventArgs e)
        {
            listViewDuLieu.Items.Clear();
            chart1.Series.Clear();
        }

        private void frmMinimuSpanningStree_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Do you want to exit?", "Exit", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
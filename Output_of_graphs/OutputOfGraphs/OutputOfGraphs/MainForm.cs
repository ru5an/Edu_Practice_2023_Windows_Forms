using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutputOfGraphs
{
    public partial class MainForm : Form
    {
        private List<List<PointF>> graphsData;
        private List<MyGraph> graphs;

        PointF centre;
        PointF gapPoint;
        MyPoint gapMyPoint;

        private float scale;
        private int selectGraph;
        private bool isMoving;

        class MyPoint
        {
            public float X { get; set; }
            public float Y { get; set; }

            public MyPoint()
            {
                X = 0.0f;
                Y = 0.0f;
            }

            public MyPoint(float x, float y)
            {
                X = x;
                Y = y;
            }

            public PointF PointConstruction()
            {
                return new PointF(X, Y);
            }

            public bool IsGap()
            {
                return X == float.MaxValue && Y == float.MaxValue;
            }
        }

        class MyGraph
        {
            public List<MyPoint> Points { get; set; }
            public float ScaleX { get; set; }
            public float ScaleY { get; set; }

            public MyGraph()
            {
                Points = new List<MyPoint>();
                ScaleX = 10.0f;
                ScaleY = 10.0f;
            }

            public MyGraph(List<MyPoint> points, float scaleX, float scaleY)
            {
                Points = points;
                ScaleX = scaleX;
                ScaleY = scaleY;
            }
        }

        public MainForm()
        {
            InitializeComponent();

            graphsData = new List<List<PointF>>();
            centre = new PointF((pictureBox.Width) /2, pictureBox.Height/2);
            gapPoint = new PointF(float.MaxValue, float.MaxValue);
            gapMyPoint = new MyPoint(float.MaxValue, float.MaxValue);
            scale = 10.0f;
            selectGraph = -1;
            isMoving = false;

            pictureBox.Paint += graphs_Paint;
            pictureBox.MouseWheel += pictureBox_MouseWheel;
            pictureBox.MouseDown += pictureBox_MouseDown;
            pictureBox.PreviewKeyDown += pictureBox_PreviewKeyDown;
        }

        private void addGraph_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = ".\\";
                ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    GetData(filePath);
                    redrawGraphs();
                }
            }
        }

        private void GetData(string filePath)
        {
            string[] filePoint = File.ReadAllLines(filePath);
            List<PointF> points = new List<PointF>();
            MyGraph myGraph = new MyGraph();

            foreach (string line in filePoint)
            {
                if (line.Equals("gap"))
                {
                    points.Add(gapPoint);
                    myGraph.Points.Add(gapMyPoint);
                }
                else
                {
                    string[] split = line.Split(' ');
                    if (split.Length == 2
                        && Single.TryParse(split[0], out float x)
                        && Single.TryParse(split[1], out float y)
                        )
                    {
                        points.Add(new PointF(x, y));
                    }
                }
            }
            graphsData.Add(points);
        }

        private void graphs_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            graphics.Clear(Color.White);
            drawAxis(graphics);
            for (int i = 0; i < graphsData.Count; i++) 
            {
                if (i == selectGraph)
                {
                    drawGraph(graphics, graphsData[i], new Pen(Color.Red, 2f));
                } 
                else
                {
                    drawGraph(graphics, graphsData[i], new Pen(Color.Blue, 2f));
                }
            }
        }

        private void drawAxis(Graphics graphics)
        {
            Pen net = new Pen(Color.LightGray, 1.1f);
            Pen axisPen = new Pen(Color.Black, 2f);

            const uint greedSize = 10;

            for (uint i = 0; i < pictureBox.Height; i += greedSize)
            {
                if (i != centre.Y)
                    graphics.DrawLine(net, 0, i, pictureBox.Width, i);
            }
            for (uint i = 0; i < pictureBox.Width; i += greedSize)
            {
                graphics.DrawLine(net, i, 0, i, pictureBox.Height);
            }

            
            graphics.DrawLine(axisPen, 0, centre.Y, pictureBox.Width, centre.Y);
            graphics.DrawLine(axisPen, centre.X, 0, centre.X, pictureBox.Height);


            for (uint i = 0; i < pictureBox.Width; i += greedSize)
            {
                graphics.DrawLine(axisPen, i, centre.Y - 3, i, centre.Y + 3);
            }

            for (uint i = 0; i < pictureBox.Height; i += greedSize)
            {
                graphics.DrawLine(axisPen, centre.X - 3, i, centre.X + 3, i);
            }
        }

        private void drawGraph(Graphics graphics, List<PointF> points, Pen pen)
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                if (points[i] != gapPoint && points[i+1] != gapPoint)
                    graphics.DrawLine(pen, new PointF( points[i].X * scale + centre.X, -points[i].Y * scale + centre.Y), 
                        new PointF( points[i + 1].X * scale + centre.X, -points[i + 1].Y * scale + centre.Y));
            }
        }

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = (int)e.Delta;

            if (delta > 0)
            {
                if (scale <= 1e6) this.scale *= 1.25f;
            } 
            else
            {
                this.scale *= 0.8f;
            }

            redrawGraphs();
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                selectGraph = getSelectGraph(e.Location);

                if (selectGraph != -1)
                {
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                }

                redrawGraphs();
            }
        }

        private int getSelectGraph(Point mouseLocation)
        {
            for(int i = 0; i < graphsData.Count; i++)
            {
                foreach(PointF point in graphsData[i])
                {
                    PointF picturePoint = new PointF(point.X * scale + centre.X, -point.Y * scale + centre.Y);
                    float distance = (float)Math.Sqrt(Math.Pow(picturePoint.X - mouseLocation.X, 2) + Math.Pow(picturePoint.Y - mouseLocation.Y, 2));
                    if (distance <= 5)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        private void redrawGraphs()
        {
            pictureBox.Invalidate();
        }

        private void pictureBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (isMoving)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        graphsData[selectGraph] = moveGraph(graphsData[selectGraph], 0, 1);
                        break;
                    case Keys.Down:
                        graphsData[selectGraph] = moveGraph(graphsData[selectGraph], 0, -1);
                        break;
                    case Keys.Left:
                        graphsData[selectGraph] = moveGraph(graphsData[selectGraph], -1, 0);
                        break;
                    case Keys.Right:
                        graphsData[selectGraph] = moveGraph(graphsData[selectGraph], 1, 0);
                        break;
                    case Keys.W:
                        graphsData[selectGraph] = stretchingGraph(graphsData[selectGraph], 1f, 1.25f);
                        break;
                    case Keys.S:
                        graphsData[selectGraph] = stretchingGraph(graphsData[selectGraph], 1f, 0.8f);
                        break;
                    case Keys.D:
                        graphsData[selectGraph] = stretchingGraph(graphsData[selectGraph], 1.25f, 1f);
                        break;
                    case Keys.A:
                        graphsData[selectGraph] = stretchingGraph(graphsData[selectGraph], 0.8f, 1f);
                        break;
                }

                e.IsInputKey = true;

                redrawGraphs();
            }
        }

        private List<PointF> moveGraph(List<PointF> points, int dx, int dy)
        {
            List<PointF> movedPoints = new List<PointF>();

            foreach (PointF point in points)
            {
                movedPoints.Add(new PointF(point.X + dx, point.Y + dy));
            }

            return movedPoints;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            pictureBox.Focus();
        }

        private List<PointF> stretchingGraph(List<PointF> points, float dx, float dy)
        {
            List<PointF> stretcgedPoints = new List<PointF>(points);

            for (int i = 1; i < points.Count; i++)
            {
                if (points[i] != gapPoint)
                {
                    if ((points[i - 1].X < points[i].X * dx) && dx != 1f)
                    {
                        stretcgedPoints[i] = new PointF(stretcgedPoints[i].X * dx, stretcgedPoints[i].Y);
                    }
                    if ((points[i - 1].Y < points[i].Y * dy) && dy != 1f)
                    {
                        stretcgedPoints[i] = new PointF(stretcgedPoints[i].X, stretcgedPoints[i].Y * dy);
                    }
                }
            }

            return stretcgedPoints;
        }
    }
}

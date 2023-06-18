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

            public bool IsGap()
            {
                return X == float.MaxValue && Y == float.MaxValue;
            }
            public PointF PointConstruction(MyGraph graph)
            {
                return new PointF(X * graph.ScaleX + graph.DisplacementX, -Y * graph.ScaleY + graph.DisplacementY);
            }
        }

        class MyGraph
        {
            public List<MyPoint> Points { get; set; }
            public float ScaleX { get; set; } = 10.0f;
            public float ScaleY { get; set; } = 10.0f;
            public float DisplacementX { get; set; } = 0.0f;
            public float DisplacementY { get; set; } = 0.0f;

            public MyGraph()
            {
                Points = new List<MyPoint>();
            }

            public MyGraph(List<MyPoint> points)
            {
                Points = points;
            }

            public void AddMyPoint(MyPoint myPoint)
            {
                Points.Add(myPoint);
            }

            public MyPoint this[int index]
            {
                get { return Points[index]; }
                set { Points[index] = value; }
            }

            public IEnumerator<MyPoint> GetEnumerator()
            {
                foreach (MyPoint myPoint in Points)
                {
                    yield return myPoint;
                }
            }

            public float AbsMaxX()
            {
                float absMaxX = float.MinValue;
                foreach (MyPoint myPoint in Points)
                {
                    if (!myPoint.IsGap())
                    {
                        if (Math.Abs(myPoint.X) > absMaxX)
                        {
                            absMaxX = Math.Abs(myPoint.X);
                        }
                    }
                }
                return absMaxX;
            }

            public float AbsMaxY()
            {
                float absMaxY = float.MinValue;
                foreach (MyPoint myPoint in Points)
                {
                    if (!myPoint.IsGap())
                    {
                        if (Math.Abs(myPoint.Y) > absMaxY)
                        {
                            absMaxY = Math.Abs(myPoint.Y);
                        }
                    }
                }
                return absMaxY;
            }

            public void PerfectSize(int pictureBoxWidth, int pictureBoxHeight)
            {
                ScaleX = (float)(pictureBoxWidth / 2 - 10) / AbsMaxX();
                ScaleY = (float)(pictureBoxHeight / 2 - 10) / AbsMaxY() ;
            }
        }

        

        public MainForm()
        {
            InitializeComponent();

            graphsData = new List<List<PointF>>();
            graphs = new List<MyGraph>();
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
                        myGraph.Points.Add(new MyPoint(x,y));
                    }
                }
            }
            myGraph.DisplacementX = centre.X;
            myGraph.DisplacementY = centre.Y;
            myGraph.PerfectSize(pictureBox.Width, pictureBox.Height);
            graphs.Add(myGraph);
        }

        private void graphs_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;

            graphics.Clear(Color.White);
            drawAxis(graphics);
            for (int i = 0; i < graphs.Count; i++)
            {
                if (i == selectGraph)
                {
                    drawMyGraph(graphics, graphs[i], new Pen(Color.Red, 2f));
                }
                else
                {
                    drawMyGraph(graphics, graphs[i], new Pen(Color.Blue, 2f));
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


            for (uint i = 0; i < pictureBox.Width - greedSize; i += greedSize)
            {
                graphics.DrawLine(axisPen, i, centre.Y - 3, i, centre.Y + 3);
            }

            for (uint i = greedSize; i < pictureBox.Height; i += greedSize)
            {
                graphics.DrawLine(axisPen, centre.X - 3, i, centre.X + 3, i);
            }

            Font font = new Font(FontFamily.GenericSansSerif, 10);
            graphics.DrawString("X", font, Brushes.Black, pictureBox.Width - 25, centre.Y - 25);
            graphics.DrawString("Y", font, Brushes.Black, centre.X + 10, 0);

            PointF[] pointerX = new PointF[] { 
                new PointF(pictureBox.Width - 3, centre.Y),
                new PointF(pictureBox.Width - 10, centre.Y - 5),
                new PointF(pictureBox.Width - 10, centre.Y + 5)
            };

            PointF[] pointerY = new PointF[] {
                new PointF(centre.X, 0.0f),
                new PointF(centre.X - 5, 10),
                new PointF(centre.X + 5, 10)
            };

            graphics.FillPolygon(new SolidBrush(Color.Black), pointerX);
            graphics.FillPolygon(new SolidBrush(Color.Black), pointerY);

            graphics.DrawPolygon(axisPen, pointerX);
            graphics.DrawPolygon(axisPen, pointerY);
        }

        private void drawMyGraph(Graphics graphics, MyGraph myGraph, Pen pen)
        {
            for (int i = 0; i < myGraph.Points.Count - 1; i++)
            {
                if (!myGraph[i].IsGap() && !myGraph[i+1].IsGap())
                {
                    graphics.DrawLine(pen, myGraph[i].PointConstruction(myGraph),
                        myGraph[i+1].PointConstruction(myGraph));
                }
            }
        }

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            int delta = (int)e.Delta;
            if (isMoving)
            {
                if (delta > 0)
                {
                    graphs[selectGraph].ScaleX *= 1.25f;
                    graphs[selectGraph].ScaleY *= 1.25f;
                }
                else
                {
                    graphs[selectGraph].ScaleX *= 0.8f;
                    graphs[selectGraph].ScaleY *= 0.8f;
                }

                redrawGraphs();
            }
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
            for (int i = 0; i < graphs.Count; i++)
            {
                foreach (MyPoint point in graphs[i])
                {
                    PointF picturePoint = point.PointConstruction(graphs[i]);
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
                        moveGraph(graphs[selectGraph], 0, -10);
                        break;
                    case Keys.Down:
                        moveGraph(graphs[selectGraph], 0, 10);
                        break;
                    case Keys.Left:
                        moveGraph(graphs[selectGraph], -10, 0);
                        break;
                    case Keys.Right:
                        moveGraph(graphs[selectGraph], 10, 0);
                        break;
                    case Keys.W:
                        stretchingGraph(graphs[selectGraph], 1f, 1.25f);
                        break;
                    case Keys.S:
                        stretchingGraph(graphs[selectGraph], 1f, 0.8f);
                        break;
                    case Keys.D:
                        stretchingGraph(graphs[selectGraph], 1.25f, 1f);
                        break;
                    case Keys.A:
                        stretchingGraph(graphs[selectGraph], 0.8f, 1f);
                        break;
                }

                e.IsInputKey = true;

                redrawGraphs();
            }
        }

        private void moveGraph(MyGraph myGraph, int dx, int dy)
        {
            myGraph.DisplacementX += dx;
            myGraph.DisplacementY += dy;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            pictureBox.Focus();
        }

        private void stretchingGraph(MyGraph myGraph, float dx, float dy)
        {
            myGraph.ScaleX *= dx;
            myGraph.ScaleY *= dy;
        }
    }
}

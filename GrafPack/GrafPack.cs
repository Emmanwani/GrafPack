using System;
using System.Drawing;
using System.Windows.Forms;

namespace GrafPack
{
    public partial class GrafPack : Form
    {
        private MainMenu mainMenu;
        private bool createShapeStatus = false;
        private bool rubberBandActive = false;
        private Point rubberBandStart;
        private Point rubberBandEnd;
        private Shape[] shapes = new Shape[100]; // Array to store shapes
        private int shapeCount = 0; // Counter to keep track of shapes
        private Shape selectedShape;

        public GrafPack()
        {
            InitializeComponent();
            // Initialize main menu and event handlers
            InitializeMenu();
            this.MouseClick += MouseClickHandler;
            this.MouseMove += MouseMoveHandler;
            this.MouseDown += MouseDownHandler;
            this.MouseUp += MouseUpHandler;
        }

        private void InitializeMenu()
        {
            // Create main menu items
            mainMenu = new MainMenu();
            MenuItem createItem = new MenuItem("&Create");
            MenuItem selectItem = new MenuItem("&Select");
            MenuItem moveItem = new MenuItem("&Move");
            MenuItem rotateItem = new MenuItem("&Rotate");
            MenuItem deleteItem = new MenuItem("&Delete");
            MenuItem exitItem = new MenuItem("&Exit");

            // Create sub-menu items for creation
            MenuItem squareItem = new MenuItem("&Square");
            MenuItem triangleItem = new MenuItem("&Triangle");
            MenuItem circleItem = new MenuItem("&Circle");

            // Add items to main menu
            mainMenu.MenuItems.Add(createItem);
            mainMenu.MenuItems.Add(selectItem);
            mainMenu.MenuItems.Add(moveItem);
            mainMenu.MenuItems.Add(rotateItem);
            mainMenu.MenuItems.Add(deleteItem);
            mainMenu.MenuItems.Add(exitItem);
            createItem.MenuItems.Add(squareItem);
            createItem.MenuItems.Add(triangleItem);
            createItem.MenuItems.Add(circleItem);

            // Attach event handlers
            selectItem.Click += SelectShape;
            moveItem.Click += MoveShape;
            rotateItem.Click += RotateShape;
            deleteItem.Click += DeleteShape;
            exitItem.Click += ExitApplication;
            squareItem.Click += SelectSquare;
            triangleItem.Click += SelectTriangle;
            circleItem.Click += SelectCircle;

            // Set main menu
            this.Menu = mainMenu;
        }

        private void SelectSquare(object sender, EventArgs e)
        {
            createShapeStatus = true;
            selectedShape = null;
        }

        private void SelectTriangle(object sender, EventArgs e)
        {
            createShapeStatus = true;
            selectedShape = null;
            MessageBox.Show("Click and drag to create a triangle using rubber band method");
        }

        private void SelectCircle(object sender, EventArgs e)
        {
            createShapeStatus = true;
            selectedShape = null;
            MessageBox.Show("Click and drag to create a circle using rubber band method");
        }

        private void MoveShape(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                // Implement move logic here
                MessageBox.Show("Move selected shape...");
            }
            else
            {
                MessageBox.Show("No shape selected. Please select a shape first.");
            }
        }

        private void RotateShape(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                // Implement rotate logic here
                MessageBox.Show("Rotate selected shape...");
            }
            else
            {
                MessageBox.Show("No shape selected. Please select a shape first.");
            }
        }

        private void DeleteShape(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                // Implement delete logic here
                MessageBox.Show("Delete selected shape...");
            }
            else
            {
                MessageBox.Show("No shape selected. Please select a shape first.");
            }
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SelectShape(object sender, EventArgs e)
        {
            MessageBox.Show("Select a shape...");
        }

        private void MouseDownHandler(object sender, MouseEventArgs e)
        {
            if (createShapeStatus)
            {
                rubberBandActive = true;
                rubberBandStart = e.Location;
                rubberBandEnd = e.Location;
            }
        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (rubberBandActive)
            {
                rubberBandEnd = e.Location;
                Refresh();
            }
        }

        private void MouseUpHandler(object sender, MouseEventArgs e)
        {
            if (rubberBandActive)
            {
                rubberBandActive = false;
                if (createShapeStatus)
                {
                    // Create the shape based on the rubber band rectangle
                    if (e.Button == MouseButtons.Left)
                    {
                        if (rubberBandStart != rubberBandEnd)
                        {
                            Shape newShape = null;
                            if (createShapeStatus)
                            {
                                newShape = new Square(rubberBandStart, rubberBandEnd);
                                newShape = new Circle(rubberBandStart, rubberBandEnd);

                            }
                            else if (createShapeStatus)
                            {
                                // Implement logic to create a triangle based on rubber band rectangle
                                // You may use the rubberBandStart and rubberBandEnd points to determine the shape
                                MessageBox.Show("Creating Triangle based on rubber band rectangle...");
                            }
                            else if (createShapeStatus)
                            {
                                // Implement logic to create a circle based on rubber band rectangle
                                // You may use the rubberBandStart and rubberBandEnd points to determine the shape
                                MessageBox.Show("Creating Circle based on rubber band rectangle...");
                                
                            }

                            if (newShape != null)
                            {
                                shapes[shapeCount++] = newShape; // Store the shape
                                Refresh(); // Redraw the form
                                selectedShape = newShape; // Select the newly created shape
                                createShapeStatus = false;
                            }
                        }
                    }
                }
            }
        }

        private void MouseClickHandler(object sender, MouseEventArgs e)
        {
            // Check if the mouse click is inside any shape
            foreach (var shape in shapes)
            {
                if (shape != null && IsPointInsideShape(e.Location, shape))
                {
                    selectedShape = shape;
                    break;
                }
            }
        }

        private bool IsPointInsideShape(Point point, Shape shape)
        {
            // Implement logic to check if the point is inside the shape
            // For simplicity, assuming all shapes are rectangles and checking if the point is inside the bounding box.
            return (point.X >= shape.MinX && point.X <= shape.MaxX && point.Y >= shape.MinY && point.Y <= shape.MaxY);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw all shapes
            Graphics g = e.Graphics;
            Pen blackpen = new Pen(Color.Black);
            for (int i = 0; i < shapeCount; i++)
            {
                shapes[i].Draw(g, blackpen);
            }
        }
    }
    abstract class Shape
    {
        // This is the base class for Shapes in the application.
        public Shape() { }

        public abstract void Draw(Graphics g, Pen pen);

        // Properties to get bounding box of the shape
        public abstract int MinX { get; }
        public abstract int MaxX { get; }
        public abstract int MinY { get; }
        public abstract int MaxY { get; }
    }

    class Square : Shape
    {
        Point keyPt, oppPt;

        public Square(Point keyPt, Point oppPt)
        {
            this.keyPt = keyPt;
            this.oppPt = oppPt;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            double xDiff, yDiff, xMid, yMid;
            xDiff = oppPt.X - keyPt.X;
            yDiff = oppPt.Y - keyPt.Y;
            xMid = (oppPt.X + keyPt.X) / 2;
            yMid = (oppPt.Y + keyPt.Y) / 2;
            g.DrawLine(pen, (int)keyPt.X, (int)keyPt.Y, (int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2));
            g.DrawLine(pen, (int)(xMid + yDiff / 2), (int)(yMid - xDiff / 2), (int)oppPt.X, (int)oppPt.Y);
            g.DrawLine(pen, (int)oppPt.X, (int)oppPt.Y, (int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2));
            g.DrawLine(pen, (int)(xMid - yDiff / 2), (int)(yMid + xDiff / 2), (int)keyPt.X, (int)keyPt.Y);
        }

        public override int MinX => Math.Min(keyPt.X, oppPt.X);
        public override int MaxX => Math.Max(keyPt.X, oppPt.X);
        public override int MinY => Math.Min(keyPt.Y, oppPt.Y);
        public override int MaxY => Math.Max(keyPt.Y, oppPt.Y);
    }

    class Triangle : Shape
    {
        Point point1, point2, point3;

        public Triangle(Point point1, Point point2, Point point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            g.DrawLine(pen, point1, point2);
            g.DrawLine(pen, point2, point3);
            g.DrawLine(pen, point3, point1);
        }

        public override int MinX => Math.Min(point1.X, Math.Min(point2.X, point3.X));
        public override int MaxX => Math.Max(point1.X, Math.Max(point2.X, point3.X));
        public override int MinY => Math.Min(point1.Y, Math.Min(point2.Y, point3.Y));
        public override int MaxY => Math.Max(point1.Y, Math.Max(point2.Y, point3.Y));
    }

    class Circle : Shape
    {
        Point center, circumference;

        public Circle(Point center, Point circumference)
        {
            this.center = center;
            this.circumference = circumference;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            int radius = (int)Math.Sqrt(Math.Pow(circumference.X - center.X, 2) + Math.Pow(circumference.Y - center.Y, 2));
            g.DrawEllipse(pen, center.X - radius, center.Y - radius, 2 * radius, 2 * radius);
        }

        public override int MinX => center.X;
        public override int MaxX => center.X;
        public override int MinY => center.Y;
        public override int MaxY => center.Y;
    }
}


﻿using System;
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
        private bool createSquare = false;
        private bool createTriangle = false;
        private bool createCircle = false;
        private TextBox currentTextBox; // Track the current text box being added


        private bool isMovingShape = false;
        private Point lastMousePosition;


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
            MenuItem moveItem = new MenuItem("&Move");
            MenuItem rotateItem = new MenuItem("&Rotate");
            MenuItem colorMenu = new MenuItem("&Colour");
            MenuItem deleteItem = new MenuItem("&Delete");
            MenuItem exitItem = new MenuItem("&Exit");

            // Create sub-menu items for creation
            MenuItem squareItem = new MenuItem("&Square");
            MenuItem triangleItem = new MenuItem("&Triangle");
            MenuItem circleItem = new MenuItem("&Circle");

            // Create sub-menu items for rotation angles
            MenuItem rotate45Item = new MenuItem("&45 Degrees");
            MenuItem rotate90Item = new MenuItem("&90 Degrees");
            MenuItem rotate180Item = new MenuItem("&180 Degrees");

            MenuItem outlineItem = new MenuItem("&Outline colour");
            MenuItem fillColorMenu = new MenuItem("&Fill Colour");

            // Add items to main menu
            mainMenu.MenuItems.Add(createItem);
            mainMenu.MenuItems.Add(moveItem);
            mainMenu.MenuItems.Add(colorMenu);
            mainMenu.MenuItems.Add(rotateItem);
            mainMenu.MenuItems.Add(deleteItem);
            mainMenu.MenuItems.Add(exitItem);


            colorMenu.MenuItems.Add(outlineItem);
            colorMenu.MenuItems.Add(fillColorMenu);
            createItem.MenuItems.Add(squareItem);
            createItem.MenuItems.Add(triangleItem);
            createItem.MenuItems.Add(circleItem);
            // Add sub-menu items for rotation angles
            rotateItem.MenuItems.Add(rotate45Item);
            rotateItem.MenuItems.Add(rotate90Item);
            rotateItem.MenuItems.Add(rotate180Item);

            MenuItem colorRed = new MenuItem("&Red");
            MenuItem colorBlue = new MenuItem("&Blue");
            MenuItem colorGreen = new MenuItem("&Green");
            MenuItem colorYellow = new MenuItem("&Yellow");
            MenuItem colorBlack = new MenuItem("&Black");

            // Create a new menu for changing the fill color
            MenuItem fillRed = new MenuItem("&Red");
            MenuItem fillBlue = new MenuItem("&Blue");
            MenuItem fillGreen = new MenuItem("&Green");
            MenuItem fillYellow = new MenuItem("&Yellow");
            MenuItem fillBlack = new MenuItem("&Black");

            outlineItem.MenuItems.Add(colorRed);
            outlineItem.MenuItems.Add(colorBlue);
            outlineItem.MenuItems.Add(colorGreen);
            outlineItem.MenuItems.Add(colorYellow);
            outlineItem.MenuItems.Add(colorBlack);

            fillColorMenu.MenuItems.Add(fillRed);
            fillColorMenu.MenuItems.Add(fillBlue);
            fillColorMenu.MenuItems.Add(fillGreen);
            fillColorMenu.MenuItems.Add(fillYellow);
            fillColorMenu.MenuItems.Add(fillBlack);

            colorRed.Click += (sender, e) => { ChangeShapeColor(Color.Red); };
            colorBlue.Click += (sender, e) => { ChangeShapeColor(Color.Blue); };
            colorGreen.Click += (sender, e) => { ChangeShapeColor(Color.Green); };
            colorYellow.Click += (sender, e) => { ChangeShapeColor(Color.Yellow); };
            colorBlack.Click += (sender, e) => { ChangeShapeColor(Color.Black); };

            // Event handlers for fill color selection
            fillRed.Click += (sender, e) => { ChangeFillColor(Color.Red); };
            fillBlue.Click += (sender, e) => { ChangeFillColor(Color.Blue); };
            fillGreen.Click += (sender, e) => { ChangeFillColor(Color.Green); };
            fillYellow.Click += (sender, e) => { ChangeFillColor(Color.Yellow); };
            fillBlack.Click += (sender, e) => { ChangeFillColor(Color.Black); };



            // Attach event handlers
            moveItem.Click += MoveShape;
            deleteItem.Click += DeleteShape;
            exitItem.Click += ExitApplication;
            squareItem.Click += SelectSquare;
            triangleItem.Click += SelectTriangle;
            circleItem.Click += SelectCircle;
            rotate45Item.Click += RotateShapeBy45;
            rotate90Item.Click += RotateShapeBy90;
            rotate180Item.Click += RotateShapeBy180;


            // Set main menu
            this.Menu = mainMenu;
        }
        private void ChangeShapeColor(Color color)
        {
            if (selectedShape != null)
            {
                // Set the color of the selected shape
                selectedShape.Color = color;
                Refresh(); // Redraw the form
            }
            else
            {
                MessageBox.Show("No shape selected. Please select a shape first.");
            }
        }
        private void ChangeFillColor(Color color)
        {
            if (selectedShape != null)
            {
                // Set the fill color of the selected shape
                selectedShape.FillColor = color;
                Refresh(); // Redraw the form
            }
            else
            {
                MessageBox.Show("No shape selected. Please select a shape first.");
            }
        }
        private void SelectSquare(object sender, EventArgs e)
        {
            createShapeStatus = true;
            createSquare = true;
            createTriangle = false;
            createCircle = false;
            selectedShape = null;
        }

        private void SelectTriangle(object sender, EventArgs e)
        {
            createShapeStatus = true;
            createSquare = false;
            createTriangle = true;
            createCircle = false;
            selectedShape = null;
        }

        private void SelectCircle(object sender, EventArgs e)
        {
            createShapeStatus = true;
            createSquare = false;
            createTriangle = false;
            createCircle = true;
            selectedShape = null;
        }


        private void MoveShape(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                // Implement move logic here
                MessageBox.Show("Click and drag to move the selected shape...");
            }
            else
            {
                MessageBox.Show("No shape selected. Please select a shape first.");
            }
        }
        // Event handlers for rotating the shape by specific angles
        private void RotateShapeBy45(object sender, EventArgs e)
        {
            RotateSelectedShape(45);
        }

        private void RotateShapeBy90(object sender, EventArgs e)
        {
            RotateSelectedShape(90);
        }

        private void RotateShapeBy180(object sender, EventArgs e)
        {
            RotateSelectedShape(180);
        }

        private void RotateSelectedShape(int angle)
        {
            if (selectedShape != null)
            {
                // Implement rotate logic here
                // For demonstration purposes, let's rotate the shape by the specified angle

                // Get the center point of the shape
                int centerX = (selectedShape.MinX + selectedShape.MaxX) / 2;
                int centerY = (selectedShape.MinY + selectedShape.MaxY) / 2;

                // Rotate each point of the shape around the center point
                if (selectedShape is Square square)
                {
                    RotatePoint(ref square.keyPt, centerX, centerY, angle);
                    RotatePoint(ref square.oppPt, centerX, centerY, angle);
                }
                else if (selectedShape is Triangle triangle)
                {
                    RotatePoint(ref triangle.point1, centerX, centerY, angle);
                    RotatePoint(ref triangle.point2, centerX, centerY, angle);
                    RotatePoint(ref triangle.point3, centerX, centerY, angle);
                }
                else if (selectedShape is Circle circle)
                {
                    // Circles don't change when rotated, so no need to do anything
                }

                // Redraw the form
                Refresh();
            }
            else
            {
                MessageBox.Show("No shape selected. Please select a shape first.");
            }
        }
        // Helper method to rotate a point around another point by a specified angle
        private void RotatePoint(ref Point point, int centerX, int centerY, double angleDegrees)
        {
            double angleRadians = angleDegrees * Math.PI / 180.0;
            double cosTheta = Math.Cos(angleRadians);
            double sinTheta = Math.Sin(angleRadians);

            // Translate the point so that the center of rotation is at the origin
            int translatedX = point.X - centerX;
            int translatedY = point.Y - centerY;

            // Rotate the translated point
            int rotatedX = (int)(translatedX * cosTheta - translatedY * sinTheta);
            int rotatedY = (int)(translatedX * sinTheta + translatedY * cosTheta);

            // Translate the rotated point back to its original position
            point.X = rotatedX + centerX;
            point.Y = rotatedY + centerY;
        }

        private void DeleteShape(object sender, EventArgs e)
        {
            if (selectedShape != null)
            {
                // Find the index of the selected shape in the shapes array
                int index = Array.IndexOf(shapes, selectedShape);

                // Shift all elements after the selected shape one position to the left
                for (int i = index; i < shapeCount - 1; i++)
                {
                    shapes[i] = shapes[i + 1];
                }

                // Decrease the shape count
                shapeCount--;

                // Clear the last element in the array
                shapes[shapeCount] = null;

                // Deselect the shape
                selectedShape = null;

                // Redraw the form
                Refresh();
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
            if (selectedShape != null)
            {
                isMovingShape = true;
                lastMousePosition = e.Location;
            }
        }

        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            if (rubberBandActive)
            {
                rubberBandEnd = e.Location;
                Refresh();
            }
            if (isMovingShape)
            {
                // Calculate the distance the mouse has moved
                int dx = e.X - lastMousePosition.X;
                int dy = e.Y - lastMousePosition.Y;

                // Update the position of the selected shape
                if (selectedShape != null)
                {
                    if (selectedShape is Square square)
                    {
                        square.keyPt = new Point(square.keyPt.X + dx, square.keyPt.Y + dy);
                        square.oppPt = new Point(square.oppPt.X + dx, square.oppPt.Y + dy);
                    }
                    else if (selectedShape is Triangle triangle)
                    {
                        triangle.point1 = new Point(triangle.point1.X + dx, triangle.point1.Y + dy);
                        triangle.point2 = new Point(triangle.point2.X + dx, triangle.point2.Y + dy);
                        triangle.point3 = new Point(triangle.point3.X + dx, triangle.point3.Y + dy);
                    }
                    else if (selectedShape is Circle circle)
                    {
                        circle.center = new Point(circle.center.X + dx, circle.center.Y + dy);
                        circle.circumference = new Point(circle.circumference.X + dx, circle.circumference.Y + dy);
                    }

                    // Update the last mouse position
                    lastMousePosition = e.Location;

                    // Redraw the form
                    Refresh();
                }
            }
        }


        public void MouseUpHandler(object sender, MouseEventArgs e)
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
                            if (createSquare)
                            {
                                newShape = new Square(rubberBandStart, rubberBandEnd);
                            }

                            else if (createTriangle)
                            {
                                // Create a triangle
                                Point midPoint = new Point((rubberBandStart.X + rubberBandEnd.X) / 2, (rubberBandStart.Y + rubberBandEnd.Y) / 2);
                                double angle = Math.Atan2(rubberBandEnd.Y - rubberBandStart.Y, rubberBandEnd.X - rubberBandStart.X);
                                double distance = Math.Sqrt(Math.Pow(rubberBandEnd.X - rubberBandStart.X, 2) + Math.Pow(rubberBandEnd.Y - rubberBandStart.Y, 2));
                                Point thirdPoint = new Point((int)(midPoint.X + distance * Math.Cos(angle - Math.PI / 3)), (int)(midPoint.Y + distance * Math.Sin(angle - Math.PI / 3)));
                                newShape = new Triangle(rubberBandStart, rubberBandEnd, thirdPoint);
                            }
                            else if (createCircle)
                            {
                                // Create a circle
                                Point center = new Point((rubberBandStart.X + rubberBandEnd.X) / 2, (rubberBandStart.Y + rubberBandEnd.Y) / 2);
                                newShape = new Circle(center, rubberBandStart);
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
            if (isMovingShape)
            {
                isMovingShape = false;
            }

        }

        public void MouseClickHandler(object sender, MouseEventArgs e)
        {
            // Check if the mouse click is inside any shape
            bool shapeSelected = false;
            foreach (var shape in shapes)
            {
                if (shape != null && IsPointInsideShape(e.Location, shape))
                {
                    // Toggle selection status of the clicked shape
                    if (selectedShape == shape)
                    {
                        // If the shape is already selected, deselect it
                        selectedShape = null;
                    }
                    else
                    {
                        // Otherwise, select the shape
                        selectedShape = shape;
                    }
                    Refresh();
                    shapeSelected = true;
                    break;
                }
            }
            if (currentTextBox != null && !currentTextBox.Bounds.Contains(e.Location))
            {
                // Finish adding text box if the click is outside of the current text box
                currentTextBox = null;
                Refresh();
            }
            else if (currentTextBox != null && e.Button == MouseButtons.Left)
            {
                // Move the text box if the click is inside of it
                currentTextBox.Location = e.Location;
                Refresh();
            }

            // If no shape was clicked, deselect any previously selected shape
            if (!shapeSelected)
            {
                selectedShape = null;
                Refresh();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Draw all shapes
            Graphics g = e.Graphics;
            for (int i = 0; i < shapeCount; i++)
            {
                // Set pen color to black by default
                Pen pen = new Pen(Color.Black);
                if (shapes[i] == selectedShape)
                {
                    // Change pen color to blue for selected shape
                    pen.Color = Color.Blue;
                }
                // Draw the shape
                shapes[i].Draw(g, pen);
            }

            // If a shape is being created and rubber band is active, draw the preview of the shape
            if (rubberBandActive && createShapeStatus)
            {
                Pen previewPen = new Pen(Color.Gray); // Use the same color for preview
                if (createSquare)
                {
                    // Create the square based on the rubber band rectangle
                    int width = Math.Abs(rubberBandEnd.X - rubberBandStart.X);
                    int height = Math.Abs(rubberBandEnd.Y - rubberBandStart.Y);
                    int x = Math.Min(rubberBandStart.X, rubberBandEnd.X);
                    int y = Math.Min(rubberBandStart.Y, rubberBandEnd.Y);
                    g.DrawRectangle(previewPen, x, y, width, height);
                }
                else if (createTriangle)
                {
                    // Create the triangle based on the rubber band rectangle
                    Point midPoint = new Point((rubberBandStart.X + rubberBandEnd.X) / 2, (rubberBandStart.Y + rubberBandEnd.Y) / 2);
                    double angle = Math.Atan2(rubberBandEnd.Y - rubberBandStart.Y, rubberBandEnd.X - rubberBandStart.X);
                    double distance = Math.Sqrt(Math.Pow(rubberBandEnd.X - rubberBandStart.X, 2) + Math.Pow(rubberBandEnd.Y - rubberBandStart.Y, 2));
                    Point thirdPoint = new Point((int)(midPoint.X + distance * Math.Cos(angle - Math.PI / 3)), (int)(midPoint.Y + distance * Math.Sin(angle - Math.PI / 3)));
                    Point[] points = { rubberBandStart, rubberBandEnd, thirdPoint };
                    g.DrawPolygon(previewPen, points);
                }
                else if (createCircle)
                {
                    // Create the circle based on the rubber band rectangle
                    int diameter = (int)Math.Sqrt(Math.Pow(rubberBandEnd.X - rubberBandStart.X, 2) + Math.Pow(rubberBandEnd.Y - rubberBandStart.Y, 2));
                    g.DrawEllipse(previewPen, rubberBandStart.X - diameter, rubberBandStart.Y - diameter, 2 * diameter, 2 * diameter);
                }
            }
            if (currentTextBox != null)
            {
                // Draw the text box
                using (Pen pen = new Pen(Color.Black))
                {
                    Rectangle textBoxRect = new Rectangle(currentTextBox.Location, currentTextBox.Size);
                    e.Graphics.DrawRectangle(pen, textBoxRect);
                }
            }
        }
        private bool IsPointInsideShape(Point point, Shape shape)
        {
            // Check if the point is inside the shape
            if (shape is Square square)
            {
                return IsPointInsideSquare(point, square);
            }
            else if (shape is Triangle triangle)
            {
                return IsPointInsideTriangle(point, triangle);
            }
            else if (shape is Circle circle)
            {
                return IsPointInsideCircle(point, circle);
            }
            // Handle other shape types here if needed
            return false;
        }

        private bool IsPointInsideSquare(Point point, Square square)
        {
            // Check if the point is inside the square
            if (point.X >= square.MinX && point.X <= square.MaxX &&
                point.Y >= square.MinY && point.Y <= square.MaxY)
            {
                return true;
            }
            return false;
        }

        private bool IsPointInsideTriangle(Point point, Triangle triangle)
        {
            // Check if the point is inside the triangle
            // For simplicity, let's assume all points inside the bounding box are inside the triangle
            if (point.X >= triangle.MinX && point.X <= triangle.MaxX &&
                point.Y >= triangle.MinY && point.Y <= triangle.MaxY)
            {
                return true;
            }
            return false;
        }

        private bool IsPointInsideCircle(Point point, Circle circle)
        {
            // Check if the point is inside the circle
            int radiusSquared = (circle.MaxX - circle.MinX) * (circle.MaxX - circle.MinX) / 4;
            int distanceSquared = (point.X - circle.MinX) * (point.X - circle.MinX) + (point.Y - circle.MinY) * (point.Y - circle.MinY);
            if (distanceSquared <= radiusSquared)
            {
                return true;
            }
            return false;
        }
    }
    abstract class Shape
    {
        public Color FillColor { get; set; } = Color.Transparent; // Default fill color is transparent

        public Color Color { get; set; } = Color.Black; // Default color is black

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
        public Point keyPt, oppPt;

        public Square(Point keyPt, Point oppPt)
        {
            this.keyPt = keyPt;
            this.oppPt = oppPt;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            // Set the fill color
            Brush brush = new SolidBrush(FillColor);

            // Draw the filled square
            g.FillRectangle(brush, keyPt.X, keyPt.Y, oppPt.X - keyPt.X, oppPt.Y - keyPt.Y);

            pen.Color = Color;
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
        public Point point1, point2, point3;

        public Triangle(Point point1, Point point2, Point point3)
        {
            this.point1 = point1;
            this.point2 = point2;
            this.point3 = point3;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            // Set the fill color
            Brush brush = new SolidBrush(FillColor);

            // Draw the filled triangle
            g.FillPolygon(brush, new Point[] { point1, point2, point3 });
            pen.Color = Color;
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
        public Point center, circumference;

        public Circle(Point center, Point circumference)
        {
            this.center = center;
            this.circumference = circumference;
        }

        public override void Draw(Graphics g, Pen pen)
        {
            // Set the fill color
            Brush brush = new SolidBrush(FillColor);

            // Draw the filled circle
            int radius = (int)Math.Sqrt(Math.Pow(circumference.X - center.X, 2) + Math.Pow(circumference.Y - center.Y, 2));
            g.FillEllipse(brush, center.X - radius, center.Y - radius, 2 * radius, 2 * radius);

            pen.Color = Color;
            g.DrawEllipse(pen, center.X - radius, center.Y - radius, 2 * radius, 2 * radius);
        }

        public override int MinX => center.X;
        public override int MaxX => center.X;
        public override int MinY => center.Y;
        public override int MaxY => center.Y;
    }
}
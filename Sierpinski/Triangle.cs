//////////////////////////////////////////////////////////////////////////////
//
// Triangle.cs 
// A Triangle class in WPF implementation.
// Copyright (C) 2018 - W. Wonneberger
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//
//////////////////////////////////////////////////////////////////////////////

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Sierpinski
{
    public sealed class Triangle
    {
        #region Triangle Class Constant Definitions

        private readonly int Top = 0;
        private readonly int LowerLeft = 1;
        private readonly int LowerRight = 2;
        private readonly int End = 3;
        private readonly int MaxPoints = 4;

        #endregion Triangle Class Constant Definitions

        #region Triangle Class Properties

        public Point[] Points { get; private set; }
        public Brush Border { get; private set; }
        public double BorderWidth { get; private set; }
        public Brush Fill { get; private set; }
        public double Width { get; private set; }
        public double Height { get; private set; }

        #endregion Triangle Class Properties

        #region Triangle Class Constructors

        public Triangle(Point[] points, Brush border, double borderWidth, Brush fill)
        {
            Points = SetPoints(points);
            Border = border;
            BorderWidth = borderWidth;
            Fill = fill;
            Width = Points[LowerRight].X - Points[LowerLeft].X;
            Height = Points[LowerRight].Y - Points[Top].Y;
        }

        public Triangle(Point apex, double width, double height, Brush border, double borderWidth, Brush fill)
        {
            Points = SetPointsUsingApexWidthAndHeight(apex, width, height);
            Border = border;
            BorderWidth = borderWidth;
            Fill = fill;
        }

        #endregion Triangle Class Constructors

        #region Triangle Class Implementation

        //
        // For WPF to render a Polygon as a Triangle, four points are 
        // needed to close the shape in full. To simplify the creation 
        // of a Triangle, the caller only needs to supply three points,
        // and the fourth point to close the outline of the shape is 
        // the first point in the array.
        //
        private Point[] SetPoints(Point[] points)
        {
            var trianglePoints = new Point[points.Length + 1];
            var i = 0;

            foreach (var point in points)
            {
                trianglePoints[(i++)] = point;
            }

            trianglePoints[i] = points[0];

            return trianglePoints;
        }

        private Point[] SetPointsUsingApexWidthAndHeight(Point apex, double width, double height)
        {
            Width = width;
            Height = height;

            var minX = apex.X - (width / 2.0d);
            var maxX = apex.X + (width / 2.0d);
            var maxY = apex.Y + height;
            var points = new Point[MaxPoints];

            points[Top] = apex;
            points[LowerLeft] = new Point(minX, maxY);
            points[LowerRight] = new Point(maxX, maxY);
            points[End] = apex;

            return points;
        }

        public void Draw(Panel drawingSurface)
        {
            var triangle = new Polygon
            {
                Points = new PointCollection(Points),
                Stroke = Border,
                StrokeThickness = BorderWidth,
                Fill = Fill
            };

            drawingSurface.SnapsToDevicePixels = true;
            drawingSurface.Children.Add(triangle);
        }

        public override string ToString()
        {
            var trace = new StringBuilder();

            trace.AppendLine($"Contents of {GetType().Name}:");
            trace.AppendLine($"         Points: {DrawingUtils.FormatPointsAsString(Points)}");
            trace.AppendLine($"         Border: {Border}");
            trace.AppendLine($"    BorderWidth: {BorderWidth}");
            trace.AppendLine($"           Fill: {Fill}");
            trace.AppendLine($"          Width: {Width}");
            trace.AppendLine($"         Height: {Height}");

            return trace.ToString();
        }

        #endregion Triangle Class Implementation
    }
}

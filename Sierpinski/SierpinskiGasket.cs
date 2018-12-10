//////////////////////////////////////////////////////////////////////////////
//
// SierpinskiGasket.cs 
// Sierpinski Gasket implementation.
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

using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Sierpinski
{
    public sealed class SierpinskiGasket
    {
        #region SierpinskiGasket Class Constant Definitions

        /////////////////////////////////////////////////////////////////////
        //
        // Points[] points:
        //      points[0] = topPoint;
        //      points[1] = leftPoint;
        //      points[2] = rightPoint;
        //
        /////////////////////////////////////////////////////////////////////
        private readonly int TopPoint = 0;
        private readonly int LeftPoint = 1;
        private readonly int RightPoint = 2;

        #endregion SierpinskiGasket Class Constant Definitions

        #region SierpinskiGasket Class Data Members

        public int Level { get; private set; }
        public Brush ForegroundColor { get; private set; }
        public Brush GasketFill { get; private set; }
        public Brush BackgroundColor { get; private set; }
        public double LineWidth { get; private set; }
        public Point[] Points { get; private set; }
        private Panel Surface { get; set; }
        private int Count { get; set; }

        #endregion SierpinskiGasket Class Data Members

        #region SierpinskiGasket Class Constructor

        public SierpinskiGasket(int level, Brush foregroundColor, Brush backgroundColor,
                                Brush fillColor, double lineWidth, Point[] points)
        {
            Level = level;
            ForegroundColor = foregroundColor;
            BackgroundColor = BackgroundColor;
            GasketFill = fillColor;
            LineWidth = lineWidth;
            Points = points;
            Count = 0;
        }

        public SierpinskiGasket(GasketSettings settings)
        {
            Level = settings.Level;
            ForegroundColor = new SolidColorBrush(settings.BorderColor);
            BackgroundColor = new SolidColorBrush(settings.BackgroundColor);
            GasketFill = new SolidColorBrush(settings.FillColor);
            LineWidth = settings.LineWidth;
            Points = settings.Points;
            Count = 0;
        }

        #endregion SierpinskiGasket Class Constructor

        #region SierpinskiGasket Class Implementation

        public void Draw(Panel drawingSurface)
        {
            Surface = drawingSurface;
            Surface.Background = BackgroundColor;
            //
            // Recursively call DrawTrianlge() to render the 
            // gasket on the caller supplied Panel.
            //
            DrawTriangle(Level, Points);
        }

        private void DrawTriangle(int level, Point[] points)
        {
            //
            // Instantiate and draw the triangle
            //
            if (level == 0)
            {
                var triangle = new Triangle(points, ForegroundColor, LineWidth, GasketFill);
                triangle.Draw(Surface);

                Count++;
                Debug.WriteLine($"Triangle {Count}:");
                Debug.WriteLine(triangle.ToString());
            }

            else
            {
                //
                // Find the edge midpoints for the smaller triangles.
                //
                var leftMiddle = new Point
                {
                    X = (points[TopPoint].X + points[LeftPoint].X) / 2d,
                    Y = (points[TopPoint].Y + points[LeftPoint].Y) / 2d
                };

                var rightMiddle = new Point
                {
                    X = (points[TopPoint].X + points[RightPoint].X) / 2d,
                    Y = (points[TopPoint].Y + points[RightPoint].Y) / 2d
                };

                var bottomMiddle = new Point
                {
                    X = (points[LeftPoint].X + points[RightPoint].X) / 2d,
                    Y = (points[LeftPoint].Y + points[RightPoint].Y) / 2d
                };

                DrawTriangle(level - 1, new Point[] { points[TopPoint], leftMiddle, rightMiddle });

                DrawTriangle(level - 1, new Point[] { leftMiddle, points[LeftPoint], bottomMiddle });

                DrawTriangle(level - 1, new Point[] { rightMiddle, bottomMiddle, points[RightPoint] });
            }
        }

        #endregion SierpinskiGasket Class Implementation
    }
}

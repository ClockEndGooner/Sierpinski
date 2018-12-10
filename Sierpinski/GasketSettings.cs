//////////////////////////////////////////////////////////////////////////////
//
// GasketSettings.cs
// Sierpinski Gasket Settings class implementation.
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
using System.Windows.Media;

namespace Sierpinski
{
    public sealed class GasketSettings
    {
        public int Level { get; private set; }
        public Color BorderColor { get; private set; }
        public Color FillColor { get; private set; }
        public Color BackgroundColor { get; private set; }
        public double LineWidth { get; private set; }
        public Point[] Points { get; set; }

        public GasketSettings(UserSettings settings)
        {
            Level = settings.Level;
            BorderColor = settings.BorderColor;
            FillColor = settings.FillColor;
            BackgroundColor = settings.BackgroundColor;
            LineWidth = settings.LineWidth;
        }

        public override string ToString()
        {
            var trace = new StringBuilder();
            var pointsTrace = DrawingUtils.FormatPointsAsString(Points);

            trace.AppendLine($"Contents of {GetType().Name}");
            trace.AppendLine($"    Level: {Level}");
            trace.AppendLine($"    BorderColor: {BorderColor}");
            trace.AppendLine($"    FillColor: {FillColor}");
            trace.AppendLine($"    BackgroundColor: {BackgroundColor}");
            trace.AppendLine($"    LineWidth: {LineWidth}");
            trace.AppendLine($"    Points: {pointsTrace}");

            return trace.ToString();
        }
    }
}

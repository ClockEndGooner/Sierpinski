//////////////////////////////////////////////////////////////////////////////
//
// UserSettings.cs 
// Sierpinski UserSettings implementation.
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

using System;
using System.Configuration;
using System.Text;
using System.Windows.Media;

namespace Sierpinski
{
    public class UserSettings : ApplicationSettingsBase
    {
        #region UserSettings Class Data Properties 

        [UserScopedSetting()]
        [DefaultSettingValue("20")]
        public double Left
        {
            get
            {
                return Convert.ToDouble(this["Left"]);
            }

            set
            {
                this["Left"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("20")]
        public double Top
        {
            get
            {
                return Convert.ToDouble(this["Top"]);
            }

            set
            {
                this["Top"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("475")]
        public double Width
        {
            get
            {
                return Convert.ToDouble(this["Width"]);
            }

            set
            {
                this["Width"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("530")]
        public double Height
        {
            get
            {
                return Convert.ToDouble(this["Height"]);
            }

            set
            {
                this["Height"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("5")]
        public int Levels
        {
            get
            {
                return Convert.ToInt32(this["Levels"]);
            }

            set
            {
                this["Levels"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("1.25")]
        public double LineWidth
        {
            get
            {
                return Convert.ToDouble(this["LineWidth"]);
            }

            set
            {
                this["LineWidth"] = value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("White")]
        public Color BorderColor
        {
            get
            {
                return ((Color)this["BorderColor"]);
            }

            set
            {
                this["BorderColor"] = (Color)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("Transparent")]
        public Color FillColor
        {
            get
            {
                return ((Color)this["FillColor"]);
            }

            set
            {
                this["FillColor"] = (Color)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("#FF5248B8")]
        public Color BackgroundColor
        {
            get
            {
                return ((Color)this["BackgroundColor"]);
            }

            set
            {
                this["BackgroundColor"] = (Color)value;
            }
        }

        #endregion UserSettings Class Data Properties 

        #region UserSettings Class Data Implementation

        public override string ToString()
        {
            var trace = new StringBuilder();

            trace.AppendLine($"Contents of {GetType().Name}:");
            trace.AppendLine($"               Left: {Left}");
            trace.AppendLine($"                Top: {Top}");
            trace.AppendLine($"              Width: {Width}");
            trace.AppendLine($"             Height: {Height}");
            trace.AppendLine($"             Levels: {Levels}");
            trace.AppendLine($"          LineWidth: {LineWidth}");
            trace.AppendLine($"        BorderColor: {BorderColor}");
            trace.AppendLine($"          FillColor: {FillColor}");
            trace.AppendLine($"    BackgroundColor: {BackgroundColor}");

            return trace.ToString();
        }

        #endregion UserSettings Class Data Implementation
    }
}

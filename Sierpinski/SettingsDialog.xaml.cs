//////////////////////////////////////////////////////////////////////////////
//
// SettingsDialog.xaml.cs
// Implementation of settings dialog box for drawing a Sierpinski Gasket.
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
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Sierpinski
{
    public partial class SettingsDialog : Window
    {
        #region SettingsDialog Class Properties

        public GasketSettings Settings { get; private set; }

        #endregion SettingsDialog Class Properties

        #region SettingsDialog Class Constructor

        public SettingsDialog(GasketSettings settings)
        {
            InitializeComponent();

            Settings = settings;
            SetDialogChildControlValues();
        }

        #endregion SettingsDialog Class Constructor

        #region SettingsDialog Class Event Handlers

        private void OnSettingsOKClick(object sender, RoutedEventArgs e)
        {
            Color? borderColor = BorderColorPicker.SelectedColor;
            Color? backgroundColor = BackgroundColorPicker.SelectedColor;
            Color? fillColor = FillColorPicker.SelectedColor;

            double? lineWidth = (double)LineThicknessDecimalUpDown.Value;
            int? levels = (int)LevelsDecimalUpDown.Value;

            if (borderColor.HasValue && backgroundColor.HasValue &&
                fillColor.HasValue && lineWidth.HasValue && levels.HasValue)
            {
                Settings = new GasketSettings(levels.Value, borderColor.Value,
                                              fillColor.Value, backgroundColor.Value,
                                              lineWidth.Value, Settings.Points);

                DialogResult = true;
                Close();
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                DialogResult = false;

                this.Close();
            }
        }

        #endregion SettingsDialog Class Event Handlers

        #region SettingsDialog Class Implementation 

        private void SetDialogChildControlValues()
        {
            BorderColorPicker.SelectedColor = Settings.BorderColor;
            BackgroundColorPicker.SelectedColor = Settings.BackgroundColor;
            FillColorPicker.SelectedColor = Settings.FillColor;

            LineThicknessDecimalUpDown.Value = Convert.ToDecimal(Settings.LineWidth);
            LevelsDecimalUpDown.Value = Convert.ToDecimal(Settings.Levels);
        }

        #endregion SettingsDialog Class Implementation
    }
}

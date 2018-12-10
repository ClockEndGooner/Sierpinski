//////////////////////////////////////////////////////////////////////////////
//
// MainWindow.xaml.cs
// Sierpinski Gasket main application window.
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
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using Microsoft.Win32;

using static System.Windows.SystemParameters;

namespace Sierpinski
{
    public partial class MainWindow : Window
    {
        private readonly double CanvasInset = 40d;
        private readonly double SurfaceMargin = 20d;

        private GasketSettings GasketSettings;
        public UserSettings UserSettings { get; private set; }

        public MainWindow(UserSettings userSettings)
        {
            InitializeComponent();

            UserSettings = userSettings;
            GasketSettings = new GasketSettings(UserSettings);
        }

        private void OnMainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void OnMainWindowLoaded(object sender, RoutedEventArgs e)
        {
            SetCanvasSize();
            DrawGasket();

        }

        private void OnRightMouseButtonClicked(object sender, MouseButtonEventArgs e)
        {
            UIElement mouseClickTarget = sender as UIElement;

            ShowGasketContextMenu(mouseClickTarget);

            Debug.WriteLine($"Window Size - Width: {Width}  Height: {Height}");
        }

        private void ShowGasketContextMenu(UIElement contextMenuTarget)
        {
            var contextMenu = (ContextMenu) FindResource("GasketContextMenu");

            if (contextMenu != null)
            {
                contextMenu.PlacementTarget = contextMenuTarget;
                contextMenu.IsOpen = true;
            }
        }

        private void OnContextMenuItemClicked(object sender, RoutedEventArgs menuItemClickEvent)
        {
            MenuItem selectedMenuItem = sender as MenuItem;

            if (selectedMenuItem.Name == "SettingsMenuItem")
            {
                UpdateGasketSettings();
            }

            else if (selectedMenuItem.Name == "SaveMenuItem")
            {
                SaveGasketImage();
            }

            else
            {
                Debug.Assert("AboutMenuItem" == selectedMenuItem.Name);

                ShowAboutDialogBox();
            }
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            Button buttonClicked = sender as Button;

            if (buttonClicked.Name == "SettingsButton")
            {
                UpdateGasketSettings();
            }

            else if (buttonClicked.Name == "SaveButton")
            {
                SaveGasketImage();
            }

            else
            {
                ShowAboutDialogBox();
            }
        }

        private void UpdateGasketSettings()
        {

        }

        private void SaveGasketImage()
        {

        }

        private void ShowAboutDialogBox()
        {

        }

        private double SquaredCanvasSize
        {
            get
            {
                var canvasWidth = MainLayoutGrid.RenderSize.Width;
                var canvasHeight = MainLayoutGrid.RenderSize.Height;
                var canvsaSize = Math.Min(canvasWidth, canvasHeight);

                return canvsaSize - CanvasInset;
            }
        }

        private void SetCanvasSize()
        {
            GasketCanvas.Width = SquaredCanvasSize;
            GasketCanvas.Height = SquaredCanvasSize;

            Debug.WriteLine($"SquaredCanvasSize: {SquaredCanvasSize}");
        }

        private void DrawGasket()
        {
            var width = GasketCanvas.Width;
            var height = GasketCanvas.Height;

            Debug.WriteLine($"Inside DrawGasket() - Width: {width}  Height: {height}");

            var topPoint = new Point
            {
                X = width / 2d,
                Y = SurfaceMargin
            };

            var leftPoint = new Point
            {
                X = SurfaceMargin,
                Y = height - SurfaceMargin
            };

            var rightPoint = new Point
            {
                X = width - SurfaceMargin,
                Y = height - SurfaceMargin
            };

            GasketSettings.Points = new Point[]
            {
                topPoint, leftPoint, rightPoint
            };

            Debug.WriteLine($"GasketSettings: {GasketSettings}");

            /*
            var gasket =
            new SierpinskiGasket(6, Brushes.White, Brushes.Transparent, 1.25d, points);
            */

            var gasket = new SierpinskiGasket(GasketSettings);
            gasket.Draw(GasketCanvas);
        }
    }
}

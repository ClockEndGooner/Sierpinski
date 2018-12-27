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

using Microsoft.Win32;

namespace Sierpinski
{
    public partial class MainWindow : Window
    {
        #region MainWindow  Class Constant Definitions

        private readonly double CanvasInset = 40d;
        private readonly double SurfaceMargin = 20d;

        private readonly string DefaultImageFileName = "Sierpinski";
        private readonly int PNGDefaultFileFiler = 4;

        #endregion MainWindow Class Constant Definitions

        #region MainWindow Class Data Attributes

        private GasketSettings GasketSettings;
        public UserSettings UserSettings { get; private set; }
        private SierpinskiGasket theGasket;

        #endregion MainWindow Class Data Attributes

        #region MainWindow Class Constructor

        public MainWindow(UserSettings userSettings)
        {
            InitializeComponent();

            UserSettings = userSettings;
            GasketSettings = new GasketSettings(UserSettings);
        }

        #endregion MainWindow Class Constructor

        #region MainWindow Class Event Handlers

        private void OnMainWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var trace =
            $"Width: {Width.ToString()}  Height: {Height.ToString()}  Top: {Top.ToString()}  Left: {Left.ToString()}  ";

            Debug.Write(trace);

            trace =
            $"GasketCanvas.Width: {GasketCanvas.Width.ToString()}  Height: {GasketCanvas.Height.ToString()}";

            Debug.WriteLine(trace);
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

        private void OnKeyUp(object sender, KeyEventArgs keyPressedEvent)
        {
            if (keyPressedEvent.Key == Key.Apps)
            {
                ShowGasketContextMenu(this);
            }
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

        #endregion MainWindow Class Event Handlers

        #region MainWindow Class Implementation

        private void UpdateGasketSettings()
        {
            var settingsDialog = new SettingsDialog(GasketSettings)
            {
                Owner = this
            };

            bool? settingsChanaged = settingsDialog.ShowDialog();

            if (settingsChanaged.HasValue && settingsChanaged.Value == true)
            {
                GasketSettings = settingsDialog.Settings;
                DrawGasket();

                UserSettings = new UserSettings()
                {
                    Levels = GasketSettings.Levels,
                    LineWidth = GasketSettings.LineWidth,
                    BorderColor = GasketSettings.BorderColor,
                    BackgroundColor = GasketSettings.BackgroundColor,
                    FillColor = GasketSettings.FillColor
                };
            }
        }

        private void SaveGasketImage()
        {
            var bitmapSettings = GetBitmapFileSettings();

            if (theGasket != null)
            {
                if (bitmapSettings != null)
                {
                    theGasket.Save(GasketCanvas,
                                   bitmapSettings.Encoder,
                                   bitmapSettings.BitmapFileName);
                }
            }
        }

        private BitmapFileSettings GetBitmapFileSettings()
        {
            BitmapFileSettings bitmapSettings = null;

            var saveFileDialog = new SaveFileDialog
            {
                Title = "Save Sierpinski Gasket Image File",
                FileName = DefaultImageFileName,
                Filter = BitmapFileSettings.FileFilters,
                FilterIndex = PNGDefaultFileFiler,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                AddExtension = true,
                OverwritePrompt = true,
                ValidateNames = true,
            };

            bool? dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                var encoding = (BitmapEncoding)(saveFileDialog.FilterIndex - 1);
                var imageFileName = saveFileDialog.FileName;

                bitmapSettings = new BitmapFileSettings(encoding, imageFileName);
            }

            return bitmapSettings;
        }

        private void ShowAboutDialogBox()
        {
            var aboutDialog = new AboutSierpinskiDialog
            {
                Owner = this
            };

            aboutDialog.ShowDialog();
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
            Mouse.OverrideCursor = Cursors.Wait;

            GasketCanvas.Children.Clear();
            SetCanvasSize();

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

            theGasket = new SierpinskiGasket(GasketSettings);
            theGasket.Draw(GasketCanvas);

            Mouse.OverrideCursor = Cursors.Arrow;
        }

        #endregion MainWindow Class Implementation
    }
}

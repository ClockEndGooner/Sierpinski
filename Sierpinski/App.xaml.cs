//////////////////////////////////////////////////////////////////////////////
//
// App.xaml.cs 
// Sierpinski application implementation.
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

namespace Sierpinski
{
    public partial class App : Application
    {
        #region Sierpinski App Data Properties 

        private UserSettings theUserSettings;
        private MainWindow theMainWindow;

        #endregion Sierpinski App Data Properties 

        #region Sierpinski App Event Handlers

        public void OnApplicationStartup(object sender, StartupEventArgs startupEvent)
        {
            theUserSettings = LoadUserSettings();

            theMainWindow = new MainWindow(theUserSettings)
            {
                Left = theUserSettings.Left,
                Top = theUserSettings.Top,
                Width = theUserSettings.Width,
                Height = theUserSettings.Height
            };

            theMainWindow.Show();
        }

        public UserSettings LoadUserSettings()
        {
            var settings = new UserSettings();

            return settings;
        }

        private void OnApplicationExit(object sender, ExitEventArgs exitEvent)
        {
            if (theMainWindow != null)
            {
                if (UpdateUserSettings())
                {
                    theUserSettings.Save();
                }
            }
        }

        private bool UpdateUserSettings()
        {
            bool updatedSettings = false;

            if (theMainWindow != null &&
                theMainWindow.UserSettings != null)
            {
                theUserSettings.Top = theMainWindow.Top;
                theUserSettings.Left = theMainWindow.Left;
                theUserSettings.Width = theMainWindow.Width;
                theUserSettings.Height = theMainWindow.Height;

                theUserSettings.BorderColor = theMainWindow.UserSettings.BorderColor;
                theUserSettings.FillColor = theMainWindow.UserSettings.FillColor;
                theUserSettings.BackgroundColor = theMainWindow.UserSettings.BackgroundColor;
                theUserSettings.LineWidth = theMainWindow.UserSettings.LineWidth;
                theUserSettings.Levels = theMainWindow.UserSettings.Levels;

                updatedSettings = true;
            }

            return updatedSettings;
        }

        #endregion Sierpinski App Event Handlers
    }
}

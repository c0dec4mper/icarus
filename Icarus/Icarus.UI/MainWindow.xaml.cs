﻿using Icarus.Core.Commands;
using Icarus.Core.DroneConfiguration;
using Icarus.Core.Enums;
using Icarus.Core.Interfaces;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Icarus.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string onlineImagePath = @"\Images\online.png";
        const string offlineImagePath = @"\Images\offline.png";

        const string upCommandMessage = "Go Up";
        const string downCommandMessage = "Go Down";
        const string leftCommandMessage = "Go Left";
        const string rightCommandMessage = "Go Right";
        const string fwdCommandMessage = "Go Forward";
        const string bwdCommandMessage = "Go Backward";

        public MainWindow()
        {
            InitializeComponent();
            ToggleButtonsState(false); 
            ConfigureDrone();
        }

        ICommandFactory CommandFactory
        {
            get
            {
                return App.Container.GetInstance<ICommandFactory>();
            }
        }

        ICommunicator Communicator
        {
            get
            {
                return App.Container.GetInstance<ICommunicator>();
            }
        }

        void ConfigureDrone()
        {
            var configureCommand = CommandFactory.CreateCommand(CommandType.Configure);
            ((ConfigureCommand)configureCommand)
                .SetConfiguration(new DroneConfiguration()
                {
                    // configuration parameters go here
                });
            Communicator.ExecuteCommand(configureCommand);
        }

        void ToggleButtonsState(bool enabled)
        {
            btnUp.IsEnabled = enabled;
            btnDown.IsEnabled = enabled;
            btnRight.IsEnabled = enabled;
            btnLeft.IsEnabled = enabled;
            btnForward.IsEnabled = enabled;
            btnBackward.IsEnabled = enabled;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            var startCommand = CommandFactory.CreateCommand(CommandType.Start);
            Communicator.ExecuteCommand(startCommand);

            btnStart.IsEnabled = false;
            btnStop.IsEnabled = true;

            SetDroneConnectionImage(onlineImagePath);
            ToggleButtonsState(true);
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            var stopCommand = CommandFactory.CreateCommand(CommandType.Stop);
            Communicator.ExecuteCommand(stopCommand);

            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;

            SetDroneConnectionImage(offlineImagePath);
            ToggleButtonsState(false);
        }

        void SetDroneConnectionImage(string imagePath)
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var image = Image.FromFile(currentDirectory + imagePath);
            var imageMem = new MemoryStream();
            image.Save(imageMem, ImageFormat.Png);
            var bmp = new BitmapImage();
            bmp.BeginInit();
            bmp.StreamSource = new MemoryStream(imageMem.ToArray());
            bmp.EndInit();
            imgConnectionStatus.Source = bmp;
        }

        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            AddCommandToList(upCommandMessage);
            var upCommand = CommandFactory.CreateCommand(CommandType.MoveUp);
            Communicator.ExecuteCommand(upCommand);
        }

        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            AddCommandToList(downCommandMessage);
            var downCommand = CommandFactory.CreateCommand(CommandType.MoveDown);
            Communicator.ExecuteCommand(downCommand);
        }

        private void btnLeft_Click(object sender, RoutedEventArgs e)
        {
            AddCommandToList(leftCommandMessage);
            var leftCommand = CommandFactory.CreateCommand(CommandType.MoveLeft);
            Communicator.ExecuteCommand(leftCommand);
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            AddCommandToList(rightCommandMessage);
            var rightCommand = CommandFactory.CreateCommand(CommandType.MoveRight);
            Communicator.ExecuteCommand(rightCommand);
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            AddCommandToList(fwdCommandMessage);
            var fwdCommand = CommandFactory.CreateCommand(CommandType.MoveForward);
            Communicator.ExecuteCommand(fwdCommand);
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            AddCommandToList(bwdCommandMessage);
            var backCommand = CommandFactory.CreateCommand(CommandType.MoveBackward);
            Communicator.ExecuteCommand(backCommand);
        }

        void AddCommandToList(string commandMessage)
        {
            lstMessageWindow.Items.Add(commandMessage + " - " + DateTime.Now.ToString());
            lstMessageWindow.SelectedIndex = lstMessageWindow.Items.Count - 1;
            lstMessageWindow.ScrollIntoView(lstMessageWindow.SelectedItem);
        }
    }
}

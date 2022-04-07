using System;
using System.Reflection;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.Text.Json;
using System.Text.Json.Serialization;
using lab8.Models;
using lab8.ViewModels;

namespace lab8.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.FindControl<Button>("AddPlanned").Click += ClickEventAddPlanned;
            this.FindControl<Button>("AddInProgress").Click += ClickEventAddInProgress;
            this.FindControl<Button>("AddCompleted").Click += ClickEventAddCompleted;
            this.FindControl<MenuItem>("Save").Click += ClickEventSave;
            this.FindControl<MenuItem>("Load").Click += ClickEventLoad;
            this.FindControl<MenuItem>("Exit").Click += ClickEventExit;
            this.FindControl<MenuItem>("About").Click += ClickEventAbout;
        }


        private async void ClickEventAddPlanned(object sender, RoutedEventArgs e)
        {
            var window = new CreateCardView();
            var item = await window.ShowDialog<Card?>((Window)this.VisualRoot);
            if (item != null)
            {
                var context = this.DataContext as MainWindowViewModel;
                context.ItemsPlanned.Add(item);
            }

        }
        private async void ClickEventAddInProgress(object sender, RoutedEventArgs e)
        {
            var window = new CreateCardView();
            var item = await window.ShowDialog<Card?>((Window)this.VisualRoot);
            if (item != null)
            {
                var context = this.DataContext as MainWindowViewModel;
                context.ItemsInProgress.Add(item);
            }
        }
        private async void ClickEventAddCompleted(object sender, RoutedEventArgs e)
        {
            var window = new CreateCardView();
            var item = await window.ShowDialog<Card?>((Window)this.VisualRoot);
            if (item != null)
            {
                var context = this.DataContext as MainWindowViewModel;
                context.ItemsCompleted.Add(item);
            }
        }
        private async void ClickEventSave(object sender, RoutedEventArgs e)
        {
            string? path;
            var taskPath = new SaveFileDialog()
            {
                Title = "Save table",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add(new FileDialogFilter() { Name = "Таблица задач (*.txt)", Extensions = { "txt" } });

            path = await taskPath.ShowAsync((Window)this.VisualRoot);

            if (path is not null)
            {
                var itemsPlanned = (this.DataContext as MainWindowViewModel).ItemsPlanned;
                var itemsInProgress = (this.DataContext as MainWindowViewModel).ItemsInProgress;
                var itemsCompleted = (this.DataContext as MainWindowViewModel).ItemsCompleted;
                using (StreamWriter wr = File.CreateText(path))
                {
                    wr.WriteLine(itemsPlanned.Count.ToString());
                    foreach (var item in itemsPlanned)
                    {
                        wr.WriteLine(item.Name);
                        wr.WriteLine(item.Description);
                        wr.WriteLine(item.ImagePath);
                    }
                    wr.WriteLine(itemsInProgress.Count.ToString());
                    foreach (var item in itemsInProgress)
                    {
                        wr.WriteLine(item.Name);
                        wr.WriteLine(item.Description);
                        wr.WriteLine(item.ImagePath);
                    }
                    wr.WriteLine(itemsCompleted.Count.ToString());
                    foreach (var item in itemsCompleted)
                    {
                        wr.WriteLine(item.Name);
                        wr.WriteLine(item.Description);
                        wr.WriteLine(item.ImagePath);
                    }

                }
            }
        }
        private async void ClickEventLoad(object sender, RoutedEventArgs e)
        {
            string? path;
            var taskPath = new OpenFileDialog()
            {
                Title = "Open table",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add(new FileDialogFilter() { Name = "Таблица задач (*.txt)", Extensions = { "txt" } });

            string[]? pathArray = await taskPath.ShowAsync((Window)this.VisualRoot);
            path = pathArray is null ? null : string.Join(@"\", pathArray);

            if (path is not null)
            {
                
                var itemsPlanned = (this.DataContext as MainWindowViewModel).ItemsPlanned;
                var itemsInProgress = (this.DataContext as MainWindowViewModel).ItemsInProgress;
                var itemsCompleted = (this.DataContext as MainWindowViewModel).ItemsCompleted;
                itemsPlanned.Clear();
                itemsInProgress.Clear();
                itemsCompleted.Clear();
                using (StreamReader sr = File.OpenText(path))
                {
                    int count;
                    if (!Int32.TryParse(sr.ReadLine(), out count)) return;

                    for (int i = 0; i < count; i++)
                    {
                        itemsPlanned.Add(new Card(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                    }
                    count = 0;
                    if (!Int32.TryParse(sr.ReadLine(), out count)) return;

                    for (int i = 0; i < count; i++)
                    {
                        itemsInProgress.Add(new Card(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                    }
                    count = 0;
                    if (!Int32.TryParse(sr.ReadLine(), out count)) return;

                    for (int i = 0; i < count; i++)
                    {
                        itemsCompleted.Add(new Card(sr.ReadLine(), sr.ReadLine(), sr.ReadLine()));
                    }
                }
            }
        }
        private async void ClickEventExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private async void ClickEventAbout(object? sender, RoutedEventArgs e)
        {
            var window = new AboutView();
            
            await window.ShowDialog((Window)this.VisualRoot);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Interactivity;
using System.ComponentModel;
using ReactiveUI;
using System.Reactive;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using lab8.Models;

namespace lab8.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(bool makeItems = true)
        {
            
            ItemsPlanned = new ObservableCollection<Card>();
            ItemsInProgress = new ObservableCollection<Card>();
            ItemsCompleted = new ObservableCollection<Card>();
           
            AddImageClick = ReactiveCommand.Create<Card, Unit>((item) =>
            {
                OpenImage(item);
                return Unit.Default;
            });
            DeletePlannedItem = ReactiveCommand.Create<Card, Unit>((item) =>
            {
                ItemsPlanned.Remove(item);
                return Unit.Default;
            });
            DeleteInProgressItem = ReactiveCommand.Create<Card, Unit>((item) =>
            {
                ItemsInProgress.Remove(item);
                return Unit.Default;
            });
            DeleteCompletedItem = ReactiveCommand.Create<Card, Unit>((item) =>
            {
                ItemsCompleted.Remove(item);
                return Unit.Default;
            });
        }
        ObservableCollection<Card> itemsPlanned;
        public ObservableCollection<Card> ItemsPlanned
        {
            get { return itemsPlanned; }
            set { this.RaiseAndSetIfChanged(ref itemsPlanned, value); }
        }
        ObservableCollection<Card> itemsInWork;
        public ObservableCollection<Card> ItemsInProgress
        {
            get { return itemsInWork; }
            set { this.RaiseAndSetIfChanged(ref itemsInWork, value); }
        }
        ObservableCollection<Card> itemsCompleted;
        public ObservableCollection<Card> ItemsCompleted
        {
            get { return itemsCompleted; }
            set { this.RaiseAndSetIfChanged(ref itemsCompleted, value); }
        }
        public ReactiveCommand<Card, Unit> AddImageClick { get; }
        public ReactiveCommand<Card, Unit> DeletePlannedItem { get; }
        public ReactiveCommand<Card, Unit> DeleteInProgressItem { get; }
        public ReactiveCommand<Card, Unit> DeleteCompletedItem { get; }
        private async void OpenImage(Card item)
        {
            string? path;
            string[]? pathArray = null;
            var taskPath = new OpenFileDialog()
            {
                Title = "Open Image",
                Filters = new List<FileDialogFilter>()
            };
            taskPath.Filters.Add
                (new FileDialogFilter() { Name = "image", Extensions = { "png", "ico", "jpg", "jpeg", "jpe" } });

            pathArray = await taskPath.ShowAsync(view);
            path = pathArray is null ? null : string.Join(@"\", pathArray);
            if (path != null)
            {
                item.Image = new Bitmap(path);
            }
        }
        public Window? view = null;
    }
}
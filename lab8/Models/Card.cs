using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System.IO;
using System.Reflection;

namespace lab8.Models
{
    public class Card : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            else return;
        }
        string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged(nameof(Name));
            }
        }
        string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                NotifyPropertyChanged(nameof(Description));
            }
        }
        Bitmap? image;
        public Bitmap? Image
        {
            get => image;
            set
            {
                image = value;
                NotifyPropertyChanged(nameof(Image));
            }
        }
        string? imagePath;
        public string? ImagePath
        {
            get => imagePath;
            set
            {
                try
                {
                    var bitmap = new Bitmap(value);
                }
                catch (Exception ex)
                {
                    return;
                }
                imagePath = value;
                Image = new Bitmap(value);
                NotifyPropertyChanged(nameof(ImagePath));
            }
        }
        public Card(string name = "", string description = "", string? pathImage = null)
        {
            Name = name;
            Description = description;
            if (pathImage is not null)
            {
                ImagePath = pathImage;
            }
        }
    }
}

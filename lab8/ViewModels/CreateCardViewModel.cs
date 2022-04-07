using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using System.ComponentModel;
using lab8.Models;

namespace lab8.ViewModels
{
    public class AddPlanViewModel : ViewModelBase
    {
        public AddPlanViewModel()
        {
            PlanToReturn = new Card();
            PlanToReturn.PropertyChanged += resetEnable;
        }
        Card planToReturn;
        public Card PlanToReturn
        {
            get { return planToReturn; }
            set { this.RaiseAndSetIfChanged(ref planToReturn, value); }
        }
        bool buttonEnable;
        public bool ButtonEnable
        {
            get { return buttonEnable; }
            set { this.RaiseAndSetIfChanged(ref buttonEnable, value); }
        }
        public void resetEnable(object? sender, PropertyChangedEventArgs e)
        {
            ButtonEnable = !string.IsNullOrEmpty(PlanToReturn.Name);
        }
    }
}

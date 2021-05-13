﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace To_Do
{

    /* INotifyPropertyChanged lets view know anytime
       a property is changed */
    public class MainPageViewModel : INotifyPropertyChanged
    {

        public MainPageViewModel()
        {
            AllToDos = new ObservableCollection<string>(); // current to-dos
            AllDone = new ObservableCollection<string>(); // to-dos marked done
        
            SaveCommand = new Command(() =>
            {
                if (TheToDo.Length == 0 || TheToDo == null)
                {
                    App.Current.MainPage.DisplayAlert("To-Do is empty! 😭", "You're lying to yourself if you have nothing to do", null, "Ok");
                }
                else
                {
                    AllToDos.Add(TheToDo); // call to TheToDo returns theToDo string
                    TheToDo = string.Empty; // clear in editor
                }
            }
            );

            MarkDoneCommand = new Command<object>(DeleteThis);
        }

        string theToDo;

        public string TheToDo
        {
            get => theToDo; // returns the to-do note
            set
            {
                theToDo = value;

                // lets listening view know that the to-do property changed
                // PropertyChangedEventArgs contains the event data
                var args = new PropertyChangedEventArgs(nameof(TheToDo));

                // sender is this view model,
                // event data sent is args
                PropertyChanged?.Invoke(this, args);
            }
        }

        /* handles checking off a to-do task */
        private void DeleteThis(object obj)
        {
            var task = obj as string;
            AllToDos.Remove(task);
            AllDone.Add(task);
        }
        
        // represents the method that will handle the PropertyChanging event
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ObservableCollection<string> AllToDos { get; set; }

        public ObservableCollection<string> AllDone { get; set; }

        // public string SelectedToDo { get; set; }

        /* button commands */
        public Command SaveCommand { get; }
        public Command<object> MarkDoneCommand { get; }
    }
}

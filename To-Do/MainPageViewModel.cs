using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

/* thanks to: https://www.youtube.com/watch?v=mBlzs5owIEY
 * 
 */
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
                // to catch if user tries to save empty to-do task
                if (TheToDo.Length == 0 || TheToDo == null)
                {
                    App.Current.MainPage.DisplayAlert("To-Do is empty! 😭", "You're lying to yourself if you have nothing to do", null, "Ok");
                }
                else
                {
                    AllToDos.Add(TheToDo);
                    numTotalTasks++;
                    TheToDo = string.Empty;
                    _testProgress = (float)AllDone.Count / numTotalTasks;
                    TestProgress = _testProgress;
                }
            }
            );

            MarkDoneCommand = new Command<object>(DeleteTask);

            UpdateCommand = new Command(UpdateTask);

            XamRightTestCommand = new Command(() =>
            {
                Debug.WriteLine("test");
            });
        }

        /* allows us to change color of PlaceholderNote by binding
           (high contrast theme) */
        public string PlaceholderNote
        { 
            get => "What do you need to do? ✍️";

            set {
                var args = new PropertyChangedEventArgs(nameof(PlaceholderNote));
                
                PropertyChanged?.Invoke(this, args);
            } 
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

        /* allows us to edit a task selected in the list
         * actual behavior is to remove currently selected task and
         * add a new one in the same place 
         */
        public void UpdateTask()
        {
            if (AllToDos.Count==0)
            {
                App.Current.MainPage.DisplayAlert("No tasks on list", "Please add a task first", null, "OK");
            }          
            if (SelectedToDo == null)
            {
                App.Current.MainPage.DisplayAlert("No task selected", "Please tap on a task in the list to update.", null, "Ok");
            }
            else {
                if (TheToDo.Equals(string.Empty))
                {
                    App.Current.MainPage.DisplayAlert("To-Do is empty! 😭", "You're lying to yourself if you have nothing to do", null, "Ok");
                    SelectedToDo = null;
                }
                else
                {
                    AllToDos[AllToDos.IndexOf(SelectedToDo)] = TheToDo;
                    SelectedToDo = TheToDo;
                    TheToDo = string.Empty; // clear the editor
                }


                //int originalIndex = AllToDos.IndexOf(SelectedToDo);

                //AllToDos.Remove(SelectedToDo);

                //AllToDos.Add(TheToDo);

                //int addedWrongIndex = AllToDos.IndexOf(TheToDo);

                //AllToDos.Move(addedWrongIndex, originalIndex);
            }
        }

        /* handles checking off a to-do task */
        private void DeleteTask(object obj)
        {
            var task = obj as string;
            AllToDos.Remove(task);
            AllDone.Add(task);
            _testProgress = (float)AllDone.Count / numTotalTasks;
            TestProgress = _testProgress;
        }

        string noteIcon;

        /* binding the image source of the note icon */
        public string NoteIcon
        { 
            get => noteIcon;

            set
            {
                noteIcon = value;

                var args = new PropertyChangedEventArgs(nameof(NoteIcon));

                PropertyChanged?.Invoke(this, args);
            }
        }

        string checkboxIcon; 

        /* binding the image source of the the checkbox icon */
        public string CheckboxIcon
        {
            get => checkboxIcon;

            set
            {
                checkboxIcon = value;

                var args = new PropertyChangedEventArgs(nameof(CheckboxIcon));

                PropertyChanged?.Invoke(this, args);
            }
        }

        // represents the method that will handle the PropertyChanging event
        public event PropertyChangedEventHandler PropertyChanged;
        
        public ObservableCollection<string> AllToDos { get; set; }

        public ObservableCollection<string> AllDone { get; set; }
        public string SelectedToDo { get; set; }

        /* button commands */
        public Command SaveCommand { get; }
        public Command<object> MarkDoneCommand { get; }

        public Command XamRightTestCommand { get; }
        public Command UpdateCommand { get; }

        int numTotalTasks;

        private float _testProgress;
        public float TestProgress
        {
            get => _testProgress;
            set
            {
                if (_testProgress != value) return;
                
                    _testProgress = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TestProgress)));
                
            }
        }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace OathKeeper
{
    class ViewModel : INotifyPropertyChanged
    {

        //
        public ViewModel(StackPanel in_stack_panel,ScrollViewer in_scroll)
        {
            var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is MainWindow);
            notes_manager = new NotesManager(in_stack_panel);



            //苦肉の策
            main_stack = in_stack_panel;
            scroll = in_scroll;



            command_add_note = new AddNoteCommand(this);
        }

        #region Guilty
        MySoundGameRuler ruler;
        StackPanel main_stack;
        ScrollViewer scroll;
        #endregion




        public event PropertyChangedEventHandler PropertyChanged;

        private float slider_value=1;
        private NotesManager notes_manager;


        public float SliderValue
        {
            get { return slider_value; }
            set
            {
                slider_value = value;

                Console.WriteLine(value);
                ruler.UpdateRendering();
                NotifyPropertyChanged("SliderValue");
            }
        }
        private void NotifyPropertyChanged(String info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public ICommand command_add_note { get; set; }

        class AddNoteCommand : ICommand
        {
            public AddNoteCommand(ViewModel in_vm)
            {
                vm = in_vm;
            }

            public event EventHandler CanExecuteChanged;
            ViewModel vm;


            public bool CanExecute(object parameter)
            {
                return true;
            }

            public void Execute(object parameter)
            {
                var element = (System.Windows.IInputElement)parameter;
                var position = Mouse.GetPosition(element);
                //vm.scroll.ScrollableHeight - vm.scroll.VerticalOffset+ vm.scroll.ActualHeight- (float)position.Y)
                vm.notes_manager.AddExtraNote((int)(Mouse.GetPosition(element).X / 72.1f), (float)(position.Y +vm.scroll.VerticalOffset));
            }
        }

    }
}

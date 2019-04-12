using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OathKeeper
{


    class NotesManager
    {
        public NotesManager(StackPanel in_stack_panel)
        {
            notes_list = new List<NoteBase>();
            stack_panel = in_stack_panel;
        }
        public const int LINES = 5;
        private List<NoteBase> notes_list;
        StackPanel stack_panel;

        public void AddSimpleNote(int in_line, float in_time)
        {
            if (in_line >= 0 && in_line < LINES)
            {
                notes_list.Add(new SimpleNote(in_time, in_line));

            }
        }
        public void AddExtraNote(int in_line, float in_time)
        {
            if (in_line >= 0 && in_line < LINES)
            {
                notes_list.Add(new ExtraNote(in_time, in_line));
                var note = new Button();
                note.Width = 73;
                note.Height = 25;
                note.Margin = new System.Windows.Thickness(0, in_time, 0, 0);
                Grid.SetColumn(note, in_line);
                // stack_panel.Children.Add(note);
                var grid = stack_panel.Children.OfType<Grid>().First();
                var canvas = grid.Children.OfType<Canvas>();
                var target_canvas = canvas.ElementAt(in_line);
                if (target_canvas != null)
                {
                    target_canvas.Children.Add(note);
                }

            }
        }

    }
}

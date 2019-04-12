using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OathKeeper
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel(ruler,main_stack,main_edit_scroll);
            main_edit_scroll.ScrollToEnd();
            scroll_max_offset = (float)main_edit_scroll.VerticalOffset;


        }
        float scroll_max_offset = 0;
     

        private void main_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if(test_text!=null)
                test_text.Text=e.NewValue.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void main_edit_scroll_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Console.WriteLine((int)(Mouse.GetPosition((sender as ScrollViewer)).X/72.1f));
            //Console.WriteLine(scroll_max_offset - main_edit_scroll.VerticalOffset);
            Console.WriteLine(main_edit_scroll.ScrollableHeight - main_edit_scroll.VerticalOffset+ ((sender as ScrollViewer).ActualHeight- Mouse.GetPosition((sender as ScrollViewer)).Y));
            
        }


    }
}

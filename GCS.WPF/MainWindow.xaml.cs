using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GCS.WPF
{
    public partial class MainWindow : Window
    {
        private State _currentState;
        public MainWindow()
        {
            InitializeComponent();
            _currentState = State.NOTDRAWING;
        }

        private void Shape_Toggle(object sender, RoutedEventArgs e)
        {
            var btn = sender as ToggleButton;
            if (btn.IsChecked ?? false)
            {
                circleBtn.IsChecked = lineBtn.IsChecked = dotBtn.IsChecked = false;
                btn.IsChecked = true;
                switch (btn.Content.ToString())
                {
                    case "circle":
                        _currentState = State.DRAWING_CIRCLE; break;
                    case "line":
                        _currentState = State.DRAWING_LINE; break;
                    case "dot":
                        _currentState = State.DRAWING_DOT; break;
                }
            }
            else _currentState = State.NOTDRAWING;
        }
    }
}

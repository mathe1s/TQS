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
using System.Windows.Shapes;

namespace TQS.View
{
    /// <summary>
    /// Lógica interna para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            _ = LongTimeFunctionAsync();
        }

        public async Task LongTimeFunctionAsync()
        {
            await Task.Delay(1000);
            Title = "W1";
            await Task.Delay(1000);
            Title = "W2";
            await Task.Delay(1000);
            Title = "W3";
            await Task.Delay(1000);
            Title = "W4";
            await Task.Delay(1000);
            Title = "END";
        }

    }
}

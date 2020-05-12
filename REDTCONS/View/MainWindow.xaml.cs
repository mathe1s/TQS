using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
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
using TQS.Consolos;
using TQS.Drawing2D;
using TQS.Drawing2D.Creation;
using TQS.ViewModel;

namespace REDTCONS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DrawingComponent drawingComponent;
        RINTCONS_VM VM;

        public MainWindow()
        {
            InitializeComponent();
            drawingComponent = DrawableComponentCreator.CreateDrawableComponent(DrawableComponentTypes.TQSJan);
            WFHost.Child = drawingComponent;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            VM = new RINTCONS_VM(drawingComponent);
            this.DataContext = VM;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            VM.MessageBoxSaveWarning();
        }

        // 
    }
}

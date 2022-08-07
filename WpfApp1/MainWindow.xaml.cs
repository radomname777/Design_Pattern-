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

namespace WpfApp1
{

    class Coordinate
    {
        private Thickness _thickness;

        public Coordinate(Thickness thickness)
        {
            _thickness = thickness;
        }

        public Thickness Increment(int num = 600)
        {
            if (_thickness.Left>=num)
            {
                return _thickness;

            }
            _thickness.Left += 25;
            return _thickness;
        }

        public Thickness decrement(int num =0)
        {
            if (_thickness.Left <=num)
            {
                return _thickness;

            }
            _thickness.Left -=25;
            return _thickness;
        }



        public Thickness Setup(CoordinateMemento memento)
        {
            _thickness = memento.thickness;
            return _thickness;
        }

        public CoordinateMemento BackUp()
            => new(_thickness);
    }


    class CoordinateMemento
    {
        public Thickness thickness { get; set; }

        public CoordinateMemento(Thickness thickness)
        {
            this.thickness = thickness;
        }
    }
    class CoordinateCaretaker
    {
        private Stack<CoordinateMemento> _memento;


        public CoordinateCaretaker()
            => _memento = new();

        public CoordinateMemento Undo(Thickness th)
        {
            if (_memento.Count!=0)
                return _memento.Pop();
            return new CoordinateMemento(th);

        }


        public void Save(CoordinateMemento memento)
            => _memento.Push(memento);

    }


    public partial class MainWindow : Window
    {

        private Coordinate coordinate = new Coordinate(new Thickness(0));
        private CoordinateCaretaker caretaker = new();
        public MainWindow()
        {
            InitializeComponent();
            caretaker.Save(coordinate.BackUp());
        }
        


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Name)
                {
                    case "Left_btn" :
                        borders.Margin = coordinate.Increment();
                        break;
                    case "Right_btn":
                        borders.Margin = coordinate.decrement();
                        break;
                    case "Ref":
                        borders.Margin = coordinate.Setup(caretaker.Undo(borders.Margin));
                        break;
                    case "Save_btn":
                        caretaker.Save(coordinate.BackUp());
                        break;
                    default:
                        MessageBox.Show("A");
                        break;
                }
            }

        }

        
    }
}

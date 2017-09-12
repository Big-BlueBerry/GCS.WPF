﻿using System;
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
using GCS.WPF.GShapes;

namespace GCS.WPF
{
    public partial class MainWindow : Window
    {
        private State _currentState;
        private Point _lastDownedPoint;
        private GShape _drawingShape;

        private List<GShape> _shapes;

        public MainWindow()
        {
            InitializeComponent();
            _currentState = State.NOTDRAWING;
            _shapes = new List<GShape>();
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

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _currentState |= State.LEFTMOUSE_DOWN;
            if (_currentState.HasFlag(State.DRAWING))
            {
                _lastDownedPoint = e.GetPosition(shapeCanvas);
                if (_drawingShape == null)
                {
                    if (_currentState.HasFlag(State.CIRCLE))
                        _drawingShape = new GEllipse();
                    else if (_currentState.HasFlag(State.LINE))
                        _drawingShape = new GLine();
                    else if (_currentState.HasFlag(State.DOT))
                        _drawingShape = new GEllipse();
                    _drawingShape.Control.Stroke = Brushes.Blue;
                    shapeCanvas.Children.Add(_drawingShape.Control);
                }
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_currentState.HasFlag(State.DRAWING)) return;
            if (!_currentState.HasFlag(State.LEFTMOUSE_DOWN)) return;

            var curPos = e.GetPosition(shapeCanvas);

            // Preview drawing
            if (_currentState.HasFlag(State.CIRCLE))
            {
                (_drawingShape.Control as Ellipse).SetCircle(_lastDownedPoint, curPos);
            }
            else if (_currentState.HasFlag(State.LINE))
            {
                (_drawingShape.Control as Line).SetTwoPoint(_lastDownedPoint, curPos);
            }
            else if (_currentState.HasFlag(State.DOT))
            {
                (_drawingShape.Control as Ellipse).SetDot(curPos);
            }
        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _currentState &= ~State.LEFTMOUSE_DOWN;
            _shapes.Add(_drawingShape);
            _drawingShape = null; // 요거 없으면 새로 생성안함
        }
    }
}

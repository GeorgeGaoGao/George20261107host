using Company.Application.Share.Process;
using Company.Core.Model;
using Company.Hardware.Detector;
using Prism.Commands;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Company.Application.Image.ViewModels
{
    public class ImageViewModel : ReactiveObject
    {
        public Grid viewport { get; private set; }
        [Reactive]
        public Point MousePoint { get; private set; } = new Point(-1, -1);
        [Reactive]
        public Visibility LineVisibility { get; set; } = Visibility.Visible;
        public Gray16ImageSource Gray16ImageSource { get; set; } = new Gray16ImageSource(1500, 1500);
        public ICommand MouseMoveCommand { get; }
        public ICommand ViewportLoadedCommand { get; private set; }
        public ImageViewModel(IDetectorProcessModel detectorProcessModel, IDetector detector)
        {
            //MouseMoveCommand = ReactiveCommand.Create<MouseEventArgs>(MouseMove);
            MouseMoveCommand = new DelegateCommand<MouseEventArgs>(MouseMove);

            //ViewportLoadedCommand = ReactiveCommand.Create<RoutedEventArgs>(ViewportLoaded);
            ViewportLoadedCommand = new DelegateCommand<RoutedEventArgs>(ViewportLoaded);
            detectorProcessModel.SourceObservable.Subscribe(source =>
            {
                System.Windows.Application.Current?.Dispatcher.Invoke(
                    () =>
                    {
                        Gray16ImageSource.Write(source);
                    }
                    );
            });
        }

        private void ViewportLoaded(RoutedEventArgs e) 
        {
            viewport=(Grid)e.Source;
            ViewportLoadedCommand = null;//viewport的loaded事件只执行一次。
        }

        private void MouseMove(MouseEventArgs e)
        {
            Point point = e.GetPosition(viewport);
            MousePoint = point;//更新鼠标位置。
        }
    }
}

using Company.Application.Share.Main;
using Company.Application.Share.Process;
using Company.Core.Enums;
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
using System.Windows.Media;

namespace Company.Application.Image.ViewModels
{
    public class ImageViewModel : ReactiveObject
    {
        [Reactive]
        public TransformGroup TransformGroup { get; private set; }=new TransformGroup();
        private ScaleTransform _scaleTransform = new ScaleTransform();
        private TranslateTransform _translateTransform = new TranslateTransform();

        private MouseWorkMode _mouseWorkMode = MouseWorkMode.默认操作;





        public Grid Viewport { get; private set; }
        public Grid ImageBox {  get; private set; }
        [Reactive]
        public int  ViewportWidth { get;private set; }
        [Reactive]
        public int ViewportHeight { get; private set; }
        [Reactive]
        public int  ImageBoxWidth { get;private set; }
        [Reactive]
        public int ImageBoxHeight { get; private set; }

        [Reactive]
        public Point MousePoint { get; private set; } = new Point(-1, -1);
        [Reactive]
        public Visibility LineVisibility { get; set; } = Visibility.Visible;
        public Gray16ImageSource Gray16ImageSource { get; set; } = new Gray16ImageSource(1500, 1500);
        public ICommand MouseMoveCommand { get; }
        public ICommand ViewportLoadedCommand { get; private set; }
        public ICommand ViewportSizeChangedCommand { get; private set; }

        public ICommand MouseLeaveCommand { get; private set; }
        public ICommand MouseLeftButtonDownCommand { get; }
        public ICommand MouseLeftButtonUpCommand { get; }
        public ICommand MouseRightButtonDownCommand { get; }
        public ICommand MouseRightButtonUpCommand { get; }
        public ICommand MouseWheelCommand { get; }
        public ImageViewModel(IDetectorProcessModel detectorProcessModel, IDetector detector,IMouseWorkModeProvider mouseWorkModeProvider)
        {
            mouseWorkModeProvider.MouseWorkObservable.Subscribe(mouseWorkMode => {

                _mouseWorkMode= mouseWorkMode;
            });



            //MouseMoveCommand = ReactiveCommand.Create<MouseEventArgs>(MouseMove);
            MouseMoveCommand = new DelegateCommand<MouseEventArgs>(MouseMove);

            //ViewportLoadedCommand = ReactiveCommand.Create<RoutedEventArgs>(ViewportLoaded);
            ViewportLoadedCommand = new DelegateCommand<RoutedEventArgs>(ViewportLoaded);
            MouseLeaveCommand=new DelegateCommand<MouseEventArgs>(e =>
            {
                MousePoint = new Point(-1, -1);
            });

            ViewportSizeChangedCommand = new DelegateCommand<SizeChangedEventArgs>(ViewportSizeChanged);

            MouseLeftButtonDownCommand = new DelegateCommand<MouseButtonEventArgs>(MouseLeftButtonDown);

            MouseLeftButtonUpCommand = new DelegateCommand<MouseButtonEventArgs>(MouseLeftButtonUp);

            MouseRightButtonDownCommand = new DelegateCommand<MouseButtonEventArgs>(MouseRightButtonDown);
            
            MouseRightButtonUpCommand = new DelegateCommand<MouseButtonEventArgs>(MouseRightButtonUp);

            MouseWheelCommand = new DelegateCommand<MouseWheelEventArgs>(MouseWheel);



            TransformGroup.Children.Add(_scaleTransform);
            TransformGroup.Children.Add(_translateTransform);



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

        private void ViewportSizeChanged(SizeChangedEventArgs args)
        {
            ViewportWidth = (int)args.NewSize.Width;
            ViewportHeight = (int)args.NewSize.Height;
            SetTransform();
        }

        private void MouseLeftButtonDown(MouseButtonEventArgs args)
        {

        }

        private void MouseLeftButtonUp(MouseButtonEventArgs args)
        {

        }

        private void MouseRightButtonDown(MouseButtonEventArgs args)
        {

        }

        private void MouseRightButtonUp(MouseButtonEventArgs args)
        {

        }

        private void MouseWheel(MouseWheelEventArgs args)
        {

        }




        private void ViewportLoaded(RoutedEventArgs e) 
        {
            Viewport=(Grid)e.Source;
            ImageBox = Viewport.Tag as Grid;
            ViewportWidth = (int)Viewport.ActualWidth;
            ViewportHeight = (int)Viewport.ActualHeight;
            ImageBoxWidth = (int)ImageBox.ActualWidth;
            ImageBoxHeight = (int)ImageBox.ActualHeight;

            SetTransform();
            ViewportLoadedCommand = null;//viewport的loaded事件只执行一次。
        }
        /// <summary>
        /// 根据viewport控件尺寸，设置图像缩放平移。
        /// </summary>
        private void SetTransform()
        {
           var scale=Math.Min(1.0*ViewportWidth/ ImageBoxWidth, 1.0 * ViewportHeight / ImageBoxHeight);
            _scaleTransform.ScaleX = scale;
            _scaleTransform.ScaleY = scale;
            _translateTransform.X = (ViewportWidth - ImageBoxWidth * scale) / 2;
            _translateTransform.Y = (ViewportHeight - ImageBoxHeight * scale) / 2;
        }

        private void MouseMove(MouseEventArgs e)
        {
            Point point = e.GetPosition(Viewport);
            MousePoint = point;//更新鼠标位置。
        }
    }
}

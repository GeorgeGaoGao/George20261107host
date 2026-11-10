using Company.Application.Share.Main;
using Company.Application.Share.Process;
using Company.Core.Enums;
using Company.Core.Model;
using Company.Hardware.Detector;
using Prism.Commands;
using Prism.Unity;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Concurrency;
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
        public TransformGroup TransformGroup { get; private set; } = new TransformGroup();
        private ScaleTransform _scaleTransform = new ScaleTransform();
        private TranslateTransform _translateTransform = new TranslateTransform();

        private MouseWorkMode _mouseWorkMode = MouseWorkMode.默认操作;

        private IZoomModeProvider _zoomModeProvider;


        public Grid Viewport { get; private set; }
        public Grid ImageBox { get; private set; }
        [Reactive]
        public int ViewportWidth { get; private set; }
        [Reactive]
        public int ViewportHeight { get; private set; }
        [Reactive]
        public int ImageBoxWidth { get; private set; }
        [Reactive]
        public int ImageBoxHeight { get; private set; }
        /// <summary>
        /// 十字架所引用的鼠标位置。
        /// </summary>
        [Reactive]
        public Point MousePoint { get; private set; } = new Point(-1, -1);
        /// <summary>
        /// 鼠标按下时的鼠标位置。
        /// </summary>
        [Reactive]
        public Point MouseDownPoint { get; private set; } = new Point(-1, -1);
        [Reactive]
        public Visibility LineVisibility { get; set; } = Visibility.Visible;
        public Gray16ImageSource Gray16ImageSource { get; set; } = new Gray16ImageSource(1500, 1500);
        public ICommand MouseMoveCommand { get; }
        //public ReactiveCommand<MouseEventArgs, Unit> MouseMoveCommand { get; }
        public ICommand ViewportLoadedCommand { get; private set; }
        public ICommand ViewportSizeChangedCommand { get; private set; }

        public ICommand MouseLeaveCommand { get; private set; }
        public ICommand MouseLeftButtonDownCommand { get; }
        public ICommand MouseLeftButtonUpCommand { get; }
        public ICommand MouseRightButtonDownCommand { get; }
        public ICommand MouseRightButtonUpCommand { get; }
        public ICommand MouseWheelCommand { get; }
        public ImageViewModel(IDetectorProcessModel detectorProcessModel, IDetector detector, IMouseWorkModeProvider mouseWorkModeProvider, IZoomModeProvider zoomModeProvider)
        {
            mouseWorkModeProvider.MouseWorkObservable.Subscribe(mouseWorkMode =>
            {
                _mouseWorkMode = mouseWorkMode;
            });

            //_zoomModeProvider = zoomModeProvider;
            //_zoomModeProvider.ZoomModeObservable.Subscribe(ZoomModeChanged);

            zoomModeProvider.ZoomModeObservable.Subscribe(mode =>
            {
                double scale = GetScaleByZoomMode(mode);

                _scaleTransform.ScaleX = scale;
                _scaleTransform.ScaleY = scale;

                _translateTransform.X = (ViewportWidth - ImageBoxWidth * scale) / 2;
                _translateTransform.Y = (ViewportHeight - ImageBoxHeight * scale) / 2;
            });



            //MouseMoveCommand = ReactiveCommand.Create<MouseEventArgs>(MouseMove,null, new DispatcherScheduler(PrismApplication.Current.Dispatcher));
            MouseMoveCommand = new DelegateCommand<MouseEventArgs>(MouseMove);

            //ViewportLoadedCommand = ReactiveCommand.Create<RoutedEventArgs>(ViewportLoaded);
            ViewportLoadedCommand = new DelegateCommand<RoutedEventArgs>(ViewportLoaded);
            MouseLeaveCommand = new DelegateCommand<MouseEventArgs>(e =>
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

        private void ZoomModeChanged(ZoomMode mode)
        {
            if (_mouseWorkMode == MouseWorkMode.图片查看)
            {
                //var zoomMode = _zoomModeProvider.ZoomMode;
                double scale = GetScaleByZoomMode(mode);
                Point point = MouseDownPoint;

                _scaleTransform.ScaleX = scale;
                _scaleTransform.ScaleY = scale; 

                _translateTransform.X = (ViewportWidth - ImageBoxWidth * scale) / 2;
                _translateTransform.Y = (ViewportHeight - ImageBoxHeight * scale) / 2;
            }
        }

        private double GetScaleByZoomMode(ZoomMode zoomMode)
        {
            double scale = 0;
            switch (zoomMode)
            {
                case ZoomMode.Uniform:
                    scale = Math.Min(1.0 * ViewportWidth / ImageBoxWidth, 1.0 * ViewportHeight / ImageBoxHeight);
                    break;
               
               
                default:
                    scale = (double)zoomMode;
                    break;
            }
            return scale;
        }

        private void ViewportSizeChanged(SizeChangedEventArgs args)
        {
            ViewportWidth = (int)args.NewSize.Width;
            ViewportHeight = (int)args.NewSize.Height;
            SetTransform();
        }
        #region 鼠标操作
        private bool _mousePressed;
        private void MouseLeftButtonDown(MouseButtonEventArgs args)
        {

        }

        private void MouseLeftButtonUp(MouseButtonEventArgs args)
        {

        }

        private void MouseRightButtonDown(MouseButtonEventArgs args)
        {
            _mousePressed = true;
            MouseDownPoint = args.GetPosition(Viewport);
            Viewport.CaptureMouse();//立刻触发鼠标移动事件。
        }

        private void MouseRightButtonUp(MouseButtonEventArgs args)
        {
            _mousePressed = false;
            MouseDownPoint = args.GetPosition(Viewport);
            Viewport.ReleaseMouseCapture();//立刻触发释放鼠标移动事件。
        }

        private void MouseMove(MouseEventArgs e)
        {
            Point point = e.GetPosition(Viewport);
            MousePoint = point;//更新鼠标位置。

            if (_mousePressed)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {

                }
                else if (e.RightButton == MouseButtonState.Pressed)
                {
                    if (this._mouseWorkMode == MouseWorkMode.图片查看)
                    {
                        var offset = MousePoint - MouseDownPoint;
                        _translateTransform.X += offset.X;
                        _translateTransform.Y += offset.Y;
                    }

                }
                MouseDownPoint = point;//每次停顿时将当前鼠标位置作为下次移动的起点。这样可以避免移动距离不断累积。


            }
        }

        private void MouseWheel(MouseWheelEventArgs args)
        {
            if (this._mouseWorkMode==MouseWorkMode.图片查看)
            {
                double scale = args.Delta * 0.0005;
                if (_scaleTransform.ScaleX+scale<0.2)
                {
                    return;
                }
                Point point = args.GetPosition(Viewport);
               Point inverse=TransformGroup.Inverse.Transform(point);

                _scaleTransform.ScaleX += scale;
                _scaleTransform.ScaleY += scale;

                _translateTransform.X=-1*(inverse.X * _scaleTransform.ScaleX - point.X);
                _translateTransform.Y=-1*(inverse.Y * _scaleTransform.ScaleY - point.Y);

                //Point newPoint = TransformGroup.Transform(inverse);
                //_translateTransform.X += point.X - newPoint.X;
                //_translateTransform.Y += point.Y - newPoint.Y;
            }
        }

        #endregion


        private void ViewportLoaded(RoutedEventArgs e)
        {
            Viewport = (Grid)e.Source;
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
            var scale = Math.Min(1.0 * ViewportWidth / ImageBoxWidth, 1.0 * ViewportHeight / ImageBoxHeight);
            _scaleTransform.ScaleX = scale;
            _scaleTransform.ScaleY = scale;
            _translateTransform.X = (ViewportWidth - ImageBoxWidth * scale) / 2;
            _translateTransform.Y = (ViewportHeight - ImageBoxHeight * scale) / 2;
        }


    }
}

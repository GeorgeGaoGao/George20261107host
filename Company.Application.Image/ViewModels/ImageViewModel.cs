using Company.Application.Share.Process;
using Company.Core.Model;
using Company.Hardware.Detector;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Image.ViewModels
{
    public class ImageViewModel : ReactiveObject
    {
        public Gray16ImageSource Gray16ImageSource { get; set; } = new Gray16ImageSource(1500, 1500);
        public ImageViewModel(IDetectorProcessModel detectorProcessModel, IDetector detector)
        {
            detectorProcessModel.SourceObservable.Subscribe(source =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(
                    () =>
                    {
                        Gray16ImageSource.Write(source);
                    }
                    );


            });
            detector.Grab();
        }
    }
}

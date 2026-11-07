using Company.Application.Process.Models;
using Company.Core.Ioc;
using Company.Hardware.Camera;
using Company.Hardware.Detector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Process.Services
{
    /// <summary>
    /// 平板探测器接收服务，长驻内存
    /// </summary>
    [ExposedService(Lifetime.Singleton, AutoInitialize = true)]
    public class DetectorReceiveManager
    {
        private DetectorProcessModel _detectorProcessModel;
        private IDetector _detector;
        public DetectorReceiveManager(DetectorProcessModel detectorProcessModel,IDetector detector)
        {
            _detectorProcessModel=detectorProcessModel;
            _detector=detector;

            _detector.OnGrabbed += _detector_OnGrabbed;
        }

        private void _detector_OnGrabbed(DetectorImage image)
        {
            _detectorProcessModel.Write(image);//真正接收16位原图并写入双缓冲内存
        }
    }
}

using Company.Application.Process.Models;
using Company.Core.Ioc;
using Company.Hardware.Camera;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Application.Process.Services
{
    /// <summary>
    /// 图像接收服务，长驻内存
    /// </summary>
    [ExposedService(Lifetime.Singleton,AutoInitialize =true)]
    public class CameraReceiveManager
    {
        private CameraProcessModel _cameraProcessModel;
        private ICamera _camera;
        
        public CameraReceiveManager(CameraProcessModel cameraProcessModel,ICamera camera)
        {
            _cameraProcessModel = cameraProcessModel;
            _camera=camera;

            _camera.OnGrabbed += _camera_OnGrabbed;
        }

        private void _camera_OnGrabbed(Photo obj)
        {
            _cameraProcessModel.Write(obj);
        }
    }
}

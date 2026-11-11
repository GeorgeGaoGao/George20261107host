using Company.Application.Main.Models;
using Company.Application.Share.Process;
using Company.Core.Enums;
using Company.Hardware.Camera;
using Company.Hardware.Detector;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Company.Application.Main.ViewModels
{
    public class MainViewModel :BindableBase
    {
        public MainModel  MainModel { get;  }
        public IDetector Detector { get; }
        public ICamera Camera { get; }
        public ICommand GrabPhotoCommand { get;  }
        public ICommand MouseWorkModeCommand {  get; }
        public MainViewModel(IDetector detector,ICamera camera,MainModel mainModel)
        {
            GrabPhotoCommand = new DelegateCommand(GrabPhoto);
            Detector=detector;
            Camera=camera;
            MainModel= mainModel;
            MouseWorkModeCommand = new DelegateCommand<CheckBox>(GetMouseWorkMode);
            GrabPhoto();
        }

        private void GetMouseWorkMode(CheckBox checkBox)
        {
            if (checkBox==null)
            {
                return;
            }
            MainModel.MouseWorkMode = checkBox.IsChecked.Value?MouseWorkMode.图片查看 : MouseWorkMode.默认操作; 
        }

        private void GrabPhoto()
        {
            Detector.Grab();
            Camera.Trigger();
        }

    }
}

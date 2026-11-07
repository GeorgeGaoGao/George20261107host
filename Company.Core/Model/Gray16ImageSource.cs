using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Company.Core.Model
{
    /// <summary>
    /// 16位灰度位图，用于WPF显示
    /// </summary>
    public class Gray16ImageSource
    {
        private readonly int _width;
        private readonly int _height;
        private readonly WriteableBitmap _bitmap;
        /// <summary>
        /// 将_bitmap对象做成属性便于WPF绑定。
        /// </summary>
        public ImageSource ImageSource => _bitmap;
        public Gray16ImageSource(int width, int height)
        {
            _width = width;
            _height = height;
            _bitmap = new WriteableBitmap(width, height, 96d, 96d, PixelFormats.Gray16, null);
        }
        /// <summary>
        /// 写入图像到WriteableBitmap,前端会刷新
        /// </summary>
        /// <param name="data"></param>
        /// <param name="isCenter"></param>
        public void Write(UnmanagedArray2D<ushort> data, bool isCenter = true)
        {
            if (data == null)
            {
                return;
            }
            if (isCenter)
            {
                WriteToCenter(data);
            }
            else
            {
                WriteToWhole(data);
            }
        }

        private void WriteToWhole(UnmanagedArray2D<ushort> data)
        {
            if (data.Width != _width || data.Height != _height)
            {
                throw new Exception("图像尺寸不一致。");
            }
            Int32Rect rect = new Int32Rect(0, 0, data.Width, data.Height);
            _bitmap.WritePixels(rect, data.Header, (int)data.Length, data.Width * sizeof(ushort));
        }

        private void WriteToCenter(UnmanagedArray2D<ushort> data)
        {
            int x0 = (data.Width - _width) / 2;
            int y0= (data.Height - _height) / 2;

            int x = x0 > 0 ? x0 : 0;
            int y = y0 > 0 ? y0 : 0;
            int w=Math.Min(data.Width, _width);
            int h = Math.Min(data.Height, _height);

            int destX = x0 > 0 ? 0 : -x0;
            int destY= y0 > 0 ? 0 : -y0;

            Int32Rect rect = new Int32Rect(x, y, w, h);
            _bitmap.WritePixels(rect, data.Header, (int)data.Length, data.Width * sizeof(ushort), destX, destY);
        }
    }
}

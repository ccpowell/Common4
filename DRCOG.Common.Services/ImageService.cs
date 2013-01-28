using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using DRCOG.Common.Services.Interfaces;

namespace DRCOG.Common.Services
{
    public class ImageService : IImageService
    {
        public readonly IFileRepository FileRepository;

        public ImageService(IFileRepository _fileRepository)
        {
            FileRepository = _fileRepository;
        }

        public virtual Guid Save(Image image)
        {
            image.Data = ProportionallyResize(Parse<Bitmap>(image.Data), image.MaxWidth, image.MaxHeight);
            return FileRepository.Save(image);
        }

        public virtual void Delete(Guid id)
        {
            FileRepository.Delete(id);
        }

        public T Parse<T>(object value)
        {
            try
            {
                return
                    (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFrom(value);
            }
            catch (Exception) { return default(T); }
        }

        public T ConvertTo<T>(object value)
        {
            try
            {
                return
                    (T)TypeDescriptor.GetConverter(value.GetType()).ConvertTo(value, typeof(T));
            }
            catch (Exception) { return default(T); }
        }

        public Byte[] ProportionallyResize(Bitmap src, int maxWidth, int maxHeight)
        {
            //// original dimensions
            //int w = src.Width;
            //int h = src.Height;

            //// Longest and shortest dimension
            //int longestDimension = (w > h) ? w : h;
            //int shortestDimension = (w < h) ? w : h;

            //// propotionality
            //float factor = ((float)longestDimension) / shortestDimension;

            //// default width is greater than height
            //double newWidth = maxWidth;
            //double newHeight = maxWidth / factor;

            //// if height greater than width recalculate
            //if (w < h)
            //{
            //    newWidth = maxHeight / factor;
            //    newHeight = maxHeight;
            //}


            var ratioX = (double)maxWidth / src.Width;
            var ratioY = (double)maxHeight / src.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(src.Width * ratio);
            var newHeight = (int)(src.Height * ratio);


            // Create new Bitmap at new dimensions
            Bitmap result = new Bitmap((int)newWidth, (int)newHeight);
            using (Graphics g = Graphics.FromImage((System.Drawing.Image)result))
                g.DrawImage(src, 0, 0, (int)newWidth, (int)newHeight);

            Byte[] test = ConvertTo<Byte[]>(result) ?? null;

            return test;
        }


        public Image GetById(Guid id)
        {
            return FileRepository.GetById(id);
        }
    }
}

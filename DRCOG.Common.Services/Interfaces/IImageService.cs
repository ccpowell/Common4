using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Services.Interfaces
{
    public interface IImageService
    {
        Guid Save(Image logo);
        void Delete(Guid id);
        Image GetById(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Services.Interfaces
{
    public interface IFileRepository
    {
        Guid Save(File file);
        void Delete(Guid id);
        Image GetById(Guid id);
    }
}

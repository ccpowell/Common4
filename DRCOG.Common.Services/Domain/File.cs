using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DRCOG.Common.Services
{
    public abstract class File
    {
        public Int32 Id { get; set; }
        public Guid Guid { get; set; }
        public String Name { get; set; }
        
        public Byte[] Data { get; set; }
        public int MediaTypeId { get; set; }
        public String MediaType { get; set; }
    }
}

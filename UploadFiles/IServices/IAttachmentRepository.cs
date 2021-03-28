using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadFiles.IServices
{
    public interface IAttachmentRepository
    {
        void Add(byte[] content);

        byte[] Get();
    }
}

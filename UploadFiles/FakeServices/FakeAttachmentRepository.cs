
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UploadFiles.IServices;

namespace UploadFiles.FakeServices
{
    public class FakeAttachmentRepository : IAttachmentRepository
    {
        private byte[] content;

        public void Add(byte[] content)
        {
            this.content = content;
        }

        public byte[] Get()
        {
            return content;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSSAttack.Models;

namespace XSSAttack.IServices
{
    public interface IPostRepository
    {
        IEnumerable<Post> Get();
        Post Get(int id);
        void Update(Post post);
    }
}

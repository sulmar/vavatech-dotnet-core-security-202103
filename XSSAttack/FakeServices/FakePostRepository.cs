using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSSAttack.IServices;
using XSSAttack.Models;

namespace XSSAttack.FakeServices
{
    public class FakePostRepository : IPostRepository
    {
        private readonly IEnumerable<Post> posts = new List<Post>();

        public FakePostRepository(Faker<Post> faker)
        {
            posts = faker.Generate(10);
        }

        public IEnumerable<Post> Get() => posts;

        public Post Get(int id) => posts.SingleOrDefault(p => p.Id == id);

        public void Update(Post post)
        {
            Post existingPost = Get(post.Id);

            existingPost.Content = post.Content;
        }
    }
}

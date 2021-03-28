using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XSSAttack.Models;

namespace XSSAttack.Fakers
{
    public class PostFaker : Faker<Post>
    {
        public PostFaker()
        {
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Title, f => f.Lorem.Sentence());
            RuleFor(p => p.Content, f => f.Lorem.Paragraph());
        }
    }
}

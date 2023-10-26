using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class seed
    {
        public static void SeedData(DataContext context)
        { 
            if(!context.Posts.Any())
            {
                var posts = new List <Post>
                {
                    new Post {
                        Title = "First post",
                        Body = "Lorem Ipsum is simply dummy text of the printing and typesetting industry",
                        Date = DateTime.Now.AddDays(-10)
                        },
                    new Post {
                        Title = "First second",
                        Body = "over the years, sometimes by accident, sometimes on purpose (injected humour and the like)",
                        Date = DateTime.Now.AddDays(-7)
                        },
                    new Post {
                        Title = "First Third",
                        Body = "It is a long established fact that a reader will be distracted by the readable",
                        Date = DateTime.Now.AddDays(-4)
                        }

        };
        context.Posts.AddRange(posts);
        context.SaveChanges();
    }
}
}
}
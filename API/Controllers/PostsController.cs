using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Persistence;

namespace API.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]

    public class PostsController : ControllerBase
    {
        private readonly DataContext context;
        public PostsController(DataContext context)
        {
            this.context = context;
        }

        // GET api / posts

        [HttpGet(Name = "GetPosts")]
        public ActionResult<List<Post>> Get()
        {
            
            return this.context.Posts.ToList();
        }
        /// <summary>
        /// GET api/post/[id]
        /// </summary>
        /// <param name="id"></param>
        
         [HttpGet("id",Name = "GetById")]
        public ActionResult<Post>GetById(Guid id)
        {
            var post = this.context.Posts.Find(id);
            if (post is null)
            {
                return NotFound();
            }
            return Ok (post);
           
        }
    
    /// <summary>
    /// Post api/post
    /// </summary>
    /// <param name="request">JSON request containing post field</param>
    /// <returns>A new post</return>
    
     [HttpGet(Name = "Create")]
        public ActionResult<Post>Create([FromBody]Post request)
        {
            var post = new Post
            {
                ID = request.ID,
                Title = request.Title,
                Body = request.Body,
                Date = request.Date
            };

            context.Posts.Add(post);
            var success = context.SaveChanges() > 0;

            if (success)
            {
                return Ok(post);
            }
            throw new Exception ("Error creating post");
        }

         ///summary
        /// PUT api/post
        /// </summary>
        /// <param name="request">JSON request containing one or more updated post fields</param>
        /// <returns> A new post</returns>
        [HttpPost(Name = "Update")]
        public ActionResult<Post> Update([FromBody]Post request)
        {
            var post = context.Posts.Find(request.ID);
            if(post == null)
            {
                throw new Exception("Could not find post"); 
            }


            post.Title = request.Title != null ? request.Title : post.Title;
            post.Body = request.Body != null ? request.Body : post.Body;
            post.Date = request.Date != DateTime.MinValue ? request.Date : post.Date;

            var success = context.SaveChanges() > 0;
                
            if (success)
            {
                return Ok(post);
            }

            throw new Exception("Error updating post");
        }
    }
}
        
    
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApiCache.Constants;
using WebApiCache.Data;

namespace WebApiCache.Controllers
{
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;
        private readonly AppDbContext _dbContext;

        public BlogController(
            IMemoryCache memoryCache,
            AppDbContext dbContext
            )
        {
            _memoryCache = memoryCache;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("GetAllBlog")]
        public IActionResult Get()
        {
            if (!_memoryCache.TryGetValue(CacheKeys.BlogCacheKey, out List<Blogs> _blogs))
            {
                _blogs = _dbContext.Blogs.ToList();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                {
                    AbsoluteExpiration=DateTime.Now.AddMinutes(5),
                    SlidingExpiration=TimeSpan.FromMinutes(2),
                    Size=1024
                };
                _memoryCache.Set<List<Blogs>>(CacheKeys.BlogCacheKey,_blogs,cacheEntryOptions);
            }
            return Ok(_blogs);
        }
    }   
}
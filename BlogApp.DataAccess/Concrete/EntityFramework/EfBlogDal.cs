﻿using BlogApp.DataAccess.Abstract;
using BlogApp.DataAccess.Context;
using BlogApp.Entity.Entities;

namespace BlogApp.DataAccess.Concrete.EntityFramework
{
    public class EfBlogDal : GenericRepository<Blog>, IBlogDal
    {
        private readonly BlogAppContext _context;
        public EfBlogDal(BlogAppContext context) : base(context)
        {
            _context = context;
        }

        public List<Blog> GetListByAppUserId(int appUserId)
        {
            return _context.Set<Blog>()
                           .Where(blog => blog.AppUserId == appUserId)
                           .ToList();
        }
    }
}

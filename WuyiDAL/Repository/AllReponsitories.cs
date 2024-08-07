﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WuyiDAL.IReponsitory;
using WuyiDAL.Models;

namespace WuyiDAL.Repository
{
    public class AllReponsitories<T> : IAllReponsitories<T> where T : class
    {
        private readonly DbSet<T> _dbset;
        private readonly AppDbContext _context;

        public AllReponsitories()
        {
            _context = new AppDbContext();
        }
        public AllReponsitories(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbset = _context.Set<T>();
        }
        public bool CreatObj(T obj)
        {
            try
            {
                _dbset.Add(obj); //thêm
                _context.SaveChanges();//lưu lại
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

        }
        public bool DeleteObj(T obj)
        {
            try
            {
                _dbset.Remove(obj); //xóa
                _context.SaveChanges();//lưu lại
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public ICollection<T> GetAll()
        {
            return _dbset.ToList();
        }

        public T GetById(dynamic id)
        {
            return _dbset.Find(id); //phương thức find mà truyền trực tiếp tham số chỉ dùng với PK
        }

        public bool UpdateObj(T obj)
        {
            try
            {
                _dbset.Update(obj); //update
                _context.SaveChanges();//lưu lại
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public T FindByName(string name)
        {
            return _dbset.FirstOrDefault(x => EF.Property<string>(x, "Title") == name);
        }
        public bool UsernameExists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }
        public bool Login(string UserName, string Password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Username == UserName);
            if (user == null || user.Password != Password)
            {
                return false;
            }
            return true;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WuyiDAL.IReponsitory
{
    public interface IAllReponsitories<T> where T : class
    {
        //read(get) lấy
        public ICollection<T> GetAll();
        public T GetById(dynamic id);// lấy theo ID
        //create - Tạo mới
        public bool CreatObj(T obj);//tạo mới đối tượng trong CSDL truyền vào cả đối tượng
        //update-sửa đối tượng trong DB
        public bool UpdateObj(T obj);
        //delete -xóa đối tượng trong DB
        public bool DeleteObj(T obj);
        public T FindByName(string Name);
        public bool UsernameExists(string username);
        public bool Login(string UserName, string Password);
    }       
}

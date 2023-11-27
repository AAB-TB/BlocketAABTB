using BlocketAAB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlocketAAB.Interface
{
    public interface ICategoryService
    {
       
        List<Category> GetAll();
        int Add(Category category);
        bool Delete(int id);
    }
}

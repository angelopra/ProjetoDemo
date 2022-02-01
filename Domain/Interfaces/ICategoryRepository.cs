using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        int AddCategory(Category request);
        Category Update(Category request);
        void Remove(int id);
        Category GetCategoryById(int id);
        List<Category> GetAllCategories();
        IQueryable<Category> GetCategorys(List<int> requestList = null);
    }
}

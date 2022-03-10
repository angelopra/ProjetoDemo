using Domain.Common;
using Domain.Entities;
using Domain.Model.Request;
using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface ICategoryComponent
    {
        int AddCategory(CategoryRequest request);
        Category Update(CategoryRequest request, int id);
        void Remove(int id);
        Category GetCategoryById(int id);
        PaginatedList<Category> GetAllCategories(int? pageNumber, int? pageSize);
        List<Category> GetCategorys(CategoryQueryRequest request);
    }
}

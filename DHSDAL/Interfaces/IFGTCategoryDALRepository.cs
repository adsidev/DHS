using DHSEntities;

namespace DHSDAL
{
    public interface IFGTCategoryDALRepository
    {
        FGTCategoryResponse GetFGTCategoriesById(FGTCategoryRequest fGTCategoryRequest);
        FGTCategoryResponse GetFGTCategories();
        FGTCategoryResponse GetFGTCategory(FGTCategoryRequest fGTCategoryRequest);
        FGTCategoryResponse SaveFGTCategory(FGTCategoryRequest fGTCategoryRequest);
    }
}

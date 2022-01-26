using DHSEntities;

namespace DHSBAL
{
    public interface IFGTCategoryRepository
    {
        FGTCategoryResponse GetFGTCategoriesById(FGTCategoryRequest fGTCategoryRequest);
        FGTCategoryResponse GetFGTCategories();
        FGTCategoryResponse GetFGTCategory(FGTCategoryRequest fGTCategoryRequest);
        FGTCategoryResponse SaveFGTCategory(FGTCategoryRequest fGTCategoryRequest);
    }
}

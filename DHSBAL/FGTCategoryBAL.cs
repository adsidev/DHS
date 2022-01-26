using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class FGTCategoryBAL : IFGTCategoryRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IFGTCategoryDALRepository fGTCategoryDALRepository;

        FGTCategoryResponse fGTCategoryResponse;
        public FGTCategoryBAL()
        {
            fGTCategoryDALRepository = new FGTCategoryDAL();
            fGTCategoryResponse = new FGTCategoryResponse();
        }

        public FGTCategoryResponse GetFGTCategoriesById(FGTCategoryRequest fGTCategoryRequest)
        {
            try
            {
                fGTCategoryResponse = fGTCategoryDALRepository.GetFGTCategoriesById(fGTCategoryRequest);
            }
            catch (Exception ex)
            {
                fGTCategoryResponse.ErrorMessage = ex.Message;
                fGTCategoryResponse.Exception = ex;
            }
            return fGTCategoryResponse;
        }

        public FGTCategoryResponse GetFGTCategories()
        {
            try
            {
                fGTCategoryResponse = fGTCategoryDALRepository.GetFGTCategories();
            }
            catch (Exception ex)
            {
                fGTCategoryResponse.ErrorMessage = ex.Message;
                fGTCategoryResponse.Exception = ex;
            }
            return fGTCategoryResponse;
        }

        public FGTCategoryResponse GetFGTCategory(FGTCategoryRequest fGTCategoryRequest)
        {
            try
            {
                fGTCategoryResponse = fGTCategoryDALRepository.GetFGTCategory(fGTCategoryRequest);
            }
            catch (Exception ex)
            {
                fGTCategoryResponse.ErrorMessage = ex.Message;
                fGTCategoryResponse.Exception = ex;
            }
            return fGTCategoryResponse;
        }

        public FGTCategoryResponse SaveFGTCategory(FGTCategoryRequest fGTCategoryRequest)
        {
            try
            {
                fGTCategoryResponse = fGTCategoryDALRepository.SaveFGTCategory(fGTCategoryRequest);
            }
            catch (Exception ex)
            {
                fGTCategoryResponse.ErrorMessage = ex.Message;
                fGTCategoryResponse.Exception = ex;
            }
            return fGTCategoryResponse;
        }
    }
}

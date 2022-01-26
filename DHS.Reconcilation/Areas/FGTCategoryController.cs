using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace Medicaid.Reconcilation.Areas
{
    [RoutePrefix("FGTCategory")]
    public class FGTCategoryController : ApiController
    {
        IFGTCategoryRepository fGTCategoryRepository;
        FGTCategoryResponse fGTCategoryResponse;

        public FGTCategoryController()
        {
            fGTCategoryRepository = new FGTCategoryBAL();
            fGTCategoryResponse = new FGTCategoryResponse();
        }

        [HttpPost]
        [Route("GetFGTCategoriesById")]
        public FGTCategoryResponse GetFGTCategoriesById(FGTCategoryRequest fGTCategoryRequest)
        {
            try
            {
                fGTCategoryResponse = fGTCategoryRepository.GetFGTCategoriesById(fGTCategoryRequest);
            }
            catch (Exception ex)
            {
                fGTCategoryResponse.ErrorMessage = ex.Message;
                fGTCategoryResponse.Exception = ex;
            }
            return fGTCategoryResponse;
        }

        [HttpPost]
        [Route("GetFGTCategories")]
        public FGTCategoryResponse GetFGTCategories(FGTCategoryRequest fGTCategoryRequest)
        {
            try
            {
                fGTCategoryResponse = fGTCategoryRepository.GetFGTCategories();
            }
            catch (Exception ex)
            {
                fGTCategoryResponse.ErrorMessage = ex.Message;
                fGTCategoryResponse.Exception = ex;
            }
            return fGTCategoryResponse;
        }


        [HttpPost]
        [Route("GetFGTCategory")]
        public FGTCategoryResponse GetFGTCategory(FGTCategoryRequest fGTCategoryRequest)
        {
            try
            {
                fGTCategoryResponse = fGTCategoryRepository.GetFGTCategory(fGTCategoryRequest);
            }
            catch (Exception ex)
            {
                fGTCategoryResponse.ErrorMessage = ex.Message;
                fGTCategoryResponse.Exception = ex;
            }
            return fGTCategoryResponse;
        }

        [HttpPost]
        [Route("SaveFGTCategory")]
        public FGTCategoryResponse SaveFGTCategory(FGTCategoryRequest fGTCategoryRequest)
        {
            try
            {
                fGTCategoryResponse = fGTCategoryRepository.SaveFGTCategory(fGTCategoryRequest);
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

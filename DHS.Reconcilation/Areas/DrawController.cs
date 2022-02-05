using DHSBAL;
using DHSEntities;
using System;
using System.Web.Http;

namespace DHS.Reconcilation.Areas
{
    [RoutePrefix("Draw")]
    public class DrawController : ApiController
    {
        IDrawRepository drawRepository;
        DrawResponse drawResponse;

        public DrawController()
        {
            drawRepository = new DrawBAL();
            drawResponse = new DrawResponse();
        }

        [HttpPost]
        [Route("GetDraws")]
        public DrawResponse GetDraws(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawRepository.GetDraws(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        [HttpPost]
        [Route("GetDraw")]
        public DrawResponse GetDraw(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawRepository.GetDraw(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        [HttpPost]
        [Route("SaveDraw")]
        public DrawResponse SaveDraw(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawRepository.SaveDraw(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        [HttpPost]
        [Route("GetDrawDocuments")]
        public DrawResponse GetDrawDocuments(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawRepository.GetDrawDocuments(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        [HttpPost]
        [Route("GetDrawDocument")]
        public DrawResponse GetDrawDocument(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawRepository.GetDrawDocument(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        [HttpPost]
        [Route("SaveDrawDocument")]
        public DrawResponse SaveDrawDocument(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawRepository.SaveDrawDocument(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        [HttpPost]
        [Route("GetTransactionByDrawId")]
        public DrawResponse GetTransactionByDrawId(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawRepository.GetTransactionByDrawId(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }
    }
}

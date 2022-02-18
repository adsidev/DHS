using DHSDAL;
using DHSEntities;
using System;

namespace DHSBAL
{
    public class DrawBAL : IDrawRepository
    {
        /// <summary>
        /// Connection string
        /// </summary>
        IDrawDALRepository drawDALRepository;

        DrawResponse drawResponse;
        public DrawBAL()
        {
            drawDALRepository = new DrawDAL();
            drawResponse = new DrawResponse();
        }

        public DrawResponse GetDraws(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawDALRepository.GetDraws(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        public DrawResponse GetDraw(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawDALRepository.GetDraw(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        public DrawResponse SaveDraw(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawDALRepository.SaveDraw(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        public DrawResponse GetDrawDocuments(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawDALRepository.GetDrawDocuments(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        public DrawResponse GetDrawDocument(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawDALRepository.GetDrawDocument(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        public DrawResponse SaveDrawDocument(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawDALRepository.SaveDrawDocument(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        public DrawResponse GetTransactionByDrawId(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawDALRepository.GetTransactionByDrawId(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        public DrawResponse CheckDrawByBatchNumber(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawDALRepository.CheckDrawByBatchNumber(drawRequest);
            }
            catch (Exception ex)
            {
                drawResponse.ErrorMessage = ex.Message;
                drawResponse.Exception = ex;
            }
            return drawResponse;
        }

        public DrawResponse DeleteDraw(DrawRequest drawRequest)
        {
            try
            {
                drawResponse = drawDALRepository.DeleteDraw(drawRequest);
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

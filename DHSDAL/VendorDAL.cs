using DataAccessLayer;
using DHSEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace DHSDAL
{
    public class VendorDAL : IVendorDALRepository
    { /// <summary>
      /// Connection string
      /// </summary>
        private readonly string _connectionString;

        VendorResponse vendorResponse;

        public VendorDAL()
        {
            _connectionString = Constant.MRAConnectionString;
            vendorResponse = new VendorResponse();
        }

        public VendorResponse GetVendors()
        {
            List<VendorEntity> vendorEntities = new List<VendorEntity>();
            SqlObject.Parameters = new object[] {
            };
            var vendorDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Vendor.USPGETVENDORS, SqlObject.Parameters);
            foreach (DataRow vendorDataRow in vendorDataSet.Tables[0].Rows)
            {
                VendorEntity vendorEntity = new VendorEntity();
                try
                {
                    vendorEntity.VendorId = Convert.ToInt32(vendorDataRow["VendorId"].ToString());
                    vendorEntity.VendorName = vendorDataRow["VendorName"].ToString();
                    vendorEntity.VendorCode = vendorDataRow["VendorCode"].ToString();
                }
                catch (Exception exception)
                {
                    vendorEntity.ErrorMessage = exception.Message;
                    vendorEntity.Exception = exception;
                }
                finally
                {
                    vendorEntities.Add(vendorEntity);
                }
            }
            if (vendorEntities.Count >= 1 || vendorEntities.Count == 0)
            {
                vendorResponse.ErrorMessage = string.Empty;
                vendorResponse.Message = string.Empty;
            }
            vendorResponse.vendorEntities = vendorEntities;
            return vendorResponse;
        }

        public VendorResponse GetVendor(VendorRequest vendorRequest)
        {
            VendorEntity vendorEntity = new VendorEntity();
            vendorEntity.ErrorMessage = string.Empty;
            vendorResponse.ErrorMessage = string.Empty;
            vendorEntity.Message = string.Empty;
            vendorResponse.Message = string.Empty;
            if (vendorRequest.vendorEntity.VendorId > 0)
            {
                SqlObject.Parameters = new object[] {
                    vendorRequest.vendorEntity.VendorId
                };
                var vendorDataSet = SqlHelper.ExecuteDataset(_connectionString, StoredProcedures.Vendor.USPGETVENDOR, SqlObject.Parameters);
                foreach (DataRow vendorDataRow in vendorDataSet.Tables[0].Rows)
                {
                    try
                    {
                        vendorEntity.VendorId = Convert.ToInt32(vendorDataRow["VendorId"].ToString());
                        vendorEntity.VendorName = vendorDataRow["VendorName"].ToString();
                        vendorEntity.VendorCode = vendorDataRow["VendorCode"].ToString();
                    }
                    catch (Exception exception)
                    {
                        vendorEntity.ErrorMessage = exception.Message;
                        vendorResponse.ErrorMessage = exception.Message;
                        vendorEntity.Exception = exception;
                        vendorResponse.Exception = exception;
                    }
                    finally
                    {

                    }
                }
            }

            vendorResponse.vendorEntity = vendorEntity;
            return vendorResponse;
        }

        public VendorResponse SaveVendor(VendorRequest vendorRequest)
        {
            try
            {
                if (vendorRequest.vendorEntity.VendorId > 0)
                    vendorRequest.vendorEntity.SaveString = "U";
                else
                    vendorRequest.vendorEntity.SaveString = "I";

                SqlObject.Parameters = new object[] {
                vendorRequest.vendorEntity.SaveString,
                vendorRequest.vendorEntity.VendorId,
                vendorRequest.vendorEntity.VendorName,
                vendorRequest.vendorEntity.ModifiedBy
                };
                var intResult = SqlHelper.ExecuteScalar(_connectionString, StoredProcedures.Vendor.USPSAVEVENDOR, SqlObject.Parameters);
                vendorResponse.Message = string.Empty;
                vendorResponse.ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                vendorResponse.ErrorMessage = ex.Message;
                vendorResponse.Exception = ex;
            }
            return vendorResponse;
        }

    }
}

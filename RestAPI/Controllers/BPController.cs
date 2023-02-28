using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using Common;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using BusinessLogic.Models;
using System.Collections.Generic;
using System.Data;

namespace RestAPI.Controllers
{
    [ApiController]
    public class BPController : Controller
    {
        BL objBL = new BL();
        
        [Route("api/v1/users" + "/{userId}/bloodPressure/readings/{readingId}")]
        [HttpGet]
        public IActionResult getBloodPressureReadings(string userId, string readingId="-1")
        {
            try
            {
                DataTable dataTable = objBL.getBPReadings(userId,readingId);
                List<BloodPressureReadings> bpReadingsList = new List<BloodPressureReadings>();

                int index = 1;
                foreach (DataRow dr in dataTable.Rows)
                {
                    BloodPressureReadings bpRow = new BloodPressureReadings();
                    bpRow.ID = dr["ID"].ToString();
                    bpRow.SNO = Convert.ToString(index++);
                    bpRow.READING_DATE = dr["READING_DATE"].ToString();
                    bpRow.READING_TIME = dr["READING_TIME"].ToString();
                    bpRow.SYS_READING = dr["SYS_READING"].ToString();
                    bpRow.DYS_READING = dr["DYS_READING"].ToString();
                    bpRow.DEVICE = dr["DEVICE"].ToString();
                    bpRow.LASTMEAL = dr["LASTMEAL"].ToString();
                    bpRow.LASTMEALTIME = dr["LASTMEALTIME"].ToString();

                    bpReadingsList.Add(bpRow);
                }


                var jsonResult = JsonConvert.SerializeObject(bpReadingsList);
                return Ok(jsonResult);
            }
            catch (Exception ex)
            {
                Logging.fileLog(System.DateTime.Today.ToString() + "| "
                                + Request.Path.ToString() + "| "
                                + ex.Message + "| "
                                + ex.StackTrace);

                ErrorResponseModel errorModel = new ErrorResponseModel
                {
                    ErrorCode = "9999",
                    Description = "Request could not be processed due to some technical reasons",
                    ErrorCategory = "InternalServer"
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorModel);
            }
        }

        [Route("api/v1/users" + "/{userId}/bloodPressure/readings/{readingId}")]
        [HttpPatch]
        public IActionResult deleteBloodPressureReading(string readingId)
        {
            try
            {
                objBL.deleteBPGraphStats(readingId);                
                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Logging.fileLog(System.DateTime.Today.ToString() + "| "
                                + Request.Path.ToString() + "| "
                                + ex.Message + "| "
                                + ex.StackTrace);

                ErrorResponseModel errorModel = new ErrorResponseModel
                {
                    ErrorCode = "9999",
                    Description = "Request could not be processed due to some technical reasons",
                    ErrorCategory = "InternalServer"
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorModel);
            }
        }

        [Route("api/v1/users" + "/{userId}/bloodPressure/graphStats")]
        [HttpGet]
        public IActionResult getBPGraphStats(string userId)
        {
            try
            {
                DataTable dataTable = objBL.getBPGraphStats(userId);
                BloodPressureReadingsGraphStats objBPGraphStats = new BloodPressureReadingsGraphStats();

                objBPGraphStats.XAxis = new List<string>();
                objBPGraphStats.YAxis_SYS = new List<string>();
                objBPGraphStats.YAXIS_DYS = new List<string>();                

                foreach (DataRow dr in dataTable.Rows)
                {
                    objBPGraphStats.XAxis.Add(dr["READINGDATE"].ToString());
                    objBPGraphStats.YAxis_SYS.Add(dr["AVG_SYS"].ToString());
                    objBPGraphStats.YAXIS_DYS.Add(dr["AVG_DYS"].ToString());
                }

                var jsonResult = JsonConvert.SerializeObject(objBPGraphStats);
                return Ok(jsonResult);
            }
            catch (Exception ex)
            {
                Logging.fileLog(System.DateTime.Today.ToString() + "| "
                                + Request.Path.ToString() + "| "
                                + ex.Message + "| "
                                + ex.StackTrace);

                ErrorResponseModel errorModel = new ErrorResponseModel
                {
                    ErrorCode = "9999",
                    Description = "Request could not be processed due to some technical reasons",
                    ErrorCategory = "InternalServer"
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorModel);
            }
        }        

        [Route("api/v1/users" + "/{userId}/bloodPressure/readings")]
        [HttpPost]
        public IActionResult saveBloodPressureReadings([FromBody] BloodPressureReadings requestModel, string userId)
        {
            try
            {
                bool response = false;

                if (string.IsNullOrEmpty(requestModel.ID))
                    response = objBL.saveBPReading(userId, "-1", requestModel.READING_DATE, requestModel.READING_TIME, requestModel.SYS_READING, requestModel.DYS_READING, requestModel.DEVICE, requestModel.LASTMEAL, requestModel.LASTMEALTIME);                
                else
                    response = objBL.saveBPReading(userId, requestModel.ID, requestModel.READING_DATE, requestModel.READING_TIME, requestModel.SYS_READING, requestModel.DYS_READING, requestModel.DEVICE, requestModel.LASTMEAL, requestModel.LASTMEALTIME);
                
                return Ok(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                Logging.fileLog(System.DateTime.Today.ToString() + "| "
                                + Request.Path.ToString() + "| "
                                + ex.Message + "| "
                                + ex.StackTrace);

                ErrorResponseModel errorModel = new ErrorResponseModel
                {
                    ErrorCode = "9999",
                    Description = "Request could not be processed due to some technical reasons",
                    ErrorCategory = "InternalServer"
                };
                return StatusCode((int)HttpStatusCode.InternalServerError, errorModel);
            }
        }
    }
}

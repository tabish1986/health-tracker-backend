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
    public class GlucoController : Controller
    {
        BL objBL = new BL();

        [Route("api/v1/users" + "/{userId}/glucose/readings/{readingId}")]
        [HttpGet]
        public IActionResult getGlucoseReadings(string userId, string readingId = "-1")
        {
            try
            {
                DataTable dataTable = objBL.getGlucoseReadings(userId, readingId);
                List<GlucoseReadings> glucoReadingsList = new List<GlucoseReadings>();

                int index = 1;
                foreach (DataRow dr in dataTable.Rows)
                {
                    GlucoseReadings bpRow = new GlucoseReadings();
                    bpRow.ID = dr["ID"].ToString();
                    bpRow.SNO = Convert.ToString(index++);
                    bpRow.READING_DATE = dr["READING_DATE"].ToString();
                    bpRow.READING_TIME = dr["READING_TIME"].ToString();
                    bpRow.READING = dr["READING"].ToString();
                    bpRow.TYPE = dr["TYPE"].ToString();
                    bpRow.DEVICE = dr["DEVICE"].ToString();
                    bpRow.LASTMEAL = dr["LASTMEAL"].ToString();
                    bpRow.LASTMEALTIME = dr["LASTMEALTIME"].ToString();

                    glucoReadingsList.Add(bpRow);
                }


                var jsonResult = JsonConvert.SerializeObject(glucoReadingsList);
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

        [Route("api/v1/users" + "/{userId}/glucose/readings/{readingId}")]
        [HttpPatch]
        public IActionResult deleteGlucoseReading(string readingId)
        {
            try
            {
                objBL.deleteGlucoseGraphStats(readingId);
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

        [Route("api/v1/users" + "/{userId}/glucose/graphStats")]
        [HttpGet]
        public IActionResult getGlucoseGraphStats(string userId)
        {
            try
            {
                DataTable dataTable = objBL.getGlucoseGraphStats(userId);
                GlucoseReadingsGraphStats objGlucoseGraphStats = new GlucoseReadingsGraphStats();

                objGlucoseGraphStats.XAxis = new List<string>();
                objGlucoseGraphStats.YAxis_FASTING = new List<string>();
                objGlucoseGraphStats.YAXIS_RANDOM = new List<string>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["ReadingDate_A"].ToString()))
                        objGlucoseGraphStats.XAxis.Add(dr["ReadingDate_A"].ToString());
                    else
                        objGlucoseGraphStats.XAxis.Add(dr["ReadingDate_B"].ToString());

                    //objBPGraphStats.XAxis.Add(dr["READINGDATE"].ToString());
                    objGlucoseGraphStats.YAxis_FASTING.Add(dr["FASTING_READING"].ToString());
                    objGlucoseGraphStats.YAXIS_RANDOM.Add(dr["RANDOM_READING"].ToString());
                }

                var jsonResult = JsonConvert.SerializeObject(objGlucoseGraphStats);
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

        [Route("api/v1/users" + "/{userId}/glucose/readings")]
        [HttpPost]
        public IActionResult saveGlucoseReadings([FromBody] GlucoseReadings requestModel, string userId)
        {
            try
            {
                bool response = false;

                if (string.IsNullOrEmpty(requestModel.ID))
                    response = objBL.saveGlucoseReading(userId, "-1", requestModel.READING_DATE, requestModel.READING_TIME, requestModel.READING, requestModel.TYPE, requestModel.DEVICE, requestModel.LASTMEAL, requestModel.LASTMEALTIME);
                else
                    response = objBL.saveGlucoseReading(userId, requestModel.ID, requestModel.READING_DATE, requestModel.READING_TIME, requestModel.READING, requestModel.TYPE, requestModel.DEVICE, requestModel.LASTMEAL, requestModel.LASTMEALTIME);

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

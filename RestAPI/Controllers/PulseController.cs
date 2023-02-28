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
    public class PulseController : Controller
    {
        BL objBL = new BL();

        [Route("api/v1/users" + "/{userId}/pulse/readings/{readingId}")]
        [HttpGet]
        public IActionResult getPulseReadings(string userId, string readingId = "-1")
        {
            try
            {
                DataTable dataTable = objBL.getPulseReadings(userId, readingId);
                List<PulseReadings> glucoReadingsList = new List<PulseReadings>();

                int index = 1;
                foreach (DataRow dr in dataTable.Rows)
                {
                    PulseReadings pulseRow = new PulseReadings();
                    pulseRow.ID = dr["ID"].ToString();
                    pulseRow.SNO = Convert.ToString(index++);
                    pulseRow.READING_DATE = dr["READING_DATE"].ToString();
                    pulseRow.READING_TIME = dr["READING_TIME"].ToString();
                    pulseRow.READING = dr["READING"].ToString();
                    pulseRow.DEVICE = dr["DEVICE"].ToString();
                    pulseRow.EXERTION_TYPE = dr["EXERTION_TYPE"].ToString();
                    pulseRow.EXERTION_TIME = dr["EXERTION_TIME"].ToString();

                    glucoReadingsList.Add(pulseRow);
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

        [Route("api/v1/users" + "/{userId}/pulse/readings/{readingId}")]
        [HttpPatch]
        public IActionResult deletePulseReading(string readingId)
        {
            try
            {
                objBL.deletePulseGraphStats(readingId);
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

        [Route("api/v1/users" + "/{userId}/pulse/graphStats")]
        [HttpGet]
        public IActionResult getPulseGraphStats(string userId)
        {
            try
            {
                DataTable dataTable = objBL.getPulseGraphStats(userId);
                PulseReadingsGraphStats objPulseGraphStats = new PulseReadingsGraphStats();

                objPulseGraphStats.XAxis = new List<string>();
                objPulseGraphStats.YAxis = new List<string>();

                foreach (DataRow dr in dataTable.Rows)
                {
                    objPulseGraphStats.XAxis.Add(dr["READINGDATE"].ToString());
                    objPulseGraphStats.YAxis.Add(dr["READING"].ToString());
                }

                var jsonResult = JsonConvert.SerializeObject(objPulseGraphStats);
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

        [Route("api/v1/users" + "/{userId}/pulse/readings")]
        [HttpPost]
        public IActionResult savePulseReadings([FromBody] PulseReadings requestModel, string userId)
        {
            try
            {
                bool response = false;

                if (string.IsNullOrEmpty(requestModel.ID))
                    response = objBL.savePulseReading(userId, "-1", requestModel.READING_DATE, requestModel.READING_TIME, requestModel.READING, requestModel.DEVICE, requestModel.EXERTION_TYPE, requestModel.EXERTION_TIME);
                else
                    response = objBL.savePulseReading(userId, requestModel.ID, requestModel.READING_DATE, requestModel.READING_TIME, requestModel.READING, requestModel.DEVICE, requestModel.EXERTION_TYPE, requestModel.EXERTION_TIME);

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


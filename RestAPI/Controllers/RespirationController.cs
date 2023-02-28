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
    public class RespirationController : Controller
    {
        BL objBL = new BL();

        [Route("api/v1/users" + "/{userId}/respiration/readings/{readingId}")]
        [HttpGet]
        public IActionResult getRespirationReadings(string userId, string readingId = "-1")
        {
            try
            {
                DataTable dataTable = objBL.getRespirationReadings(userId, readingId);
                List<RespirationReadings> glucoReadingsList = new List<RespirationReadings>();

                int index = 1;
                foreach (DataRow dr in dataTable.Rows)
                {
                    RespirationReadings bpRow = new RespirationReadings();
                    bpRow.ID = dr["ID"].ToString();
                    bpRow.SNO = Convert.ToString(index++);
                    bpRow.READING_DATE = dr["READING_DATE"].ToString();
                    bpRow.READING_TIME = dr["READING_TIME"].ToString();
                    bpRow.READING = dr["READING"].ToString();                    
                    bpRow.DEVICE = dr["DEVICE"].ToString();
                    bpRow.EXERTION_TYPE = dr["EXERTION_TYPE"].ToString();
                    bpRow.EXERTION_TIME = dr["EXERTION_TIME"].ToString();

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

        [Route("api/v1/users" + "/{userId}/respiration/readings/{readingId}")]
        [HttpPatch]
        public IActionResult deleteRespirationReading(string readingId)
        {
            try
            {
                objBL.deleteRespirationGraphStats(readingId);
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

        [Route("api/v1/users" + "/{userId}/respiration/graphStats")]
        [HttpGet]
        public IActionResult getRespirationGraphStats(string userId)
        {
            try
            {
                DataTable dataTable = objBL.getRespirationGraphStats(userId);
                RespirationReadingsGraphStats objRespirationGraphStats = new RespirationReadingsGraphStats();

                objRespirationGraphStats.XAxis = new List<string>();
                objRespirationGraphStats.YAxis = new List<string>();                

                foreach (DataRow dr in dataTable.Rows)
                {
                    objRespirationGraphStats.XAxis.Add(dr["READINGDATE"].ToString());
                    objRespirationGraphStats.YAxis.Add(dr["READING"].ToString());
                }

                var jsonResult = JsonConvert.SerializeObject(objRespirationGraphStats);
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

        [Route("api/v1/users" + "/{userId}/respiration/readings")]
        [HttpPost]
        public IActionResult saveRespirationReadings([FromBody] RespirationReadings requestModel, string userId)
        {
            try
            {
                bool response = false;

                if (string.IsNullOrEmpty(requestModel.ID))
                    response = objBL.saveRespirationReading(userId, "-1", requestModel.READING_DATE, requestModel.READING_TIME, requestModel.READING, requestModel.DEVICE, requestModel.EXERTION_TYPE, requestModel.EXERTION_TIME);
                else
                    response = objBL.saveRespirationReading(userId, requestModel.ID, requestModel.READING_DATE, requestModel.READING_TIME, requestModel.READING, requestModel.DEVICE, requestModel.EXERTION_TYPE, requestModel.EXERTION_TIME);

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


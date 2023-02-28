using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using Common;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using BusinessLogic.Models.AuthenticationModels;
using BusinessLogic.Models.UserModel;
using BusinessLogic.Models;
using System.Data;

namespace RestAPI.Controllers
{
    [ApiController]
    public class ConfigurationController: ControllerBase
    {
        BL objBL = new BL();

        [Route("api/v1/users" + "/{userId}/administrators/configurations")]
        [HttpGet]
        public IActionResult getSystemConfiguration(string userId)
        {            
            GenericResponse objResponse;
            var jsonResult = string.Empty;

            try
            {
                var dtUser = objBL.getUser(userId);
                if (dtUser.Rows[0]["STATUS"].ToString() != "Admin")
                {
                    objResponse = new GenericResponse()
                    {
                        responseCode = "-1",
                        responseValue = "Authorization Failed. You are not authorized to access this resource."
                    };
                    
                    jsonResult = JsonConvert.SerializeObject(objResponse);
                    return Unauthorized(jsonResult);
                }
                else
                {
                    DataTable dtSystemConfiguration = objBL.getSystemConfiguration();
                    ConfigurationModel model = new ConfigurationModel();
                    model.ClientID = dtSystemConfiguration.Rows[0]["CLIENT_ID"].ToString();
                    model.Token = dtSystemConfiguration.Rows[0]["TOKEN"].ToString();
                    model.MobileNo = dtSystemConfiguration.Rows[0]["MOB_NO"].ToString();                   

                    jsonResult = JsonConvert.SerializeObject(model);
                    return Ok(jsonResult);
                }                
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

        [Route("api/v1/users" + "/{userId}/administrators/configurations")]
        [HttpPost]
        public IActionResult updateProfileBasics([FromBody] ConfigurationModel requestModel, string userId)
        {
            try
            {
                var jsonResult = string.Empty;
                GenericResponse objResponse;

                var dtUser = objBL.getUser(userId);
                if (dtUser.Rows[0]["STATUS"].ToString() != "Admin")
                {
                    objResponse = new GenericResponse()
                    {
                        responseCode = "-1",
                        responseValue = "Authorization Failed. You are not authorized to access this resource."
                    };

                    jsonResult = JsonConvert.SerializeObject(objResponse);
                    return Unauthorized(jsonResult);
                }
                else
                {
                    objBL.saveSystemConfiguration(requestModel.ClientID, requestModel.Token, requestModel.MobileNo);

                    objResponse = new GenericResponse()
                    {
                        responseCode = "1",
                        responseValue = String.Empty
                    };

                    jsonResult = JsonConvert.SerializeObject(objResponse);
                    return Ok(jsonResult);
                }                
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

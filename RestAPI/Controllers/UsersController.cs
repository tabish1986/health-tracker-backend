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
    public class UsersController : ControllerBase
    {
        BL objBL = new BL();
                
        [Route("api/v1/authentication")]
        [HttpPost]
        public IActionResult authenticate([FromBody] UserCredentials requestModel)
        {
            try
            {
                var jsonResult = string.Empty;
                GenericResponse objResponse;

                var dataTable = objBL.getUser(requestModel.UserId);
                if (dataTable.Rows.Count == 0)
                {
                    objResponse = new GenericResponse()
                    {
                        responseCode = "-1",
                        responseValue = "Invalid Username"
                    };
                }   
                else
                {
                    if (objBL.validatePassword(requestModel.Password, dataTable.Rows[0]["PASSWORD"].ToString()) == false)
                    {
                        objResponse = new GenericResponse()
                        {
                            responseCode = "-2",
                            responseValue = "Invalid Password"
                        };
                    }
                    else
                    {
                        string oAuthToken = objBL.generateOAuthToken(requestModel.UserId, requestModel.Password);

                        objResponse = new GenericResponse()
                        {
                            responseCode = "1",
                            responseValue = oAuthToken + ',' + dataTable.Rows[0]["FIRST_NAME"].ToString()
                        };                        
                    }
                }
                
                jsonResult = JsonConvert.SerializeObject(objResponse);
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

        [Route("api/v1/users/signup")]
        [HttpPost]
        public IActionResult signUp([FromBody] RegisterationInfo requestModel)
        {
            try
            {
                var jsonResult = string.Empty;
                GenericResponse objResponse;

                if (string.IsNullOrEmpty(requestModel.OTP))
                {
                    var dataTable = objBL.getUser(requestModel.PreferredUserId);
                    if (dataTable.Rows.Count != 0)
                    {
                        objResponse = new GenericResponse()
                        {
                            responseCode = "-1",
                            responseValue = "Username is not available. Please choose another."
                        };
                    }
                    else
                    {

                        objBL.SendOTP(requestModel.Name, requestModel.PreferredUserId, requestModel.MobNo);

                        objResponse = new GenericResponse()
                        {
                            responseCode = "1",
                            responseValue = "OTP Sent for verification."
                        };
                    }
                }
                else
                {
                    if (objBL.ValidateOTP(requestModel.PreferredUserId, requestModel.OTP) == true)
                    {
                        objBL.signUp(requestModel);

                        objResponse = new GenericResponse()
                        {
                            responseCode = "2",
                            responseValue = "Congratulations you have successfully Signed Up to Health Watch."
                        };
                    }
                    else
                    {
                        objResponse = new GenericResponse()
                        {
                            responseCode = "-2",
                            responseValue = "Invalid OTP."
                        };
                    }
                }

                jsonResult = JsonConvert.SerializeObject(objResponse);
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

        [Route("api/v1/users/resetPassword")]
        [HttpPost]
        public IActionResult ResetPassword([FromBody] ForgotPassword requestModel)
        {
            try
            {
                var jsonResult = string.Empty;
                GenericResponse objResponse;

                if (string.IsNullOrEmpty(requestModel.OTP))
                {
                    var dataTable = objBL.getUser(requestModel.UserId);
                    if (dataTable.Rows.Count == 0)
                    {
                        objResponse = new GenericResponse()
                        {
                            responseCode = "-1",
                            responseValue = "Invalid Username"
                        };
                    }
                    else
                    {
                        objBL.SendOTP(dataTable.Rows[0]["FIRST_NAME"].ToString(), requestModel.UserId, dataTable.Rows[0]["MOBILE_NO"].ToString());
                        objResponse = new GenericResponse()
                        {
                            responseCode = "1",
                            responseValue = "OTP Sent for verification."
                        };
                    }
                }
                else
                {
                    if (objBL.ValidateOTP(requestModel.UserId, requestModel.OTP) == true)
                    {                
                        //Create and set new password
                        string newPassword = objBL.forgotPassword(requestModel.UserId);

                        objResponse = new GenericResponse()
                        {
                            responseCode = "2",
                            responseValue = "Your Password has been reset. Your new Password is " + newPassword
                        };
                    }
                    else
                    {
                        objResponse = new GenericResponse()
                        {
                            responseCode = "-2",
                            responseValue = "Invalid OTP."
                        };
                    }
                }
                
                jsonResult = JsonConvert.SerializeObject(objResponse);
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

        [Route("api/v1/users/{userId}/changePassword")]
        [HttpPost]
        public IActionResult changePassword([FromBody] PasswordChange requestModel, string userId)
        {
            try
            {
                var jsonResult = string.Empty;
                GenericResponse objResponse;

                var dataTable = objBL.getUser(userId);
                if (dataTable.Rows.Count == 0)
                {
                    objResponse = new GenericResponse()
                    {
                        responseCode = "-1",
                        responseValue = "Invalid Username"
                    };
                }
                else 
                {
                    if (objBL.validatePassword(requestModel.OldPassword, dataTable.Rows[0]["PASSWORD"].ToString()) == true)
                    {
                        bool response = objBL.changePassword(userId, requestModel.NewPassword);
                        objResponse = new GenericResponse()
                        {
                            responseCode = "1",
                            responseValue = "Password has been changed successfully."
                        };
                    }
                    else
                    {
                        objResponse = new GenericResponse()
                        {
                            responseCode = "-1",
                            responseValue = "Invalid Old Password"
                        };
                    }
                }
             
                jsonResult = JsonConvert.SerializeObject(objResponse);
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

        [Route("api/v1/users" + "/{userId}/profilebasics")]
        [HttpGet]
        public IActionResult getProfileAndBasics(string userId)
        {
            try
            {
                DataTable dataTable = objBL.getProfileAndBasics(userId);
                var jsonResult = JsonConvert.SerializeObject(dataTable);
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

        [Route("api/v1/users/{userId}/profilebasics")]
        [HttpPost]
        public IActionResult updateProfileBasics([FromBody] ProfileAndBasic requestModel, string userId)
        {
            try
            {
                var jsonResult = string.Empty;
                GenericResponse objResponse;

                objBL.saveProfileAndBasics(userId, requestModel);

                //jsonResult = JsonConvert.SerializeObject(objResponse);
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

        [Route("api/v1/users" + "/{userId}")]
        [HttpGet]
        public IActionResult getUserDetails(string userId)
        {
            try
            {
                var dataTable = objBL.getUser(userId);
                var jsonResult = JsonConvert.SerializeObject(dataTable);
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
    }
    
}

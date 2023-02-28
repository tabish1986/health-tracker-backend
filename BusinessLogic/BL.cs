using Common;
using OracleDataAccessLayer.DatabaseComponent;
using OracleDataAccessLayer.Enumeration;
using System.Data;

namespace BusinessLogic
{
    public class BL
    {
        public DataTable getUser(string userId)
        {
            
            string storedProcedure = "PKGUSERS.getUser";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[2];                
                generalParams[0] = new GeneralParams("inUserId", 200, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);                
                generalParams[1] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }
        public DataTable getUserDetails(string userId)
        {

            string storedProcedure = "PKGUSERS.getUserDetail";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[2];
                generalParams[0] = new GeneralParams("UserId", 200, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);              
                generalParams[1] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable getBPReadings(string userId, string readingId)
        {

            string storedProcedure = "PKGBP.GetBPReadings";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[3];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getGlucoseReadings(string userId, string readingId)
        {

            string storedProcedure = "PKGGLUCOSE.GetGlucoReadings";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[3];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getRespirationReadings(string userId, string readingId)
        {

            string storedProcedure = "PKGRESPIRATION.GetRespirationReadings";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[3];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getPulseReadings(string userId, string readingId)
        {

            string storedProcedure = "PKGPULSE.GetPulseReadings";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[3];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool deleteBPGraphStats(string readingId)
        {

            string storedProcedure = "PKGBP.DeleteBPReading";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[1];
                generalParams[0] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);                
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool deleteGlucoseGraphStats(string readingId)
        {

            string storedProcedure = "PKGGLUCOSE.DeleteGlucoReading";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[1];
                generalParams[0] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool deleteRespirationGraphStats(string readingId)
        {

            string storedProcedure = "PKGRESPIRATION.DeleteRespirationReading";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[1];
                generalParams[0] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool deletePulseGraphStats(string readingId)
        {

            string storedProcedure = "PKGPULSE.DeletePulseReading";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[1];
                generalParams[0] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getBPGraphStats(string userId)
        {

            string storedProcedure = "PKGBP.GetWeeklyBPAverage";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[2];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getGlucoseGraphStats(string userId)
        {

            string storedProcedure = "PKGGLUCOSE.GetWeeklyGlucoAverage";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[2];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getRespirationGraphStats(string userId)
        {

            string storedProcedure = "PKGRESPIRATION.GetWeeklyRespirationAverage";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[2];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable getPulseGraphStats(string userId)
        {

            string storedProcedure = "PKGPULSE.GetWeeklyPulseAverage";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[2];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool saveBPReading(string userId, string readingId, string date, string time, string systolic, string diastolic, string device, string lastMeal, string lastMealTime)
        {

            string storedProcedure = "PKGBP.SaveBPReading";
            try
            {
                string unformattedDate=DateTime.ParseExact(date,"dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture).ToString("ddMMyyyy");


                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[9];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("inDate", 100, GeneralDatabaseTypes.VarChar, unformattedDate, ParameterDirection.Input);
                generalParams[3] = new GeneralParams("inTime", 100, GeneralDatabaseTypes.VarChar, time, ParameterDirection.Input);
                generalParams[4] = new GeneralParams("inSystolic", 100, GeneralDatabaseTypes.VarChar, systolic, ParameterDirection.Input);
                generalParams[5] = new GeneralParams("inDiastolic", 100, GeneralDatabaseTypes.VarChar, diastolic, ParameterDirection.Input);
                generalParams[6] = new GeneralParams("inDevice", 100, GeneralDatabaseTypes.VarChar, device, ParameterDirection.Input);
                generalParams[7] = new GeneralParams("inLastMeal", 100, GeneralDatabaseTypes.VarChar, lastMeal, ParameterDirection.Input);
                generalParams[8] = new GeneralParams("inLastMealTime", 100, GeneralDatabaseTypes.VarChar, lastMealTime, ParameterDirection.Input);                
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool saveGlucoseReading(string userId, string readingId, string date, string time, string reading, string type, string device, string lastMeal, string lastMealTime)
        {

            string storedProcedure = "PKGGLUCOSE.SaveGlucoReading";
            try
            {
                string unformattedDate = DateTime.ParseExact(date, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture).ToString("ddMMyyyy");


                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[9];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("inDate", 100, GeneralDatabaseTypes.VarChar, unformattedDate, ParameterDirection.Input);
                generalParams[3] = new GeneralParams("inTime", 100, GeneralDatabaseTypes.VarChar, time, ParameterDirection.Input);
                generalParams[4] = new GeneralParams("inReading", 100, GeneralDatabaseTypes.VarChar, reading, ParameterDirection.Input);
                generalParams[5] = new GeneralParams("inType", 100, GeneralDatabaseTypes.VarChar, type, ParameterDirection.Input);
                generalParams[6] = new GeneralParams("inDevice", 100, GeneralDatabaseTypes.VarChar, device, ParameterDirection.Input);
                generalParams[7] = new GeneralParams("inLastMeal", 100, GeneralDatabaseTypes.VarChar, lastMeal, ParameterDirection.Input);
                generalParams[8] = new GeneralParams("inLastMealTime", 100, GeneralDatabaseTypes.VarChar, lastMealTime, ParameterDirection.Input);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool saveRespirationReading(string userId, string readingId, string date, string time, string reading, string device, string exertionType, string exertionTime)
        {

            string storedProcedure = "PKGRESPIRATION.SaveRespirationReading";
            try
            {
                string unformattedDate = DateTime.ParseExact(date, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture).ToString("ddMMyyyy");


                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[8];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("inDate", 100, GeneralDatabaseTypes.VarChar, unformattedDate, ParameterDirection.Input);
                generalParams[3] = new GeneralParams("inTime", 100, GeneralDatabaseTypes.VarChar, time, ParameterDirection.Input);
                generalParams[4] = new GeneralParams("inReading", 100, GeneralDatabaseTypes.VarChar, reading, ParameterDirection.Input);                
                generalParams[5] = new GeneralParams("inDevice", 100, GeneralDatabaseTypes.VarChar, device, ParameterDirection.Input);
                generalParams[6] = new GeneralParams("inLastExertion", 100, GeneralDatabaseTypes.VarChar, exertionType, ParameterDirection.Input);
                generalParams[7] = new GeneralParams("inLastExertionTime", 100, GeneralDatabaseTypes.VarChar, exertionTime, ParameterDirection.Input);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool savePulseReading(string userId, string readingId, string date, string time, string reading, string device, string exertionType, string exertionTime)
        {

            string storedProcedure = "PKGPULSE.SavePulseReading";
            try
            {
                string unformattedDate = DateTime.ParseExact(date, "dd-MMM-yy", System.Globalization.CultureInfo.InvariantCulture).ToString("ddMMyyyy");


                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[8];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inReadingId", 100, GeneralDatabaseTypes.VarChar, readingId, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("inDate", 100, GeneralDatabaseTypes.VarChar, unformattedDate, ParameterDirection.Input);
                generalParams[3] = new GeneralParams("inTime", 100, GeneralDatabaseTypes.VarChar, time, ParameterDirection.Input);
                generalParams[4] = new GeneralParams("inReading", 100, GeneralDatabaseTypes.VarChar, reading, ParameterDirection.Input);
                generalParams[5] = new GeneralParams("inDevice", 100, GeneralDatabaseTypes.VarChar, device, ParameterDirection.Input);
                generalParams[6] = new GeneralParams("inLastExertion", 100, GeneralDatabaseTypes.VarChar, exertionType, ParameterDirection.Input);
                generalParams[7] = new GeneralParams("inLastExertionTime", 100, GeneralDatabaseTypes.VarChar, exertionTime, ParameterDirection.Input);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool validatePassword(string inPassword, string encryptedPassword)
        {
            return (AESEncryption.Decrypt(encryptedPassword) == inPassword);
        }
        public string generateOAuthToken(string UserId, string Password)
        {
            return AESEncryption.Encrypt(UserId + ":" + Password);
        }
        public bool validateoAuthToken(string token)
        {
            try
            {
                string decryptedToken = AESEncryption.Decrypt(token);
                string token_username = decryptedToken.Split(':')[0];
                string token_password = decryptedToken.Split(':')[1];
                
                DataTable dtUser = getUser(token_username);
                if (dtUser.Rows.Count == 0)
                    return false;
                else
                {
                    if (!validatePassword(token_password, dtUser.Rows[0]["PASSWORD"].ToString()))
                        return false;
                    else
                        return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }            
        }
        public bool signUp(BusinessLogic.Models.AuthenticationModels.RegisterationInfo objRegisterationModel)
        {
            objRegisterationModel.PreferredPassword = AESEncryption.Encrypt(objRegisterationModel.PreferredPassword);

            string storedProcedure = "PKGUSERS.SignUP";
            try
            {                
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[5];
                generalParams[0] = new GeneralParams("inName", 100, GeneralDatabaseTypes.VarChar, objRegisterationModel.Name, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inEmail", 100, GeneralDatabaseTypes.VarChar, objRegisterationModel.Email, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("inMobNo", 100, GeneralDatabaseTypes.VarChar, objRegisterationModel.MobNo, ParameterDirection.Input);
                generalParams[3] = new GeneralParams("inPrefUserId", 100, GeneralDatabaseTypes.VarChar, objRegisterationModel.PreferredUserId, ParameterDirection.Input);
                generalParams[4] = new GeneralParams("inEncPrefPwd", 100, GeneralDatabaseTypes.VarChar, objRegisterationModel.PreferredPassword, ParameterDirection.Input);                
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public void SendOTP(string Name, string preferredUserId, string mobNo)
        {            
            //Generate OTP
            Random _rdm = new Random();
            string OTP = _rdm.Next(1000, 9999).ToString();
            string EncyptedOTP = AESEncryption.Encrypt(OTP);

            //Store OTP
            string storedProcedure = "PKGUSERS.otp_AddDelete";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[3];
                generalParams[0] = new GeneralParams("inUserId", 200, GeneralDatabaseTypes.VarChar, preferredUserId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inOTP", 200, GeneralDatabaseTypes.VarChar, EncyptedOTP, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("inAction", 200, GeneralDatabaseTypes.VarChar, "add", ParameterDirection.Input);                
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //Send SMS
            string smsMessage = "Dear " + Name + ", your OTP (One-Time Passcode) is " + OTP;
            DataTable dtSystemConfig = getSystemConfiguration();

            SMSGateway objSMSGateway = new SMSGateway();
            objSMSGateway.SendSMS(dtSystemConfig.Rows[0]["CLIENT_ID"].ToString(),
                                  dtSystemConfig.Rows[0]["TOKEN"].ToString(),
                                  dtSystemConfig.Rows[0]["MOB_NO"].ToString(),
                                  smsMessage,
                                  mobNo);
        }
        public bool ValidateOTP(string preferredUserId, string OTP)
        {
            string storedProcedure = "PKGUSERS.getOTP";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[2];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, preferredUserId, ParameterDirection.Input);                
                generalParams[1] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);

                if (objDs.Tables[0].Rows.Count > 0)
                {
                    if (AESEncryption.Decrypt(objDs.Tables[0].Rows[0]["OTP"].ToString()) == OTP)
                    {
                        string storedProcedure2 = "PKGUSERS.otp_AddDelete";
                        GeneralParams[] generalParams1 = new GeneralParams[3];
                        generalParams1[0] = new GeneralParams("inUserId", 200, GeneralDatabaseTypes.VarChar, preferredUserId, ParameterDirection.Input);
                        generalParams1[1] = new GeneralParams("inOTP", 200, GeneralDatabaseTypes.VarChar, OTP, ParameterDirection.Input);
                        generalParams1[2] = new GeneralParams("inAction", 200, GeneralDatabaseTypes.VarChar, "delete", ParameterDirection.Input);
                        OracleDBManager.ExecuteSP(storedProcedure2, generalParams1, AppConfiguration.ConnString, String.Empty);
                        return true;
                    }                        
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        public string forgotPassword(string UserId)
        {
            //Generate OTP
            Random _rdm = new Random();
            string newPassword = _rdm.Next(10000, 99999).ToString() + "_" + UserId;            

            string storedProcedure2 = "PKGUSERS.changePassword";
            GeneralParams[] generalParams1 = new GeneralParams[2];
            generalParams1[0] = new GeneralParams("inUserId", 200, GeneralDatabaseTypes.VarChar, UserId, ParameterDirection.Input);
            generalParams1[1] = new GeneralParams("inNewPassword", 200, GeneralDatabaseTypes.VarChar, AESEncryption.Encrypt(newPassword), ParameterDirection.Input);            
            OracleDBManager.ExecuteSP(storedProcedure2, generalParams1, AppConfiguration.ConnString, String.Empty);
            return newPassword;
        }
        public bool changePassword(string UserId, string NewPassword)
        {            
            string storedProcedure2 = "PKGUSERS.changePassword";
            GeneralParams[] generalParams1 = new GeneralParams[2];
            generalParams1[0] = new GeneralParams("inUserId", 200, GeneralDatabaseTypes.VarChar, UserId, ParameterDirection.Input);
            generalParams1[1] = new GeneralParams("inNewPassword", 200, GeneralDatabaseTypes.VarChar, AESEncryption.Encrypt(NewPassword), ParameterDirection.Input);
            OracleDBManager.ExecuteSP(storedProcedure2, generalParams1, AppConfiguration.ConnString, String.Empty);
            return true;
        }
        public DataTable getProfileAndBasics(string userId)
        {

            string storedProcedure = "PKGUSERS.getProfileAndBasics";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[2];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, userId, ParameterDirection.Input);                
                generalParams[1] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool saveProfileAndBasics(string UserId, BusinessLogic.Models.UserModel.ProfileAndBasic objProfileAndBasic)
        {
            string storedProcedure = "PKGUSERS.saveProfileAndBasics";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[10];
                generalParams[0] = new GeneralParams("inUserId", 100, GeneralDatabaseTypes.VarChar, UserId, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inCateogry", 100, GeneralDatabaseTypes.VarChar, objProfileAndBasic.Category, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("inName", 100, GeneralDatabaseTypes.VarChar, objProfileAndBasic.Name, ParameterDirection.Input);
                generalParams[3] = new GeneralParams("inEmail", 100, GeneralDatabaseTypes.VarChar, objProfileAndBasic.Email, ParameterDirection.Input);
                generalParams[4] = new GeneralParams("inMobNo", 100, GeneralDatabaseTypes.VarChar, objProfileAndBasic.MobNo, ParameterDirection.Input);
                generalParams[5] = new GeneralParams("inGender", 100, GeneralDatabaseTypes.VarChar, objProfileAndBasic.Gender, ParameterDirection.Input);
                generalParams[6] = new GeneralParams("inAge", 100, GeneralDatabaseTypes.VarChar, objProfileAndBasic.Age, ParameterDirection.Input);
                generalParams[7] = new GeneralParams("inHeight", 100, GeneralDatabaseTypes.VarChar, objProfileAndBasic.Height, ParameterDirection.Input);
                generalParams[8] = new GeneralParams("inWeight", 100, GeneralDatabaseTypes.VarChar, objProfileAndBasic.Weight, ParameterDirection.Input);
                generalParams[9] = new GeneralParams("inDiseases", 100, GeneralDatabaseTypes.VarChar, objProfileAndBasic.Disease, ParameterDirection.Input);                
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable getSystemConfiguration()
        {

            string storedProcedure = "PKGCONFIGURATION.GetSystemConfig";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[1];                
                generalParams[0] = new GeneralParams("OUTCURSOR", 0, GeneralDatabaseTypes.Cursor, null, ParameterDirection.Output);
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return objDs.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool saveSystemConfiguration(string ClientID, string Token, string UseMobNo)
        {
            string storedProcedure = "PKGCONFIGURATION.SaveSystemConfig";
            try
            {
                DataSet objDs;
                GeneralParams[] generalParams = new GeneralParams[3];
                generalParams[0] = new GeneralParams("inClienID", 100, GeneralDatabaseTypes.VarChar, ClientID, ParameterDirection.Input);
                generalParams[1] = new GeneralParams("inToken", 100, GeneralDatabaseTypes.VarChar, Token, ParameterDirection.Input);
                generalParams[2] = new GeneralParams("inMobNo", 100, GeneralDatabaseTypes.VarChar, UseMobNo, ParameterDirection.Input);                
                objDs = OracleDBManager.ExecuteSP(storedProcedure, generalParams, AppConfiguration.ConnString, String.Empty);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
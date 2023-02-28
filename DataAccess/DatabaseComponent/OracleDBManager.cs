//using Logging;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using OracleDataAccessLayer.Enumeration;
using System;
using System.Data;
using System.Threading.Tasks;

namespace OracleDataAccessLayer.DatabaseComponent
{
    /// <summary>
    /// Static class as database handler to execute queries. On each query execution, a connection is opened, closed, and disposed properly.
    /// </summary>
    public static class OracleDBManager
    {
        private const string ClassName = nameof(OracleDBManager);
      

        /// <summary>
        /// This executes stored procedure with given parameters against provided database (connection string)
        /// </summary>
        /// <param name="storedProcedure">Has value like packageName.storedprocedure</param>
        /// <param name="parameters"> The parameters required for stored procedure</param>
        /// <param name="connectionString">The decrypted connection string</param>
        /// <param name="actor">Unique identifier for logs</param>
        /// <returns>Returns number of affected rows when awaited</returns>
        public static int ExecuteNonQuery(string storedProcedure, GeneralParams[] parameters, string connectionString, string actor)
        {

            int num = -1;
            using var oracleConnection = OpenConnection(connectionString, actor);
            try
            {
                using var command = CreateCommand(oracleConnection, storedProcedure, parameters, CommandType.StoredProcedure, actor);
                num = command.ExecuteNonQuery();
                SetParameters(ref parameters, command, actor);
                oracleConnection.Close();
            }
            catch (Exception ex)
            {
                //FileLogger.Error($"{ClassName}.{nameof(ExecuteNonQuery)}", actor, $"storedProcedure: [{storedProcedure}]", ex);
            }
            finally
            {
                /*
                * This check was used to prevent another exception. Assuming connection could not open then it would throw exception on closing it.
                */
                //FileLogger.Trace($"{ClassName}.{nameof(ExecuteNonQuery)}", actor, $"ConnectionState: [{oracleConnection?.State}]");
                if (oracleConnection?.State != ConnectionState.Closed)
                    oracleConnection.Close();
            }
            return num;
        }

        /// <summary>
        /// This executes stored procedure with given parameters against provided database (connection string)
        /// </summary>
        /// <param name="storedProcedure">Has value like packageName.storedprocedure</param>
        /// <param name="parameters"> The parameters required for stored procedure</param>
        /// <param name="connectionString">The decrypted connection string</param>
        /// <param name="actor">Unique identifier for logs</param>
        /// <returns>Returns dataset</returns>
        public static DataSet ExecuteSP(string storedProcedure, GeneralParams[] parameters, string connectionString, string actor)
        {
            using var oracleConnection = OpenConnection(connectionString, actor);
            DataSet dataSet = new DataSet();
            try
            {

                using var command = CreateCommand(oracleConnection, storedProcedure, parameters, CommandType.StoredProcedure, actor);
                var oracleDataAdapter = new OracleDataAdapter(command);
                oracleDataAdapter.Fill(dataSet);
                SetParameters(ref parameters, command, actor);
                oracleConnection.Close();
            }
            catch (Exception ex)
            {
                //FileLogger.Error($"{ClassName}.{nameof(ExecuteSP)}", actor, $"storedProcedure: [{storedProcedure}]", ex);
            }
            finally
            {
                /*
                * This check was used to prevent another exception. Assuming connection could not open then it would throw exception on closing it.
                */
                //FileLogger.Trace($"{ClassName}.{nameof(ExecuteSP)}", actor, $"ConnectionState: [{oracleConnection?.State}]");
                if (oracleConnection?.State != ConnectionState.Closed)
                    oracleConnection.Close();
            }
            return dataSet;
        }


        private static OracleCommand CreateCommand(OracleConnection connection, string storedProcedure, GeneralParams[] parameters, CommandType commandType, string actor)
        {
            var oracleCommand = new OracleCommand(storedProcedure, connection);
            try
            {
                oracleCommand.CommandType = commandType;
                if (parameters != null)
                {
                    foreach (var parameter in GetParameters(parameters, actor))
                        oracleCommand.Parameters.Add(parameter);
                }

            }
            catch (Exception ex)
            {
                //FileLogger.Error($"{ClassName}.{nameof(CreateCommand)}", actor, ex);
            }
            return oracleCommand;
        }

        private static OracleDbType GetOracleDbType(GeneralDatabaseTypes type) => type switch
        {
            GeneralDatabaseTypes.VarChar => OracleDbType.Varchar2,
            GeneralDatabaseTypes.Int => OracleDbType.Int32,
            GeneralDatabaseTypes.DateTime => OracleDbType.Date,
            GeneralDatabaseTypes.Decimal => OracleDbType.Decimal,
            GeneralDatabaseTypes.Cursor => OracleDbType.RefCursor,
            GeneralDatabaseTypes.Text => OracleDbType.Clob,
            GeneralDatabaseTypes.Blob => OracleDbType.Blob,
            GeneralDatabaseTypes.Char => OracleDbType.Char,
            GeneralDatabaseTypes.Number => OracleDbType.Int64,
            GeneralDatabaseTypes.XML => OracleDbType.XmlType,
            _ => OracleDbType.Varchar2 // default case
        };

        private static OracleParameter[] GetParameters(GeneralParams[] parameters, string actor)
        {
            var oracleParameterArray = new OracleParameter[parameters.Length];
            try
            {
                for (int index = 0; index < parameters.Length; ++index)
                    oracleParameterArray[index] = new OracleParameter(parameters[index].ParamName, GetOracleDbType(parameters[index].ParamDBType), parameters[index].Size, parameters[index].ParamDirection, false, parameters[index].Precision, parameters[index].Scale, "", DataRowVersion.Current, parameters[index].InputValue);

            }
            catch (Exception ex)
            {
                //FileLogger.Error($"{ClassName}.{nameof(GetParameters)}", actor, ex);
            }
            return oracleParameterArray;
        }

        private static OracleConnection OpenConnection(string connectionString, string actor)
        {
            var oracleConnection = new OracleConnection(connectionString);
            try
            {
                oracleConnection.Open();

            }
            catch (Exception ex)
            {
                //FileLogger.Error($"{ClassName}.{nameof(OpenConnection)}", actor, ex);
            }
            return oracleConnection;
        }

        private static void SetParameters(ref GeneralParams[] parameters, OracleCommand command, string actor)
        {
            try
            {
                if (parameters == null)
                    return;
                foreach (var generalParam in parameters)
                {
                    if (generalParam.ParamDirection == ParameterDirection.Output || generalParam.ParamDirection == ParameterDirection.ReturnValue)
                    {
                        if (generalParam.ParamDBType == GeneralDatabaseTypes.Text)
                        {
                            try
                            {
                                generalParam.OutputValue = ((OracleClob)command.Parameters[generalParam.ParamName].Value).Value;
                            }
                            catch (OracleNullValueException ex)
                            {
                                //FileLogger.Error($"{ClassName}.{nameof(SetParameters)}", "", $"generalParam?.ParamName= {generalParam?.ParamName}", ex);
                                generalParam.OutputValue = string.Empty;
                            }
                        }
                        else
                        {
                            generalParam.OutputValue = command.Parameters[generalParam.ParamName].Value.ToString();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                //FileLogger.Error($"{ClassName}.{nameof(SetParameters)}", actor, ex);
            }
        }

    }
}

using OracleDataAccessLayer.Enumeration;
using System;
using System.Data;

namespace OracleDataAccessLayer.DatabaseComponent
{
    public class GeneralParams
    {
        private int iSize;
        private string sParamName;
        private GeneralDatabaseTypes eParamDBType;
        private ParameterDirection sParamDirection;
        private byte bPrecision;
        private string sResult;
        private byte bScale;

        public GeneralParams(string Name, int Size, GeneralDatabaseTypes Type, object Value, ParameterDirection Direction)
        {
            try
            {
                this.ParamName = Name;
                this.Size = Size;
                this.ParamDBType = Type;
                this.InputValue = Value;
                this.ParamDirection = Direction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GeneralParams(string Name, int Size, GeneralDatabaseTypes Type, object Value, ParameterDirection Direction, byte Precision, byte Scale)
        {
            try
            {
                this.ParamName = Name;
                this.Size = Size;
                this.ParamDBType = Type;
                this.InputValue = Value;
                this.ParamDirection = Direction;
                this.Precision = Precision;
                this.Scale = Scale;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ParamName
        {
            get
            {
                return this.sParamName;
            }
            set
            {
                this.sParamName = value;
            }
        }

        public int Size
        {
            get
            {
                return this.iSize;
            }
            set
            {
                this.iSize = value;
            }
        }

        public GeneralDatabaseTypes ParamDBType
        {
            get
            {
                return this.eParamDBType;
            }
            set
            {
                this.eParamDBType = value;
            }
        }

        public object InputValue
        {
            get
            {
                return this.ParamValue;
            }
            set
            {
                this.ParamValue = value;
            }
        }

        public ParameterDirection ParamDirection
        {
            get
            {
                return this.sParamDirection;
            }
            set
            {
                this.sParamDirection = value;
            }
        }

        public byte Precision
        {
            get
            {
                return this.bPrecision;
            }
            set
            {
                this.bPrecision = value;
            }
        }

        public byte Scale
        {
            get
            {
                return this.bScale;
            }
            set
            {
                this.bScale = value;
            }
        }

        public string OutputValue
        {
            get
            {
                return this.sResult;
            }
            set
            {
                this.sResult = value;
            }
        }

        public object ParamValue { get; private set; }
    }
}

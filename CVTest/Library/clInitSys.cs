#region Header
///----------------------------------------------------------------------------------------------------
///
/// FILE NAME: clInitSys
///
///	DESCRIPTION:
///	
///    History
///	***********************************************************************************************
///     Date            Editor      Version         Description
///	***********************************************************************************************
///   
///----------------------------------------------------------------------------------------------------
///         
///
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Mirle.Def;
using Mirle.Logger;

namespace CVTest
{
    public class clsInitSys
    {
        //public static clsDbConfig DbConfig = new clsDbConfig();
        public static clsPlcConfig[] CV_Config = new clsPlcConfig[4];
        public static clsPlcConfig S800_Config = new clsPlcConfig();
        public static WebApiConfig WCSApi_Config = new WebApiConfig();
        public static WebApiConfig SMTC_Config = new WebApiConfig();
        public static AutoArchive archive = new AutoArchive();
        public static bool ByPassSts;

        //API
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString
            (string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// For LOG
        /// </summary>
        public static Log gobjLog = new Log();

        //系統
        public const string gstrSYS_USER = "ADM";
        public const string gstrSYS_NAME = "Admin";

        //登入User資訊
        public static string gstrLoginUser_ID = "";           //使用者編號
        public static string gstrLoginUser_Pwd = "";
        public static string gstrLoginUser_Name = "";         //使用者名稱
        public static string gstrSysPath = "";                //參數位置
        public static string gstrAPFont = "微軟正黑體";       //系統字型
        public static int gintAPFontSizeDSP = 10;             //系統字型SIZE FOR DISPLAY
        public static int gintAPFontSizeEDT = 10;             //系統字型SIZE FOR EDIT
        public static int gintCacheTimeOut = 60;              //Cache Timeout (分鐘)
        public static int gintLogInTimeOut = 10;

        public static void LogOut()
        {
            gstrLoginUser_ID = "";
            gstrLoginUser_Pwd = "";
            gstrLoginUser_Name = "";
        }
        
        //For SYSCODE Struct Define
        public struct stuSYSCODE
        {
            public string Code;
            public string CodeName;
            public bool IsDefault;
        }
        public static Dictionary<string, List<stuSYSCODE>> gdicSYSCODE = new Dictionary<string, List<stuSYSCODE>>();
        public static Dictionary<string, List<stuSYSCODE>> gdicDEFCODE = new Dictionary<string, List<stuSYSCODE>>();

        //程式使用權
        public static Dictionary<string, stuProgInfo> gdicProgList = new Dictionary<string, stuProgInfo>();
        public struct stuProgInfo
        {
            public string Type;
            public string ProgID;
            public string ProgName;
            public string Directory;
            public string PrgAuthorize;
            public void Clear()
            {
                Type = "";
                ProgID = "";
                Directory = "";
                PrgAuthorize = "";
                ProgName = "";
            }
        }

        //欄位名稱
        public static Dictionary<string, stuColumnName> gdicColumnName = new Dictionary<string, stuColumnName>();
        public struct stuColumnName
        {
            public string ColumnName;
            public string Lang;
            public int Length;
            public bool Visible;
            public void Clear()
            {
                ColumnName = "";
                Lang = "";
                Length = 0;
                Visible = false;
            }
        }

        public static Dictionary<string, List<stuStnNo>> gdicStnNo = new Dictionary<string, List<stuStnNo>>();
        public static Dictionary<string, List<stuStnNo>> gdicStnNo_all = new Dictionary<string, List<stuStnNo>>();
        public struct stuStnNo
        {
            public string StnNo;
            public string StnName;
        }

        //DataGirdView Page Size
        public static int gintPageSize = 100;

        #region AP Start
        public static void subInitSystem()
        {
            try
            {
                for (int i = 1; i <= 4; i++)
                {
                    CV_Config[i-1] = new clsPlcConfig();
                }
                SubLoadCVSysIni();
                archive.Start();
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }
        #endregion

        #region SysIni
       
        public static void SubLoadCVSysIni()
        {
            try
            {
                string sFileName = null;

                sFileName = Application.StartupPath + "\\Config\\ASRS.ini";

                if (!File.Exists(sFileName))
                {
                    MessageBox.Show("找不到.ini資料，請洽系統管理人員 !!", "MIRLE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    Environment.Exit(0);
                }

                //subGetDBConfig(sFileName);
                for (int i = 1; i <= 4; i++)
                {
                    subGetCVConfig(i, sFileName, ref CV_Config[i-1]);
                }
                subGetBypassStatusConfig(sFileName);
                subGetAPIConfig(sFileName);
                subGetS800Config(sFileName, ref S800_Config);

                FunWriTraceLog_CV("設定PLC連線完成......");
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }
        public static string[] gsBuffer_ErrCode = new string[16];

        //public static void SubLoadSysIni_Alarm()
        //{
        //    string sFileName = null;
        //    string sAppName = ""; string sKeyName = "";

        //    //Initial Data
        //    for (int i = 0; i <= gsBuffer_ErrCode.Length - 1; i++)
        //    {
        //        gsBuffer_ErrCode[i] = "";
        //    }

        //    sFileName = Application.StartupPath + "\\Config\\ALARM_CODE.ini";
        //    if (!File.Exists(sFileName)) { return; }

        //    //Get Parameter
        //    sAppName = "Buffer_Error_Code";
        //    for (int i = 0; i <= gsBuffer_ErrCode.Length - 1; i++)
        //    {
        //        sKeyName = "BIT" + i.ToString();
        //        gsBuffer_ErrCode[i] = funReadParam(sFileName, sAppName, sKeyName);
        //    }
        //}

        

        //public static void subGetDBConfig(string strIniPathName, string strAppName = "Data Base")
        //{
        //    string strKeyName;
        //    try
        //    {
        //        #region ASRS DB INFO
        //        //把列舉型別常數名稱轉回列舉
        //        string strTemp = funReadParam(strIniPathName, strAppName, "DBMS");
        //        switch (strTemp)
        //        {
        //            case clsDef.DB_Type.SqlServer:
        //                DbConfig.DBType = DBTypes.SqlServer;
        //                break;
        //            case clsDef.DB_Type.OracleClient:
        //                DbConfig.DBType = DBTypes.OracleClient;
        //                break;
        //            case clsDef.DB_Type.SQLite:
        //                DbConfig.DBType = DBTypes.SQLite;
        //                break;
        //            default:
        //                throw new Exception($"DBMS的名稱有誤 => {strTemp}");
        //        }

        //        DbConfig.DbServer = funReadParam(strIniPathName, strAppName, "DbServer");
        //        DbConfig.FODBServer = funReadParam(strIniPathName, strAppName, "FODbServer");
        //        DbConfig.DbName = funReadParam(strIniPathName, strAppName, "DataBase");
        //        DbConfig.DbUser = funReadParam(strIniPathName, strAppName, "DbUser");
        //        DbConfig.DbPassword = funReadParam(strIniPathName, strAppName, "DbPswd");
        //        strKeyName = "DBPort";
        //        DbConfig.DbPort = int.Parse(funReadParam(strIniPathName, strAppName, strKeyName));

        //        strKeyName = "ConnectTimeOut";
        //        DbConfig.ConnectTimeOut = int.Parse(funReadParam(strIniPathName, strAppName, strKeyName));

        //        strKeyName = "CommandTimeOut";
        //        DbConfig.CommandTimeOut = int.Parse(funReadParam(strIniPathName, strAppName, strKeyName));
        //        DbConfig.WriteLog = true;
        //        #endregion ASRS DB INFO
        //    }
        //    catch (Exception ex)
        //    {
        //        var cmet = System.Reflection.MethodBase.GetCurrentMethod();
        //        subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
        //    }
        //}

       

        public static void subGetCVConfig(int plcNo, string strIniPathName, ref clsPlcConfig plcConfig, string strAppName = "CV PLC")
        {
            string strKeyName;
            try
            {
                string tempstrAppName = strAppName + Convert.ToString(plcNo);
                strKeyName = "MPLCNo";
                plcConfig.MPLCNo = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName));
                strKeyName = "MPLCIP";
                plcConfig.MPLCIP = funReadParam(strIniPathName, tempstrAppName, strKeyName);
                strKeyName = "MPLCPort";
                plcConfig.MPLCPort = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName));
                strKeyName = "MPLCTimeout";
                plcConfig.MPLCTimeout = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName));
                strKeyName = "UseMCProtocol";
                plcConfig.UseMCProtocol = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName)) == 1 ? true : false;
                strKeyName = "InMemorySimulator";
                plcConfig.InMemorySimulator = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName)) == 1 ? true : false;


            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }
        public static void subGetS800Config(string strIniPathName, ref clsPlcConfig plcConfig, string strAppName = "S800 PLC")
        {
            string strKeyName;
            try
            {
                string tempstrAppName = strAppName;
                strKeyName = "MPLCNo";
                plcConfig.MPLCNo = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName));
                strKeyName = "MPLCIP";
                plcConfig.MPLCIP = funReadParam(strIniPathName, tempstrAppName, strKeyName);
                strKeyName = "MPLCPort";
                plcConfig.MPLCPort = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName));
                strKeyName = "MPLCTimeout";
                plcConfig.MPLCTimeout = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName));
                strKeyName = "UseMCProtocol";
                plcConfig.UseMCProtocol = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName)) == 1 ? true : false;
                strKeyName = "InMemorySimulator";
                plcConfig.InMemorySimulator = int.Parse(funReadParam(strIniPathName, tempstrAppName, strKeyName)) == 1 ? true : false;


            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }
        public static void subGetBypassStatusConfig(string strIniPathName, string strAppName = "BYPASS CHECK")
        {
            string strKeyName;
            try
            {
                strKeyName = "ByPassStatus";
                string tempSts = funReadParam(strIniPathName, strAppName, strKeyName);
                ByPassSts = tempSts == clsConstValue.CheckBool.FALSE ? false : true;

            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }
        public static void subGetAPIConfig(string strIniPathName, string strAppName = "WCS_API")
        {
            string strKeyName;
            try
            {
                strKeyName = "IP";
                WCSApi_Config.IP = funReadParam(strIniPathName, strAppName, strKeyName);
                strAppName = "SMTC_API";
                SMTC_Config.IP = funReadParam(strIniPathName, strAppName, strKeyName);
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }

        /// <summary>
        /// 讀取ini檔的單一欄位
        /// </summary>
        /// <param name="sFileName">INI檔檔名</param>
        /// <param name="sAppName">區段名</param>
        /// <param name="sKeyName">KEY名稱</param>
        /// <param name="strDefault">Default</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string funReadParam(string sFileName, string sAppName, string sKeyName, string strDefault = "")
        {
            StringBuilder sResult = new StringBuilder(255);

            int intResult;

            try
            {
                intResult = GetPrivateProfileString(sAppName, sKeyName, strDefault, sResult, sResult.Capacity, sFileName);
                return sResult.ToString().Trim();
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
                return strDefault;
            }
        }
        #endregion

        #region Security
        /// <summary>
        /// 取得程式功能權限清單
        /// </summary>
        /// <param name="sAPID"></param>
        /// <returns></returns>
        public static string[] gstrProgSecurity(string sAPID)
        {
            try
            {
                if (gdicProgList.ContainsKey(sAPID))
                    return gdicProgList[sAPID].PrgAuthorize.Trim().ToUpper().Split(new char[] { ',' });
                else
                    return "Q".Trim().ToUpper().Split(new char[] { ',' });
            }
            catch
            {
                return "Q".Trim().ToUpper().Split(new char[] { ',' });
            }
        }
        #endregion

        #region Other
        /// <summary>
        /// 記錄Exception Try Catch
        /// </summary>
        /// <param name="strFunSubName">Function Sub Name</param>
        /// <param name="strMsg">Message</param>
        /// <remarks></remarks>
        public static void subWriteExLog(string strFunSubName, string strMsg)
        {
            try
            {
                gobjLog.WriteLogFile("WCS_Exception.log", strFunSubName.PadRight(60, ' ') + ":" + strMsg);
            }
            catch (FieldAccessException exFile)
            {
                MessageBox.Show("clsComSubFun.subWriteExLog Exception:" + exFile.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Write Trace Log (周邊)
        /// </summary>
        /// <param name="sValue">Trace Msg</param>
        public static void FunWriTraceLog_CV(string sValue)
        {
            try
            {
                gobjLog.WriteLogFile("SMTC_Trace_" + DateTime.Now.ToString("yyyyMMddHH") + ".log", sValue);
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }

        public static void FunWriTraceLog_Remark(string sValue)
        {
            try
            {
                gobjLog.WriteLogFile("SMTC_Trace_Remark_" + DateTime.Now.ToString("yyyyMMddHH") + ".log", sValue);
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }

        public static void FunWriIPC_EventLog(string sValue)
        {
            try
            {
                gobjLog.WriteLogFile("IPC_Comu_" + DateTime.Now.ToString("yyyyMMddHH") + ".log", sValue);
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }

        /// <summary>
        /// UI Click Button的記錄
        /// </summary>
        /// <param name="sValue">Trace Msg</param>
        public static void FunWriUI_ClickButton(string sValue)
        {
            try
            {
                gobjLog.WriteLogFile("UI_ClickButton.log", sValue);
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }

        /// <summary>
        /// Write Alarm Log (周邊)
        /// </summary>
        /// <param name="sValue"></param>
        public static void FunWriAlarmLog_CV(string sValue)
        {
            try
            {
                gobjLog.WriteLogFile("WCS_Alarm.log", sValue);
            }
            catch (Exception ex)
            {
                var cmet = System.Reflection.MethodBase.GetCurrentMethod();
                subWriteExLog(cmet.DeclaringType.FullName + "." + cmet.Name, ex.Message);
            }
        }
        #endregion
    }
}

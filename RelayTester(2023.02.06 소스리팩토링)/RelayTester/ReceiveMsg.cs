using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Office.Interop.Excel;

namespace RelayTester
{
    public class ReceiveMsg
    {
       
        DbLink Dblink = new DbLink();

        DataSet mainDS = new DataSet();
        DataSet ContactCaliDS = new DataSet();
        DataSet CaliDS = new DataSet();

        public string ErrorChkSeq = string.Empty; //2022.12.27 추가
       

        public ReceiveMsg() 
        {
            //Dblink.ConnCoForm();
        }

        public String[] RMsgMain(string Lot, string strMsg, bool reportChk, string testNum, string testerNum, string relayType)
        {
            
            try
            {
                int icaliseq = 0;

                String[] rtnArray = new String[23];

                int istx = strMsg.IndexOf("10 02");
                int ietx = strMsg.IndexOf("10 03");
                
               

                //본 메시지만 추리기, STX 다음부터 ETX전까지
                string rmsg = strMsg.Substring(istx + 6, strMsg.Length - (strMsg.Length - ietx) - (istx + 6));

                rtnArray[0] = ConvertHexToInt(rmsg.Substring(0, 2));                    //장치ID
                rtnArray[1] = ChkTestType(ConvertHexToString(rmsg.Substring(3, 2)));    //작업종류
                rtnArray[2] = "0";
                rtnArray[3] = "0";
                rtnArray[4] = "0";
                rtnArray[5] = "0";
                rtnArray[6] = "0";
                rtnArray[7] = "0";
                rtnArray[8] = "0";
                rtnArray[9] = "0";
                rtnArray[10] = "0";
                rtnArray[11] = "0";
                rtnArray[12] = "0";
                rtnArray[13] = "0";
                rtnArray[14] = "0";
                rtnArray[15] = "0";
                rtnArray[16] = "0";
                rtnArray[17] = "0";
                rtnArray[18] = "0";
                rtnArray[19] = "0";
                rtnArray[20] = "0";
                rtnArray[21] = "0";
                rtnArray[22] = "0";

                switch (rtnArray[1])
                {
                    case "01":      //동작전류
                        //보정테이블 select
                        string pQueryCali1 = string.Empty;

                        pQueryCali1 = string.Format("EXEC _SCaliQuery '" + ConvertHexToInt(rmsg.Substring(0, 2)) + "', '1'");
                        CaliDS.Clear();
                        Dblink.AllSelect(pQueryCali1, CaliDS);

                        double dOperA = 0;
                        double dOperB = 0;
                        double dDropA = 0;
                        double dDropB = 0;
                        
                        for (int i = 0; i < CaliDS.Tables[0].Rows.Count; i++)
                        {
                            icaliseq = Convert.ToInt32(CaliDS.Tables[0].Rows[i]["CaliSeq"]);
                            dOperA = Convert.ToDouble(CaliDS.Tables[0].Rows[i]["OperA"]);
                            dOperB = Convert.ToDouble(CaliDS.Tables[0].Rows[i]["OperB"]);
                            dDropA = Convert.ToDouble(CaliDS.Tables[0].Rows[i]["DropA"]);
                            dDropB = Convert.ToDouble(CaliDS.Tables[0].Rows[i]["DropB"]);
                        }

                        rtnArray[2] = ConvertHexToInt(rmsg.Substring(6, 2));                    //계전기ID
                        rtnArray[3] = ((Convert.ToDouble(ConvertHexToInt(rmsg.Substring(9, 5))) / 10) * dOperA + dOperB).ToString();
                        rtnArray[4] = ((Convert.ToDouble(ConvertHexToInt(rmsg.Substring(15, 5))) / 10) * dDropA + dDropB).ToString();
                       
                        break;

                    case "02":      //코일저항
                        //보정테이블 select
                        string pQueryCali2 = string.Empty;

                        pQueryCali2 = string.Format("EXEC _SCaliQuery '" + ConvertHexToInt(rmsg.Substring(0, 2)) + "', '1'");


                        CaliDS.Clear();
                        Dblink.AllSelect(pQueryCali2, CaliDS);

                        double dCVoltA = 0;
                        double dCCurrA = 0;
                        double dCResiB = 0;

                        for (int i = 0; i < CaliDS.Tables[0].Rows.Count; i++)
                        {
                            icaliseq = Convert.ToInt32(CaliDS.Tables[0].Rows[i]["CaliSeq"]);
                            dCVoltA = Convert.ToDouble(CaliDS.Tables[0].Rows[i]["CVoltA"]);
                            dCCurrA = Convert.ToDouble(CaliDS.Tables[0].Rows[i]["CCurrA"]);
                            dCResiB = Convert.ToDouble(CaliDS.Tables[0].Rows[i]["CResiB"]);
                        }

                        rtnArray[2] = ConvertHexToInt(rmsg.Substring(6, 2));                    //계전기ID
                        rtnArray[3] = ((Convert.ToDouble(ConvertHexToInt(rmsg.Substring(9, 5))) / 10) * dCVoltA).ToString();
                        rtnArray[4] = ((Convert.ToDouble(ConvertHexToInt(rmsg.Substring(15, 5))) /10000) * dCCurrA).ToString();

                        Console.WriteLine((Convert.ToDouble(ConvertHexToInt(rmsg.Substring(9, 5))) / 10).ToString());
                        Console.WriteLine((Convert.ToDouble(ConvertHexToInt(rmsg.Substring(15, 5))) / 10000).ToString());

                        if (double.IsInfinity((Convert.ToDouble(rtnArray[3]) / Convert.ToDouble(rtnArray[4]))))
                        {
                            rtnArray[5] = "65535";
                        }
                        else if(double.IsNaN((Convert.ToDouble(rtnArray[3]) / Convert.ToDouble(rtnArray[4]))))
                        {
                            rtnArray[5] = "65535";
                        }
                        else
                        {
                            rtnArray[5] = ((Convert.ToDouble(rtnArray[3]) / Convert.ToDouble(rtnArray[4])) + dCResiB).ToString();
                        }
                            break;
                    case "03":     //동작시간
                        rtnArray[2] = ConvertHexToInt(rmsg.Substring(6, 2));                    //계전기ID
                        rtnArray[3] = ConvertHexToInt(rmsg.Substring(9, 2));
                        rtnArray[4] = ConvertHexToInt(rmsg.Substring(12, 2));
                        rtnArray[5] = ConvertHexToInt(rmsg.Substring(15, 2));
                        rtnArray[6] = ConvertHexToInt(rmsg.Substring(18, 2));
                        rtnArray[7] = ConvertHexToInt(rmsg.Substring(21, 2));
                        rtnArray[8] = ConvertHexToInt(rmsg.Substring(24, 2));
                        rtnArray[9] = ConvertHexToInt(rmsg.Substring(27, 2));
                        rtnArray[10] = ConvertHexToInt(rmsg.Substring(30, 2));
                        rtnArray[11] = ConvertHexToInt(rmsg.Substring(33, 2));
                        rtnArray[12] = ConvertHexToInt(rmsg.Substring(36, 2));
                        break;
                    case "04":     //복구시간
                        rtnArray[2] = ConvertHexToInt(rmsg.Substring(6, 2));                    //계전기ID
                        rtnArray[3] = ConvertHexToInt(rmsg.Substring(9, 2));
                        rtnArray[4] = ConvertHexToInt(rmsg.Substring(12, 2));
                        rtnArray[5] = ConvertHexToInt(rmsg.Substring(15, 2));
                        rtnArray[6] = ConvertHexToInt(rmsg.Substring(18, 2));
                        rtnArray[7] = ConvertHexToInt(rmsg.Substring(21, 2));
                        rtnArray[8] = ConvertHexToInt(rmsg.Substring(24, 2));
                        rtnArray[9] = ConvertHexToInt(rmsg.Substring(27, 2));
                        rtnArray[10] = ConvertHexToInt(rmsg.Substring(30, 2));
                        rtnArray[11] = ConvertHexToInt(rmsg.Substring(33, 2));
                        rtnArray[12] = ConvertHexToInt(rmsg.Substring(36, 2));
                        break;

                    case "05":      //접촉저항

                        string[] splitmsg = rmsg.Substring(5).Split(',');

                        for (int i = 0; i < splitmsg.Length; i++)
                        {
                            splitmsg[i] = splitmsg[i].Trim();
                        }
                       
                        //rtnArray[2]는 비워둠
                        rtnArray[3] = (Convert.ToDouble(splitmsg[0].Substring(0, splitmsg[0].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[4] = (Convert.ToDouble(splitmsg[1].Substring(0, splitmsg[1].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[5] = (Convert.ToDouble(splitmsg[2].Substring(0, splitmsg[2].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[6] = (Convert.ToDouble(splitmsg[3].Substring(0, splitmsg[3].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[7] = (Convert.ToDouble(splitmsg[4].Substring(0, splitmsg[4].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[8] = (Convert.ToDouble(splitmsg[5].Substring(0, splitmsg[5].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[9] = (Convert.ToDouble(splitmsg[6].Substring(0, splitmsg[6].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[10] = (Convert.ToDouble(splitmsg[7].Substring(0, splitmsg[7].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[11] = (Convert.ToDouble(splitmsg[8].Substring(0, splitmsg[8].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[12] = (Convert.ToDouble(splitmsg[9].Substring(0, splitmsg[9].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[13] = (Convert.ToDouble(splitmsg[10].Substring(0, splitmsg[10].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[14] = (Convert.ToDouble(splitmsg[11].Substring(0, splitmsg[11].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[15] = (Convert.ToDouble(splitmsg[12].Substring(0, splitmsg[12].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[16] = (Convert.ToDouble(splitmsg[13].Substring(0, splitmsg[13].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[17] = (Convert.ToDouble(splitmsg[14].Substring(0, splitmsg[14].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[18] = (Convert.ToDouble(splitmsg[15].Substring(0, splitmsg[15].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[19] = (Convert.ToDouble(splitmsg[16].Substring(0, splitmsg[16].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[20] = (Convert.ToDouble(splitmsg[17].Substring(0, splitmsg[17].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[21] = (Convert.ToDouble(splitmsg[18].Substring(0, splitmsg[18].IndexOf('.'))) * 0.1).ToString();
                        rtnArray[22] = (Convert.ToDouble(splitmsg[19].Substring(0, splitmsg[19].IndexOf('.'))) * 0.1).ToString();


                        
                        break;

                    case "90":      //카운트클리어
                        rtnArray[2] = ConvertHexToString(rmsg.Substring(6, 2));                    //계전기ID
                        break;

                    case "98":      //에이징(횟수)
                        rtnArray[2] = ConvertHexToString(rmsg.Substring(6, 2));                    //계전기ID
                        break;

                    case "99":      //에이징(시간)
                        rtnArray[2] = ConvertHexToString(rmsg.Substring(6, 2));                    //계전기ID
                        break;

                }


                string PalletId = string.Empty;
                string pQuery = string.Empty;

                if (rtnArray[1] == "99" || rtnArray[1] == "98" || rtnArray[1] == "90")   //에이징(시간), 에이징(횟수), 카운트클리어
                {
                    //Add3에다가 RelayNum 넣어놨음(받은 메시지 업데이트 할때 찾아가기 위해)
                   
                    pQuery = string.Format("EXEC _SFindSendMsgQuery '{0}', '{1}', '', '', '2'", Lot, rtnArray[0]);
                }
                else if (rtnArray[1] == "05")  //접촉저항, 접촉저항기 시힘기 변경으로 인해 개별 로직 타도록 로직 수정함(2022.02.16)
                {
                    
                    pQuery = string.Format("EXEC _SFindSendMsgQuery '{0}', '{1}', '','{2}', '1'", Lot, rtnArray[1], relayType);
                    strMsg = rmsg.Substring(5);
                    
                }
                else
                {
                    DataSet PalletIdDS = new DataSet();
                    PalletIdDS.Clear();
                    string pQueryPalletId = string.Format("EXEC _SJobMasterQuery2 '{0}', '{1}', '{2}','{3}', '1'", Lot, String.Format("{0:00}", Convert.ToInt32(rtnArray[2])), rtnArray[0], relayType);
                    
                    
                    Dblink.AllSelect(pQueryPalletId, PalletIdDS);

                    PalletId = PalletIdDS.Tables[0].Rows[0][0].ToString().ToUpper();

                    pQuery = string.Format("EXEC _SFindSendMsgQuery '{0}', '{1}', '{2}', '{3}', '1'", Lot, rtnArray[1], PalletId, relayType);
                }

                DataSet tempDS = new DataSet();
                string seq = string.Empty;

                tempDS.Clear();
                Dblink.AllSelect(pQuery, tempDS);
                seq = tempDS.Tables[0].Rows[0][0].ToString().ToUpper();
                Console.WriteLine(seq.ToString());

                string pUpdate = string.Empty;

                
                pUpdate = string.Format("EXEC _FJobDtlUpdate {0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},'{21}','{22}',{23},{24},'{25}','{26}', '1'"
                     , seq, 
                     rtnArray[3], 
                     rtnArray[4], 
                     rtnArray[5], 
                     rtnArray[6], 
                     rtnArray[7], 
                     rtnArray[8], 
                     rtnArray[9], 
                     rtnArray[10], 
                     rtnArray[11], 
                     rtnArray[12], 
                     rtnArray[13], 
                     rtnArray[14], 
                     rtnArray[15], 
                     rtnArray[16], 
                     rtnArray[17], 
                     rtnArray[18], 
                     rtnArray[19], 
                     rtnArray[20], 
                     rtnArray[21], 
                     rtnArray[22], 
                     strMsg, 
                     string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), 
                     reportChk, 
                     icaliseq, 
                     testNum, 
                     testerNum
                     );

                Dblink.ModifyMethod(pUpdate);

                ErrorChkSeq = seq;

                return rtnArray;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        //검사유형 판단
        string ChkTestType(string TestType)
        {
            string rtnType = string.Empty;
            string pQuery = string.Empty;

            pQuery = "_STestTypeQuery'" + TestType.ToUpper() + "', '4'";
            mainDS.Clear();

            Dblink.AllSelect(pQuery, mainDS);

            rtnType = mainDS.Tables[0].Rows[0]["Code_dtl"].ToString().ToUpper();
            return rtnType;
        }


        //HEX코드를 숫자로 변환
        public string ConvertHexToInt(string val)
        {
            double rDouble = Convert.ToInt32(val.Replace(" ",""), 16);

            return rDouble.ToString();
        }

        //HEX코드를 문자로 변환
        string ConvertHexToString(string HexValue)
        {
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }

            return StrValue;
        }
    }
}
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace RelayTester
{
    public class CreateMsg
    {
        DbLink Dblink = new DbLink();
        CommonRelay commonRelay = new CommonRelay();
        //FormRelay formRelay = new FormRelay();

        public CreateMsg() 
        {
           
        }

        //errCode, worker 추가 (2022.12.26)
        public byte[] MsgMain(string Lot, string RelayType, string RelayNum, string TestType, string SchedLoc, string Add1, string Add2, string Add3, string errorCode, string worker)
        {
            try
            {
                //메시지 배열
                List<byte> bytestosend = new List<byte>();

                DataSet RelayDS = new DataSet();
                string sRelayCode = string.Empty;
                string sContectU = string.Empty;
                string sContectD = string.Empty;
                string pQueryRelay = string.Empty;

                pQueryRelay = string.Format("EXEC _SRelayQuery '" + RelayType + "', '4'");
                RelayDS.Clear();
                Dblink.AllSelect(pQueryRelay, RelayDS);

                for (int i = 0; i < RelayDS.Tables[0].Rows.Count; i++)
                {
                    sRelayCode = RelayDS.Tables[0].Rows[i]["RelayCode"].ToString(); //계전기 유형
                    sContectU = RelayDS.Tables[0].Rows[i]["ContectU"].ToString();  // 접점형태 U
                    sContectD = RelayDS.Tables[0].Rows[i]["ContectD"].ToString();  // 접점형태 R
                }

                switch (TestType)
                {
                    case "01":      //동작전류, 동작전류는 전압수치여서 100을 곱해준다
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(RelayNum));
                        bytestosend = bytestosend.Concat(StringToByte(ChkTestType(TestType)).ToList()).ToList();
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(SchedLoc));
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(((Convert.ToInt32(Add1) * 100 >> 8) & 0xff)));
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte((Convert.ToInt32(Add1) * 100 & 0xff)));
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(((Convert.ToInt32(Add2) * 100 >> 8) & 0xff)));
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte((Convert.ToInt32(Add2) * 100 & 0xff)));
      
                        bytestosend.Insert(bytestosend.Count, commonRelay.CreateMsgType1(RelayType));        //공통로직 CommonRelay로 뺌(2022.10.26)
                      
                        break;

                    case "02":      //코일저항, 코일저항은 전압수치여서 100을 곱해준다
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(RelayNum));
                        bytestosend = bytestosend.Concat(StringToByte(ChkTestType(TestType)).ToList()).ToList();
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(SchedLoc));
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(((Convert.ToInt32(Add1) * 100 >> 8) & 0xff)));
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte((Convert.ToInt32(Add1) * 100 & 0xff)));
                        break;
                    case "03":      //동작시간, 시간은 받은숫자 그대로 HEX코드 변경해서 보낸다
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(RelayNum));
                        bytestosend = bytestosend.Concat(StringToByte(ChkTestType(TestType)).ToList()).ToList();
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(SchedLoc));
                        bytestosend = bytestosend.Concat(StringToByteArray(sContectU).ToList()).ToList();
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(((Convert.ToInt32(Add1) * 100 >> 8) & 0xff)));
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte((Convert.ToInt32(Add1) * 100 & 0xff)));
                        break;
                    case "04":      //복귀시간, 시간은 받은숫자 그대로 HEX코드 변경해서 보낸다
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(RelayNum));
                        bytestosend = bytestosend.Concat(StringToByte(ChkTestType(TestType)).ToList()).ToList();
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(SchedLoc));
                        bytestosend = bytestosend.Concat(StringToByteArray(sContectD).ToList()).ToList();
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(((Convert.ToInt32(Add1) * 100 >> 8) & 0xff)));
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte((Convert.ToInt32(Add1) * 100 & 0xff)));
                        break;
                    case "05":      //접촉저항
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(RelayNum));
                        bytestosend = bytestosend.Concat(StringToByte(ChkTestType(TestType)).ToList()).ToList();
                        if (sRelayCode.Substring(0, 3) == "DR1" || sRelayCode.Substring(0, 3) == "DR2" || sRelayCode.Substring(0, 3) == "TR1")      //계전기 타입이 무극&유극일때는 0, 자기유지일떄는 1, TR1 추가(2022.10.26)
                        {
                            bytestosend.Insert(bytestosend.Count, 0x30);
                        }
                        else
                        {
                            bytestosend.Insert(bytestosend.Count, 0x31);
                        }
                        bytestosend = bytestosend.Concat(StringToByteArray(sContectU).ToList()).ToList();
                        bytestosend = bytestosend.Concat(StringToByteArray(sContectD).ToList()).ToList();
                        break;
                    case "90":       //카운트 클리어
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(RelayNum));
                        bytestosend = bytestosend.Concat(StringToByte(ChkTestType(TestType)).ToList()).ToList();
                        bytestosend.Insert(bytestosend.Count, 0x31);
                        break;
                    case "98":       //에이징(횟수)
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(RelayNum));
                        bytestosend = bytestosend.Concat(StringToByte(ChkTestType(TestType)).ToList()).ToList();
                        if (Add1 == "1")     //에이징(횟수) 마지막 차수가 아닐경우는 1
                        {
                            bytestosend.Insert(bytestosend.Count, 0x31);   //STATUS를 여기에 넣어서 보냄
                        }
                        else     //에이징(횟수) 마지막 차수일 경우에는 0
                        {
                            bytestosend.Insert(bytestosend.Count, 0x30);   //STATUS를 여기에 넣어서 보냄
                        }
                        break;
                    case "99":       //에이징(시간)
                        bytestosend.Insert(bytestosend.Count, Convert.ToByte(RelayNum));
                        bytestosend = bytestosend.Concat(StringToByte(ChkTestType(TestType)).ToList()).ToList();
                        if (Add1 == "1")     //에이징시작
                        {
                            bytestosend.Insert(bytestosend.Count, 0x31);   //STATUS를 여기에 넣어서 보냄
                        }
                        else     //에이징종료
                        {
                            bytestosend.Insert(bytestosend.Count, 0x30);   //STATUS를 여기에 넣어서 보냄
                        }
                        break;
                }

                // 데이타중 '10' 이 있는 경우 '10 10' 구조로 송신한다
                int Index = 0;
                while (Index >= 0)
                {
                    Index = bytestosend.IndexOf((byte)16, Index);
                    if (Index > 0)
                    {
                        bytestosend.Insert(Index + 1, (byte)16);
                        Index = Index + 2;
                    }
                    else
                    {
                        Index = -1;
                    }
                }

                //STX, ETX 붙이기
                bytestosend.Insert(0, 0x10);
                bytestosend.Insert(1, 0x02);
                bytestosend.Insert(bytestosend.Count, 0x10);
                bytestosend.Insert(bytestosend.Count, 0x03);

                //보낸 메시지 저장을 위해 String으로 만들어줌.
                int intRecSize = bytestosend.Count;
                string[] tempArray = new string[intRecSize];
                string sendMsg = string.Empty;

                for (int iTemp = 0; iTemp < intRecSize; iTemp++)
                {
                    sendMsg += string.Format("{0:X2} ", bytestosend[iTemp]);
                }
                sendMsg = sendMsg.Substring(0, sendMsg.Length - 1);

                string PalletId = string.Empty;
                string pQuery1 = string.Empty;

                if (TestType == "99" || TestType == "98" || TestType == "90")   //에이징(시간), 에이징(횟수), 카운트클리어
                {
                    if (Add1 == "1")     //에이징시작
                    {
                        PalletId = "AGING_START";
                    }
                    else if(Add1 == "0")     //에이징종료
                    {
                        PalletId = "AGING_FINISH";
                    }
                    else if(Add1 == "2")    //카운트 클리어
                    {
                        PalletId = "COUNT_CLEAR";
                    }
                    //Add3에다가 RelayNum 넣음(받은 메시지 업데이트 할때 찾아가기 위해)
                   

                    pQuery1 = string.Format("EXEC _FJobResult '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}'," +
                    "'1'",
                    Lot, 
                    RelayType,
                    PalletId, 
                    TestType, 
                    "", 
                    "", 
                    "", 
                    RelayNum,
                    sendMsg,
                    string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                    errorCode,
                    worker);

                }
                else if( TestType == "05")  //접촉저항, 접촉저항기 시힘기 변경으로 인해 개별 로직 타도록 로직 수정함(2022.02.16)
                {
                    PalletId = Add3;
                    Add3 = "";
                    sendMsg = "READ?";

                    pQuery1 = string.Format("EXEC _FJobResult '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}', " +
                    "'1'",
                    Lot, 
                    RelayType, 
                    PalletId, 
                    TestType, 
                    SchedLoc, 
                    Add1, 
                    Add2,
                    Add3, 
                    sendMsg,
                    string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                    errorCode,
                    worker);

                }
                else
                {
                    DataSet tempDS = new DataSet();
                    tempDS.Clear();
                    //formRelay.Add_Log("msg_mian_1");
                    string pQuery = string.Format("EXEC _SJobMasterQuery2 '{0}', '{1}', '{2}','{3}', '1'", Lot, SchedLoc, RelayNum, RelayType);
                    Dblink.AllSelect(pQuery, tempDS);
                    //string pQuery = string.Format("EXEC _SJobMasterQuery '{0}', '{1}', '{2}', '2'", Lot, SchedLoc, RelayNum); 
                    //string pQuery = string.Format("EXEC _SJobMasterQuery '{0}', '{1}', '{2}', '{3}', '2'", Lot, SchedLoc, RelayNum, RelayType); //01.30 RelayType 추가
                    Dblink.AllSelect(pQuery, tempDS);
                    //formRelay.Add_Log("msg_mian_2");
                    PalletId = tempDS.Tables[0].Rows[0][0].ToString().ToUpper();

                    //마스터에서 끌고온걸로 들어가서 임시로 바꿔줌
                    RelayType = commonRelay.CreateMsgType2(PalletId.Substring(0, 5));        //공통로직 CommonRelay로 뺌(2022.10.26)  //중단점
                   
                    pQuery1 = string.Format("EXEC _FJobResult '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}', '1'",
                    Lot, 
                    RelayType, 
                    PalletId, 
                    TestType, 
                    SchedLoc, 
                    Add1, 
                    Add2, 
                    Add3, 
                    sendMsg,
                    string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second), 
                    errorCode, 
                    worker);

                }

                //마스터 저장 쿼리
                Dblink.ModifyMethod(pQuery1);
                return bytestosend.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        // String을 바이트 배열로 변환 
        private byte[] StringToByte(string strValue)
        {
            byte[] StrByte = Encoding.ASCII.GetBytes(strValue);
            return StrByte;
        }

        string ChkTestType(string TestType)
        {
            DataSet tempDS = new DataSet();

            string rtnType = string.Empty;
            string pQuery = string.Empty;
            
            pQuery = "EXEC _STestTypeQuery '" + TestType + "',  '3'";
            tempDS.Clear();

            Dblink.AllSelect(pQuery, tempDS);

            rtnType = tempDS.Tables[0].Rows[0]["Remark"].ToString().ToUpper();
            return rtnType;
        }

    }
}

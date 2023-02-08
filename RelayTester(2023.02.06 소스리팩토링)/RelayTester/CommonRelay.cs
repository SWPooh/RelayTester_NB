using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RelayTester
{
    public class CommonRelay
    {
        public Boolean InputBarcode(string relayType, string inputBarcode)
        {
            switch (relayType)
            {
                case "01":  //무극
                    if (inputBarcode != "DR100")
                    {
                        return false;
                    }
                    break;

                case "02":  //유극
                    if (inputBarcode != "DR200")
                    {
                        return false;
                    }
                    break;

                case "03":  //자기유지
                    if (inputBarcode != "DR300")
                    {
                        return false;
                    }
                    break;

                case "04":  //유극-저전류
                    if (inputBarcode != "DR211")
                    {
                        return false;
                    }
                    break;

                case "05":  //무극-중부하
                    if (inputBarcode != "DR130")
                    {
                        return false;
                    }
                    break;

                case "06":  //무극-ABS
                    if (inputBarcode != "DR170")
                    {
                        return false;
                    }
                    break;

                case "07":  //무극-PGS
                    if (inputBarcode != "DR190")
                    {
                        return false;
                    }
                    break;

                case "08":  //유극-PGS
                    if (inputBarcode != "DR290")
                    {
                        return false;
                    }
                    break;

                case "09":  //무극-중부하(1-4)
                    if (inputBarcode != "DR131")
                    {
                        return false;
                    }
                    break;

                case "10":  //무극-테크빌
                    if (inputBarcode != "TR100")
                    {
                        return false;
                    }
                    break;
                case "11": //중부하-테크빌
                    if(inputBarcode != "TR130")
                    {
                        return false;
                    }
                    break;
#if (TRADD) 
                case "12": //유극-테크빌
                    if (inputBarcode != "TR200")
                    {
                        return false;
                    }
                    break;
                case "13": //자기유지-테크빌
                    if (inputBarcode != "TR300")
                    {
                        return false;
                    }
                    break;
#endif //TRADD1
            }

            return true;
        }

        public Color BtnColor(string barC)
        {
            Color btnColor = Color.Black; ;

            switch (barC)
            {
                case "DR1":  //무극
                    btnColor = Color.FromArgb(100, 165, 42, 42);
                    break;
                case "TR1":  //무극-테크빌
                    btnColor = Color.FromArgb(100, 165, 42, 42);
                    break;
                case "DR2":  //유극
                    btnColor = Color.FromArgb(100, 0, 0, 139);
                    break;
                case "DR3": //자기유지
                    btnColor = Color.FromArgb(100, 0, 128, 0);
                    break;
#if (TRADD)
                case "TR2":  //유극 - 테크빌
                    btnColor = Color.FromArgb(100, 0, 0, 139);
                    break;
                case "TR3":  //자기유지 - 테크빌
                    btnColor = Color.FromArgb(100, 0, 0, 139);
                    break;
#endif
            }

            return btnColor;

        }






        public string[] ResultColName1(string relayCode)    //동작전류
        {
            string[] rtn = new string[4];

            switch (relayCode)
            {
                case "DR100":   //무극
                    rtn[0] = "CNOMin";
                    rtn[1] = "CNOMax";
                    rtn[2] = "CNDMin";
                    rtn[3] = "CNDMax";
                    break;

                case "TR100":   //무극 - 테크빌
                    rtn[0] = "CNOMin";
                    rtn[1] = "CNOMax";
                    rtn[2] = "CNDMin";
                    rtn[3] = "CNDMax";
                    break;

                case "DR130":   //무극 - 중부하
                    rtn[0] = "CNOMin";
                    rtn[1] = "CNOMax";
                    rtn[2] = "CNDMin";
                    rtn[3] = "CNDMax";
                    break;

                case "DR131":   //무극 - 중부하(1-4)
                    rtn[0] = "CNOMin";
                    rtn[1] = "CNOMax";
                    rtn[2] = "CNDMin";
                    rtn[3] = "CNDMax";
                    break;

                case "DR170":   //무극 - ABS
                    rtn[0] = "CNOMin";
                    rtn[1] = "CNOMax";
                    rtn[2] = "CNDMin";
                    rtn[3] = "CNDMax";
                    break;

                case "DR190":   //무극 - PGS
                    rtn[0] = "CNOMin";
                    rtn[1] = "CNOMax";
                    rtn[2] = "CNDMin";
                    rtn[3] = "CNDMax";
                    break;

                case "DR200":   //유극
                    rtn[0] = "CPOMin";
                    rtn[1] = "CPOMax";
                    rtn[2] = "CPDMin";
                    rtn[3] = "CPDMax";
                    break;

                case "DR211":   //유극-저전류
                    rtn[0] = "CPOLMin";
                    rtn[1] = "CPOLMax";
                    rtn[2] = "CPDLMin";
                    rtn[3] = "CPDLMax";
                    break;

                case "DR290":   //유극-PGS
                    rtn[0] = "CPOMin";
                    rtn[1] = "CPOMax";
                    rtn[2] = "CPDMin";
                    rtn[3] = "CPDMax";
                    break;

                case "DR300":   //자기유지
                    rtn[0] = "CSOMin";
                    rtn[1] = "CSOMax";
                    rtn[2] = "CSDMin";
                    rtn[3] = "CSDMax";
                    break;

                case "TR130":   //중부하 - 테크빌
                    rtn[0] = "CNOMin";
                    rtn[1] = "CNOMax";
                    rtn[2] = "CNDMin";
                    rtn[3] = "CNDMax";
                    break;
#if (TRADD)
                case "TR200": //유극-테크빌
                    rtn[0] = "CPOMin";
                    rtn[1] = "CPOMax";
                    rtn[2] = "CPDMin";
                    rtn[3] = "CPDMax";
                    break;
                case "TR300": //자기유지-테크빌
                    rtn[0] = "CSOMin";
                    rtn[1] = "CSOMax";
                    rtn[2] = "CSDMin";
                    rtn[3] = "CSDMax";
                    break;
#endif //TRADD2
            }

            return rtn;

        }

        public byte CreateMsgType1(string relayCode)    //동작전류
        {
            byte rtn = 0x11;
            switch (relayCode)
            {
                case "01":  //무극
                    rtn = 0x11;
                    break;
                case "02":  //유극
                    rtn = 0x21;
                    break;
                case "03":  //자기유지
                    rtn = 0x31;
                    break;
                case "04":  //유극-저전류
                    rtn = 0x22;
                    break;
                case "05":  //무극-중부하
                    rtn = 0x12;
                    break;
                case "06":  //무극-ABS    *무극이랑 같은 코드 사용
                    rtn = 0x11;
                    break;
                case "07":  //무극-PGS    *무극이랑 같은 코드 사용
                    rtn = 0x11;
                    break;
                case "08":  //유극-PGS    *유극이랑 같은 코드 사용
                    rtn = 0x21;
                    break;
                case "09":  //무극-중부하(1-4)
                    rtn = 0x12;
                    break;
                case "10":  //무극 - 테크빌
                    rtn = 0x11;
                    break;
                case "11":  //무극-중부하-테크빌
                    rtn = 0x12;
                    break;

#if (TRADD)
                case "12": //유극-테크빌
                    rtn = 0x21;
                    break;
                case "13": //자기유지-테크빌
                    rtn = 0x31;
                    break;
#endif //TRADD8
            }

            return rtn;
        }

        public string[] ResultColName2(string relayCode)    //코일저항
        {
            string[] rtn = new string[2];

            switch (relayCode)
            {
                case "DR100":   //무극
                    rtn[0] = "RNMin";
                    rtn[1] = "RNMax";
                    break;

                case "TR100":   //무극 - 테크빌
                    rtn[0] = "RNMin";
                    rtn[1] = "RNMax";
                    break;

                case "DR130":   //무극 - 중부하
                    rtn[0] = "RNMin";
                    rtn[1] = "RNMax";
                    break;

                case "DR131":   //무극 - 중부하(1-4)
                    rtn[0] = "RNMin";
                    rtn[1] = "RNMax";
                    break;

                case "DR170":   //무극 - ABS
                    rtn[0] = "RNMin";
                    rtn[1] = "RNMax";
                    break;

                case "DR190":   //무극 - PGS
                    rtn[0] = "RNMin";
                    rtn[1] = "RNMax";
                    break;

                case "DR200":   //유극
                    rtn[0] = "RPMin";
                    rtn[1] = "RPMax";
                    break;

                case "DR211":   //유극-저전류
                    rtn[0] = "RPLMin";
                    rtn[1] = "RPLMax";
                    break;

                case "DR290":   //유극-PGS
                    rtn[0] = "RPMin";
                    rtn[1] = "RPMax";
                    break;

                case "DR300":   //자기유지
                    rtn[0] = "RSMin";
                    rtn[1] = "RSMax";
                    break;

                case "TR130":   //무극 - 중부하 - 테크빌
                    rtn[0] = "RNMin";
                    rtn[1] = "RNMax";
                    break;
#if (TRADD)
                case "TR200":   //유극-테크빌
                    rtn[0] = "RPMin";
                    rtn[1] = "RPMax";
                    break;
                case "TR300":   //자기유지-테크빌
                    rtn[0] = "RSMin";
                    rtn[1] = "RSMax";
                    break;
#endif //TRADD3
            }

            return rtn;

        }

        public string[] ResultColName3(string relayCode)    //동작시간
        {
            string[] rtn = new string[2];

            switch (relayCode)
            {
                case "DR100":   //무극
                    rtn[0] = "TNRNMin";
                    rtn[1] = "TNRNMax";
                    break;

                case "TR100":   //무극 - 테크빌
                    rtn[0] = "TNRNMin";
                    rtn[1] = "TNRNMax";
                    break;

                case "DR130":   //무극 - 중부하
                    rtn[0] = "TNRNMin";
                    rtn[1] = "TNRNMax";
                    break;

                case "DR131":   //무극 - 중부하(1-4)
                    rtn[0] = "TNRNMin";
                    rtn[1] = "TNRNMax";
                    break;

                case "DR170":   //무극 - ABS
                    rtn[0] = "TNRNMin";
                    rtn[1] = "TNRNMax";
                    break;

                case "DR190":   //무극 - PGS
                    rtn[0] = "TNRNMin";
                    rtn[1] = "TNRNMax";
                    break;

                case "DR200":   //유극
                    rtn[0] = "TPRNMin";
                    rtn[1] = "TPRNMax";
                    break;

                case "DR211":   //유극-저전류
                    rtn[0] = "TPRNLMin";
                    rtn[1] = "TPRNLMax";
                    break;

                case "DR290":   //유극-PGS
                    rtn[0] = "TPRNMin";
                    rtn[1] = "TPRNMax";
                    break;

                case "DR300":   //자기유지
                    rtn[0] = "TSRNMin";
                    rtn[1] = "TSRNMax";
                    break;

                case "TR130":   //무극 - 중부하 - 테크빌
                    rtn[0] = "TNRNMin";
                    rtn[1] = "TNRNMax";
                    break;

#if (TRADD)
                case "TR200":   //유극-테크빌
                    rtn[0] = "TPRNMin";
                    rtn[1] = "TPRNMax";
                    break;

                case "TR300":   //자기유지-테크빌
                    rtn[0] = "TSRNMin";
                    rtn[1] = "TSRNMax";
                    break;

#endif //tradd4
            }

            return rtn;

        }

        public string[] ResultColName4(string relayCode)    //복구시간
        {
            string[] rtn = new string[2];

            switch (relayCode)
            {
                case "DR100":   //무극
                    rtn[0] = "TNNRMin";
                    rtn[1] = "TNNRMax";
                    break;

                case "TR100":   //무극 - 테크빌
                    rtn[0] = "TNNRMin";
                    rtn[1] = "TNNRMax";
                    break;

                case "DR130":   //무극 - 중부하
                    rtn[0] = "TNNRMin";
                    rtn[1] = "TNNRMax";
                    break;

                case "DR131":   //무극 - 중부하(1-4)
                    rtn[0] = "TNNRMin";
                    rtn[1] = "TNNRMax";
                    break;

                case "DR170":   //무극 - ABS
                    rtn[0] = "TNNRMin";
                    rtn[1] = "TNNRMax";
                    break;

                case "DR190":   //무극 - PGS
                    rtn[0] = "TNNRMin";
                    rtn[1] = "TNNRMax";
                    break;

                case "DR200":   //유극
                    rtn[0] = "TPNRMin";
                    rtn[1] = "TPNRMax";
                    break;

                case "DR211":   //유극-저전류
                    rtn[0] = "TPNRLMin";
                    rtn[1] = "TPNRLMax";
                    break;

                case "DR290":   //유극-PGS
                    rtn[0] = "TPNRMin";
                    rtn[1] = "TPNRMax";
                    break;

                case "DR300":   //자기유지
                    rtn[0] = "TSNRMin";
                    rtn[1] = "TSNRMax";
                    break;

                case "TR130":   //무극 - 중부하 - 테크빌
                    rtn[0] = "TNNRMin";
                    rtn[1] = "TNNRMax";
                    break;

#if (TRADD)
                case "TR200":   //유극-테크빌
                    rtn[0] = "TPNRMin";
                    rtn[1] = "TPNRMax";
                    break;

                case "TR300":   //자기유지-테크빌
                    rtn[0] = "TSNRMin";
                    rtn[1] = "TSNRMax";
                    break;

#endif //tradd5
            }

            return rtn;

        }







        public string[] ResultColName5(string relayCode)    //접촉저항
        {
            string[] rtn = new string[2];

            switch (relayCode)
            {
                case "DR100":   //무극
                    rtn[0] = "CRNMin";
                    rtn[1] = "CRNMax";
                    break;

                case "TR100":   //무극 - 테크빌
                    rtn[0] = "CRNMin";
                    rtn[1] = "CRNMax";
                    break;

                case "DR130":   //무극 - 중부하
                    rtn[0] = "CRNMin";
                    rtn[1] = "CRNMax";
                    break;

                case "DR131":   //무극 - 중부하(1-4)
                    rtn[0] = "CRNMin";
                    rtn[1] = "CRNMax";
                    break;

                case "DR170":   //무극 - ABS
                    rtn[0] = "CRNMin";
                    rtn[1] = "CRNMax";
                    break;

                case "DR190":   //무극 - PGS
                    rtn[0] = "CRNMin";
                    rtn[1] = "CRNMax";
                    break;

                case "DR200":   //유극
                    rtn[0] = "CRPMin";
                    rtn[1] = "CRPMax";
                    break;

                case "DR211":   //유극 - 저전류
                    rtn[0] = "CRPLMin";
                    rtn[1] = "CRPLMax";
                    break;

                case "DR290":   //유극 - PGS
                    rtn[0] = "CRPMin";
                    rtn[1] = "CRPMax";
                    break;

                case "DR300":   //자기유지
                    rtn[0] = "CRSMin";
                    rtn[1] = "CRSMax";

                    break;

                case "TR130":   //무극 - 중부하 - 테크빌
                    rtn[0] = "CRNMin";
                    rtn[1] = "CRNMax";
                    break;

#if (TRADD)
                case "TR200":   //유극-테크빌
                    rtn[0] = "CRPMin";
                    rtn[1] = "CRPMax";
                    break;

                case "TR300":   //자기유지-테크빌
                    rtn[0] = "CRSMin";
                    rtn[1] = "CRSMax";
                    break;

#endif //tradd6
            }

            return rtn;

        }

        public Boolean[] ContactResiPoint(string relayCode)    //접촉저항
        {
            Boolean[] rtn = new Boolean[14];

            switch (relayCode)
            {
                case "DR100":   //무극
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "TR100":   //무극 - 테크빌
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "DR130":   //무극-중부하
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "DR131":   //무극-중부하(1-4)
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "DR170":   //무극-ABS
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "DR190":   //무극-PGS
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "DR200":   //유극
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "DR211":   //유극-저전류
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = false;
                    rtn[5] = false;
                    rtn[6] = false;
                    rtn[7] = false;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = false;
                    rtn[13] = false;
                    break;

                case "DR290":   //유극-PGS
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "DR300":   //자기유지
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = false;
                    rtn[5] = false;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "TR130":   //무극-중부하-테크빌
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

#if (TRADD)
                case "TR200":   //유극-테크빌
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = true;
                    rtn[5] = true;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

                case "TR300":   //자기유지-테크빌
                    rtn[0] = true;
                    rtn[1] = true;
                    rtn[2] = true;
                    rtn[3] = true;
                    rtn[4] = false;
                    rtn[5] = false;
                    rtn[6] = true;
                    rtn[7] = true;
                    rtn[8] = true;
                    rtn[9] = true;
                    rtn[10] = true;
                    rtn[11] = true;
                    rtn[12] = true;
                    rtn[13] = true;
                    break;

#endif //tradd7
            }

            return rtn;

        }

        

        public string CreateMsgType2(string relayCode)    //접촉저항
        {
            string rtn = string.Empty;
            switch (relayCode)
            {
                case "DR100":           //무극
                    rtn = "01";
                    break;
                case "TR100":           //무극 - 테크빌
                    rtn = "10";
                    break;
                case "DR200":           //유극
                    rtn = "02";
                    break;
                case "DR300":           //자기유지
                    rtn = "03";
                    break;
                case "DR211":           //유극-저전류
                    rtn = "04";
                    break;
                case "DR130":           //무극-중부하
                    rtn = "05";
                    break;
                case "DR170":           //무극-ABS
                    rtn = "06";
                    break;
                case "DR190":           //무극-PGS
                    rtn = "07";
                    break;
                case "DR290":           //유극-PGS
                    rtn = "08";
                    break;
                case "DR131":           //무극-중부하(1-4)
                    rtn = "09";
                    break;
                case "TR130":           //무극-중부하
                    rtn = "11";
                    break;

#if (TRADD)
                case "TR200": //유극-테크빌
                    rtn = "12";
                    break;
                case "TR300": //자기유지-테크빌
                    rtn = "13";
                    break;
#endif //TRADD9
            }

            return rtn;
        }
    }
}

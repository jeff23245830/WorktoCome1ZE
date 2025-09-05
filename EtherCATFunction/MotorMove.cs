using EtherCAT_DLL;
using EtherCAT_DLL_Err;
using System;
using System.Security.Cryptography;

namespace EtherCATFunction
{
    public class MotorMove
    {
        private ushort g_uRet = 0;
        ushort g_uESCCardNo = 0;
 
        /// <summary>
        /// 單軸移動
        /// </summary>
        /// <param name="nDir">距離</param>
        /// <param name="ChkAbsMove">0相對/1絕對</param>
        /// <param name="nTargetPos">目標位置</param>
        /// <param name="uConstVel"></param>
        /// <param name="uAcceleration">正加速度</param>
        /// <param name="uDeceleration">負加速度</param>
        /// <param name="ESCNodeID"></param>
        /// <param name="ESCSlotID"></param>
        public void AxisMove(int nDir ,bool ChkAbsMove , int nTargetPos , uint uConstVel, uint uAcceleration, uint uDeceleration , ushort ESCNodeID, ushort ESCSlotID)
        { 
            ushort uAbsMove = 0; 

            if (ChkAbsMove == true)
                uAbsMove = 1; // 0：相對位移(Default) 1：絕對位移

            if (nDir == 0)
                nTargetPos = 0 - nTargetPos;

            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_PP_Start_Move(g_uESCCardNo, ESCNodeID, ESCSlotID, nTargetPos, uConstVel, uAcceleration, uDeceleration, uAbsMove);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("CS_ECAT_Slave_PP_Start_Move, ErrorCode = " + g_uRet.ToString(), true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nDir">Left:1/Right:0</param>
        /// <param name="nStrVel">起始速</param>
        /// <param name="nConstVel">最大速</param>
        /// <param name="nEndVel">結束速</param>
        /// <param name="bSCurve">S曲線</param>
        /// <param name="bkAbsMove">0=相對移動、1=絕對位置</param>
        /// <param name="MoveAxesCount">欲移動的軸數</param>
        public void MultiAxesMove(int nDir,ushort[] g_uESCNodeID, ushort[] g_uESCSlotID, int nStrVel, int nConstVel, int nEndVel ,double dTAcc,double dTDec, bool bSCurve ,bool bkAbsMove, ushort MoveAxesCount)
        {
            ushort uDir = 0, uCycleNum = 0, uSCurve = 0, uAbsMove = 0;
            int[] nCenPot = { 0, 0 };
            int[] nEndPot = { 0, 0 };
            int[] nDist = { 0, 0, 0 };
            int[] nDist2 = { 0, 0, 0 };
            string strMsg = "";

            if (bSCurve == true)
                uSCurve = 1;

            if (bkAbsMove == true)
                uAbsMove = 1;

            g_uRet = (ushort)CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR;


            //nDist[0] = (nDir == 0) ? (0 - Convert.ToInt32(TxtParam01.Text)) : (Convert.ToInt32(TxtParam01.Text));
            //nDist[1] = (nDir == 0) ? (0 - Convert.ToInt32(TxtParam02.Text)) : (Convert.ToInt32(TxtParam02.Text));
            

            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Multiaxes_Move(g_uESCCardNo, MoveAxesCount, ref g_uESCNodeID[0], ref g_uESCSlotID[0], ref nDist[0], nStrVel, nConstVel, nEndVel, dTAcc, dTDec, uSCurve, uAbsMove);
            //if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            //    strMsg = "_ECAT_Slave_CSP_Start_Multiaxes_Move, ErrorCode = " + g_uRet.ToString();
            
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg(strMsg);
            }
        }

        //public void StopMove()
        //{

        //    double dTdec;

        //    dTdec = Convert.ToDouble(Deceleration);
        //    g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Sd_Stop(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, dTdec);

        //    if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
        //    {
        //        //AddErrMsg("_ECAT_Slave_Motion_Sd_Stop, ErrorCode = " + g_uRet.ToString(), true);
        //    }
        //}

        public void MultiServoOnOrOff(bool RdoSVON,int g_nSelectAxesCount, ushort[] g_uESCNodeID, ushort[] g_uESCSlotID)
        {
            if (g_uESCNodeID == null || g_uESCSlotID == null) return;
            // 真實可用的軸數：以兩個陣列的最小長度為準；不信 caller 傳的 g_nSelectAxesCount
            int count = Math.Min(g_uESCNodeID.Length, g_uESCSlotID.Length);
            if (count <= 0) return;


            ushort uCheckOnOff = RdoSVON == false ? (ushort)0 : (ushort)1;

            for (int i = 0; i < count; i++)
            {
                int Errocount = 0;
                ushort node = g_uESCNodeID[i];
                ushort slot = g_uESCSlotID[i];

                // 跳過未選的「洞」（例如 X/Z 被選，但 Y/R 沒選時，中間 0 不要下指令）
                if (node == 0) continue;

                ushort rt = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Svon(g_uESCCardNo, node, slot, uCheckOnOff);
                if (rt != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    Errocount++;
                    // TODO: 串你的 logger / 回傳給 UI；類別庫先別彈視窗
                    // Log($"SV{(onOff==1?"ON":"OFF")} Fail @i={i} Node={node}, Slot={slot}, Err=0x{rt:X4}");
                }
            }
        }


        public void ServoOnOrOff(bool RdoSVON, ushort ESCNodeID , ushort ESCSlotID)
        {
            ushort uCheckOnOff = RdoSVON == false ? (ushort)0 : (ushort)1;

            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Svon(g_uESCCardNo, ESCNodeID, ESCSlotID, uCheckOnOff);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("CS_ECAT_Slave_Motion_Set_Svon, Error Code = " + g_uRet.ToString(), true);
            }
        }

        public void MoveHome(ushort ESCNodeID, ushort ESCSlotID, ushort uMode, int nOffset,uint nFV, uint nSV, uint uDeceleration)
        { 
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Home_Config(g_uESCCardNo, ESCNodeID , ESCSlotID, uMode, nOffset, nFV, nSV, uDeceleration);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("_ECAT_Slave_Home_Config, ErrorCode = " + g_uRet.ToString(), true);
            }
            else
            {
                g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Home_Move(g_uESCCardNo, ESCNodeID, ESCSlotID);
                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    //AddErrMsg("_ECAT_Slave_Home_Move, ErrorCode = " + g_uRet.ToString(), true);
                }
            }
        }


        public bool checkDone(ushort ESCNodeID, ushort ESCSlotID)
        {
            ushort uDone = 0;
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Get_Mdone(g_uESCCardNo, ESCNodeID, ESCSlotID, ref uDone);
            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("CS_ECAT_Slave_PP_Get_Done, ErrorCode = " + g_uRet.ToString(), true);
                return false;
            }
            if (uDone == 1)
                return true;
            else
                return false;
        }

    }
}

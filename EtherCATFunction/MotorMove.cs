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
        /// 多軸移動
        /// </summary>
        /// <param name="nDir">方向</param>
        /// <param name="g_uESCNodeID"></param>
        /// <param name="g_uESCSlotID"></param>
        /// <param name="X_Axis">X軸</param>
        /// <param name="Y_Axis">Y軸</param>
        /// <param name="Z_Axis">Z軸</param>
        /// <param name="R_Axis">R軸</param>
        /// <param name="nStrVel">初始速度</param>
        /// <param name="nConstVel">最高速度</param>
        /// <param name="nEndVel">結束速度</param>
        /// <param name="dTAcc">加速度</param>
        /// <param name="dTDec">負加速度</param>
        /// <param name="bSCurve">S曲線 0:不/1:要</param>
        /// <param name="bkAbsMove">絕對:1/相對:0</param>
        /// <param name="MoveAxesCount">軸數量</param>
        public ushort MultiAxesMove(
    int nDir,
    ushort[] g_uESCNodeID, ushort[] g_uESCSlotID,
    int[] moveVals,
    int nStrVel, int nConstVel, int nEndVel,
    double dTAcc, double dTDec,
    bool bSCurve, bool bkAbsMove)
        {
            if (g_uESCNodeID == null || g_uESCSlotID == null || moveVals == null) return 0xFFFF;

            int count = Math.Min(g_uESCNodeID.Length, Math.Min(g_uESCSlotID.Length, moveVals.Length));
            if (count <= 0) return 0xFFFF;

            ushort uSCurve = (ushort)(bSCurve ? 1 : 0);
            ushort uAbsMove = (ushort)(bkAbsMove ? 1 : 0);
            int sign = bkAbsMove ? 1 : (nDir == 0 ? -1 : +1);

            int[] nDist = new int[count];
            for (int i = 0; i < count; i++)
                nDist[i] = bkAbsMove ? moveVals[i] : sign * moveVals[i];

            // ★ 要接回傳值！
            ushort rt = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Multiaxes_Move(
                g_uESCCardNo, (ushort)count,
                ref g_uESCNodeID[0], ref g_uESCSlotID[0], ref nDist[0],
                nStrVel, nConstVel, nEndVel, dTAcc, dTDec, uSCurve, uAbsMove
            );
            return rt;
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

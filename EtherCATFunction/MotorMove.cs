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
        
        //public void MultiaxesMove(int nDir)
        //{
        //    ushort uDir = 0, uCycleNum = 0, uSCurve = 0, uAbsMove = 0;
        //    int[] nCenPot = { 0, 0 };
        //    int[] nEndPot = { 0, 0 };
        //    int[] nDist = { 0, 0, 0 };
        //    int[] nDist2 = { 0, 0, 0 };
        //    int nDepth = 0, nPitch = 0, nStrVel = 0, nConstVel = 0, nEndVel = 0;
        //    int nSpiralInterval = 0;
        //    double dTAcc = 0, dTDec = 0, dAngle = 0;
        //    string strMsg = "";

        //    nDist[0] = (nDir == 0) ? (0 - Convert.ToInt32(TxtParam01.Text)) : (Convert.ToInt32(TxtParam01.Text));
        //    nDist[1] = (nDir == 0) ? (0 - Convert.ToInt32(TxtParam02.Text)) : (Convert.ToInt32(TxtParam02.Text));
        //    nStrVel = Convert.ToInt32(TxtParam03.Text);
        //    nConstVel = Convert.ToInt32(TxtParam04.Text);
        //    nEndVel = Convert.ToInt32(TxtParam05.Text);
        //    dTAcc = Convert.ToDouble(TxtParam06.Text);
        //    dTDec = Convert.ToDouble(TxtParam07.Text);



        //    g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Multiaxes_Move(g_uESCCardNo, 2, ref g_uESCNodeID[0], ref g_uESCSlotID[0], ref nDist[0], nStrVel, nConstVel, nEndVel, dTAcc, dTDec, uSCurve, uAbsMove);
        //    //if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
        //    //    strMsg = "_ECAT_Slave_CSP_Start_Multiaxes_Move, ErrorCode = " + g_uRet.ToString();

        //}




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

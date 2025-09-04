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
        //private void MultiAxesMove(int nDir ,int nStrVel ,int nConstVel,int nEndVel)
        //{
        //    ushort uDir = 0, uCycleNum = 0, uSCurve = 0, uAbsMove = 0;
        //    int[] nCenPot = { 0, 0 };
        //    int[] nEndPot = { 0, 0 };
        //    int[] nDist = { 0, 0, 0 };
        //    int[] nDist2 = { 0, 0, 0 };
        //    int nDepth = 0, nPitch = 0;
        //    int nSpiralInterval = 0;
        //    double dTAcc = 0, dTDec = 0, dAngle = 0;
        //    string strMsg = "";

        //    if (ChkSCurve.Checked == true)
        //        uSCurve = 1;

        //    if (ChkAbsMove.Checked == true)
        //        uAbsMove = 1;

        //    g_uRet = (ushort)CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR;

        //    if (nDir == 0)
        //        uDir = 1;

        //    if (g_nSelectMode > 2)
        //    {
        //        if (CmbNode1.SelectedIndex == CmbNode2.SelectedIndex)
        //        {
        //            strMsg = "Node 2 select error";
        //            AddErrMsg(strMsg, true);
        //            return;
        //        }
        //    }
        //    if (g_nSelectMode > 8)
        //    {
        //        if ((CmbNode1.SelectedIndex == CmbNode3.SelectedIndex)
        //        || (CmbNode2.SelectedIndex == CmbNode3.SelectedIndex))
        //        {
        //            strMsg = "Node 3 select error";
        //            AddErrMsg(strMsg, true);
        //            return;
        //        }
        //    }
        //    switch (g_nSelectMode)
        //    {
        //        case 1:
        //            nDist[0] = (nDir == 1) ? Convert.ToInt32(TxtParam01.Text) : 0 - Convert.ToInt32(TxtParam01.Text);
        //            nStrVel = Convert.ToInt32(TxtParam02.Text);
        //            nConstVel = Convert.ToInt32(TxtParam03.Text);
        //            nEndVel = Convert.ToInt32(TxtParam04.Text);
        //            dTAcc = Convert.ToDouble(TxtParam05.Text);
        //            dTDec = Convert.ToDouble(TxtParam06.Text);

        //            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Move(g_uESCCardNo, g_uESCNodeID[0], g_uESCSlotID[0], nDist[0], nStrVel, nConstVel, nEndVel, dTAcc, dTDec, uSCurve, uAbsMove);
        //            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
        //                strMsg = "_ECAT_Slave_CSP_Start_Move, ErrorCode = " + g_uRet.ToString();
        //            break;

        //        case 3:
        //            nDist[0] = (nDir == 0) ? (0 - Convert.ToInt32(TxtParam01.Text)) : (Convert.ToInt32(TxtParam01.Text));
        //            nDist[1] = (nDir == 0) ? (0 - Convert.ToInt32(TxtParam02.Text)) : (Convert.ToInt32(TxtParam02.Text));
        //            nStrVel = Convert.ToInt32(TxtParam03.Text);
        //            nConstVel = Convert.ToInt32(TxtParam04.Text);
        //            nEndVel = Convert.ToInt32(TxtParam05.Text);
        //            dTAcc = Convert.ToDouble(TxtParam06.Text);
        //            dTDec = Convert.ToDouble(TxtParam07.Text);

        //            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Multiaxes_Move(g_uESCCardNo, 2, ref g_uESCNodeID[0], ref g_uESCSlotID[0], ref nDist[0], nStrVel, nConstVel, nEndVel, dTAcc, dTDec, uSCurve, uAbsMove);
        //            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
        //                strMsg = "_ECAT_Slave_CSP_Start_Multiaxes_Move, ErrorCode = " + g_uRet.ToString();
        //            break;

        //        case 10:
        //            nDist[0] = (nDir == 0) ? (0 - Convert.ToInt32(TxtParam01.Text)) : (Convert.ToInt32(TxtParam01.Text));
        //            nDist[1] = (nDir == 0) ? (0 - Convert.ToInt32(TxtParam02.Text)) : (Convert.ToInt32(TxtParam02.Text));
        //            nDist[2] = (nDir == 0) ? (0 - Convert.ToInt32(TxtParam03.Text)) : (Convert.ToInt32(TxtParam03.Text));
        //            nStrVel = Convert.ToInt32(TxtParam04.Text);
        //            nConstVel = Convert.ToInt32(TxtParam05.Text);
        //            nEndVel = Convert.ToInt32(TxtParam06.Text);
        //            dTAcc = Convert.ToDouble(TxtParam07.Text);
        //            dTDec = Convert.ToDouble(TxtParam08.Text);

        //            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_CSP_Start_Multiaxes_Move(g_uESCCardNo, 3, ref g_uESCNodeID[0], ref g_uESCSlotID[0], ref nDist[0], nStrVel, nConstVel, nEndVel, dTAcc, dTDec, uSCurve, uAbsMove);
        //            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
        //                strMsg = "_ECAT_Slave_CSP_Start_Multiaxes_Move, ErrorCode = " + g_uRet.ToString();
        //            break;

        //    }
        //    if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
        //        AddErrMsg(strMsg);
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

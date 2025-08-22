using EtherCAT_DLL;
using EtherCAT_DLL_Err;

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
    }
}

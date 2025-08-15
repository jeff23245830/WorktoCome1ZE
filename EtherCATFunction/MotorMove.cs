using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EtherCAT_DLL;
using EtherCAT_DLL_Err;



namespace EtherCATFunction
{
    public class MotorMove
    {
        bool g_bInitialFlag = false;
        ushort g_uRet = 0;
        ushort g_nESCExistCards = 0, g_uESCCardNo = 0, g_uESCNodeID = 0, g_uESCSlotID;
        ushort[] g_uESCCardNoList = new ushort[32];

        public string TargetPos { get; set; } // 目標位置
        public string ConstVel { get; set; } // 目標速度

        public string Acceleration { get; set; } // 加速度
        public string Deceleration { get; set; } // 減速度



        public void AxisMove(int nDir ,bool ChkAbsMove )
        {
            int nTargetPos = 0;
            ushort uAbsMove = 0;
            uint uAcceleration = 0, uDeceleration = 0, uConstVel = 0;


             

            nTargetPos = Convert.ToInt32(TargetPos); // 目標速度，(0x60FF Sub 0) inc代表的是驅動器內部設定之單位值
            uConstVel = Convert.ToUInt32(ConstVel); // 脈波數/秒 inc/s (0x6081 Sub 0)
            uAcceleration = Convert.ToUInt32(Acceleration); // 加速度 (0x6083 Sub 0)
            uDeceleration = Convert.ToUInt32(Deceleration); // 減速度 (0x6084 Sub 0)

            if (ChkAbsMove == true)
                uAbsMove = 1; // 0：相對位移(Default) 1：絕對位移

            if (nDir == 0)
                nTargetPos = 0 - nTargetPos;

            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_PP_Start_Move(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, nTargetPos, uConstVel, uAcceleration, uDeceleration, uAbsMove);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("CS_ECAT_Slave_PP_Start_Move, ErrorCode = " + g_uRet.ToString(), true);
            }
        }
        public void StopMove()
        {

            double dTdec;

            dTdec = Convert.ToDouble(Deceleration);
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Sd_Stop(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, dTdec);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("_ECAT_Slave_Motion_Sd_Stop, ErrorCode = " + g_uRet.ToString(), true);
            }
        }
        public void ServoOnOrOff(bool RdoSVON, ushort ESCNodeID , ushort ESCSlotID)
        {
            ushort uCheckOnOff = 0;
            if (RdoSVON == true)
                uCheckOnOff = 1;
            g_uESCNodeID = ESCNodeID;
            g_uESCSlotID = ESCSlotID;
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_Motion_Set_Svon(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, uCheckOnOff);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("CS_ECAT_Slave_Motion_Set_Svon, Error Code = " + g_uRet.ToString(), true);
            }
        }

    }
}

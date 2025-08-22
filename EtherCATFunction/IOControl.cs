using EtherCAT_DLL;
using EtherCAT_DLL_Err;

namespace EtherCATFunction
{
    public class IOControl
    {
        ushort g_uRet = 0;
        ushort g_uESCCardNo = 0;
        public ushort g_nESCExistCards;
        public bool DOcontorlOutOrOff(ushort uOutputStatus , ushort g_uESCNodeID , ushort g_uESCSlotID)
        {
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Set_Output_Value(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, uOutputStatus);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("_ECAT_Slave_DIO_Set_Output, ErrorCode = " + g_uRet.ToString(), true);
                return true;
            }
            else
            {
                return false;
            }
        } 

        public bool DIcontrolInOrOff( ushort g_uESCNodeID, ushort g_uESCSlotID,ref ushort uValue)
        {
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Get_Input_Value(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, ref uValue);

            if (g_uRet == CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DOcontrolRead(ushort g_uESCNodeID, ushort g_uESCSlotID, ref ushort uOutputStatus)
        {
            g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Get_Output_Value(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, ref uOutputStatus);

            if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
            {
                //AddErrMsg("_ECAT_Slave_DIO_Get_Output, ErrorCode = " + g_uRet.ToString(), true);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

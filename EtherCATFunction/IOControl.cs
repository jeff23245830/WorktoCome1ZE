using EtherCAT_DLL;
using EtherCAT_DLL_Err;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtherCATFunction
{
    public class IOControl
    {
        ushort g_uRet = 0;
        ushort g_uESCCardNo = 0;
        public ushort g_nESCExistCards;
        public void DOcontorlOutOrOff(ushort uOutputStatus , ushort g_uESCNodeID , ushort g_uESCSlotID)
        {
             
                g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Set_Output_Value(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, uOutputStatus);

                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    //AddErrMsg("_ECAT_Slave_DIO_Set_Output, ErrorCode = " + g_uRet.ToString(), true);
                } 

        }
        public void DIcontorlInOrOff()
        {


        }




    }
}

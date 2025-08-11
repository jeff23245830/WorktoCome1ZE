using EtherCAT_DLL;
using EtherCAT_DLL_Err;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorktoCome1
{
    public partial class UcMain : UserControl
    {
        public ushort CardCount {get;set;}


        CheckBox[] g_pOutputLab = new CheckBox[1];
        public UcMain()
        {
            InitializeComponent();
            g_pOutputLab[0] = ChkBit00;
        }

        private void ChkBit00_CheckedChanged(object sender, EventArgs e)
        {
            ushort uOutputStatus = 0;
            int nOutBit = 0, nStat = 0x0;
            for (nOutBit = 0; nOutBit < 16; nOutBit++)
            {
                if (g_pOutputLab[nOutBit].Checked == true)
                {
                    nStat = nStat + (0x1 << nOutBit);
                    g_pOutputLab[nOutBit].BackColor = Color.Green;
                }
                else
                {
                    g_pOutputLab[nOutBit].BackColor = Color.Red;
                }
            }
            uOutputStatus = (ushort)nStat;
            /*
            if (g_nESCExistCards > 0)
            {
                g_uRet = CEtherCAT_DLL.CS_ECAT_Slave_DIO_Set_Output_Value(g_uESCCardNo, g_uESCNodeID, g_uESCSlotID, uOutputStatus);

                if (g_uRet != CEtherCAT_DLL_Err.ERR_ECAT_NO_ERROR)
                {
                    AddErrMsg("_ECAT_Slave_DIO_Set_Output, ErrorCode = " + g_uRet.ToString(), true);
                }
            }*/
        }
    }
}

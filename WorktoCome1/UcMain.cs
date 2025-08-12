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
            //_pOutputLab[0] = ChkBit00;
        }

        
    }
}

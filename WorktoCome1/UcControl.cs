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
    public partial class UcControl : UserControl
    {
        public UcControl()
        {
            InitializeComponent();
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add("就緒", "");
            dataGridView1.Rows.Add("切入", "");
            dataGridView1.Rows.Add("允許運轉", "");
            dataGridView1.Rows.Add("故障", "");
            dataGridView1.Rows.Add("電壓關閉", "");
            dataGridView1.Rows.Add("快速停止", "");
            dataGridView1.Rows.Add("關閉", "");
            dataGridView1.Rows.Add("警告", "");
            dataGridView1.Rows.Add("無狀態", "");
            dataGridView1.Rows.Add("遠端", "");
            dataGridView1.Rows.Add("目標達成", "");
            dataGridView1.Rows.Add("內部極限", "");
            dataGridView1.Rows.Add("負極限", "");
            dataGridView1.Rows.Add("正極限", "");
            dataGridView1.Rows.Add("原點", "");
        }
    }
}

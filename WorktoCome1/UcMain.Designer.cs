namespace WorktoCome1
{
    partial class UcMain
    {
        /// <summary> 
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 元件設計工具產生的程式碼

        /// <summary> 
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ChkBit00 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // ChkBit00
            // 
            this.ChkBit00.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChkBit00.AutoSize = true;
            this.ChkBit00.BackColor = System.Drawing.Color.Red;
            this.ChkBit00.Location = new System.Drawing.Point(125, 147);
            this.ChkBit00.Name = "ChkBit00";
            this.ChkBit00.Size = new System.Drawing.Size(27, 22);
            this.ChkBit00.TabIndex = 0;
            this.ChkBit00.Text = "00";
            this.ChkBit00.UseVisualStyleBackColor = false;
            this.ChkBit00.CheckedChanged += new System.EventHandler(this.ChkBit00_CheckedChanged);
            // 
            // UcMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ChkBit00);
            this.Name = "UcMain";
            this.Size = new System.Drawing.Size(1083, 539);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox ChkBit00;
    }
}

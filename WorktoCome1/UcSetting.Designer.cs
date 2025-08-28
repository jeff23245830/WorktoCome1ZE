namespace WorktoCome1
{
    partial class UcSetting
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.CbArea = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMoveDown = new System.Windows.Forms.Button();
            this.btnMoveBottom = new System.Windows.Forms.Button();
            this.btnMoveUp = new System.Windows.Forms.Button();
            this.btnMoveTop = new System.Windows.Forms.Button();
            this.btnMotionSave = new System.Windows.Forms.Button();
            this.DgMotionPoint = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.教導 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.移動 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.點位名稱 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Z = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.R = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgMotionPoint)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1083, 539);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1075, 513);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "定點設定";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Controls.Add(this.tabPage5);
            this.tabControl2.Controls.Add(this.tabPage6);
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(1079, 513);
            this.tabControl2.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1071, 487);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "黃金樣品";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label1);
            this.tabPage5.Controls.Add(this.CbArea);
            this.tabPage5.Controls.Add(this.groupBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1071, 487);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "點位架設";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(86, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "區域";
            // 
            // CbArea
            // 
            this.CbArea.FormattingEnabled = true;
            this.CbArea.Location = new System.Drawing.Point(121, 6);
            this.CbArea.Name = "CbArea";
            this.CbArea.Size = new System.Drawing.Size(295, 20);
            this.CbArea.TabIndex = 1;
            this.CbArea.SelectedIndexChanged += new System.EventHandler(this.CbArea_SelectedIndexChanged);
            this.CbArea.TextChanged += new System.EventHandler(this.CbArea_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnMoveDown);
            this.groupBox1.Controls.Add(this.btnMoveBottom);
            this.groupBox1.Controls.Add(this.btnMoveUp);
            this.groupBox1.Controls.Add(this.btnMoveTop);
            this.groupBox1.Controls.Add(this.btnMotionSave);
            this.groupBox1.Controls.Add(this.DgMotionPoint);
            this.groupBox1.Location = new System.Drawing.Point(3, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(629, 455);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Location = new System.Drawing.Point(6, 389);
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(36, 27);
            this.btnMoveDown.TabIndex = 5;
            this.btnMoveDown.Text = "▼";
            this.btnMoveDown.UseVisualStyleBackColor = true;
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveBottom
            // 
            this.btnMoveBottom.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnMoveBottom.Location = new System.Drawing.Point(6, 422);
            this.btnMoveBottom.Name = "btnMoveBottom";
            this.btnMoveBottom.Size = new System.Drawing.Size(36, 27);
            this.btnMoveBottom.TabIndex = 4;
            this.btnMoveBottom.Text = "↓↓";
            this.btnMoveBottom.UseVisualStyleBackColor = true;
            this.btnMoveBottom.Click += new System.EventHandler(this.btnMoveBottom_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.Location = new System.Drawing.Point(6, 54);
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(36, 27);
            this.btnMoveUp.TabIndex = 3;
            this.btnMoveUp.Text = "▲";
            this.btnMoveUp.UseVisualStyleBackColor = true;
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveTop
            // 
            this.btnMoveTop.Font = new System.Drawing.Font("新細明體", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnMoveTop.Location = new System.Drawing.Point(6, 21);
            this.btnMoveTop.Name = "btnMoveTop";
            this.btnMoveTop.Size = new System.Drawing.Size(36, 27);
            this.btnMoveTop.TabIndex = 2;
            this.btnMoveTop.Text = "↑↑";
            this.btnMoveTop.UseVisualStyleBackColor = true;
            this.btnMoveTop.Click += new System.EventHandler(this.btnMoveTop_Click);
            // 
            // btnMotionSave
            // 
            this.btnMotionSave.Location = new System.Drawing.Point(581, 413);
            this.btnMotionSave.Name = "btnMotionSave";
            this.btnMotionSave.Size = new System.Drawing.Size(42, 36);
            this.btnMotionSave.TabIndex = 1;
            this.btnMotionSave.Text = "存檔";
            this.btnMotionSave.UseVisualStyleBackColor = true;
            this.btnMotionSave.Click += new System.EventHandler(this.btnMotionSave_Click);
            // 
            // DgMotionPoint
            // 
            this.DgMotionPoint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgMotionPoint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.教導,
            this.移動,
            this.點位名稱,
            this.X,
            this.Y,
            this.Z,
            this.R});
            this.DgMotionPoint.Location = new System.Drawing.Point(48, 21);
            this.DgMotionPoint.Name = "DgMotionPoint";
            this.DgMotionPoint.RowTemplate.Height = 24;
            this.DgMotionPoint.Size = new System.Drawing.Size(527, 428);
            this.DgMotionPoint.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(1071, 487);
            this.tabPage6.TabIndex = 2;
            this.tabPage6.Text = "校正";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1075, 513);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "參數設定";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1075, 513);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "速度設定";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // 教導
            // 
            this.教導.FillWeight = 50F;
            this.教導.HeaderText = "教導";
            this.教導.Name = "教導";
            this.教導.Width = 50;
            // 
            // 移動
            // 
            this.移動.FillWeight = 50F;
            this.移動.HeaderText = "移動";
            this.移動.Name = "移動";
            this.移動.Width = 50;
            // 
            // 點位名稱
            // 
            this.點位名稱.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.點位名稱.HeaderText = "點位名稱";
            this.點位名稱.Name = "點位名稱";
            // 
            // X
            // 
            this.X.FillWeight = 50F;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.Width = 50;
            // 
            // Y
            // 
            this.Y.FillWeight = 50F;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.Width = 50;
            // 
            // Z
            // 
            this.Z.FillWeight = 50F;
            this.Z.HeaderText = "Z";
            this.Z.Name = "Z";
            this.Z.Width = 50;
            // 
            // R
            // 
            this.R.FillWeight = 50F;
            this.R.HeaderText = "R";
            this.R.Name = "R";
            this.R.Width = 50;
            // 
            // UcSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "UcSetting";
            this.Size = new System.Drawing.Size(1083, 538);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DgMotionPoint)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnMoveDown;
        private System.Windows.Forms.Button btnMoveBottom;
        private System.Windows.Forms.Button btnMoveUp;
        private System.Windows.Forms.Button btnMoveTop;
        private System.Windows.Forms.Button btnMotionSave;
        private System.Windows.Forms.DataGridView DgMotionPoint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CbArea;
        private System.Windows.Forms.DataGridViewTextBoxColumn 教導;
        private System.Windows.Forms.DataGridViewTextBoxColumn 移動;
        private System.Windows.Forms.DataGridViewTextBoxColumn 點位名稱;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn Z;
        private System.Windows.Forms.DataGridViewTextBoxColumn R;
    }
}

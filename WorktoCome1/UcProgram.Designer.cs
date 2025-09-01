namespace WorktoCome1
{
    partial class UcProgram
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
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDeleteRecipe = new System.Windows.Forms.Button();
            this.btCreateRecipe = new System.Windows.Forms.Button();
            this.btnRecipeRename = new System.Windows.Forms.Button();
            this.btnLoadParameters = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtSelectedProduct = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCurrentProduct = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.LbProducts = new System.Windows.Forms.ListBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(834, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(232, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "稼動時間歸零";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1080, 536);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1072, 510);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "產品選擇";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.LbProducts);
            this.groupBox3.Controls.Add(this.btnDeleteRecipe);
            this.groupBox3.Controls.Add(this.btCreateRecipe);
            this.groupBox3.Controls.Add(this.btnRecipeRename);
            this.groupBox3.Controls.Add(this.btnLoadParameters);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Location = new System.Drawing.Point(15, 143);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(497, 348);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "產品清單";
            // 
            // btnDeleteRecipe
            // 
            this.btnDeleteRecipe.Location = new System.Drawing.Point(398, 291);
            this.btnDeleteRecipe.Name = "btnDeleteRecipe";
            this.btnDeleteRecipe.Size = new System.Drawing.Size(92, 51);
            this.btnDeleteRecipe.TabIndex = 5;
            this.btnDeleteRecipe.Text = "刪除";
            this.btnDeleteRecipe.UseVisualStyleBackColor = true;
            this.btnDeleteRecipe.Click += new System.EventHandler(this.btnDeleteRecipe_Click);
            // 
            // btCreateRecipe
            // 
            this.btCreateRecipe.Location = new System.Drawing.Point(300, 291);
            this.btCreateRecipe.Name = "btCreateRecipe";
            this.btCreateRecipe.Size = new System.Drawing.Size(92, 51);
            this.btCreateRecipe.TabIndex = 4;
            this.btCreateRecipe.Text = "新增";
            this.btCreateRecipe.UseVisualStyleBackColor = true;
            this.btCreateRecipe.Click += new System.EventHandler(this.btCreateRecipe_Click);
            // 
            // btnRecipeRename
            // 
            this.btnRecipeRename.Location = new System.Drawing.Point(202, 291);
            this.btnRecipeRename.Name = "btnRecipeRename";
            this.btnRecipeRename.Size = new System.Drawing.Size(92, 51);
            this.btnRecipeRename.TabIndex = 3;
            this.btnRecipeRename.Text = "重新命名";
            this.btnRecipeRename.UseVisualStyleBackColor = true;
            this.btnRecipeRename.Click += new System.EventHandler(this.btnRecipeRename_Click);
            // 
            // btnLoadParameters
            // 
            this.btnLoadParameters.Location = new System.Drawing.Point(104, 291);
            this.btnLoadParameters.Name = "btnLoadParameters";
            this.btnLoadParameters.Size = new System.Drawing.Size(92, 51);
            this.btnLoadParameters.TabIndex = 2;
            this.btnLoadParameters.Text = "載入參數";
            this.btnLoadParameters.UseVisualStyleBackColor = true;
            this.btnLoadParameters.Click += new System.EventHandler(this.btnLoadParameters_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 291);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 51);
            this.button3.TabIndex = 1;
            this.button3.Text = "儲存參數";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtSelectedProduct);
            this.groupBox2.Location = new System.Drawing.Point(15, 75);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(497, 62);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "產品選擇";
            // 
            // txtSelectedProduct
            // 
            this.txtSelectedProduct.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtSelectedProduct.Location = new System.Drawing.Point(6, 21);
            this.txtSelectedProduct.Name = "txtSelectedProduct";
            this.txtSelectedProduct.ReadOnly = true;
            this.txtSelectedProduct.Size = new System.Drawing.Size(485, 33);
            this.txtSelectedProduct.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCurrentProduct);
            this.groupBox1.Location = new System.Drawing.Point(15, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(497, 63);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "目前產品";
            // 
            // txtCurrentProduct
            // 
            this.txtCurrentProduct.Font = new System.Drawing.Font("新細明體", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.txtCurrentProduct.Location = new System.Drawing.Point(6, 21);
            this.txtCurrentProduct.Name = "txtCurrentProduct";
            this.txtCurrentProduct.ReadOnly = true;
            this.txtCurrentProduct.Size = new System.Drawing.Size(485, 33);
            this.txtCurrentProduct.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1072, 510);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "探針教導";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(834, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(232, 32);
            this.button2.TabIndex = 1;
            this.button2.Text = "稼動時間歸零";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // LbProducts
            // 
            this.LbProducts.FormattingEnabled = true;
            this.LbProducts.ItemHeight = 12;
            this.LbProducts.Location = new System.Drawing.Point(6, 21);
            this.LbProducts.Name = "LbProducts";
            this.LbProducts.Size = new System.Drawing.Size(480, 256);
            this.LbProducts.TabIndex = 6;
            this.LbProducts.SelectedIndexChanged += new System.EventHandler(this.LbProducts_SelectedIndexChanged);
            // 
            // UcProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Name = "UcProgram";
            this.Size = new System.Drawing.Size(1083, 539);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSelectedProduct;
        private System.Windows.Forms.TextBox txtCurrentProduct;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDeleteRecipe;
        private System.Windows.Forms.Button btCreateRecipe;
        private System.Windows.Forms.Button btnRecipeRename;
        private System.Windows.Forms.Button btnLoadParameters;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox LbProducts;
    }
}

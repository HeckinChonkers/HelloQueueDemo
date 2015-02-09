namespace DemoHelloQueue
{
    partial class Commands
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.commandTxt = new System.Windows.Forms.TextBox();
            this.resultTxt = new System.Windows.Forms.TextBox();
            this.addCmdBtn = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.commandHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.resultHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Marker", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "New Command:  !";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe Marker", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Result:";
            // 
            // commandTxt
            // 
            this.commandTxt.Location = new System.Drawing.Point(136, 8);
            this.commandTxt.Name = "commandTxt";
            this.commandTxt.Size = new System.Drawing.Size(203, 20);
            this.commandTxt.TabIndex = 3;
            // 
            // resultTxt
            // 
            this.resultTxt.Location = new System.Drawing.Point(68, 37);
            this.resultTxt.Name = "resultTxt";
            this.resultTxt.Size = new System.Drawing.Size(516, 20);
            this.resultTxt.TabIndex = 4;
            // 
            // addCmdBtn
            // 
            this.addCmdBtn.Location = new System.Drawing.Point(346, 8);
            this.addCmdBtn.Name = "addCmdBtn";
            this.addCmdBtn.Size = new System.Drawing.Size(238, 23);
            this.addCmdBtn.TabIndex = 5;
            this.addCmdBtn.Text = "Add Command";
            this.addCmdBtn.UseVisualStyleBackColor = true;
            this.addCmdBtn.Click += new System.EventHandler(this.addCmdBtn_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.commandHeader,
            this.resultHeader});
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(16, 63);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(568, 294);
            this.listView1.TabIndex = 6;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // commandHeader
            // 
            this.commandHeader.Text = "!Command";
            this.commandHeader.Width = 128;
            // 
            // resultHeader
            // 
            this.resultHeader.Text = "Result";
            this.resultHeader.Width = 436;
            // 
            // Commands
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 369);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.addCmdBtn);
            this.Controls.Add(this.resultTxt);
            this.Controls.Add(this.commandTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Commands";
            this.Text = "Commands";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox commandTxt;
        private System.Windows.Forms.TextBox resultTxt;
        private System.Windows.Forms.Button addCmdBtn;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader resultHeader;
        public System.Windows.Forms.ColumnHeader commandHeader;
    }
}
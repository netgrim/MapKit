namespace MapKit.Demo
{
    partial class MatrixWindow
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblM12 = new System.Windows.Forms.TextBox();
            this.lblM11 = new System.Windows.Forms.TextBox();
            this.lblM10 = new System.Windows.Forms.TextBox();
            this.lblM02 = new System.Windows.Forms.TextBox();
            this.lblM01 = new System.Windows.Forms.TextBox();
            this.lblM00 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.lblM12, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblM11, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblM10, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblM02, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblM01, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblM00, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(169, 35);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lblM12
            // 
            this.lblM12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblM12.Location = new System.Drawing.Point(115, 20);
            this.lblM12.Name = "lblM12";
            this.lblM12.Size = new System.Drawing.Size(51, 20);
            this.lblM12.TabIndex = 6;
            this.lblM12.Text = "label6";
            this.lblM12.Validated += new System.EventHandler(this.lblM00_Validated);
            // 
            // lblM11
            // 
            this.lblM11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblM11.Location = new System.Drawing.Point(59, 20);
            this.lblM11.Name = "lblM11";
            this.lblM11.Size = new System.Drawing.Size(50, 20);
            this.lblM11.TabIndex = 5;
            this.lblM11.Text = "label5";
            this.lblM11.Validated += new System.EventHandler(this.lblM00_Validated);
            // 
            // lblM10
            // 
            this.lblM10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblM10.Location = new System.Drawing.Point(3, 20);
            this.lblM10.Name = "lblM10";
            this.lblM10.Size = new System.Drawing.Size(50, 20);
            this.lblM10.TabIndex = 4;
            this.lblM10.Text = "label4";
            this.lblM10.Validated += new System.EventHandler(this.lblM00_Validated);
            // 
            // lblM02
            // 
            this.lblM02.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblM02.Location = new System.Drawing.Point(115, 3);
            this.lblM02.Name = "lblM02";
            this.lblM02.Size = new System.Drawing.Size(51, 20);
            this.lblM02.TabIndex = 3;
            this.lblM02.Text = "label3";
            this.lblM02.Validated += new System.EventHandler(this.lblM00_Validated);
            // 
            // lblM01
            // 
            this.lblM01.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblM01.Location = new System.Drawing.Point(59, 3);
            this.lblM01.Name = "lblM01";
            this.lblM01.Size = new System.Drawing.Size(50, 20);
            this.lblM01.TabIndex = 2;
            this.lblM01.Text = "label2";
            this.lblM01.Validated += new System.EventHandler(this.lblM00_Validated);
            // 
            // lblM00
            // 
            this.lblM00.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblM00.Location = new System.Drawing.Point(3, 3);
            this.lblM00.Name = "lblM00";
            this.lblM00.Size = new System.Drawing.Size(50, 20);
            this.lblM00.TabIndex = 1;
            this.lblM00.Text = "label1";
            this.lblM00.Validated += new System.EventHandler(this.lblM00_Validated);
            // 
            // MatrixWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(169, 35);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "MatrixWindow";
            this.Text = "matrix";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox lblM12;
        private System.Windows.Forms.TextBox lblM11;
        private System.Windows.Forms.TextBox lblM10;
        private System.Windows.Forms.TextBox lblM02;
        private System.Windows.Forms.TextBox lblM01;
        private System.Windows.Forms.TextBox lblM00;
    }
}
namespace MapKit.Spatialite
{
    partial class SourceProperties
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblDatabase = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.txtFid = new System.Windows.Forms.TextBox();
            this.lblFid = new System.Windows.Forms.Label();
            this.txtGeometry = new System.Windows.Forms.TextBox();
            this.lblGeometry = new System.Windows.Forms.Label();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.txtSql = new System.Windows.Forms.TextBox();
            this.lblSql = new System.Windows.Forms.Label();
            this.lblGenerate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Location = new System.Drawing.Point(3, 13);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(56, 13);
            this.lblDatabase.TabIndex = 0;
            this.lblDatabase.Text = "Database:";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDatabase.Location = new System.Drawing.Point(113, 10);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(252, 20);
            this.txtDatabase.TabIndex = 1;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(3, 39);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(37, 13);
            this.lblTable.TabIndex = 6;
            this.lblTable.Text = "Table:";
            // 
            // txtFid
            // 
            this.txtFid.Location = new System.Drawing.Point(113, 62);
            this.txtFid.Name = "txtFid";
            this.txtFid.Size = new System.Drawing.Size(126, 20);
            this.txtFid.TabIndex = 9;
            // 
            // lblFid
            // 
            this.lblFid.AutoSize = true;
            this.lblFid.Location = new System.Drawing.Point(3, 65);
            this.lblFid.Name = "lblFid";
            this.lblFid.Size = new System.Drawing.Size(62, 13);
            this.lblFid.TabIndex = 8;
            this.lblFid.Text = "Fid Column:";
            this.lblFid.Click += new System.EventHandler(this.label2_Click);
            // 
            // txtGeometry
            // 
            this.txtGeometry.Location = new System.Drawing.Point(113, 88);
            this.txtGeometry.Name = "txtGeometry";
            this.txtGeometry.Size = new System.Drawing.Size(126, 20);
            this.txtGeometry.TabIndex = 11;
            // 
            // lblGeometry
            // 
            this.lblGeometry.AutoSize = true;
            this.lblGeometry.Location = new System.Drawing.Point(3, 91);
            this.lblGeometry.Name = "lblGeometry";
            this.lblGeometry.Size = new System.Drawing.Size(93, 13);
            this.lblGeometry.TabIndex = 10;
            this.lblGeometry.Text = "Geometry Column:";
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdBrowse.Location = new System.Drawing.Point(371, 8);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowse.TabIndex = 12;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            // 
            // cboTable
            // 
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(113, 36);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(188, 21);
            this.cboTable.TabIndex = 13;
            // 
            // txtSql
            // 
            this.txtSql.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSql.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSql.Location = new System.Drawing.Point(6, 149);
            this.txtSql.Multiline = true;
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(440, 112);
            this.txtSql.TabIndex = 14;
            // 
            // lblSql
            // 
            this.lblSql.AutoSize = true;
            this.lblSql.Location = new System.Drawing.Point(3, 125);
            this.lblSql.Name = "lblSql";
            this.lblSql.Size = new System.Drawing.Size(31, 13);
            this.lblSql.TabIndex = 15;
            this.lblSql.Text = "SQL:";
            // 
            // lblGenerate
            // 
            this.lblGenerate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGenerate.Location = new System.Drawing.Point(335, 120);
            this.lblGenerate.Name = "lblGenerate";
            this.lblGenerate.Size = new System.Drawing.Size(111, 23);
            this.lblGenerate.TabIndex = 16;
            this.lblGenerate.Text = "Generate SQL";
            this.lblGenerate.UseVisualStyleBackColor = true;
            this.lblGenerate.Click += new System.EventHandler(this.button2_Click);
            // 
            // SourceProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblGenerate);
            this.Controls.Add(this.lblSql);
            this.Controls.Add(this.txtSql);
            this.Controls.Add(this.cboTable);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.txtGeometry);
            this.Controls.Add(this.lblGeometry);
            this.Controls.Add(this.txtFid);
            this.Controls.Add(this.lblFid);
            this.Controls.Add(this.lblTable);
            this.Controls.Add(this.txtDatabase);
            this.Controls.Add(this.lblDatabase);
            this.Name = "SourceProperties";
            this.Size = new System.Drawing.Size(449, 264);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDatabase;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.TextBox txtFid;
        private System.Windows.Forms.Label lblFid;
        private System.Windows.Forms.TextBox txtGeometry;
        private System.Windows.Forms.Label lblGeometry;
        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.TextBox txtSql;
        private System.Windows.Forms.Label lblSql;
        private System.Windows.Forms.Button lblGenerate;
    }
}

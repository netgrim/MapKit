namespace MapKit.UI
{
    partial class frmExpressionEditor
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblFields = new System.Windows.Forms.Label();
            this.lstFields = new System.Windows.Forms.ListBox();
            this.cmdApply = new System.Windows.Forms.Button();
            this.txtMinimum = new System.Windows.Forms.TextBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.txtExpression = new System.Windows.Forms.TextBox();
            this.lblSelect = new System.Windows.Forms.Label();
            this.lstUniqueValues = new System.Windows.Forms.ListBox();
            this.cmdGetUniqueValues = new System.Windows.Forms.Button();
            this.cmdEquals = new System.Windows.Forms.Button();
            this.cmdNotEquals = new System.Windows.Forms.Button();
            this.cmdGreater = new System.Windows.Forms.Button();
            this.cmdLower = new System.Windows.Forms.Button();
            this.cmdIsNull = new System.Windows.Forms.Button();
            this.cmdIsNotNull = new System.Windows.Forms.Button();
            this.cmdGreaterOrEquals = new System.Windows.Forms.Button();
            this.cmdLowerOrEquals = new System.Windows.Forms.Button();
            this.cmdAnd = new System.Windows.Forms.Button();
            this.cmdOr = new System.Windows.Forms.Button();
            this.cmdNot = new System.Windows.Forms.Button();
            this.cmdLike = new System.Windows.Forms.Button();
            this.lblUniqueValues = new System.Windows.Forms.Label();
            this.cmdParentisis = new System.Windows.Forms.Button();
            this.cmdStar = new System.Windows.Forms.Button();
            this.l = new System.Windows.Forms.Label();
            this.lblMaximumValue = new System.Windows.Forms.Label();
            this.txtMaximum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblFields
            // 
            this.lblFields.AutoSize = true;
            this.lblFields.Location = new System.Drawing.Point(12, 8);
            this.lblFields.Name = "lblFields";
            this.lblFields.Size = new System.Drawing.Size(37, 13);
            this.lblFields.TabIndex = 0;
            this.lblFields.Text = "Fields:";
            // 
            // lstFields
            // 
            this.lstFields.DisplayMember = "ColumnName";
            this.lstFields.FormattingEnabled = true;
            this.lstFields.IntegralHeight = false;
            this.lstFields.Location = new System.Drawing.Point(15, 25);
            this.lstFields.Name = "lstFields";
            this.lstFields.Size = new System.Drawing.Size(147, 198);
            this.lstFields.Sorted = true;
            this.lstFields.TabIndex = 1;
            this.lstFields.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.list_MouseDoubleClick);
            this.lstFields.SelectedValueChanged += new System.EventHandler(this.lstFields_SelectedValueChanged);
            // 
            // cmdApply
            // 
            this.cmdApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdApply.Location = new System.Drawing.Point(303, 380);
            this.cmdApply.Name = "cmdApply";
            this.cmdApply.Size = new System.Drawing.Size(75, 23);
            this.cmdApply.TabIndex = 2;
            this.cmdApply.Text = "&Apply";
            this.cmdApply.UseVisualStyleBackColor = true;
            // 
            // txtMinimum
            // 
            this.txtMinimum.Location = new System.Drawing.Point(241, 222);
            this.txtMinimum.Name = "txtMinimum";
            this.txtMinimum.Size = new System.Drawing.Size(120, 20);
            this.txtMinimum.TabIndex = 3;
            // 
            // cmdOk
            // 
            this.cmdOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdOk.Location = new System.Drawing.Point(141, 380);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(75, 23);
            this.cmdOk.TabIndex = 4;
            this.cmdOk.Text = "&OK";
            this.cmdOk.UseVisualStyleBackColor = true;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(222, 380);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(75, 23);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "&Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            // 
            // txtExpression
            // 
            this.txtExpression.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtExpression.Location = new System.Drawing.Point(15, 311);
            this.txtExpression.Multiline = true;
            this.txtExpression.Name = "txtExpression";
            this.txtExpression.Size = new System.Drawing.Size(363, 63);
            this.txtExpression.TabIndex = 6;
            // 
            // lblSelect
            // 
            this.lblSelect.AutoSize = true;
            this.lblSelect.Location = new System.Drawing.Point(12, 294);
            this.lblSelect.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
            this.lblSelect.Name = "lblSelect";
            this.lblSelect.Size = new System.Drawing.Size(169, 13);
            this.lblSelect.TabIndex = 7;
            this.lblSelect.Text = "SELECT * FROM [Table] WHERE";
            // 
            // lstUniqueValues
            // 
            this.lstUniqueValues.FormattingEnabled = true;
            this.lstUniqueValues.IntegralHeight = false;
            this.lstUniqueValues.Location = new System.Drawing.Point(241, 25);
            this.lstUniqueValues.Name = "lstUniqueValues";
            this.lstUniqueValues.Size = new System.Drawing.Size(120, 140);
            this.lstUniqueValues.TabIndex = 8;
            this.lstUniqueValues.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.list_MouseDoubleClick);
            // 
            // cmdGetUniqueValues
            // 
            this.cmdGetUniqueValues.Location = new System.Drawing.Point(241, 171);
            this.cmdGetUniqueValues.Name = "cmdGetUniqueValues";
            this.cmdGetUniqueValues.Size = new System.Drawing.Size(120, 23);
            this.cmdGetUniqueValues.TabIndex = 9;
            this.cmdGetUniqueValues.Text = "Get";
            this.cmdGetUniqueValues.UseVisualStyleBackColor = true;
            this.cmdGetUniqueValues.Click += new System.EventHandler(this.cmdGetUniqueValues_Click);
            // 
            // cmdEquals
            // 
            this.cmdEquals.Location = new System.Drawing.Point(168, 26);
            this.cmdEquals.Name = "cmdEquals";
            this.cmdEquals.Size = new System.Drawing.Size(67, 23);
            this.cmdEquals.TabIndex = 10;
            this.cmdEquals.Text = "=";
            this.cmdEquals.UseVisualStyleBackColor = true;
            this.cmdEquals.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdNotEquals
            // 
            this.cmdNotEquals.Location = new System.Drawing.Point(168, 55);
            this.cmdNotEquals.Name = "cmdNotEquals";
            this.cmdNotEquals.Size = new System.Drawing.Size(67, 23);
            this.cmdNotEquals.TabIndex = 11;
            this.cmdNotEquals.Text = "<>";
            this.cmdNotEquals.UseVisualStyleBackColor = true;
            this.cmdNotEquals.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdGreater
            // 
            this.cmdGreater.Location = new System.Drawing.Point(168, 84);
            this.cmdGreater.Name = "cmdGreater";
            this.cmdGreater.Size = new System.Drawing.Size(67, 23);
            this.cmdGreater.TabIndex = 12;
            this.cmdGreater.Text = ">";
            this.cmdGreater.UseVisualStyleBackColor = true;
            this.cmdGreater.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdLower
            // 
            this.cmdLower.Location = new System.Drawing.Point(168, 113);
            this.cmdLower.Name = "cmdLower";
            this.cmdLower.Size = new System.Drawing.Size(67, 23);
            this.cmdLower.TabIndex = 13;
            this.cmdLower.Text = "<";
            this.cmdLower.UseVisualStyleBackColor = true;
            this.cmdLower.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdIsNull
            // 
            this.cmdIsNull.Location = new System.Drawing.Point(168, 200);
            this.cmdIsNull.Name = "cmdIsNull";
            this.cmdIsNull.Size = new System.Drawing.Size(67, 23);
            this.cmdIsNull.TabIndex = 14;
            this.cmdIsNull.Text = "Is Null";
            this.cmdIsNull.UseVisualStyleBackColor = true;
            this.cmdIsNull.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdIsNotNull
            // 
            this.cmdIsNotNull.Location = new System.Drawing.Point(168, 229);
            this.cmdIsNotNull.Name = "cmdIsNotNull";
            this.cmdIsNotNull.Size = new System.Drawing.Size(67, 23);
            this.cmdIsNotNull.TabIndex = 15;
            this.cmdIsNotNull.Text = "Is Not Null";
            this.cmdIsNotNull.UseVisualStyleBackColor = true;
            this.cmdIsNotNull.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdGreaterOrEquals
            // 
            this.cmdGreaterOrEquals.Location = new System.Drawing.Point(168, 171);
            this.cmdGreaterOrEquals.Name = "cmdGreaterOrEquals";
            this.cmdGreaterOrEquals.Size = new System.Drawing.Size(67, 23);
            this.cmdGreaterOrEquals.TabIndex = 16;
            this.cmdGreaterOrEquals.Text = ">=";
            this.cmdGreaterOrEquals.UseVisualStyleBackColor = true;
            this.cmdGreaterOrEquals.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdLowerOrEquals
            // 
            this.cmdLowerOrEquals.Location = new System.Drawing.Point(168, 142);
            this.cmdLowerOrEquals.Name = "cmdLowerOrEquals";
            this.cmdLowerOrEquals.Size = new System.Drawing.Size(67, 23);
            this.cmdLowerOrEquals.TabIndex = 17;
            this.cmdLowerOrEquals.Text = "<=";
            this.cmdLowerOrEquals.UseVisualStyleBackColor = true;
            this.cmdLowerOrEquals.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdAnd
            // 
            this.cmdAnd.Location = new System.Drawing.Point(15, 258);
            this.cmdAnd.Name = "cmdAnd";
            this.cmdAnd.Size = new System.Drawing.Size(46, 23);
            this.cmdAnd.TabIndex = 18;
            this.cmdAnd.Text = "And";
            this.cmdAnd.UseVisualStyleBackColor = true;
            this.cmdAnd.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdOr
            // 
            this.cmdOr.Location = new System.Drawing.Point(64, 258);
            this.cmdOr.Name = "cmdOr";
            this.cmdOr.Size = new System.Drawing.Size(46, 23);
            this.cmdOr.TabIndex = 19;
            this.cmdOr.Text = "Or";
            this.cmdOr.UseVisualStyleBackColor = true;
            this.cmdOr.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdNot
            // 
            this.cmdNot.Location = new System.Drawing.Point(116, 258);
            this.cmdNot.Name = "cmdNot";
            this.cmdNot.Size = new System.Drawing.Size(46, 23);
            this.cmdNot.TabIndex = 20;
            this.cmdNot.Text = "Not";
            this.cmdNot.UseVisualStyleBackColor = true;
            this.cmdNot.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // cmdLike
            // 
            this.cmdLike.Location = new System.Drawing.Point(168, 258);
            this.cmdLike.Name = "cmdLike";
            this.cmdLike.Size = new System.Drawing.Size(67, 23);
            this.cmdLike.TabIndex = 21;
            this.cmdLike.Text = "Like";
            this.cmdLike.UseVisualStyleBackColor = true;
            this.cmdLike.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // lblUniqueValues
            // 
            this.lblUniqueValues.AutoSize = true;
            this.lblUniqueValues.Location = new System.Drawing.Point(238, 8);
            this.lblUniqueValues.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
            this.lblUniqueValues.Name = "lblUniqueValues";
            this.lblUniqueValues.Size = new System.Drawing.Size(79, 13);
            this.lblUniqueValues.TabIndex = 22;
            this.lblUniqueValues.Text = "Unique Values:";
            // 
            // cmdParentisis
            // 
            this.cmdParentisis.Location = new System.Drawing.Point(67, 229);
            this.cmdParentisis.Name = "cmdParentisis";
            this.cmdParentisis.Size = new System.Drawing.Size(46, 23);
            this.cmdParentisis.TabIndex = 24;
            this.cmdParentisis.Text = "( )";
            this.cmdParentisis.UseVisualStyleBackColor = true;
            this.cmdParentisis.Click += new System.EventHandler(this.cmdParentisis_Click);
            // 
            // cmdStar
            // 
            this.cmdStar.Location = new System.Drawing.Point(15, 229);
            this.cmdStar.Name = "cmdStar";
            this.cmdStar.Size = new System.Drawing.Size(46, 23);
            this.cmdStar.TabIndex = 25;
            this.cmdStar.Text = "*";
            this.cmdStar.UseVisualStyleBackColor = true;
            this.cmdStar.Click += new System.EventHandler(this.cmdInsert_Click);
            // 
            // l
            // 
            this.l.AutoSize = true;
            this.l.Location = new System.Drawing.Point(238, 205);
            this.l.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
            this.l.Name = "l";
            this.l.Size = new System.Drawing.Size(51, 13);
            this.l.TabIndex = 26;
            this.l.Text = "Minimum:";
            // 
            // lblMaximumValue
            // 
            this.lblMaximumValue.AutoSize = true;
            this.lblMaximumValue.Location = new System.Drawing.Point(238, 244);
            this.lblMaximumValue.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
            this.lblMaximumValue.Name = "lblMaximumValue";
            this.lblMaximumValue.Size = new System.Drawing.Size(54, 13);
            this.lblMaximumValue.TabIndex = 28;
            this.lblMaximumValue.Text = "Maximum:";
            // 
            // txtMaximum
            // 
            this.txtMaximum.Location = new System.Drawing.Point(241, 261);
            this.txtMaximum.Name = "txtMaximum";
            this.txtMaximum.Size = new System.Drawing.Size(120, 20);
            this.txtMaximum.TabIndex = 27;
            // 
            // frmExpressionEditor
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cmdCancel;
            this.ClientSize = new System.Drawing.Size(390, 409);
            this.Controls.Add(this.lblMaximumValue);
            this.Controls.Add(this.txtMaximum);
            this.Controls.Add(this.l);
            this.Controls.Add(this.cmdStar);
            this.Controls.Add(this.cmdParentisis);
            this.Controls.Add(this.lblUniqueValues);
            this.Controls.Add(this.cmdLike);
            this.Controls.Add(this.cmdNot);
            this.Controls.Add(this.cmdOr);
            this.Controls.Add(this.cmdAnd);
            this.Controls.Add(this.cmdLowerOrEquals);
            this.Controls.Add(this.cmdGreaterOrEquals);
            this.Controls.Add(this.cmdIsNotNull);
            this.Controls.Add(this.cmdIsNull);
            this.Controls.Add(this.cmdLower);
            this.Controls.Add(this.cmdGreater);
            this.Controls.Add(this.cmdNotEquals);
            this.Controls.Add(this.cmdEquals);
            this.Controls.Add(this.cmdGetUniqueValues);
            this.Controls.Add(this.lstUniqueValues);
            this.Controls.Add(this.lblSelect);
            this.Controls.Add(this.txtExpression);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.txtMinimum);
            this.Controls.Add(this.cmdApply);
            this.Controls.Add(this.lstFields);
            this.Controls.Add(this.lblFields);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmExpressionEditor";
            this.Text = "Expression Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFields;
        private System.Windows.Forms.ListBox lstFields;
        private System.Windows.Forms.Button cmdApply;
        private System.Windows.Forms.TextBox txtMinimum;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.TextBox txtExpression;
        private System.Windows.Forms.Label lblSelect;
        private System.Windows.Forms.ListBox lstUniqueValues;
        private System.Windows.Forms.Button cmdGetUniqueValues;
        private System.Windows.Forms.Button cmdEquals;
        private System.Windows.Forms.Button cmdNotEquals;
        private System.Windows.Forms.Button cmdGreater;
        private System.Windows.Forms.Button cmdLower;
        private System.Windows.Forms.Button cmdIsNull;
        private System.Windows.Forms.Button cmdIsNotNull;
        private System.Windows.Forms.Button cmdGreaterOrEquals;
        private System.Windows.Forms.Button cmdLowerOrEquals;
        private System.Windows.Forms.Button cmdAnd;
        private System.Windows.Forms.Button cmdOr;
        private System.Windows.Forms.Button cmdNot;
        private System.Windows.Forms.Button cmdLike;
        private System.Windows.Forms.Label lblUniqueValues;
        private System.Windows.Forms.Button cmdParentisis;
        private System.Windows.Forms.Button cmdStar;
        private System.Windows.Forms.Label l;
        private System.Windows.Forms.Label lblMaximumValue;
        private System.Windows.Forms.TextBox txtMaximum;
    }
}
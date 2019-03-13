namespace StockManagementSystem
{
    partial class ItemSetupUi
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
            this.components = new System.ComponentModel.Container();
            this.reorderValidationLabel = new System.Windows.Forms.Label();
            this.itemValiditionLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.reorderLevelTextBox = new System.Windows.Forms.TextBox();
            this.itemnameTextBox = new System.Windows.Forms.TextBox();
            this.companyComboBox = new System.Windows.Forms.ComboBox();
            this.companySetupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.categorySetupBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.companySetupBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.categorySetupBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reorderValidationLabel
            // 
            this.reorderValidationLabel.AutoSize = true;
            this.reorderValidationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reorderValidationLabel.ForeColor = System.Drawing.Color.Red;
            this.reorderValidationLabel.Location = new System.Drawing.Point(354, 217);
            this.reorderValidationLabel.Name = "reorderValidationLabel";
            this.reorderValidationLabel.Size = new System.Drawing.Size(0, 16);
            this.reorderValidationLabel.TabIndex = 40;
            // 
            // itemValiditionLabel
            // 
            this.itemValiditionLabel.AutoSize = true;
            this.itemValiditionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemValiditionLabel.ForeColor = System.Drawing.Color.Red;
            this.itemValiditionLabel.Location = new System.Drawing.Point(351, 169);
            this.itemValiditionLabel.Name = "itemValiditionLabel";
            this.itemValiditionLabel.Size = new System.Drawing.Size(0, 16);
            this.itemValiditionLabel.TabIndex = 39;
            // 
            // SaveButton
            // 
            this.SaveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SaveButton.Location = new System.Drawing.Point(212, 272);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(121, 23);
            this.SaveButton.TabIndex = 38;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // reorderLevelTextBox
            // 
            this.reorderLevelTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reorderLevelTextBox.Location = new System.Drawing.Point(212, 211);
            this.reorderLevelTextBox.Name = "reorderLevelTextBox";
            this.reorderLevelTextBox.Size = new System.Drawing.Size(121, 22);
            this.reorderLevelTextBox.TabIndex = 37;
            // 
            // itemnameTextBox
            // 
            this.itemnameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemnameTextBox.Location = new System.Drawing.Point(212, 162);
            this.itemnameTextBox.Name = "itemnameTextBox";
            this.itemnameTextBox.Size = new System.Drawing.Size(121, 22);
            this.itemnameTextBox.TabIndex = 36;
            // 
            // companyComboBox
            // 
            this.companyComboBox.DataSource = this.companySetupBindingSource;
            this.companyComboBox.DisplayMember = "Name";
            this.companyComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.companyComboBox.FormattingEnabled = true;
            this.companyComboBox.Location = new System.Drawing.Point(212, 98);
            this.companyComboBox.Name = "companyComboBox";
            this.companyComboBox.Size = new System.Drawing.Size(121, 24);
            this.companyComboBox.TabIndex = 35;
            this.companyComboBox.ValueMember = "Id";
            // 
            // companySetupBindingSource
            // 
            this.companySetupBindingSource.DataSource = typeof(StockManagementSystem.Models.CompanySetup);
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DataSource = this.categorySetupBindingSource;
            this.categoryComboBox.DisplayMember = "Name";
            this.categoryComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(212, 41);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(121, 24);
            this.categoryComboBox.TabIndex = 34;
            this.categoryComboBox.ValueMember = "Id";
            // 
            // categorySetupBindingSource
            // 
            this.categorySetupBindingSource.DataSource = typeof(StockManagementSystem.Models.CategorySetup);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(90, 219);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 16);
            this.label4.TabIndex = 30;
            this.label4.Text = "Reorder Level";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(106, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 31;
            this.label3.Text = "Item Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(113, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 16);
            this.label2.TabIndex = 32;
            this.label2.Text = "Company";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(113, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 33;
            this.label1.Text = "Category";
            // 
            // ItemSetupUi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 347);
            this.Controls.Add(this.reorderValidationLabel);
            this.Controls.Add(this.itemValiditionLabel);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.reorderLevelTextBox);
            this.Controls.Add(this.itemnameTextBox);
            this.Controls.Add(this.companyComboBox);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ItemSetupUi";
            this.Text = "Item Setup";
            this.Load += new System.EventHandler(this.ItemSetupUi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.companySetupBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.categorySetupBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label reorderValidationLabel;
        private System.Windows.Forms.Label itemValiditionLabel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox reorderLevelTextBox;
        private System.Windows.Forms.TextBox itemnameTextBox;
        private System.Windows.Forms.ComboBox companyComboBox;
        private System.Windows.Forms.ComboBox categoryComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource companySetupBindingSource;
        private System.Windows.Forms.BindingSource categorySetupBindingSource;
    }
}
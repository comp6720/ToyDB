namespace ToyDB
{
    partial class TOYODB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TOYODB));
            this.resultsBox = new System.Windows.Forms.RichTextBox();
            this.sqlLabel = new System.Windows.Forms.Label();
            this.resultLabel = new System.Windows.Forms.Label();
            this.executeButton = new System.Windows.Forms.Button();
            this.commitButton = new System.Windows.Forms.Button();
            this.sqlInputBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // resultsBox
            // 
            this.resultsBox.Location = new System.Drawing.Point(1000, 474);
            this.resultsBox.Margin = new System.Windows.Forms.Padding(7);
            this.resultsBox.Name = "resultsBox";
            this.resultsBox.Size = new System.Drawing.Size(1790, 527);
            this.resultsBox.TabIndex = 0;
            this.resultsBox.Text = "";
            // 
            // sqlLabel
            // 
            this.sqlLabel.AutoSize = true;
            this.sqlLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sqlLabel.Location = new System.Drawing.Point(784, 338);
            this.sqlLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.sqlLabel.Name = "sqlLabel";
            this.sqlLabel.Size = new System.Drawing.Size(123, 55);
            this.sqlLabel.TabIndex = 1;
            this.sqlLabel.Text = "SQL";
            this.sqlLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resultLabel.Location = new System.Drawing.Point(689, 687);
            this.resultLabel.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(220, 55);
            this.resultLabel.TabIndex = 2;
            this.resultLabel.Text = "RESULT";
            // 
            // executeButton
            // 
            this.executeButton.BackColor = System.Drawing.SystemColors.Info;
            this.executeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.executeButton.Location = new System.Drawing.Point(1183, 1068);
            this.executeButton.Margin = new System.Windows.Forms.Padding(7);
            this.executeButton.Name = "executeButton";
            this.executeButton.Size = new System.Drawing.Size(520, 104);
            this.executeButton.TabIndex = 3;
            this.executeButton.Text = "EXECUTE";
            this.executeButton.UseVisualStyleBackColor = false;
            // 
            // commitButton
            // 
            this.commitButton.BackColor = System.Drawing.SystemColors.Info;
            this.commitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commitButton.Location = new System.Drawing.Point(2126, 1068);
            this.commitButton.Margin = new System.Windows.Forms.Padding(7);
            this.commitButton.Name = "commitButton";
            this.commitButton.Size = new System.Drawing.Size(511, 104);
            this.commitButton.TabIndex = 4;
            this.commitButton.Text = "COMMIT";
            this.commitButton.UseVisualStyleBackColor = false;
            // 
            // sqlInputBox
            // 
            this.sqlInputBox.Location = new System.Drawing.Point(1000, 289);
            this.sqlInputBox.Margin = new System.Windows.Forms.Padding(7);
            this.sqlInputBox.Name = "sqlInputBox";
            this.sqlInputBox.Size = new System.Drawing.Size(1790, 129);
            this.sqlInputBox.TabIndex = 5;
            this.sqlInputBox.Text = "";
            // 
            // TOYODB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(19F, 37F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(3460, 1725);
            this.Controls.Add(this.sqlInputBox);
            this.Controls.Add(this.commitButton);
            this.Controls.Add(this.executeButton);
            this.Controls.Add(this.resultLabel);
            this.Controls.Add(this.sqlLabel);
            this.Controls.Add(this.resultsBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(7);
            this.MinimumSize = new System.Drawing.Size(1000, 1000);
            this.Name = "TOYODB";
            this.Text = "ToyDB";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox resultsBox;
        private System.Windows.Forms.Label sqlLabel;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Button executeButton;
        private System.Windows.Forms.Button commitButton;
        private System.Windows.Forms.RichTextBox sqlInputBox;
    }
}


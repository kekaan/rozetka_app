
namespace rozetka_desk
{
    partial class AddingDeviceForm
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
            this.name_textBox = new System.Windows.Forms.TextBox();
            this.ip_textBox = new System.Windows.Forms.TextBox();
            this.adding_button = new System.Windows.Forms.Button();
            this.back_adding_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name_textBox
            // 
            this.name_textBox.Location = new System.Drawing.Point(117, 32);
            this.name_textBox.Name = "name_textBox";
            this.name_textBox.Size = new System.Drawing.Size(100, 20);
            this.name_textBox.TabIndex = 0;
            // 
            // ip_textBox
            // 
            this.ip_textBox.Location = new System.Drawing.Point(117, 80);
            this.ip_textBox.Name = "ip_textBox";
            this.ip_textBox.Size = new System.Drawing.Size(100, 20);
            this.ip_textBox.TabIndex = 1;
            // 
            // adding_button
            // 
            this.adding_button.Location = new System.Drawing.Point(128, 120);
            this.adding_button.Name = "adding_button";
            this.adding_button.Size = new System.Drawing.Size(75, 23);
            this.adding_button.TabIndex = 2;
            this.adding_button.Text = "Add";
            this.adding_button.UseVisualStyleBackColor = true;
            this.adding_button.Click += new System.EventHandler(this.adding_button_Click);
            // 
            // back_adding_button
            // 
            this.back_adding_button.Location = new System.Drawing.Point(128, 156);
            this.back_adding_button.Name = "back_adding_button";
            this.back_adding_button.Size = new System.Drawing.Size(75, 23);
            this.back_adding_button.TabIndex = 3;
            this.back_adding_button.Text = "Back";
            this.back_adding_button.UseVisualStyleBackColor = true;
            this.back_adding_button.Click += new System.EventHandler(this.back_adding_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(223, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Device Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Device IP";
            // 
            // AddingDeviceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(340, 191);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.back_adding_button);
            this.Controls.Add(this.adding_button);
            this.Controls.Add(this.ip_textBox);
            this.Controls.Add(this.name_textBox);
            this.Name = "AddingDeviceForm";
            this.Text = "AddingDevice";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddingDeviceForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name_textBox;
        private System.Windows.Forms.TextBox ip_textBox;
        private System.Windows.Forms.Button adding_button;
        private System.Windows.Forms.Button back_adding_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
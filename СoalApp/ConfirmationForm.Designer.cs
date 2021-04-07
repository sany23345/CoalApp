
namespace СoalApp
{
    partial class ConfirmationForm
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
            this.telTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.providerTextBox = new System.Windows.Forms.TextBox();
            this.stampTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pricePerTonTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.requiredWeightTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.addressTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.distanceTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.shippingCostTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.fullPriceTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 286);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(394, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Введите свой номер телефона для подтверждения заказа";
            // 
            // telTextBox
            // 
            this.telTextBox.Location = new System.Drawing.Point(12, 307);
            this.telTextBox.MaxLength = 12;
            this.telTextBox.Name = "telTextBox";
            this.telTextBox.Size = new System.Drawing.Size(377, 26);
            this.telTextBox.TabIndex = 1;
            this.telTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.telTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Поставщик";
            // 
            // providerTextBox
            // 
            this.providerTextBox.Location = new System.Drawing.Point(200, 12);
            this.providerTextBox.Name = "providerTextBox";
            this.providerTextBox.ReadOnly = true;
            this.providerTextBox.Size = new System.Drawing.Size(283, 26);
            this.providerTextBox.TabIndex = 3;
            // 
            // stampTextBox
            // 
            this.stampTextBox.Location = new System.Drawing.Point(200, 44);
            this.stampTextBox.Name = "stampTextBox";
            this.stampTextBox.ReadOnly = true;
            this.stampTextBox.Size = new System.Drawing.Size(283, 26);
            this.stampTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 19);
            this.label3.TabIndex = 4;
            this.label3.Text = "Марка угля";
            // 
            // pricePerTonTextBox
            // 
            this.pricePerTonTextBox.Location = new System.Drawing.Point(199, 76);
            this.pricePerTonTextBox.Name = "pricePerTonTextBox";
            this.pricePerTonTextBox.ReadOnly = true;
            this.pricePerTonTextBox.Size = new System.Drawing.Size(283, 26);
            this.pricePerTonTextBox.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 19);
            this.label4.TabIndex = 6;
            this.label4.Text = "Ценна за тонну(руб.)";
            // 
            // requiredWeightTextBox
            // 
            this.requiredWeightTextBox.Location = new System.Drawing.Point(199, 108);
            this.requiredWeightTextBox.Name = "requiredWeightTextBox";
            this.requiredWeightTextBox.ReadOnly = true;
            this.requiredWeightTextBox.Size = new System.Drawing.Size(283, 26);
            this.requiredWeightTextBox.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 19);
            this.label5.TabIndex = 8;
            this.label5.Text = "Требуемая масса(тонн)";
            // 
            // addressTextBox
            // 
            this.addressTextBox.Location = new System.Drawing.Point(199, 140);
            this.addressTextBox.Multiline = true;
            this.addressTextBox.Name = "addressTextBox";
            this.addressTextBox.ReadOnly = true;
            this.addressTextBox.Size = new System.Drawing.Size(283, 47);
            this.addressTextBox.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 143);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(122, 19);
            this.label6.TabIndex = 10;
            this.label6.Text = "Адресс доставки";
            // 
            // distanceTextBox
            // 
            this.distanceTextBox.Location = new System.Drawing.Point(199, 193);
            this.distanceTextBox.Name = "distanceTextBox";
            this.distanceTextBox.ReadOnly = true;
            this.distanceTextBox.Size = new System.Drawing.Size(283, 26);
            this.distanceTextBox.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 19);
            this.label7.TabIndex = 12;
            this.label7.Text = "Растояние(км)";
            // 
            // shippingCostTextBox
            // 
            this.shippingCostTextBox.Location = new System.Drawing.Point(199, 225);
            this.shippingCostTextBox.Name = "shippingCostTextBox";
            this.shippingCostTextBox.ReadOnly = true;
            this.shippingCostTextBox.Size = new System.Drawing.Size(283, 26);
            this.shippingCostTextBox.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 228);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(185, 19);
            this.label8.TabIndex = 14;
            this.label8.Text = "Стоимость доставки(руб.)";
            // 
            // fullPriceTextBox
            // 
            this.fullPriceTextBox.Location = new System.Drawing.Point(199, 257);
            this.fullPriceTextBox.Name = "fullPriceTextBox";
            this.fullPriceTextBox.ReadOnly = true;
            this.fullPriceTextBox.Size = new System.Drawing.Size(283, 26);
            this.fullPriceTextBox.TabIndex = 17;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 260);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(165, 19);
            this.label9.TabIndex = 16;
            this.label9.Text = "Общая стоимость(руб.)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(252, 362);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 30);
            this.button1.TabIndex = 18;
            this.button1.Text = "Подтвердить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(12, 365);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(234, 26);
            this.textBox10.TabIndex = 19;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 343);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(207, 19);
            this.label10.TabIndex = 20;
            this.label10.Text = "Введите код указанный в смс";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(395, 304);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 30);
            this.button2.TabIndex = 21;
            this.button2.Text = "Ок";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(370, 362);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 30);
            this.button3.TabIndex = 22;
            this.button3.Text = "Назад";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(495, 399);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fullPriceTextBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.shippingCostTextBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.distanceTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.addressTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.requiredWeightTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pricePerTonTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.stampTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.providerTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.telTextBox);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ConfirmationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Подтверждение заказа";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfirmationForm_FormClosed);
            this.VisibleChanged += new System.EventHandler(this.ConfirmationForm_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox telTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox providerTextBox;
        private System.Windows.Forms.TextBox stampTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox pricePerTonTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox requiredWeightTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox addressTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox distanceTextBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox shippingCostTextBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox fullPriceTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}
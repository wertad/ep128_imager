namespace Ep128_imager
{
    partial class Main_Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Form));
            this.textBox_watchFolder = new System.Windows.Forms.TextBox();
            this.button_watchBrowser = new System.Windows.Forms.Button();
            this.label_watchFolder = new System.Windows.Forms.Label();
            this.comboBox_ImageDrives = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox_console = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // textBox_watchFolder
            // 
            this.textBox_watchFolder.Location = new System.Drawing.Point(8, 43);
            this.textBox_watchFolder.Name = "textBox_watchFolder";
            this.textBox_watchFolder.Size = new System.Drawing.Size(449, 20);
            this.textBox_watchFolder.TabIndex = 0;
            this.textBox_watchFolder.TextChanged += new System.EventHandler(this.textBox_watchFolder_TextChanged);
            // 
            // button_watchBrowser
            // 
            this.button_watchBrowser.Location = new System.Drawing.Point(457, 42);
            this.button_watchBrowser.Name = "button_watchBrowser";
            this.button_watchBrowser.Size = new System.Drawing.Size(26, 23);
            this.button_watchBrowser.TabIndex = 1;
            this.button_watchBrowser.Text = "...";
            this.button_watchBrowser.UseVisualStyleBackColor = true;
            this.button_watchBrowser.Click += new System.EventHandler(this.button_watchBrowser_Click);
            // 
            // label_watchFolder
            // 
            this.label_watchFolder.AutoSize = true;
            this.label_watchFolder.Location = new System.Drawing.Point(8, 24);
            this.label_watchFolder.Name = "label_watchFolder";
            this.label_watchFolder.Size = new System.Drawing.Size(71, 13);
            this.label_watchFolder.TabIndex = 2;
            this.label_watchFolder.Text = "Watch folder:";
            // 
            // comboBox_ImageDrives
            // 
            this.comboBox_ImageDrives.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_ImageDrives.FormattingEnabled = true;
            this.comboBox_ImageDrives.Location = new System.Drawing.Point(489, 43);
            this.comboBox_ImageDrives.Name = "comboBox_ImageDrives";
            this.comboBox_ImageDrives.Size = new System.Drawing.Size(125, 21);
            this.comboBox_ImageDrives.TabIndex = 3;
            this.comboBox_ImageDrives.SelectedIndexChanged += new System.EventHandler(this.comboBox_ImageDrives_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(479, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Floppy Image Drives (800KB):";
            // 
            // richTextBox_console
            // 
            this.richTextBox_console.BackColor = System.Drawing.Color.Black;
            this.richTextBox_console.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox_console.ForeColor = System.Drawing.Color.White;
            this.richTextBox_console.Location = new System.Drawing.Point(0, 71);
            this.richTextBox_console.Name = "richTextBox_console";
            this.richTextBox_console.ReadOnly = true;
            this.richTextBox_console.Size = new System.Drawing.Size(626, 418);
            this.richTextBox_console.TabIndex = 5;
            this.richTextBox_console.Text = "";
            // 
            // Main_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(626, 489);
            this.Controls.Add(this.richTextBox_console);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_ImageDrives);
            this.Controls.Add(this.label_watchFolder);
            this.Controls.Add(this.button_watchBrowser);
            this.Controls.Add(this.textBox_watchFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main_Form";
            this.Text = "Ep128 Imager";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_watchFolder;
        private System.Windows.Forms.Button button_watchBrowser;
        private System.Windows.Forms.Label label_watchFolder;
        private System.Windows.Forms.ComboBox comboBox_ImageDrives;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox_console;
    }
}


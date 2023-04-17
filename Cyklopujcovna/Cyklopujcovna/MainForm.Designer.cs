
namespace cyklopujcovna
{
    partial class MainForm
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
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnRentals = new System.Windows.Forms.Button();
            this.btnBicycles = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelName = new System.Windows.Forms.Panel();
            this.labelLogo = new System.Windows.Forms.Label();
            this.panelUp = new System.Windows.Forms.Panel();
            this.labelTop = new System.Windows.Forms.Label();
            this.panelChildForm = new System.Windows.Forms.Panel();
            this.btnThreads = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.panelName.SuspendLayout();
            this.panelUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.panelMenu.Controls.Add(this.btnThreads);
            this.panelMenu.Controls.Add(this.btnRentals);
            this.panelMenu.Controls.Add(this.btnBicycles);
            this.panelMenu.Controls.Add(this.btnLogout);
            this.panelMenu.Controls.Add(this.panelName);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 678);
            this.panelMenu.TabIndex = 0;
            // 
            // btnRentals
            // 
            this.btnRentals.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRentals.FlatAppearance.BorderSize = 0;
            this.btnRentals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRentals.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnRentals.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnRentals.Image = global::cyklopujcovna.Properties.Resources.document_32;
            this.btnRentals.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRentals.Location = new System.Drawing.Point(0, 160);
            this.btnRentals.Name = "btnRentals";
            this.btnRentals.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnRentals.Size = new System.Drawing.Size(220, 60);
            this.btnRentals.TabIndex = 3;
            this.btnRentals.Text = "  Výpůjčky";
            this.btnRentals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRentals.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnRentals.UseVisualStyleBackColor = true;
            this.btnRentals.Click += new System.EventHandler(this.btnRentals_Click);
            // 
            // btnBicycles
            // 
            this.btnBicycles.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBicycles.FlatAppearance.BorderSize = 0;
            this.btnBicycles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBicycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnBicycles.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBicycles.Image = global::cyklopujcovna.Properties.Resources.bicycle_32;
            this.btnBicycles.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBicycles.Location = new System.Drawing.Point(0, 100);
            this.btnBicycles.Name = "btnBicycles";
            this.btnBicycles.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnBicycles.Size = new System.Drawing.Size(220, 60);
            this.btnBicycles.TabIndex = 2;
            this.btnBicycles.Text = "  Kola";
            this.btnBicycles.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBicycles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBicycles.UseVisualStyleBackColor = true;
            this.btnBicycles.Click += new System.EventHandler(this.btnBicycles_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnLogout.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLogout.Image = global::cyklopujcovna.Properties.Resources.logout_32;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(0, 618);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(220, 60);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "  Odhlásit";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panelName
            // 
            this.panelName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(102)))), ((int)(((byte)(175)))));
            this.panelName.Controls.Add(this.labelLogo);
            this.panelName.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelName.Location = new System.Drawing.Point(0, 0);
            this.panelName.Name = "panelName";
            this.panelName.Size = new System.Drawing.Size(220, 100);
            this.panelName.TabIndex = 0;
            // 
            // labelLogo
            // 
            this.labelLogo.AutoSize = true;
            this.labelLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelLogo.ForeColor = System.Drawing.Color.LightGray;
            this.labelLogo.Location = new System.Drawing.Point(22, 38);
            this.labelLogo.Name = "labelLogo";
            this.labelLogo.Size = new System.Drawing.Size(180, 30);
            this.labelLogo.TabIndex = 0;
            this.labelLogo.Text = "Cyklopůjčovna";
            // 
            // panelUp
            // 
            this.panelUp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(144)))), ((int)(((byte)(248)))));
            this.panelUp.Controls.Add(this.labelTop);
            this.panelUp.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelUp.Location = new System.Drawing.Point(220, 0);
            this.panelUp.Name = "panelUp";
            this.panelUp.Size = new System.Drawing.Size(958, 100);
            this.panelUp.TabIndex = 1;
            // 
            // labelTop
            // 
            this.labelTop.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTop.Font = new System.Drawing.Font("Microsoft Tai Le", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTop.ForeColor = System.Drawing.Color.White;
            this.labelTop.Location = new System.Drawing.Point(376, 33);
            this.labelTop.Name = "labelTop";
            this.labelTop.Size = new System.Drawing.Size(259, 37);
            this.labelTop.TabIndex = 0;
            this.labelTop.Text = "Cyklopůjčovna";
            this.labelTop.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelChildForm
            // 
            this.panelChildForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelChildForm.Location = new System.Drawing.Point(220, 100);
            this.panelChildForm.Margin = new System.Windows.Forms.Padding(0);
            this.panelChildForm.Name = "panelChildForm";
            this.panelChildForm.Padding = new System.Windows.Forms.Padding(50, 10, 50, 10);
            this.panelChildForm.Size = new System.Drawing.Size(958, 578);
            this.panelChildForm.TabIndex = 2;
            // 
            // btnThreads
            // 
            this.btnThreads.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThreads.FlatAppearance.BorderSize = 0;
            this.btnThreads.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThreads.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnThreads.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnThreads.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThreads.Location = new System.Drawing.Point(0, 837);
            this.btnThreads.Name = "btnThreads";
            this.btnThreads.Padding = new System.Windows.Forms.Padding(11, 0, 0, 0);
            this.btnThreads.Size = new System.Drawing.Size(220, 60);
            this.btnThreads.TabIndex = 4;
            this.btnThreads.Text = "Vlákna";
            this.btnThreads.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThreads.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThreads.UseVisualStyleBackColor = true;
            this.btnThreads.Click += new System.EventHandler(this.btnThreads_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1178, 678);
            this.Controls.Add(this.panelChildForm);
            this.Controls.Add(this.panelUp);
            this.Controls.Add(this.panelMenu);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.panelMenu.ResumeLayout(false);
            this.panelName.ResumeLayout(false);
            this.panelName.PerformLayout();
            this.panelUp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelName;
        private System.Windows.Forms.Panel panelUp;
        private System.Windows.Forms.Label labelTop;
        private System.Windows.Forms.Label labelLogo;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelChildForm;
        private System.Windows.Forms.Button btnBicycles;
        private System.Windows.Forms.Button btnRentals;
        private System.Windows.Forms.Button btnThreads;
    }
}


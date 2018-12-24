namespace Landscaper {
    partial class LandscaperForm {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent() {
            this.ReloadBtn = new System.Windows.Forms.Button();
            this.SeaLevelBar = new System.Windows.Forms.TrackBar();
            this.Show3DBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.SeaLevelBar)).BeginInit();
            this.SuspendLayout();
            // 
            // ReloadBtn
            // 
            this.ReloadBtn.Location = new System.Drawing.Point(0, 0);
            this.ReloadBtn.Name = "ReloadBtn";
            this.ReloadBtn.Size = new System.Drawing.Size(75, 43);
            this.ReloadBtn.TabIndex = 0;
            this.ReloadBtn.Text = "Reload";
            this.ReloadBtn.UseVisualStyleBackColor = true;
            this.ReloadBtn.Click += new System.EventHandler(this.ReloadBtn_Click);
            // 
            // SeaLevelBar
            // 
            this.SeaLevelBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.SeaLevelBar.Location = new System.Drawing.Point(609, 0);
            this.SeaLevelBar.Maximum = 256;
            this.SeaLevelBar.Name = "SeaLevelBar";
            this.SeaLevelBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.SeaLevelBar.Size = new System.Drawing.Size(69, 644);
            this.SeaLevelBar.TabIndex = 1;
            this.SeaLevelBar.Value = 64;
            this.SeaLevelBar.ValueChanged += new System.EventHandler(this.SeaLevelBar_ValueChanged);
            // 
            // Show3DBtn
            // 
            this.Show3DBtn.Location = new System.Drawing.Point(0, 50);
            this.Show3DBtn.Name = "Show3DBtn";
            this.Show3DBtn.Size = new System.Drawing.Size(75, 43);
            this.Show3DBtn.TabIndex = 2;
            this.Show3DBtn.Text = "3D";
            this.Show3DBtn.UseVisualStyleBackColor = true;
            this.Show3DBtn.Click += new System.EventHandler(this.Show3DBtn_Click);
            // 
            // LandscaperForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 644);
            this.Controls.Add(this.Show3DBtn);
            this.Controls.Add(this.SeaLevelBar);
            this.Controls.Add(this.ReloadBtn);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "LandscaperForm";
            this.Text = "Landscaper";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.LandscaperForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.LandscaperForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.SeaLevelBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ReloadBtn;
        private System.Windows.Forms.TrackBar SeaLevelBar;
        private System.Windows.Forms.Button Show3DBtn;
    }
}


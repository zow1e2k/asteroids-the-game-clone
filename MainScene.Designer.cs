namespace asteroids_the_game_clone {
    public partial class MainScene {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent() {
            resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScene));
            this.shipPicture = new System.Windows.Forms.PictureBox();
            //this.asteroidPicture = new System.Windows.Forms.PictureBox();
            //this.bulletPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.shipPicture)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.asteroidPicture)).BeginInit();
            //((System.ComponentModel.ISupportInitialize)(this.bulletPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // shipPicture
            // 
            this.shipPicture.Image = ((System.Drawing.Image)(resources.GetObject("shipPicture.Image")));
            this.shipPicture.Location = new System.Drawing.Point(358, 357);
            this.shipPicture.Name = "shipPicture";
            this.shipPicture.Size = new System.Drawing.Size(52, 42);
            this.shipPicture.TabIndex = 0;
            this.shipPicture.TabStop = false;
            // 
            // asteroidPicture
            // 
            //this.asteroidPicture.Image = ((System.Drawing.Image)(resources.GetObject("asteroidPicture.Image")));
            //this.asteroidPicture.Location = new System.Drawing.Point(551, 242);
            //this.asteroidPicture.Name = "asteroidPicture";
            //this.asteroidPicture.Size = new System.Drawing.Size(40, 36);
            //this.asteroidPicture.TabIndex = 1;
            //this.asteroidPicture.TabStop = false;
            // 
            // bulletPicture
            // 
            //this.bulletPicture.Image = ((System.Drawing.Image)(resources.GetObject("bulletPicture.Image")));
            //this.bulletPicture.Location = new System.Drawing.Point(207, 242);
            //this.bulletPicture.Name = "bulletPicture";
            //this.bulletPicture.Size = new System.Drawing.Size(27, 24);
            //this.bulletPicture.TabIndex = 2;
            //this.bulletPicture.TabStop = false;
            //this.bulletPicture.Visible = false;
            // 
            // MainScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            //this.Controls.Add(this.bulletPicture);
            //this.Controls.Add(this.asteroidPicture);
            this.Controls.Add(this.shipPicture);
            this.MaximumSize = new System.Drawing.Size(800, 600);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "MainScene";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SceneLoad);
            ((System.ComponentModel.ISupportInitialize)(this.shipPicture)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.asteroidPicture)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.bulletPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox asteroidPicture;
        private System.Windows.Forms.PictureBox bulletPicture;
    }
}


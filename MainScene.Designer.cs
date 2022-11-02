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
            //this.bulletPicture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.shipPicture)).BeginInit();
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
            // bulletPicture
            // 
            /*this.bulletPicture.Image = ((System.Drawing.Image)(resources.GetObject("bulletPicture.Image")));
            this.bulletPicture.Location = new System.Drawing.Point(370, 307);
            this.bulletPicture.Name = "bulletPicture";
            this.bulletPicture.Size = new System.Drawing.Size(28, 27);
            this.bulletPicture.Visible = false;
            this.bulletPicture.TabIndex = 1;
            this.bulletPicture.TabStop = false;*/
            // 
            // MainScene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            //this.Controls.Add(this.bulletPicture);
            this.Controls.Add(this.shipPicture);
            this.Name = "MainScene";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.SceneLoad);
            ((System.ComponentModel.ISupportInitialize)(this.shipPicture)).EndInit();
            //((System.ComponentModel.ISupportInitialize)(this.bulletPicture)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
    }
}


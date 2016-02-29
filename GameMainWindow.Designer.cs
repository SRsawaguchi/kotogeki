namespace TypingShoot
{
    partial class GameMainWindow
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose (bool disposing)
        {
            if ( disposing && ( components != null ) ) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent ()
        {
            this.gameArea = new System.Windows.Forms.PictureBox();
            this.targetStringPanel = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.targetString_roman = new System.Windows.Forms.Label();
            this.targetString_kana = new System.Windows.Forms.Label();
            this.targetString_kanji = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.quitGameButton = new System.Windows.Forms.Button();
            this.poseLabel = new System.Windows.Forms.Label();
            this.pBoxLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.gameArea)).BeginInit();
            this.targetStringPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // gameArea
            // 
            this.gameArea.Location = new System.Drawing.Point(12, 12);
            this.gameArea.Name = "gameArea";
            this.gameArea.Size = new System.Drawing.Size(800, 450);
            this.gameArea.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.gameArea.TabIndex = 0;
            this.gameArea.TabStop = false;
            // 
            // targetStringPanel
            // 
            this.targetStringPanel.Controls.Add(this.label2);
            this.targetStringPanel.Controls.Add(this.label1);
            this.targetStringPanel.Controls.Add(this.targetString_roman);
            this.targetStringPanel.Controls.Add(this.targetString_kana);
            this.targetStringPanel.Controls.Add(this.targetString_kanji);
            this.targetStringPanel.Location = new System.Drawing.Point(226, 470);
            this.targetStringPanel.Name = "targetStringPanel";
            this.targetStringPanel.Size = new System.Drawing.Size(412, 133);
            this.targetStringPanel.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 101);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "プログラム開発";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 2;
            // 
            // targetString_roman
            // 
            this.targetString_roman.AutoSize = true;
            this.targetString_roman.Location = new System.Drawing.Point(10, 69);
            this.targetString_roman.Name = "targetString_roman";
            this.targetString_roman.Size = new System.Drawing.Size(53, 12);
            this.targetString_roman.TabIndex = 2;
            this.targetString_roman.Text = "教室装飾";
            // 
            // targetString_kana
            // 
            this.targetString_kana.AutoSize = true;
            this.targetString_kana.Location = new System.Drawing.Point(10, 40);
            this.targetString_kana.Name = "targetString_kana";
            this.targetString_kana.Size = new System.Drawing.Size(204, 12);
            this.targetString_kana.TabIndex = 1;
            this.targetString_kana.Text = "サウンド制作 小玉恭兵(アプリ開発コース）";
            this.targetString_kana.Click += new System.EventHandler(this.targetString_kana_Click);
            // 
            // targetString_kanji
            // 
            this.targetString_kanji.AutoSize = true;
            this.targetString_kanji.Location = new System.Drawing.Point(13, 11);
            this.targetString_kanji.Name = "targetString_kanji";
            this.targetString_kanji.Size = new System.Drawing.Size(63, 12);
            this.targetString_kanji.TabIndex = 0;
            this.targetString_kanji.Text = "イラスト制作";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(13, 470);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(198, 34);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "ゲームスタート";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(12, 517);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(198, 34);
            this.pauseButton.TabIndex = 3;
            this.pauseButton.Text = "一時停止";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // quitGameButton
            // 
            this.quitGameButton.Location = new System.Drawing.Point(13, 560);
            this.quitGameButton.Name = "quitGameButton";
            this.quitGameButton.Size = new System.Drawing.Size(198, 34);
            this.quitGameButton.TabIndex = 3;
            this.quitGameButton.Text = "タイトル画面に戻る";
            this.quitGameButton.UseVisualStyleBackColor = true;
            this.quitGameButton.Click += new System.EventHandler(this.quitGameButton_Click);
            // 
            // poseLabel
            // 
            this.poseLabel.AutoSize = true;
            this.poseLabel.Font = new System.Drawing.Font("MS UI Gothic", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.poseLabel.Location = new System.Drawing.Point(712, 539);
            this.poseLabel.Name = "poseLabel";
            this.poseLabel.Size = new System.Drawing.Size(0, 20);
            this.poseLabel.TabIndex = 2;
            // 
            // pBoxLogo
            // 
            this.pBoxLogo.Location = new System.Drawing.Point(664, 470);
            this.pBoxLogo.Name = "pBoxLogo";
            this.pBoxLogo.Size = new System.Drawing.Size(150, 133);
            this.pBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pBoxLogo.TabIndex = 4;
            this.pBoxLogo.TabStop = false;
            // 
            // GameMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 615);
            this.Controls.Add(this.pBoxLogo);
            this.Controls.Add(this.quitGameButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.poseLabel);
            this.Controls.Add(this.targetStringPanel);
            this.Controls.Add(this.gameArea);
            this.Name = "GameMainWindow";
            this.Text = "コトゲキ";
            this.Load += new System.EventHandler(this.GameMainWindow_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnInputKeyboard);
            ((System.ComponentModel.ISupportInitialize)(this.gameArea)).EndInit();
            this.targetStringPanel.ResumeLayout(false);
            this.targetStringPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox gameArea;
        private System.Windows.Forms.Panel targetStringPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label targetString_roman;
        private System.Windows.Forms.Label targetString_kana;
        private System.Windows.Forms.Label targetString_kanji;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button quitGameButton;
        private System.Windows.Forms.Label poseLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pBoxLogo;
    }
}


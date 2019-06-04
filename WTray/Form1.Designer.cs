namespace WTray
{
    partial class FormMain
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
            this.picIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuWTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.прописатьсяВАвтозагрузкуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.убратьИзАвтозагрузкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.скопироватьВБуферОбменаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.настройкиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerDraw = new System.Windows.Forms.Timer(this.components);
            this.timerDownload = new System.Windows.Forms.Timer(this.components);
            this.обновитьПрогнозToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuWTray.SuspendLayout();
            this.SuspendLayout();
            // 
            // picIcon
            // 
            this.picIcon.BalloonTipText = "...";
            this.picIcon.BalloonTipTitle = "...";
            this.picIcon.ContextMenuStrip = this.contextMenuWTray;
            this.picIcon.Text = "Осадки...";
            this.picIcon.Visible = true;
            this.picIcon.BalloonTipClosed += new System.EventHandler(this.picIcon_BalloonTipClosed);
            this.picIcon.BalloonTipClicked += new System.EventHandler(this.picIcon_BalloonTipClicked);
            this.picIcon.BalloonTipShown += new System.EventHandler(this.picIcon_BalloonTipShown);
            this.picIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picIcon_MouseClick);
            // 
            // contextMenuWTray
            // 
            this.contextMenuWTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.прописатьсяВАвтозагрузкуToolStripMenuItem,
            this.убратьИзАвтозагрузкиToolStripMenuItem,
            this.toolStripSeparator1,
            this.обновитьПрогнозToolStripMenuItem,
            this.скопироватьВБуферОбменаToolStripMenuItem,
            this.toolStripSeparator3,
            this.настройкиToolStripMenuItem,
            this.toolStripSeparator2,
            this.выходToolStripMenuItem});
            this.contextMenuWTray.Name = "contextMenuWTray";
            this.contextMenuWTray.Size = new System.Drawing.Size(236, 176);
            // 
            // прописатьсяВАвтозагрузкуToolStripMenuItem
            // 
            this.прописатьсяВАвтозагрузкуToolStripMenuItem.Name = "прописатьсяВАвтозагрузкуToolStripMenuItem";
            this.прописатьсяВАвтозагрузкуToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.прописатьсяВАвтозагрузкуToolStripMenuItem.Text = "Прописаться в автозагрузку";
            this.прописатьсяВАвтозагрузкуToolStripMenuItem.Click += new System.EventHandler(this.прописатьсяВАвтозагрузкуToolStripMenuItem_Click);
            // 
            // убратьИзАвтозагрузкиToolStripMenuItem
            // 
            this.убратьИзАвтозагрузкиToolStripMenuItem.Name = "убратьИзАвтозагрузкиToolStripMenuItem";
            this.убратьИзАвтозагрузкиToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.убратьИзАвтозагрузкиToolStripMenuItem.Text = "Убрать из автозагрузки";
            this.убратьИзАвтозагрузкиToolStripMenuItem.Click += new System.EventHandler(this.убратьИзАвтозагрузкиToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(232, 6);
            // 
            // скопироватьВБуферОбменаToolStripMenuItem
            // 
            this.скопироватьВБуферОбменаToolStripMenuItem.Name = "скопироватьВБуферОбменаToolStripMenuItem";
            this.скопироватьВБуферОбменаToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.скопироватьВБуферОбменаToolStripMenuItem.Text = "Скопировать в буфер обмена";
            this.скопироватьВБуферОбменаToolStripMenuItem.Click += new System.EventHandler(this.скопироватьВБуферОбменаToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(232, 6);
            // 
            // настройкиToolStripMenuItem
            // 
            this.настройкиToolStripMenuItem.Name = "настройкиToolStripMenuItem";
            this.настройкиToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.настройкиToolStripMenuItem.Text = "Настройки";
            this.настройкиToolStripMenuItem.Click += new System.EventHandler(this.настройкиToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(232, 6);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // txtIcon
            // 
            this.txtIcon.BalloonTipText = "...";
            this.txtIcon.BalloonTipTitle = "...";
            this.txtIcon.ContextMenuStrip = this.contextMenuWTray;
            this.txtIcon.Text = "Температура...";
            this.txtIcon.Visible = true;
            this.txtIcon.BalloonTipClosed += new System.EventHandler(this.txtIcon_BalloonTipClosed);
            this.txtIcon.BalloonTipClicked += new System.EventHandler(this.txtIcon_BalloonTipClicked);
            this.txtIcon.BalloonTipShown += new System.EventHandler(this.txtIcon_BalloonTipShown);
            this.txtIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtIcon_MouseClick);
            // 
            // timerDraw
            // 
            this.timerDraw.Tick += new System.EventHandler(this.timerDraw_Tick);
            // 
            // timerDownload
            // 
            this.timerDownload.Tick += new System.EventHandler(this.timerDownload_Tick);
            // 
            // обновитьПрогнозToolStripMenuItem
            // 
            this.обновитьПрогнозToolStripMenuItem.Name = "обновитьПрогнозToolStripMenuItem";
            this.обновитьПрогнозToolStripMenuItem.Size = new System.Drawing.Size(235, 22);
            this.обновитьПрогнозToolStripMenuItem.Text = "Обновить прогноз";
            this.обновитьПрогнозToolStripMenuItem.Click += new System.EventHandler(this.обновитьПрогнозToolStripMenuItem_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormMain";
            this.ShowInTaskbar = false;
            this.Text = "WTray";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.contextMenuWTray.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon picIcon;
        private System.Windows.Forms.NotifyIcon txtIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuWTray;
        private System.Windows.Forms.ToolStripMenuItem настройкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.Timer timerDraw;
        private System.Windows.Forms.Timer timerDownload;
        private System.Windows.Forms.ToolStripMenuItem прописатьсяВАвтозагрузкуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem скопироватьВБуферОбменаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem убратьИзАвтозагрузкиToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem обновитьПрогнозToolStripMenuItem;
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using Microsoft.Win32;


namespace WTray
{
    public partial class FormMain : Form
    {
        //иконка с картинкой
        Bitmap picBitmap = new Bitmap(16, 16);
        //рисовалка по иконке с картинкой
        Graphics picGraphics;

        //иконка с текстом
        Bitmap txtBitmap = new Bitmap(16, 16);
        //рисовалка по иконке с текстом
        Graphics txtGraphics;

        //шрифт для написания
        Font textFont = new Font("Arial Narrow", 8);

        //настройки приложения
        WTrayOptions wTrayOptions = new WTrayOptions();

        //форма с настройками приложения
        FormOptions formOptions = new FormOptions();

        //веб-клиент для работы с удалёнными файлами
        WebClient webClient = new WebClient();

        //тут будет лежать вся погода
        WTrayWeather wTrayWeather = new WTrayWeather();

        bool tipShown = false;

        //автоматически после загрузки файла
        public void OnDownloadFileCompleted(Object sender, AsyncCompletedEventArgs e)  //по окончании загрузки контента
        {
            //если загрузка нового файла прошла без ошибок
            if (e.Error == null)
            {
                //то начинаем работу с текущим файлом
                FileInfo fileInfoCurrent = new FileInfo(System.Environment.CurrentDirectory + "\\Weather\\" + wTrayOptions.CityOption.ToString() + ".xml");
                //и если текущий файл существует, то мы его удаляем
                if (fileInfoCurrent.Exists == true) fileInfoCurrent.Delete();

                //начинаем рабоать с новым файлом
                FileInfo fileInfoNew = new FileInfo(System.Environment.CurrentDirectory + "\\Weather\\" + wTrayOptions.CityOption.ToString() + ".xml.new");
                //и делаем его текущим
                fileInfoNew.MoveTo(System.Environment.CurrentDirectory + "\\Weather\\" + wTrayOptions.CityOption.ToString() + ".xml");
            }
            else
            {
                //перо для отрисовки
                Pen myPen = new Pen(Color.FromArgb(140, 140, 140));

                //создаём рисовалку по иконке с картинкой
                this.picGraphics = Graphics.FromImage(this.picBitmap);
                //рисуем квадратик, ограничивающий иконку
                this.picGraphics.DrawRectangle(myPen, 0, 0, 15, 15);
                //размер текста
                Size textSize = TextRenderer.MeasureText("?", this.textFont);
                //отрисовка текста
                TextRenderer.DrawText(this.picGraphics, "?", this.textFont, new Point((16 - textSize.Width) / 2 + 1, 0), Color.Black);

                //создаём рисовалку по иконке с текстом
                this.txtGraphics = Graphics.FromImage(this.txtBitmap);
                //рисуем квадартик, ограничивающий иконку
                this.txtGraphics.DrawRectangle(myPen, 0, 0, 15, 15);
                //размер текста
                textSize = TextRenderer.MeasureText("t°", this.textFont);
                //отрисовка текста
                TextRenderer.DrawText(this.txtGraphics, "t°", this.textFont, new Point((16 - textSize.Width) / 2 + 3, 0), Color.Black);

                //отрисовываем иконку в трее
                this.picIcon.Icon = Icon.FromHandle(this.picBitmap.GetHicon());
                //отрисовываем иконку в трее
                this.txtIcon.Icon = Icon.FromHandle(this.txtBitmap.GetHicon());

                this.picIcon.BalloonTipTitle = "Ошибка";
                this.picIcon.BalloonTipText = "Соединение с сервером не может быть установлено.\nДостоверность прогноза не гарантирована.";
                this.picIcon.ShowBalloonTip(10000);
            }

            //файл загружен, нужно из него всё теперь прочитать
            this.wTrayWeather.ReadFromFile(System.Environment.CurrentDirectory + "\\Weather\\" + wTrayOptions.CityOption.ToString() + ".xml");

            //запускаем таймер для отрисовки погоды
            this.timerDraw.Interval = 100;
            this.timerDraw.Enabled = true;
           
        }
        
        public FormMain()
        {
            InitializeComponent();

            System.Environment.CurrentDirectory = Application.StartupPath;

            //перо для отрисовки
            Pen myPen = new Pen(Color.FromArgb(140, 140, 140));

            //создаём рисовалку по иконке с картинкой
            this.picGraphics = Graphics.FromImage(this.picBitmap);
            //рисуем квадратик, ограничивающий иконку
            this.picGraphics.DrawRectangle(myPen, 0, 0, 15, 15);
            //размер текста
            Size textSize = TextRenderer.MeasureText("?", this.textFont);
            //отрисовка текста
            TextRenderer.DrawText(this.picGraphics, "?", this.textFont, new Point((16 - textSize.Width) / 2 + 1, 0), Color.Black);

            //создаём рисовалку по иконке с текстом
            this.txtGraphics = Graphics.FromImage(this.txtBitmap);
            //рисуем квадартик, ограничивающий иконку
            this.txtGraphics.DrawRectangle(myPen, 0, 0, 15, 15);
            //размер текста
            textSize = TextRenderer.MeasureText("t°", this.textFont);
            //отрисовка текста
            TextRenderer.DrawText(this.txtGraphics, "t°", this.textFont, new Point((16 - textSize.Width) / 2 + 3, 0), Color.Black);

            //отрисовываем иконку в трее
            this.picIcon.Icon = Icon.FromHandle(this.picBitmap.GetHicon());
            //отрисовываем иконку в трее
            this.txtIcon.Icon = Icon.FromHandle(this.txtBitmap.GetHicon());             

            //загружаем настройки
            WTrayOptions.LoadFromFile(ref this.wTrayOptions);

            //прописываем их в форму с настройками
            formOptions.comboBoxCities.SelectedIndex = formOptions.iCities.IndexOf(wTrayOptions.CityOption);
            if (wTrayOptions.ShowOption == 1) formOptions.radioButton1.Checked = true;
            if (wTrayOptions.ShowOption == 2) formOptions.radioButton2.Checked = true;
            if (wTrayOptions.ShowOption == 3) formOptions.radioButton3.Checked = true;
            if (wTrayOptions.UpdateOption == 4) formOptions.radioButton4.Checked = true;
            if (wTrayOptions.UpdateOption == 5) formOptions.radioButton5.Checked = true;
            if (wTrayOptions.UpdateOption == 6) formOptions.radioButton6.Checked = true;
            if (wTrayOptions.UpdateOption == 7) formOptions.radioButton7.Checked = true;
            formOptions.checkBoxProxy.Checked = wTrayOptions.UseProxy;
            formOptions.textBoxProxy.Text = wTrayOptions.Proxy;

            try
            {
                if (wTrayOptions.UseProxy == true)
                    this.webClient.Proxy = new WebProxy(wTrayOptions.Proxy);
                else
                    this.webClient.Proxy = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Настройки прокси ошибочны!\n"+e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //и применяем их
            if (wTrayOptions.ShowOption == 1)
            {
                txtIcon.Visible = true;
                picIcon.Visible = false;
            }
            if (wTrayOptions.ShowOption == 2)
            {
                txtIcon.Visible = false;
                picIcon.Visible = true;
            }
            if (wTrayOptions.ShowOption == 3)
            {
                txtIcon.Visible = true;
                picIcon.Visible = true;
            }
            if (wTrayOptions.UpdateOption == 4)
            {
                this.timerDownload.Interval = 3 * 60 * 60 * 1000;
                this.timerDownload.Enabled = true;
            }
            if (wTrayOptions.UpdateOption == 5)
            {
                this.timerDownload.Interval = 6 * 60 * 60 * 1000;
                this.timerDownload.Enabled = true;
            }
            if (wTrayOptions.UpdateOption == 6)
            {
                this.timerDownload.Interval = 12 * 60 * 60 * 1000;
                this.timerDownload.Enabled = true;
            }
            if (wTrayOptions.UpdateOption == 7)
            {
                this.timerDownload.Enabled = false;
            }

            //вешаем обработчик на завершение загрузки
            this.webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.OnDownloadFileCompleted);
            //запускаем асинхронную загрузку дабы не забивать основной поток
            this.webClient.DownloadFileAsync(new Uri("http://informer.gismeteo.ru/xml/" + wTrayOptions.CityOption.ToString() + ".xml"), System.Environment.CurrentDirectory + "\\Weather\\" + wTrayOptions.CityOption.ToString() + ".xml.new");
        }

        private void настройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //показать окно с настройками
            formOptions.ShowDialog();

            //если поменялся город, или прокси
            if ((wTrayOptions.CityOption != formOptions.iCities[formOptions.comboBoxCities.SelectedIndex])
                ||
                (wTrayOptions.UseProxy != formOptions.checkBoxProxy.Checked)
                ||
                (wTrayOptions.Proxy != formOptions.textBoxProxy.Text && formOptions.checkBoxProxy.Checked == true))
            {
                wTrayOptions.CityOption = formOptions.iCities[formOptions.comboBoxCities.SelectedIndex];

                //запускаем асинхронную загрузку дабы не забивать основной поток
                this.webClient.DownloadFileAsync(new Uri("http://informer.gismeteo.ru/xml/" + wTrayOptions.CityOption.ToString() + ".xml"), System.Environment.CurrentDirectory + "\\Weather\\" + wTrayOptions.CityOption.ToString() + ".xml.new");
            }

            //настройки могли поменяться, поэтому переприсваиваем их
            if (formOptions.radioButton1.Checked == true) wTrayOptions.ShowOption = 1;
            if (formOptions.radioButton2.Checked == true) wTrayOptions.ShowOption = 2;
            if (formOptions.radioButton3.Checked == true) wTrayOptions.ShowOption = 3;
            if (formOptions.radioButton4.Checked == true) wTrayOptions.UpdateOption = 4;
            if (formOptions.radioButton5.Checked == true) wTrayOptions.UpdateOption = 5;
            if (formOptions.radioButton6.Checked == true) wTrayOptions.UpdateOption = 6;
            if (formOptions.radioButton7.Checked == true) wTrayOptions.UpdateOption = 7;
            wTrayOptions.UseProxy = formOptions.checkBoxProxy.Checked;
            wTrayOptions.Proxy = formOptions.textBoxProxy.Text;

            try
            {
                if (wTrayOptions.UseProxy == true)
                    this.webClient.Proxy = new WebProxy(wTrayOptions.Proxy);
                else
                    this.webClient.Proxy = null;
            }
            catch (Exception er)
            {
                MessageBox.Show("Настройки прокси ошибочны!\n" + er.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //и применяем их
            if (wTrayOptions.ShowOption == 1)
            {
                txtIcon.Visible = true;
                picIcon.Visible = false;
            }
            if (wTrayOptions.ShowOption == 2)
            {
                txtIcon.Visible = false;
                picIcon.Visible = true;
            }
            if (wTrayOptions.ShowOption == 3)
            {
                txtIcon.Visible = true;
                picIcon.Visible = true;
            }
            if (wTrayOptions.UpdateOption == 4)
            {
                this.timerDownload.Interval = 3 * 60 * 60 * 1000;
                this.timerDownload.Enabled = true;
            }
            if (wTrayOptions.UpdateOption == 5)
            {
                this.timerDownload.Interval = 6 * 60 * 60 * 1000;
                this.timerDownload.Enabled = true;
            }
            if (wTrayOptions.UpdateOption == 6)
            {
                this.timerDownload.Interval = 12 * 60 * 60 * 1000;
                this.timerDownload.Enabled = true;
            }
            if (wTrayOptions.UpdateOption == 7)
            {
                this.timerDownload.Enabled = false;
            }

            //и сохраняем
            WTrayOptions.SaveToFile(this.wTrayOptions);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //закрыть приложение
            this.Close();
        }

        private void timerDraw_Tick(object sender, EventArgs e)
        {
            //таймер для отрисовки каждый час
            this.timerDraw.Interval = 3600000;

            formOptions.Text = "WTray - " + wTrayWeather.CityName;

            picIcon.BalloonTipTitle = wTrayWeather.CityName;
            txtIcon.BalloonTipTitle = wTrayWeather.CityName;

            int i = wTrayWeather.getI();

            txtIcon.Text = wTrayWeather.getTempShort(i);
            picIcon.Text = wTrayWeather.getCloudShort(i);

            txtIcon.BalloonTipText = wTrayWeather.getW(i) + "\r\n\r\n" + wTrayWeather.getW(i + 1);
            picIcon.BalloonTipText = txtIcon.BalloonTipText;

            if (i != -1)
            {
                Bitmap tmpBitmap = new Bitmap(System.Environment.CurrentDirectory + "\\Img\\cloudiness" + wTrayWeather.W[i].cloudiness.ToString() + ".png");                
                if (wTrayWeather.W[i].precipitation == 8) tmpBitmap = new Bitmap(System.Environment.CurrentDirectory + "\\Img\\storm.png");

                this.picGraphics.Clear(Color.Transparent);
                if (wTrayWeather.W[i].rpower == 0) this.picGraphics.DrawImageUnscaled(tmpBitmap, 0, 0);

                if (wTrayWeather.W[i].rpower == 0 && (wTrayWeather.W[i].precipitation == 4 || wTrayWeather.W[i].precipitation == 5))
                {
                    tmpBitmap = new Bitmap(System.Environment.CurrentDirectory + "\\Img\\lrain.png");
                    this.picGraphics.DrawImageUnscaled(tmpBitmap, 0, 0);
                }

                if (wTrayWeather.W[i].rpower == 1 && (wTrayWeather.W[i].precipitation == 4 || wTrayWeather.W[i].precipitation == 5))
                {
                    tmpBitmap = new Bitmap(System.Environment.CurrentDirectory + "\\Img\\rain.png");
                    this.picGraphics.DrawImageUnscaled(tmpBitmap, 0, 0);
                }

                if (wTrayWeather.W[i].precipitation == 6 || wTrayWeather.W[i].precipitation == 7)
                {
                    tmpBitmap = new Bitmap(System.Environment.CurrentDirectory + "\\Img\\snow.png");
                    this.picGraphics.DrawImageUnscaled(tmpBitmap, 0, 0);
                }

                this.picIcon.Icon = Icon.FromHandle(this.picBitmap.GetHicon());

                //=============================================================

                int t = (wTrayWeather.W[i].temperature_min + wTrayWeather.W[i].temperature_max) / 2;
                string s;
                Color c;
                if (t > 0)
                {
                    s = "+" + t.ToString();
                    c = Color.Red;
                }
                else
                {
                    s = t.ToString();
                    c = Color.Blue;
                }

                Size textSize = TextRenderer.MeasureText(s, this.textFont);
                this.txtGraphics.Clear(Color.Transparent);
                TextRenderer.DrawText(this.txtGraphics, s, this.textFont, new Point((16 - textSize.Width) / 2 + 1, 0), c);
                this.txtIcon.Icon = Icon.FromHandle(this.txtBitmap.GetHicon());
            }
        }

        private void timerDownload_Tick(object sender, EventArgs e)
        {
            //запускаем асинхронную загрузку дабы не забивать основной поток
            this.webClient.DownloadFileAsync(new Uri("http://informer.gismeteo.ru/xml/" + wTrayOptions.CityOption.ToString() + ".xml"), System.Environment.CurrentDirectory + "\\Weather\\" + wTrayOptions.CityOption.ToString() + ".xml.new");
        }

        private void picIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.tipShown == false) this.picIcon.ShowBalloonTip(10000);
        }

        private void txtIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.tipShown == false) this.txtIcon.ShowBalloonTip(10000);
        }

        private void picIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            this.tipShown = false;
        }

        private void txtIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            this.tipShown = false;
        }

        private void picIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            this.tipShown = false;
        }

        private void txtIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            this.tipShown = false;
        }

        private void picIcon_BalloonTipShown(object sender, EventArgs e)
        {
            this.tipShown = true;
        }

        private void txtIcon_BalloonTipShown(object sender, EventArgs e)
        {
            this.tipShown = true;
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            Hide();
        }

        private void скопироватьВБуферОбменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtIcon.BalloonTipText);
        }

        private void прописатьсяВАвтозагрузкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ExePath = System.Windows.Forms.Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {

                reg.SetValue("WTray", ExePath);
                reg.Close();
            }
            catch
            {
                MessageBox.Show("Не могу прописаться в реестр!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void убратьИзАвтозагрузкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string ExePath = System.Windows.Forms.Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                reg.DeleteValue("WTray");
                reg.Close();
            }
            catch
            {
                MessageBox.Show("Не могу убрать запись из реестра!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void обновитьПрогнозToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //запускаем асинхронную загрузку дабы не забивать основной поток
            this.webClient.DownloadFileAsync(new Uri("http://informer.gismeteo.ru/xml/" + wTrayOptions.CityOption.ToString() + ".xml"), System.Environment.CurrentDirectory + "\\Weather\\" + wTrayOptions.CityOption.ToString() + ".xml.new");
        }
        
      
       
    }
}

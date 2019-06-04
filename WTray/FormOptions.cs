using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;            //для файлового ввода-вывода

namespace WTray
{
    public partial class FormOptions : Form
    {
        
        
        //список номеров городов
        public List<int> iCities = new List<int>();

        public FormOptions()
        {
            InitializeComponent();

            System.Environment.CurrentDirectory = Application.StartupPath;

            string fileName = System.Environment.CurrentDirectory + "\\Data\\WTrayCities.txt";  //имя файла с городами
            StreamReader streamReader = new StreamReader(fileName);              //поток streamReader для чтения файла со списком городов

            string nextLine;                                                                    //очередная строка из потока
            while ((nextLine = streamReader.ReadLine()) != null)                                //пока есть строки во входном потоке
            {
                string [] split = nextLine.Split(new char[] {';'});                             //пилим строку
                iCities.Add(Convert.ToInt32(split[0]));                                         //номер города в список
                comboBoxCities.Items.Add(split[1] + ", " + split[3]);                           //название города в другой список
            }
            streamReader.Close();                                                               //закрываем поток
        }
    }
}

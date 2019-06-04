using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;     //для хранения настроек
using System.IO;                    //для ввода-вывода

namespace WTray
{
   
    //класс для хранения настроек
    public class WTrayOptions
    {
        //какие иконки отображать в трее
        public int ShowOption;

        //как часто обновлять погоду
        public int UpdateOption;

        //город для которого делается погода
        public int CityOption;

        //нужно ли использовать прокси
        public bool UseProxy;

        //сам прокси
        public string Proxy;

        //загружает настройки из файла
        public static void LoadFromFile(ref WTrayOptions wTrayOptions)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(WTrayOptions));                     //экземпляр xmlSer класса XmlSerializer
            string fileName = System.Environment.CurrentDirectory + "\\Data\\WTrayOptions.xml"; //имя файла с настройками
            FileStream fileStream = new FileStream(fileName, FileMode.Open);                    //поток fileStream для чтения XML-файла
            wTrayOptions = (WTrayOptions)xmlSer.Deserialize(fileStream);                        //десериализация
            fileStream.Close();                                                                 //закрываем поток
        }

        //выгружает настройки в файл
        public static void SaveToFile(WTrayOptions wTrayOptions)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(WTrayOptions));                     //экземпляр xmlSer класса XmlSerializer
            string fileName = System.Environment.CurrentDirectory + "\\Data\\WtrayOptions.xml"; //имя файла с настройками
            FileStream fileStream = new FileStream(fileName, FileMode.Create);                  //поток fileStream для создания XML-файла
            xmlSer.Serialize(fileStream, wTrayOptions);                                         //сериализация
            fileStream.Close();                                                                 //закрываем поток
        }
    }
}

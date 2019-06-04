using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Web;

namespace WTray
{
     
    //структура для хранения одной записи о погоде
    public class WStruct
    {
        public string year;                             //год
        public string month;                            //месяц
        public string day;                              //день
        public string hour;                             //час

        public int cloudiness;                          //облачность
        public int precipitation;                       //осадки
        public int rpower;                              //сила осадков
        public int spower;                              //сила грозы

        public int pressure_max;                        //максимальное давление
        public int pressure_min;                        //минимальное давление

        public int temperature_max;                     //максимальная температура
        public int temperature_min;                     //минимальная температура

        public int wind_max;                            //максимальный ветер
        public int wind_min;                            //минимальный ветер
        public int wind_direction;                      //направление ветра

        public int relwet_max;                          //максимальная влажность
        public int relwet_min;                          //минимальная влажность

        public int heat_max;                            //максимальная "комфорт" температура
        public int heat_min;                            //минимальная "комфорт" температура
    }

    //класс для полной информации о погоде
    class WTrayWeather
    {
        //система обозначений
        private static string[] cloudiness = { "ясно", "малооблачно", "облачно", "пасмурно" };
        private static string[] precipitation = { "", "", "", "", "дождь", "ливень", "снег", "снег", "гроза", "", "без осадков" };
        private static string[] rpower = { "возможна ", "" };
        private static string[] spower = { "возможен ", "" };
        private static string[] wind_direction = { "северный", "северо-восточный", "восточный", "юго-восточный", "южный", "юго-западный", "западный", "северо-западный" };

        //массив в котором будет лежать вся информация о погоде
        public WStruct[] W = new WStruct[4];
        public string CityName;

        //получить индекс текущей записи о погоде
        public int getI()
        {
            string cur_date = DateTime.Now.ToString("yyyyMMddHH");
            int rec = 0;
            foreach (WStruct x in this.W)
            {
                if (x == null) continue;
                if (cur_date.CompareTo(x.year + x.month + x.day + x.hour) <= 0)
                {
                    return rec;
                }
                rec++;
            }
            return -1;
        }

        //получить погоду полностью
        public string getW(int i)
        {
            if (i == -1) return "...";

            if (i > 3) return "...";

            if (this.W[i] == null) return "...";

            string s = this.W[i].hour + ":00" + " " + this.W[i].day + "." + this.W[i].month;

            s += "\r\n";

            s += cloudiness[this.W[i].cloudiness] + ", ";
            if (this.W[i].precipitation == 4 || this.W[i].precipitation == 5 || this.W[i].precipitation == 6 || this.W[i].precipitation == 7)
                s += spower[this.W[i].spower];
            if (this.W[i].precipitation == 8)
                s += rpower[this.W[i].rpower];

            s += precipitation[this.W[i].precipitation];

            s += ", ";

            s += "ветер " + wind_direction[this.W[i].wind_direction] + " " + this.W[i].wind_min.ToString() + "-" + this.W[i].wind_max.ToString() + " м/с";

            s += "\r\n";

            if (this.W[i].temperature_min > 0) s += "+";
            s += this.W[i].temperature_min.ToString();
            s += "°";
            s += " ";
            if (this.W[i].temperature_max > 0) s += "+";
            s += this.W[i].temperature_max.ToString();
            s += "°";

            s += "   ";

            s += this.W[i].pressure_min.ToString() + "-" + this.W[i].pressure_max.ToString() + "мм";

            s += "   ";

            s += this.W[i].relwet_min.ToString() + "-" + this.W[i].relwet_max.ToString() + "%";

            return s;
        }

        //получить температуру коротко
        public string getTempShort(int i)
        {
            if (i == -1) return "...";

            string s = this.W[i].hour + ":00" + " " + this.W[i].day + "." + this.W[i].month;

            s += "\n";

            if (this.W[i].temperature_min > 0) s += "+";
            s += this.W[i].temperature_min.ToString();
            s += "°";
            s += " ";
            if (this.W[i].temperature_max > 0) s += "+";
            s += this.W[i].temperature_max.ToString();
            s += "°";

            s += "\n";

            s += this.W[i].pressure_min.ToString() + "-" + this.W[i].pressure_max.ToString() + "мм";

            s += "\n";

            s += this.W[i].relwet_min.ToString() + "-" + this.W[i].relwet_max.ToString() + "%";

            if (s.Length>63) return s.Substring(0, 63);
            return s;
        }

        //получить облачность коротко
        public string getCloudShort(int i)
        {
            if (i == -1) return "...";

            string s = this.W[i].hour + ":00" + " " + this.W[i].day + "." + this.W[i].month;

            s += "\n";

            s += cloudiness[this.W[i].cloudiness] + ", ";
            if (this.W[i].precipitation == 4 || this.W[i].precipitation == 5 || this.W[i].precipitation == 6 || this.W[i].precipitation == 7)
                s += spower[this.W[i].spower];
            if (this.W[i].precipitation == 8)
                s += rpower[this.W[i].rpower];

            s += precipitation[this.W[i].precipitation];

            s += "\n";

            s += "ветер " + wind_direction[this.W[i].wind_direction] + " " + this.W[i].wind_min.ToString() + "-" + this.W[i].wind_max.ToString() + " м/с";

            if (s.Length > 63) return s.Substring(0, 63);
            return s;
        }
        
        //метод для чтения полной информации о погоде из файла
        public void ReadFromFile(string fileName)
        {
            //начинаем работать с текущим файлом
            FileInfo fileInfo = new FileInfo(fileName);
            //если он существует
            if (fileInfo.Exists == true)
            {
                //то из него можно вытянуть погоду
                using (XmlReader reader = XmlReader.Create(fileName))
                {
                    reader.Read();
                    reader.ReadToFollowing("MMWEATHER");
                    reader.ReadToFollowing("REPORT");
                    reader.ReadToFollowing("TOWN");

                    CityName = HttpUtility.UrlDecode(reader.GetAttribute("sname"), Encoding.GetEncoding(1251));

                    for (int i = 0; i < this.W.Length; i++)
                    {
                        reader.ReadToFollowing("FORECAST");
                        this.W[i] = new WStruct();
                        this.W[i].year = reader.GetAttribute("year");
                        this.W[i].month = reader.GetAttribute("month");
                        this.W[i].day = reader.GetAttribute("day");
                        this.W[i].hour = reader.GetAttribute("hour");

                        reader.ReadToFollowing("PHENOMENA");
                        this.W[i].cloudiness = Convert.ToInt32(reader.GetAttribute("cloudiness"));
                        this.W[i].precipitation = Convert.ToInt32(reader.GetAttribute("precipitation"));
                        this.W[i].rpower = Convert.ToInt32(reader.GetAttribute("rpower"));
                        this.W[i].spower = Convert.ToInt32(reader.GetAttribute("spower"));

                        reader.ReadToFollowing("PRESSURE");
                        this.W[i].pressure_max = Convert.ToInt32(reader.GetAttribute("max"));
                        this.W[i].pressure_min = Convert.ToInt32(reader.GetAttribute("min"));

                        reader.ReadToFollowing("TEMPERATURE");
                        this.W[i].temperature_max = Convert.ToInt32(reader.GetAttribute("max"));
                        this.W[i].temperature_min = Convert.ToInt32(reader.GetAttribute("min"));

                        reader.ReadToFollowing("WIND");
                        this.W[i].wind_max = Convert.ToInt32(reader.GetAttribute("max"));
                        this.W[i].wind_min = Convert.ToInt32(reader.GetAttribute("min"));
                        this.W[i].wind_direction = Convert.ToInt32(reader.GetAttribute("direction"));

                        reader.ReadToFollowing("RELWET");
                        this.W[i].relwet_max = Convert.ToInt32(reader.GetAttribute("max"));
                        this.W[i].relwet_min = Convert.ToInt32(reader.GetAttribute("min"));

                        reader.ReadToFollowing("HEAT");
                        this.W[i].heat_max = Convert.ToInt32(reader.GetAttribute("max"));
                        this.W[i].heat_min = Convert.ToInt32(reader.GetAttribute("min"));
                    }
                }
            }
        }
    }
}

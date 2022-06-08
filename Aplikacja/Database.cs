using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Aplikacja
{
    public class HistoryRecord
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CityName { get; set; }
        public DateTime? Date { get; set; }
        public string Quality { get; set; }

        public override string ToString()
        {
            return $"[{Date}] {CityName} - {Quality}";
        }
    }

    internal class Database
    {
        static string dbName = "AirQualityHistory.db";
        static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string dbPath = System.IO.Path.Combine(filePath, dbName);
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Aplikacja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddCity("Kraków", 401);
            AddCity("Wrocław", 129);
            AddCity("Warszawa", 552);
            LoadDatabase();
        }

        private void UpdateSelection()
        {
            Selection.Items.Clear();
            foreach (var city in SelectionOptions)
            {
                Selection.Items.Add(city.Key);
            }
            if (Selection.Items.Count > 0) Selection.SelectedIndex = 0;
        }

        private CurrentMeasurment? currentMeasurment = null;
        private Dictionary<string, int> SelectionOptions = new Dictionary<string, int>();
        private Dictionary<int, int> SavedMeasurmentsList = new Dictionary<int, int>();
        private bool downloadSuccess = false;

        private void LoadDatabase()
        {
            using (SQLiteConnection conn = new SQLiteConnection(Database.dbPath))
            {
                conn.CreateTable<HistoryRecord>();
                conn.Close();
            }
            ReadDatabase();
        }

        private void ReadDatabase()
        {
            SavedMeasurments.Items.Clear();
            SavedMeasurmentsList.Clear();
            using (SQLiteConnection conn = new SQLiteConnection(Database.dbPath))
            {
                var list = conn.Table<HistoryRecord>().ToList().OrderBy(c => c.Id).ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    SavedMeasurments.Items.Add(list[i].ToString());
                    SavedMeasurmentsList.Add(i, list[i].Id);
                }
                conn.Close();
            }
        }

        private class CurrentMeasurment
        {
            public string cityName { get; set; }
            public Measurment measurment { get; set; }

            public CurrentMeasurment(string cityName, Measurment measurment)
            {
                this.cityName = cityName;
                this.measurment = measurment;
            }
        }

        private void AddCity(string cityName, int stationId)
        {
            SelectionOptions.Add(cityName, stationId);
            UpdateSelection();
        }

        private Measurment GetMeasurmentFromApi(int stationId)
        {
            WebClient client = new WebClient();
            try
            {
                Measurment result = JsonSerializer.Deserialize<Measurment>(client.DownloadString($"https://api.gios.gov.pl/pjp-api/rest/aqindex/getIndex/{stationId}"));
                StatusAPI.Text = "Nawiązano połączenie.";
                downloadSuccess = true;
                return result;
            } catch (WebException)
            {
            }
            downloadSuccess = false;
            StatusAPI.Text = "Brak połączenia.";

            return null;
        }

        private void DownloadData(object sender, RoutedEventArgs e)
        {
            try
            {
                currentMeasurment = new CurrentMeasurment(Selection.Text, GetMeasurmentFromApi(SelectionOptions[Selection.Text]));
                SelectedCity.Text = currentMeasurment.cityName;
                AirQuality.Text = currentMeasurment.measurment.StIndexLevel.IndexLevelName;
            }
            catch (Exception x) {
                AirQuality.Text = $"Brak danych";
            }
        }

        private void SaveMeasurment(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Database.dbPath))
            {
                if (downloadSuccess)
                {
                    if (currentMeasurment != null)
                    {
                        if (!SavedMeasurments.Items.Contains($"[{DateTime.Now}] {currentMeasurment.cityName} = {currentMeasurment.measurment.StIndexLevel.IndexLevelName}"))
                        {
                            conn.Insert(new HistoryRecord() { CityName = currentMeasurment.cityName, Date = DateTime.Now, Quality = currentMeasurment.measurment.StIndexLevel.IndexLevelName });
                        }
                    }
                }
                conn.Close();
            }
            ReadDatabase();
        }

        private void DeleteMeasurment(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(Database.dbPath))
            {
                if (SavedMeasurments.SelectedIndex != -1)
                {
                    SavedMeasurmentsList.TryGetValue(SavedMeasurments.SelectedIndex, out int value);
                    conn.Delete<HistoryRecord>(value);
                    SavedMeasurmentsList.Remove(SavedMeasurments.SelectedIndex);
                }
                conn.Close();
            }
            ReadDatabase();
        }
    }
}

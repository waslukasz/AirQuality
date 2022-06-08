using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Aplikacja
{
    public class No2IndexLevel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("indexLevelName")]
        public string IndexLevelName { get; set; }
    }

    public class O3IndexLevel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("indexLevelName")]
        public string IndexLevelName { get; set; }
    }

    public class Pm10IndexLevel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("indexLevelName")]
        public string IndexLevelName { get; set; }
    }

    public class Measurment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("stCalcDate")]
        public string StCalcDate { get; set; }

        [JsonPropertyName("stIndexLevel")]
        public StIndexLevel StIndexLevel { get; set; }

        [JsonPropertyName("stSourceDataDate")]
        public string StSourceDataDate { get; set; }

        [JsonPropertyName("so2CalcDate")]
        public string So2CalcDate { get; set; }

        [JsonPropertyName("so2IndexLevel")]
        public So2IndexLevel So2IndexLevel { get; set; }

        [JsonPropertyName("so2SourceDataDate")]
        public string So2SourceDataDate { get; set; }

        [JsonPropertyName("no2CalcDate")]
        public string No2CalcDate { get; set; }

        [JsonPropertyName("no2IndexLevel")]
        public No2IndexLevel No2IndexLevel { get; set; }

        [JsonPropertyName("no2SourceDataDate")]
        public string No2SourceDataDate { get; set; }

        [JsonPropertyName("pm10CalcDate")]
        public string Pm10CalcDate { get; set; }

        [JsonPropertyName("pm10IndexLevel")]
        public Pm10IndexLevel Pm10IndexLevel { get; set; }

        [JsonPropertyName("pm10SourceDataDate")]
        public string Pm10SourceDataDate { get; set; }

        [JsonPropertyName("pm25CalcDate")]
        public string Pm25CalcDate { get; set; }

        [JsonPropertyName("pm25IndexLevel")]
        public object Pm25IndexLevel { get; set; }

        [JsonPropertyName("pm25SourceDataDate")]
        public object Pm25SourceDataDate { get; set; }

        [JsonPropertyName("o3CalcDate")]
        public string O3CalcDate { get; set; }

        [JsonPropertyName("o3IndexLevel")]
        public O3IndexLevel O3IndexLevel { get; set; }

        [JsonPropertyName("o3SourceDataDate")]
        public string O3SourceDataDate { get; set; }

        [JsonPropertyName("stIndexStatus")]
        public bool StIndexStatus { get; set; }

        [JsonPropertyName("stIndexCrParam")]
        public string StIndexCrParam { get; set; }
    }

    public class So2IndexLevel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("indexLevelName")]
        public string IndexLevelName { get; set; }
    }

    public class StIndexLevel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("indexLevelName")]
        public string IndexLevelName { get; set; }
    }


}

using System;
using System.Collections.Generic;

[Serializable]
public class BitcoinInfoDic
{
    public TimeUpdate time { get; set; }
    public string chartName { get; set; }
    public Dictionary<string, Currency> bpi { get; set; }

    [Serializable]
    public class Currency
    {
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
        public double rate_float { get; set; }
    }

    [Serializable]
    public class TimeUpdate
    {
        public string updated { get; set; }
        public DateTime updatedISO { get; set; }
        public string updateduk { get; set; }
    }
}


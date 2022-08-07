using System;

[Serializable]
public class BitcoinInfo
{
    public TimeUpdate time;
    public string chartName;
    public Bpi bpi;
}

[Serializable]
public class Bpi
{
    public USD USD;
    public GBP GBP;
    public EUR EUR;
}

[Serializable]
public class EUR
{
    public string code;
    public string symbol;
    public string rate;
    public string description;
    public double rate_float;
}

[Serializable]
public class GBP
{
    public string code;
    public string symbol;
    public string rate;
    public string description;
    public double rate_float;
}

[Serializable]
public class TimeUpdate
{
    public string updated;
    public DateTime updatedISO;
    public string updateduk;
}

[Serializable]
public class USD
{
    public string code;
    public string symbol;
    public string rate;
    public string description;
    public double rate_float;
}
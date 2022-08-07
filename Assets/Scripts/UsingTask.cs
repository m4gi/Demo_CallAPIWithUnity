using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UsingTask : MonoBehaviour
{
    private const float API_CHECK_MAXTIME = 1f;
    public Text timeText;
    public Text usdText;
    public Text eurText;
    public Text gbpText;
    private float apiCheckCountdown = API_CHECK_MAXTIME;

    void Start()
    {
        CheckBitcoinStatus();
    }
    void Update()
    {
        timeText.text = Time.time.ToString();
        apiCheckCountdown -= Time.deltaTime;
        if (apiCheckCountdown <= 0)
        {
            CheckBitcoinStatus();
            apiCheckCountdown = API_CHECK_MAXTIME;
        }
    }
    public async void CheckBitcoinStatus()
    {
        BitcoinInfoDic bitcoinInfo = (await GetBitcoinInfoDic());
        Debug.Log(bitcoinInfo);
        usdText.text = bitcoinInfo.bpi["USD"].rate;
        gbpText.text = bitcoinInfo.bpi["GBP"].rate;
        eurText.text = bitcoinInfo.bpi["EUR"].rate;
    }
    private async Task<BitcoinInfoDic> GetBitcoinInfoDic()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.coindesk.com/v1/bpi/currentprice.json"));
        HttpWebResponse response = (HttpWebResponse)(await request.GetResponseAsync());
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        BitcoinInfoDic info = JsonConvert.DeserializeObject<BitcoinInfoDic>(jsonResponse);
        Debug.Log(jsonResponse);
        return info;
    }
}

using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class BasicAPI : MonoBehaviour
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
    public void CheckBitcoinStatus()
    {
        BitcoinInfo bitcoinInfo = GetBitCoinInfo();
        Debug.Log(bitcoinInfo);
        usdText.text = bitcoinInfo.bpi.USD.rate;
        gbpText.text = bitcoinInfo.bpi.GBP.rate;
        eurText.text = bitcoinInfo.bpi.EUR.rate;
    }
    private BitcoinInfo GetBitCoinInfo()
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(string.Format("https://api.coindesk.com/v1/bpi/currentprice.json"));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        BitcoinInfo info = JsonUtility.FromJson<BitcoinInfo>(jsonResponse);
        Debug.Log(jsonResponse);
        return info;
    }
}

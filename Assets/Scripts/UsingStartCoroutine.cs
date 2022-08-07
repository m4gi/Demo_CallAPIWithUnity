using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UsingStartCoroutine : MonoBehaviour
{
    private const float API_CHECK_MAXTIME = 1f;
    public Text timeText;
    public Text usdText;
    public Text eurText;
    public Text gbpText;
    private float apiCheckCountdown = API_CHECK_MAXTIME;

    void Start()
    {
        StartCoroutine(GetBitCoinInfo(CheckBitcoinStatus));
    }
    void Update()
    {
        timeText.text = Time.time.ToString();
        apiCheckCountdown -= Time.deltaTime;
        if (apiCheckCountdown <= 0)
        {
            StartCoroutine(GetBitCoinInfo(CheckBitcoinStatus));
            apiCheckCountdown = API_CHECK_MAXTIME;
        }
    }
    void CheckBitcoinStatus(BitcoinInfoDic bitcoinInfo)
    {
        Debug.Log(bitcoinInfo);
        usdText.text = bitcoinInfo.bpi["USD"].rate;
        gbpText.text = bitcoinInfo.bpi["GBP"].rate;
        eurText.text = bitcoinInfo.bpi["EUR"].rate;
    }

    IEnumerator GetBitCoinInfo(Action<BitcoinInfoDic> onSuccess)
    {
        using (UnityWebRequest req = UnityWebRequest.Get(String.Format("https://api.coindesk.com/v1/bpi/currentprice.json")))
        {
            yield return req.SendWebRequest();
            while (!req.isDone)
                yield return null;
            byte[] result = req.downloadHandler.data;
            string bitcoinJSON = System.Text.Encoding.Default.GetString(result);
            BitcoinInfoDic info = JsonConvert.DeserializeObject<BitcoinInfoDic>(bitcoinJSON);
            onSuccess(info);
        }
    }
}

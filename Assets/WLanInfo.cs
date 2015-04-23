using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class WLanInfo : MonoBehaviour
{
    #region UI Transforms

    public class InfoParameters
    {
        public Button getInfoButton = null;
        public Button stopButton = null;
        public Text ssidText = null;
        public Text rssiText = null;
        public Text bssidText = null;
        public Text powerText = null;
        public Text rateText = null;
        public Text proxyText = null;
    }

    protected Transform infoCanvas = null;
    protected InfoParameters infoParams = new InfoParameters();

    protected void InitInfoParams()
    {
        infoCanvas = GameObject.Find("WLanInfoCanvas").transform;
        this.infoParams.getInfoButton = infoCanvas.FindChild("Offset/Bottom/CheckSSIDButton").GetComponent<Button>();
        this.infoParams.stopButton = infoCanvas.FindChild("Offset/Bottom/StopButton").GetComponent<Button>();
        this.infoParams.ssidText = infoCanvas.FindChild("Offset/Top/Texts/SSIDText").GetComponent<Text>();
        this.infoParams.rssiText = infoCanvas.FindChild("Offset/Top/Texts/RSSIText").GetComponent<Text>();
        this.infoParams.bssidText = infoCanvas.FindChild("Offset/Top/Texts/BSSIDText").GetComponent<Text>();
        this.infoParams.rateText = infoCanvas.FindChild("Offset/Top/Texts/RateText").GetComponent<Text>();
        this.infoParams.powerText = infoCanvas.FindChild("Offset/Top/Texts/PowerText").GetComponent<Text>();
        this.infoParams.proxyText = infoCanvas.FindChild("Offset/Top/Texts/ProxyText").GetComponent<Text>();
        this.infoParams.getInfoButton.interactable = true;
        this.infoParams.stopButton.interactable = false;
    }

    #endregion

    #region about Events
    /// <summary>
    /// CheckSSIDボタンが押された時に呼ばれる処理
    /// </summary>
    private void OnClickSsidButton()
    {
        this.infoParams.getInfoButton.interactable = false;
        this.infoParams.stopButton.interactable = true;
        InvokeRepeating("SetWifiInfoToUI", 0f, 1f);
    }

    private void OnClickStopButton()
    {
        this.infoParams.stopButton.interactable = false;
        this.infoParams.getInfoButton.interactable = true;
        CancelInvoke();
    }

    private void SetWifiInfoToUI()
    {
        WifiInfo info = WifiPlugins.GetWifiInfo();
        this.infoParams.ssidText.text = string.Format("SSID:{0}", info.ssid);
        this.infoParams.rssiText.text = string.Format("RSSI:{0}dBm",info.rssi);
        this.infoParams.bssidText.text = string.Format("BSSID:{0}",info.bssid);
        this.infoParams.rateText.text = string.Format("Rate:{0}",info.transmitRate);
        this.infoParams.powerText.text = string.Format("Power:{0}",info.transmitPower);
        this.infoParams.proxyText.text = info.isProxy ? "Proxy:ON":"Proxy:OFF";
    }

    /// <summary>
    /// イベントを登録する
    /// </summary>
    private void RegisterEvents()
    {
        this.infoParams.getInfoButton.onClick.AddListener(OnClickSsidButton);
        this.infoParams.stopButton.onClick.AddListener(OnClickStopButton);
    }

    /// <summary>
    /// 登録されているイベントを全て解除する
    /// </summary>
    private void RemoveAllEvents()
    {
        this.infoParams.getInfoButton.onClick.RemoveAllListeners();
        this.infoParams.stopButton.onClick.RemoveAllListeners();
    }
    #endregion
    private void InitWifiInfo()
    {
        InitInfoParams();
        RegisterEvents();
    }

    void Awake()
    {
        InitWifiInfo();
    }

    void OnDestroy()
    {
        RemoveAllEvents();
    }
}

using System.Runtime.InteropServices;
using System;
public class WifiPlugins 
{
#if UNITY_EDITOR_OSX
	[DllImport("WifiPlugin")]
    private static extern IntPtr GetSsid_();
    [DllImport("WifiPlugin")]
    private static extern IntPtr GetBssi_();
    [DllImport("WifiPlugin")]
    private static extern int GetRssi_();
    [DllImport("WifiPlugin")]
    private static extern int GetTransmitPower_();
    [DllImport("WifiPlugin")]
    private static extern double GetTransmitRate_();
    [DllImport("WifiPlugin")]
    private static extern bool GetIsProxy_();
#endif

#if UNITY_IOS
	[DllImport("__Internal")]
	public static extern IntPtr GetSsid_();
#endif
    public static string GetSsid()
	{
        return Marshal.PtrToStringAuto(WifiPlugins.GetSsid_());
	}

    public static string GetBssid()
    {
        return Marshal.PtrToStringAuto(WifiPlugins.GetBssi_());
    }

    public static int GetRssi()
    {
        return GetRssi_();
    }

    public static int GetTransmitPower()
    {
        return GetTransmitPower_();
    }

    public static double GetTransmitRate()
    {
        return GetTransmitRate_();
    }

    public static bool GetIsProxy()
    {
        return GetIsProxy_();
    }

    public static WifiInfo GetWifiInfo()
    {
        WifiInfo info = new WifiInfo();
        info.ssid = WifiPlugins.GetSsid();
        info.bssid = WifiPlugins.GetBssid();
        info.rssi = WifiPlugins.GetRssi();
        info.transmitPower = WifiPlugins.GetTransmitPower();
        info.transmitRate = WifiPlugins.GetTransmitRate();
        info.isProxy = WifiPlugins.GetIsProxy();

        return info;
    }
}

public struct WifiInfo
{
    public string ssid;
    public string bssid;
    public int rssi;
    public int transmitPower;
    public double transmitRate;
    public bool isProxy;
}
    


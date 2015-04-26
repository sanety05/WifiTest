using System.Runtime.InteropServices;
using System;
using UnityEngine;
public class WifiPlugins 
{
#if UNITY_EDITOR_OSX
	[DllImport("WifiPlugin")]
    private static extern IntPtr GetSsid_();
    [DllImport("WifiPlugin")]
    private static extern IntPtr GetBssi_();
    [DllImport("WifiPlugin")]
    private static extern int GetRssi_();

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
		
	public static WifiInfo GetWifiInfo()
	{
		WifiInfo info = new WifiInfo();
		info.ssid = WifiPlugins.GetSsid();
		info.bssid = WifiPlugins.GetBssid();
		info.rssi = WifiPlugins.GetRssi();

		return info;
	}
#endif

#if UNITY_IOS
	[DllImport("__Internal")]
	public static extern IntPtr GetSsid_();
#endif

#if UNITY_ANDROID && !UNITY_EDITOR

	const string JAVA_CLASS_NAME = "com.example.daiki.uniwifi.UniWifi";

	static AndroidJavaObject mWiFiManager = null;
	static AndroidJavaObject activity = null;

	public static string GetSsid(AndroidJavaObject act)
	{
		return mWiFiManager.CallStatic<string> ("GetSSID_",act);
	}

	public static string GetBssid(AndroidJavaObject act)
	{
		return mWiFiManager.CallStatic<string> ("GetBssid_",act);
	}

	public static int GetRssi(AndroidJavaObject act)
	{
		return mWiFiManager.CallStatic<int> ("GetRssi_",act);
	}
		
	public static WifiInfo GetWifiInfo()
	{
		if(mWiFiManager == null)
		{
			mWiFiManager = new AndroidJavaClass (JAVA_CLASS_NAME);
			using (AndroidJavaClass player = new AndroidJavaClass ("com.unity3d.player.UnityPlayer"))
			{
				activity = player.GetStatic<AndroidJavaObject> ("currentActivity");
			}
		}

		WifiInfo info = new WifiInfo ();

		info.ssid = WifiPlugins.GetSsid (activity);
		info.bssid = WifiPlugins.GetBssid (activity);
		info.rssi = WifiPlugins.GetRssi (activity);
			
		return info;
	}
#endif
}

public struct WifiInfo
{
    public string ssid;
    public string bssid;
    public int rssi;
}
    


//
//  WifiPlugin.m
//  WifiPlugin
//
//  Created by 実方大樹 on 2015/04/23.
//  Copyright (c) 2015年 実方大樹. All rights reserved.
//
#import <Foundation/Foundation.h>
#import <SystemConfiguration/SystemConfiguration.h>
#import <SystemConfiguration/CaptiveNetwork.h>

const char* MakeStringCopy (const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

const char* GetSsid_(){
    NSString* ssid = nil;
    //3G接続の場合はnilが戻されるので、以降のコードで注意する。
    CFArrayRef interfaces = CNCopySupportedInterfaces();
    
    CFDictionaryRef dicRef = CNCopyCurrentNetworkInfo(CFArrayGetValueAtIndex(interfaces, 0));
    
    if (dicRef) {
        ssid = CFDictionaryGetValue(dicRef, kCNNetworkInfoKeySSID);
        // Macアドレスを取得
        //macAddress = CFDictionaryGetValue(dicRef, kCNNetworkInfoKeyBSSID);
        
        NSLog(@"%@", ssid);
    }
    
    return MakeStringCopy([ssid UTF8String]);
}



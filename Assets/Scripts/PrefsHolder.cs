using System;
using UnityEngine;

public static class PrefsHolder
{
    private const string Lang = "Lang";

    public static void SaveLang(String lang)
    {
        PlayerPrefs.SetString(Lang, lang);
    }

    public static String GetLang()
    {
        return PlayerPrefs.GetString(Lang, SystemLanguage.English.ToString());
    }
}
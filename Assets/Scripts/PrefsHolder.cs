using System;
using UnityEngine;

public static class PrefsHolder
{
    private const string Lang = "Lang";

    public static void SaveLang(SystemLanguage lang)
    {
        PlayerPrefs.SetString(Lang, lang.ToString());
    }

    public static SystemLanguage GetLang()
    {
        Enum.TryParse(
            PlayerPrefs.GetString(Lang, Application.systemLanguage.ToString()),
            out SystemLanguage language);
        return language;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LangResolver : MonoBehaviour
{
    private const char Separator = '=';

    private readonly Dictionary<SystemLanguage, LangData> _langData = new Dictionary<SystemLanguage, LangData>();
    private readonly List<SystemLanguage> _supportedLanguages = new List<SystemLanguage>();

    private SystemLanguage _language;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ReadProperties();
    }

    private void ReadProperties()
    {
        foreach (var file in Resources.LoadAll<TextAsset>("LangFiles"))
        {
            Enum.TryParse(file.name, out SystemLanguage language);
            var lang = new Dictionary<string, string>();
            foreach (var line in file.text.Split('\n'))
            {
                var prop = line.Split(Separator);
                lang[prop[0]] = prop[1];
            }

            _langData[language] = new LangData(lang);
            _supportedLanguages.Add(language);
        }

        ResolveLanguage();
    }
    
    private void ResolveLanguage()
    {
        _language = PrefsHolder.GetLang();
        if (!_supportedLanguages.Contains(_language))
        {
            _language = SystemLanguage.English;
        }
    }

    public void ResolveTexts()
    {
        var lang = _langData[_language].Lang;
        foreach (var langText in Resources.FindObjectsOfTypeAll<LangText>())
        {
            langText.ChangeText(lang[langText.Identifier]);
        }
    }

    public void ChangeLanguage()
    {
        var currentLanguageIndex = _supportedLanguages.IndexOf(_language);
        _language = currentLanguageIndex ==
                    _supportedLanguages.Count - 1
            ? _supportedLanguages.First()
            : _supportedLanguages[currentLanguageIndex + 1];

        ResolveTexts();
        PrefsHolder.SaveLang(_language);
    }

    private class LangData
    {
        public readonly Dictionary<string, string> Lang;

        public LangData(Dictionary<string, string> lang)
        {
            Lang = lang;
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResolver : MonoBehaviour
{
    private Image _langButtonImage;
    private LangResolver _langResolver;
    private readonly Dictionary<string, Sprite> _langImages = new Dictionary<string, Sprite>();

    private void Start()
    {
        _langButtonImage = FindObjectOfType<LangButton>().GetComponent<Image>();
        _langResolver = FindObjectOfType<LangResolver>();
        _langResolver.ResolveTexts();

        foreach (var sprite in Resources.LoadAll<Sprite>("LanguageImages"))
        {
            _langImages[sprite.name] = sprite;
        }

        ResolveLangImage();
    }

    public void ChangeLanguage()
    {
        _langResolver.ChangeLanguage();
        ResolveLangImage();
    }

    private void ResolveLangImage()
    {
        _langButtonImage.sprite = _langImages[PrefsHolder.GetLang().ToString()];
    }
}
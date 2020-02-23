using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class LangText : MonoBehaviour
{
    public string Identifier;

    public void ChangeText(string text)
    {
        GetComponent<Text>().text = Regex.Unescape(text);
    }
}
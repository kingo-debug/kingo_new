using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputFieldLimiter : MonoBehaviour
{
    public TMP_InputField inputField;
    public int maxCharacters = 100;
    public string[] badWords = { "fuck", "bitch", "ass", "whore", "hoe", "faggot", "gay", "dick", "pussy", "sex", "boobs", "tities", "tits", "vagina", "penis", "cum", "ass","nigga","nigger","suck" }; // Add your bad words here

    void Start()
    {
        if (inputField == null)
        {
            inputField = GetComponent<TMP_InputField>();
        }

        if (inputField != null)
        {
            inputField.onValueChanged.AddListener(OnInputValueChanged);
        }
        else
        {
            Debug.LogError("No TMP_InputField component found.");
        }
    }

    void OnInputValueChanged(string input)
    {
        if (input.Length > maxCharacters)
        {
            inputField.text = input.Substring(0, maxCharacters);  // size limiter
            inputField.caretPosition = maxCharacters;
        }


        // Censor bad words
        foreach (string badWord in badWords)
        {
            if (input.Contains(badWord))
            {
                string replacement = new string('*', badWord.Length); // Censor  word detection
                input = input.Replace(badWord, replacement);
            }
        }

        // Update the input field text without triggering additional value change events
        inputField.SetTextWithoutNotify(input);
    }
}

using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;

public class NameCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject Page1;
    [SerializeField]
    private GameObject Page2;
    [SerializeField]
    private GameObject NameError;
    [SerializeField]
    private TMP_InputField inputfield;

    // Define a HashSet for banned words (case-insensitive).
    private static readonly HashSet<string> bannedWords = new HashSet<string>()
    {
        "fuck", "fuckk", "ffuck", "sex", "ssex", "sexx",
        "nigger", "niggha", "nigga", "niger", "niggerr",
        "gay", "gayy", "ass", "assss",
        "faggot", "bitch", "bitchh", "bitchhh",
        "motherfucker", "pussy", "boobs"
        // Add any additional words as needed
    };

    public void CheckName()
    {
        // Convert input text to lowercase for case-insensitive comparison.
        string lowerInput = inputfield.text.ToLower().Trim();

        // Check if the name is too short or contains any banned words.
        if (lowerInput.Length < 3 || bannedWords.Any(bannedWord => lowerInput.Contains(bannedWord)))
        {
            NameError.SetActive(true);
        }
        else
        {
            NameError.SetActive(false); // Hide the error if any
            Page1.SetActive(false);
            Page2.SetActive(true);
        }
    }
}

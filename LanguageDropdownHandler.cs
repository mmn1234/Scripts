using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* This script is attached to the dropdown menu on the MainMenu GameObject */

public class LanguageDropdownHandler : MonoBehaviour
{
    // Reference to the TMP_Dropdown component in the scene, which should be assigned in the Inspector.
    public TMP_Dropdown languageDropdown;

    // Static variable to store the currently selected language. Default is set to "Arabic".
    public static string selectedLanguage = "Arabic";

    // Called at the start of the game, before the first frame update.
    void Start()
    {
        // Initialize the selectedLanguage variable based on the dropdown's default value.
        // This ensures that the selectedLanguage is set to the initial option of the dropdown.
        selectedLanguage = languageDropdown.options[languageDropdown.value].text;

        // Add a listener to the dropdown to detect when the value changes.
        // The OnLanguageChanged method will be called whenever the user selects a new option in the dropdown.
        languageDropdown.onValueChanged.AddListener(delegate { OnLanguageChanged(); });
    }

    // Method called when the value of the dropdown changes.
    void OnLanguageChanged()
    {
        // Update the selectedLanguage variable based on the current dropdown value.
        // This ensures that the selectedLanguage always reflects the user's current selection.
        selectedLanguage = languageDropdown.options[languageDropdown.value].text;

        // Log the currently selected language to the console for debugging purposes.
        Debug.Log("Selected Language: " + selectedLanguage);
    }
}
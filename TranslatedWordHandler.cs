using UnityEngine;
using TMPro;
using UnityEditor;

/* This script is attached on every translatable object */

public class TranslatedWordHandler : MonoBehaviour
{
    // Prefab for the English popup. This allows you to assign a blank English popup in the Inspector.
    public GameObject popupPrefab;

    // Prefab for the Arabic popup. This allows you to assign a blank Arabic popup in the Inspector.
    public GameObject arabicPopupPrefab;

    // Reference to the instantiated English popup game object.
    private GameObject englishPopup;

    // Reference to the instantiated translated popup game object.
    private GameObject translatedPopup;

    // Audio source component to play audio clips.
    public AudioSource audioSource;

    // Collider of the object that triggers the translation popup.
    public Collider objectCollider;

    // Audio clip for the Arabic translation.
    public AudioClip arabicAudioClip;

    // Audio clip for the French translation.
    public AudioClip frenchAudioClip;

    // Dictionary containing translations for various words in English, Arabic, and French.
    private static readonly System.Collections.Generic.Dictionary<string, (string Arabic, string French)> TranslationDatabase =
        new System.Collections.Generic.Dictionary<string, (string, string)>
        {
            { "Tree", ("شجرة", "Arbre") },
            { "Rock", ("صخرة", "Roche") },
            { "Tiger", ("نمر", "Tigre") },
            { "Mushroom", ("فطر", "champignon") },
            { "Deer", ("غزال", "biche") },
            { "River", ("نهر", "rivière") },
            { "Grass", ("عشب", "herbe") },
            { "Horse", ("حصان", "jument") },
            { "Wolf", ("ذئب", "louve") },
            { "Soup", ("حساء", "soupe") },
            { "Campfire", ("نار المخيم", "feu de camp") },
            { "Mountain", ("جبل", "montagne") },
            { "Sand", ("رمل", "sable") },
            { "Bridge", ("جسر", "pont") },
            { "Red tent", ("خيمة حمراء", "tente rouge") },
            { "Yellow tent", ("خيمة صفراء", "tente jaune") },
            { "Blue tent", ("خيمة زرقاء", "tente bleue") },
        };

    // The English name of the object being translated.
    public string objectNameEnglish;

    // Called at the start of the game.
    private void Start()
    {
        // If the audio source is not already assigned, add an AudioSource component to the game object.
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // Called when the object is clicked to show the translated word.
    public void ShowTranslatedWord()
    {
        // Check if the object's English name is in the translation database.
        if (!TranslationDatabase.ContainsKey(objectNameEnglish))
        {
            // Log an error if the translation is not found.
            Debug.LogError($"Translation for {objectNameEnglish} not found!");
            return;
        }

        // Get the translations for the object's English name.
        var translations = TranslationDatabase[objectNameEnglish];

        // Determine the translated word based on the selected language.
        string translatedWord = LanguageDropdownHandler.selectedLanguage == "Arabic"
            ? translations.Arabic
            : translations.French;

        // Play the audio clip for the translated word.
        PlayTranslatedWordAudio();

        // Calculate the position above the collider to display the popups.
        Vector3 popupPosition = objectCollider.bounds.center + Vector3.up * 2.5f;

        // Create and display the English popup if it does not already exist.
        if (popupPrefab != null && englishPopup == null)
        {
            englishPopup = Instantiate(popupPrefab, popupPosition, Quaternion.identity);
            TextMeshProUGUI englishText = englishPopup.GetComponentInChildren<TextMeshProUGUI>();
            englishText.text = objectNameEnglish; // Display the English word.
        }

        // Create and display the translated popup if it does not already exist.
        if (translatedPopup == null)
        {
            if (LanguageDropdownHandler.selectedLanguage == "Arabic" && arabicPopupPrefab != null)
            {
                // Instantiate the Arabic popup prefab.
                translatedPopup = Instantiate(arabicPopupPrefab, popupPosition + Vector3.up * 0.42f, Quaternion.identity);
                TextMeshProUGUI arabicText = translatedPopup.GetComponentInChildren<TextMeshProUGUI>();
                arabicText.text = translations.Arabic; // Display the Arabic translation.
            }
            else if (LanguageDropdownHandler.selectedLanguage == "French" && popupPrefab != null)
            {
                // Instantiate the French popup using the same prefab as English.
                translatedPopup = Instantiate(popupPrefab, popupPosition + Vector3.up * 0.42f, Quaternion.identity);
                TextMeshProUGUI frenchText = translatedPopup.GetComponentInChildren<TextMeshProUGUI>();
                frenchText.text = translations.French; // Display the French translation.
            }
        }
    }

    // Hide the pop-up when the user moves away or after a certain time.
    public void HideTranslatedWord()
    {
        // Destroy the English popup if it exists.
        if (englishPopup != null)
        {
            Destroy(englishPopup);
            englishPopup = null;
        }

        // Destroy the translated popup if it exists.
        if (translatedPopup != null)
        {
            Destroy(translatedPopup);
            translatedPopup = null;
        }
    }

    // Play the audio clip for the translated word based on the selected language.
    private void PlayTranslatedWordAudio()
    {
        // Play the Arabic audio clip if Arabic is selected and the clip exists.
        if (LanguageDropdownHandler.selectedLanguage == "Arabic" && arabicAudioClip != null)
        {
            audioSource.PlayOneShot(arabicAudioClip);
        }
        // Play the French audio clip if French is selected and the clip exists.
        else if (LanguageDropdownHandler.selectedLanguage == "French" && frenchAudioClip != null)
        {
            audioSource.PlayOneShot(frenchAudioClip);
        }
        else
        {
            // Log an error if the audio clip for the selected language is not found.
            Debug.LogError("Audio clip not found for the selected language!");
        }
    }
}
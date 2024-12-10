using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

/* This script is used on the translatable GameObjects in the XR Simple Interactable script 
   and more specifically inside the Interaction Manager slot */

public class XRInteractionHandler : MonoBehaviour
{
    // Reference to the TranslatedWordHandler script, which manages the translation popups.
    public TranslatedWordHandler translatedWordHandler;

    // Input action property for the action that triggers the translation popup.
    public InputActionProperty actionProperty;

    // Called at the start of the game, before the first frame update.
    private void Start()
    {
        // If the translatedWordHandler is not assigned in the Inspector, try to find it on the same GameObject.
        if (translatedWordHandler == null)
        {
            translatedWordHandler = GetComponent<TranslatedWordHandler>();
        }
    }

    // Called once per frame. This method is used for continuous updates.
    void Update()
    {
        // Check if the action was pressed this frame.
        if (actionProperty.action.WasPressedThisFrame())
        {
            // Call the ShowTranslatedWord method on the translatedWordHandler to display the translation popup.
            translatedWordHandler.ShowTranslatedWord();
        }

        // Check if the action was released this frame.
        if (actionProperty.action.WasReleasedThisFrame())
        {
            // Call the HideTranslatedWord method on the translatedWordHandler to hide the translation popup.
            translatedWordHandler.HideTranslatedWord();
        }
    }
}
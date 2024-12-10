using UnityEngine;
using UnityEngine.Assertions;

public class CanvasGroupAlphaToggle : MonoBehaviour
{
    // Reference to the CanvasGroup component that will be used to control the alpha (transparency) of the UI elements.
    public CanvasGroup canvasGroup;

    // Speed at which the alpha will be animated when toggling visibility.
    public float animationSpeed;

    // Boolean flag to track whether the UI elements should be visible or not.
    private bool visible;

    // Method to toggle the visibility of the UI elements.
    public void ToggleVisible()
    {
        // Invert the current visibility state.
        visible = !visible;
    }

    // Called at the start of the game, before the first frame update.
    void Start()
    {
        // Ensure that the canvasGroup is not null to avoid runtime errors.
        Assert.IsNotNull(canvasGroup);
    }

    // Called once per frame. This method is used for continuous updates.
    void Update()
    {
        // Use Mathf.Lerp to smoothly animate the alpha value of the CanvasGroup.
        // The target alpha value is 1.0f if visible is true, and 0.0f if visible is false.
        // The animationSpeed multiplied by Time.deltaTime controls the speed of the animation.
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, visible ? 1.0f : 0.0f, animationSpeed * Time.deltaTime);
    }
}
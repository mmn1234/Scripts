using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/* This script is attached to the main XR Origin parent to allow continuous movement using the left stick */

public class VRPointer : MonoBehaviour
{
    // The origin of the pointer, typically the controller or the hand in a VR setup.
    public Transform pointerOrigin;

    // Optional LineRenderer component to visually represent the pointer.
    public LineRenderer pointerLine;

    // Maximum length of the pointer.
    public float pointerLength = 5.0f;

    // Called once per frame to update the pointer's position and handle interactions.
    private void Update()
    {
        // Create a ray starting from the pointer origin and extending in the forward direction.
        Ray pointerRay = new Ray(pointerOrigin.position, pointerOrigin.forward);

        // Cast a ray to check for collisions within the specified length.
        if (Physics.Raycast(pointerRay, out RaycastHit hit, pointerLength))
        {
            // Check if the hit object is a UI element by looking for a Selectable component in its hierarchy.
            if (hit.collider.GetComponentInParent<Selectable>() != null)
            {
                // Provide visual feedback by updating the LineRenderer to point to the hit object.
                if (pointerLine != null)
                {
                    // Set the starting point of the LineRenderer to the pointer origin.
                    pointerLine.SetPosition(0, pointerOrigin.position);
                    // Set the ending point of the LineRenderer to the hit point.
                    pointerLine.SetPosition(1, hit.point);
                }

                // Handle interaction when the primary index trigger is pressed.
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    // Simulate a click event on the UI element that was hit.
                    ExecuteEvents.Execute(hit.collider.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
                }
            }
        }
        else if (pointerLine != null)
        {
            // If no hit is detected, extend the pointer to its maximum length.
            pointerLine.SetPosition(0, pointerOrigin.position);
            pointerLine.SetPosition(1, pointerOrigin.position + pointerOrigin.forward * pointerLength);
        }
    }
}
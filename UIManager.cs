using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

/* This script is attached on all UI elements that need to face the player's head (MainCamera) */ 
public class MainMenuManager : MonoBehaviour
{
    // Reference to the head transform, typically the camera or the player's head in a VR/AR context.
    public Transform head;

    // The main menu game object that will be toggled on and off.
    public GameObject mainMenu;

    // Input action property for the button that toggles the main menu.
    public InputActionProperty showButton;

    // Called before the first frame update. This method is often used for initialization.
    void Start()
    {
        // Currently empty, but initialization code could be added here if needed.
    }

    // Called once per frame. This method is used for continuous updates.
    void Update()
    {
        // Check if the show button was pressed this frame.
        if (showButton.action.WasPressedThisFrame())
        {
            // Toggle the active state of the main menu game object.
            mainMenu.SetActive(!mainMenu.activeSelf);
        }

        // Make the main menu face the head (camera or player's head) to ensure it is always visible.
        // Calculate the position to look at, keeping the y-position of the main menu constant.
        mainMenu.transform.LookAt(new Vector3(head.position.x, mainMenu.transform.position.y, head.position.z));

        // Invert the forward direction of the main menu to make it face towards the head.
        mainMenu.transform.forward *= -1;
    }
}
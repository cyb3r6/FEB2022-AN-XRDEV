using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristMenu : MonoBehaviour
{
    public GameObject paintUI;
    public GameObject locomotionUI;
    public InputActionReference paintSelectionToggleReference;
    public InputActionReference locomotionSelectionToggleReference;

    private bool paintSelectionActive = false;
    private bool locomotionSelectionActive = false;


    void Start()
    {
        paintSelectionToggleReference.action.performed += PrimaryButtonPressed;
        locomotionSelectionToggleReference.action.performed += SecondaryButtonPressed ;
    }

    private void OnDestroy()
    {
        paintSelectionToggleReference.action.performed -= PrimaryButtonPressed;
        locomotionSelectionToggleReference.action.performed -= SecondaryButtonPressed;
    }

    public void PrimaryButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayPaintUI();
        }
    }
    public void SecondaryButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayLocomotionUI();
        }
    }

    private void DisplayPaintUI()
    {
        paintSelectionActive = !paintSelectionActive;
        paintUI.SetActive(paintSelectionActive);
    }
    private void DisplayLocomotionUI()
    {
        locomotionSelectionActive = !locomotionSelectionActive;
        locomotionUI.SetActive(paintSelectionActive);
    }
}

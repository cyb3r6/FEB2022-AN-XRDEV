using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WristMenu : MonoBehaviour
{
    public GameObject paintUI;
    public InputActionReference paintSelectionToggleReference;

    private bool paintSelectionActive = false;

    // Start is called before the first frame update
    void Start()
    {
        paintSelectionToggleReference.action.performed += PrimaryButtonPressed;
    }

    private void OnDestroy()
    {
        paintSelectionToggleReference.action.performed -= PrimaryButtonPressed;
    }

    public void PrimaryButtonPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            DisplayPaintUI();
        }
    }

    private void DisplayPaintUI()
    {
        paintSelectionActive = !paintSelectionActive;
        paintUI.SetActive(paintSelectionActive);
    }
}

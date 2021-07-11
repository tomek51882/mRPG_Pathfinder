using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterPanelUI : MonoBehaviour
{
    public GameObject characterPanel;

    public void OnToggleCaracterPanel(InputAction.CallbackContext value)
    {
        characterPanel.SetActive(!characterPanel.activeSelf);
    }
}

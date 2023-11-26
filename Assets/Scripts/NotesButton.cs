using UnityEngine;
using UnityEngine.InputSystem;

public class NotesButton : MonoBehaviour
{
    [SerializeField] private GameObject checklist;
    [SerializeField] private PlayerInput playerInput;

    public void ToggleUI() {
        checklist.SetActive(!checklist.activeSelf);
        playerInput.enabled = !checklist.activeSelf;
    }
}
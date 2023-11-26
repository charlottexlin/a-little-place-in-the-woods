using UnityEngine;
using UnityEngine.InputSystem;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    // This is kind of hacky...`blocker` should be a game object with a collider that blocks player's clicks
    [SerializeField] private GameObject blocker;
    [SerializeField] private PlayerInput playerInput;
    private CanvasGroup canvasGroup;

    private void Start() {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Toggles another UI object
    public void ToggleUI() {
        ui.SetActive(!ui.activeSelf);
        blocker.SetActive(!blocker.activeSelf);
        playerInput.enabled = !ui.activeSelf;
    }

    // Toggles this button
    public void ToggleSelf() {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    // Toggle's this button's visibility (the button will still work, just won't be visible)
    public void ToggleVisibility() {
        canvasGroup.alpha = canvasGroup.alpha > 0f ? 0f : 1f;
    }
}
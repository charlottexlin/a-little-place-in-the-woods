using UnityEngine;
using UnityEngine.InputSystem;

public class UIButton : MonoBehaviour
{
    [SerializeField] private GameObject checklist;
    [SerializeField] private GameObject darkened;
    [SerializeField] private PlayerInput playerInput;

    private void Update() {
        //  Detect mouse click
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            OnClick();
        }
    }

    private void OnClick() {
        // Use raycast to determine if the mouse is over this object's collider
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        // Clicked button
        if (hit.collider != null && hit.collider.gameObject == gameObject) {
            ToggleUI();
        }
    }

    // Toggle UI
    public void ToggleUI() {
        checklist.SetActive(!checklist.activeSelf);
        darkened.SetActive(checklist.activeSelf);
        playerInput.enabled = !checklist.activeSelf;
    }
}

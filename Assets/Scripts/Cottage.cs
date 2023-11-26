using UnityEngine;
using UnityEngine.InputSystem;

// When the player touches the cottage, end the game (i.e. cover everything up with the end screen)
public class Cottage : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject endScreen;

    private void Update() {
        //  Detect mouse click on the cottage
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            OnClick();
        }
    }

    private void OnClick() {
        // Use raycast to determine if the mouse is over this object's collider
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        // Cottage was clicked
        if (hit.collider != null && hit.collider.gameObject == gameObject && playerInput && endScreen) {
            // Disable player input
            playerInput.enabled = false;
            // Activate end screen
            endScreen.SetActive(true);
        }
    }
}
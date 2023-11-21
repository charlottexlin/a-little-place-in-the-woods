using UnityEngine;
using UnityEngine.InputSystem;

public class Clickable : MonoBehaviour
{
    [SerializeField] private ClickBehavior clickBehavior;

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
        if (hit.collider != null && hit.collider.gameObject == gameObject) {
            // Perform the appropriate action
            switch (clickBehavior) {
                case ClickBehavior.PickUp:
                    PickUp();
                    break;
                case ClickBehavior.ChangeSprite:
                    ChangeSprite();
                    break;
                case ClickBehavior.Disappear:
                    Disappear();
                    break;
            }

        }
    }

    private void PickUp() {
        if (ItemsToCollect.items.ContainsKey(gameObject.name)) {
            ItemsToCollect.items[gameObject.name] = true;
        }
        Destroy(gameObject);
    }

    private void ChangeSprite() {
        Debug.Log("changing sprite");
        // TODO
    }

    private void Disappear() {
        Destroy(gameObject);
    }
}

public enum ClickBehavior {
    PickUp,
    ChangeSprite,
    Disappear
}

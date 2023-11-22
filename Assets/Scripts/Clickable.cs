using UnityEngine;
using UnityEngine.InputSystem;

public class Clickable : MonoBehaviour
{
    [Header("Click Behavior")]
    [SerializeField] private ClickBehavior clickBehavior;
    [Header("Change Sprite Settings")]
    [SerializeField] private Sprite[] sprites;
    [Header("Move Settings")]
    [SerializeField] private Vector2 newPosition;
    [SerializeField] private float zRotation;
    [Header("Destroy Object Settings")]
    [SerializeField] private GameObject objectToDestroy;
    [Header("Enable Clickable Settings")]
    [SerializeField] private Clickable clickableToEnable;
    private Collider2D coll;
    private SpriteRenderer sprite;
    private int currentSprite = 0;

    private void Start() {
        if (clickBehavior == ClickBehavior.ChangeSprite) {
            sprite = GetComponent<SpriteRenderer>();
            coll = GetComponent<Collider2D>();
        }
    }

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
                case ClickBehavior.Move:
                    Move();
                    break;
                case ClickBehavior.DestroyObject:
                    DestroyObject();
                    break;
                case ClickBehavior.EnableClickable:
                    EnableClickable();
                    break;
            }
        }
    }

    private void PickUp() {
        if (ItemsToCollect.items.ContainsKey(gameObject.name)) {
            ItemsToCollect.items[gameObject.name] = true;
            Destroy(gameObject);
        }
    }

    private void ChangeSprite() {
        if (sprites.Length > 0) {
            if (currentSprite < sprites.Length-1) {
                currentSprite += 1;
            }
            sprite.sprite = sprites[currentSprite];
            // If item can no longer be clicked, remove the collider
            if (currentSprite == sprites.Length-1) {
                Destroy(coll);
            }
        }
    }

    private void Move() {
        gameObject.transform.localPosition = newPosition;
        gameObject.transform.Rotate(new Vector3(0, 0, zRotation));
        // Remove this behaviour so item can no longer be clicked
        Destroy(this);
    }

    private void DestroyObject() {
        if (objectToDestroy) {
            Destroy(objectToDestroy);
        }
    }

    private void Disappear() {
        Destroy(gameObject);
    }

    private void EnableClickable() {
        clickableToEnable.enabled = true;
    }
}

public enum ClickBehavior {
    PickUp, // For collectable items
    ChangeSprite, // Change the sprite of this object
    Move, // Move this object
    DestroyObject, // Destroy another object
    Disappear, // Destroy this object
    EnableClickable // Enable another object to be clickable
}

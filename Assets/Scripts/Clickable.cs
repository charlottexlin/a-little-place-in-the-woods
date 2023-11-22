using UnityEngine;
using UnityEngine.InputSystem;

public class Clickable : MonoBehaviour
{
    [Header("Click Behavior")]
    [SerializeField] private ClickBehavior clickBehavior;
    [Header("Change Sprite Settings")]
    [SerializeField] private Sprite newSprite;
    [Header("Move Settings")]
    // This is a bit inconsistent because I can't get it to properly translate a local position into a world position :(
    // So if animatedMove is true, new position should be a WORLD POSITION.
    // If animatedMove is false, new position should be a LOCAL POSITION.
    [SerializeField] private Vector2 newPosition;
    [SerializeField] private float zRotation;
    [SerializeField] private bool animatedMove = false;
    [SerializeField] private float animationSpeed;
    [Header("Destroy Object Settings")]
    [SerializeField] private GameObject objectToDestroy;
    [Header("Enable Clickable Settings")]
    [SerializeField] private Clickable clickableToEnable;
    private Collider2D coll;
    private SpriteRenderer sprite;
    private bool moving;

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
        // Move animation
        if (moving) {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, animationSpeed * Time.deltaTime);
            // Complete animation and destroy this behavior
            if (Vector2.Distance(transform.position, newPosition) < 0.1f) {
                moving = false;
                Destroy(this);
            }
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

    // So, I originally programmed this thinking some objects would have multiple sprites to change to in sequence
    // Ultimately, I only ended up using one sprite change per object, so some of this code is superfluous
    private void ChangeSprite() {
        sprite.sprite = newSprite;
        Destroy(coll);
    }

    private void Move() {
        if (animatedMove) {
            moving = true;
        } else {
            gameObject.transform.localPosition = newPosition;
            gameObject.transform.Rotate(new Vector3(0, 0, zRotation));
            // Remove this behaviour so item can no longer be clicked
            Destroy(this);
        }
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

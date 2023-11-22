using UnityEngine;
using UnityEngine.InputSystem;

public class Clickable : MonoBehaviour
{
    [SerializeField] private ClickBehavior clickBehavior;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Vector2 newPosition;
    private SpriteRenderer sprite;
    private Collider2D coll;
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
        if (currentSprite < sprites.Length-1) {
            currentSprite += 1;
        }
        // If item can no longer be clicked, remove the collider
        if (currentSprite == sprites.Length-1) {
            Destroy(coll);
        }
        sprite.sprite = sprites[currentSprite];
    }

    private void Move() { // TODO do we need
        gameObject.transform.position = newPosition;
    }

    private void Disappear() {
        Destroy(gameObject);
    }
}

public enum ClickBehavior {
    PickUp,
    ChangeSprite,
    Move,
    Disappear
}

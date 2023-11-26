using UnityEngine;
using UnityEngine.InputSystem;

public class Clickable : MonoBehaviour
{
    [Header("Pick up Settings")]
    [SerializeField] private bool pickUp;
    [Header("Change Sprite Settings")]
    [SerializeField] private bool changeSprite;
    [SerializeField] private Sprite newSprite;
    [Header("Move Settings")]
    [SerializeField] private bool move;
    // This is a bit inconsistent because I can't get it to properly translate a local position into a world position :(
    // So if animatedMove is true, new position should be a WORLD POSITION.
    // If animatedMove is false, new position should be a LOCAL POSITION.
    [SerializeField] private Vector2 newPosition;
    [SerializeField] private float zRotation;
    [SerializeField] private bool animatedMove = false;
    [SerializeField] private float animationSpeed;
    [Header("Destroy Object Settings")]
    [SerializeField] private bool destroyObject;
    [SerializeField] private GameObject objectToDestroy;
    [Header("Dialogue Settings")]
    [SerializeField] private bool dialogue;
    [SerializeField] private string dialogueKey;
    [Header("Enable Clickable Settings")]
    [SerializeField] private bool enableClickable;
    [SerializeField] private Clickable clickableToEnable;
    [Header("Disappear Settings")]
    [SerializeField] private bool disappear;
    private Collider2D coll;
    private SpriteRenderer sprite;
    private bool moving;

    private void Start() {
        if (changeSprite) {
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
                gameObject.transform.Rotate(new Vector3(0, 0, zRotation));
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
            // Perform the appropriate actions
            if (pickUp) PickUp();
            if (changeSprite && newSprite is not null) ChangeSprite();
            if (move) Move();
            if (enableClickable && clickableToEnable is not null) EnableClickable();
            if (dialogue && dialogueKey is not null) ShowDialogue(dialogueKey);
            if (destroyObject && objectToDestroy is not null) DestroyObject();
            if (disappear) Disappear();
        }
    }

    private void PickUp() {
        if (ItemsToCollect.items.ContainsKey(gameObject.name)) {
            ItemsToCollect.items[gameObject.name] = true;
            // Increment items collected counter
            ItemsToCollect.itemsCollected += 1;
            // Display text for this item in the dialogue UI
            ShowDialogue(gameObject.name);
            Destroy(gameObject);
        }
    }

    private void ShowDialogue(string dialogueKey) {
        GameManager.instance.dialogueUI.ShowText(ItemsToCollect.texts[dialogueKey]);
    }

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
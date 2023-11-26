using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// This script is for the leaf on the jar platform
public class Leaf : MonoBehaviour
{
    [Header("Jar Settings")]
    [SerializeField] private Sprite[] jarSprites;
    [SerializeField] private SpriteRenderer jar;
    [Header("Acorn Cap Settings")]
    [SerializeField] private GameObject acornCap;
    [SerializeField] private Vector2[] acornCapPositions;
    [Header("Water Drop Settings")]
    [SerializeField] private Transform waterDrop;
    [SerializeField] private Vector2 waterDropTarget; // in world space coordinates
    [SerializeField] private float waterDropSpeed;
    [SerializeField] private float animationTime;
    private int currentSprite;
    private Collider2D coll;
    private Clickable acornCapClickable;
    private Vector2 waterDropInitial;
    private bool waterDropping;

    private void Start() {
        coll = GetComponent<Collider2D>();
        acornCapClickable = acornCap.GetComponent<Clickable>();
        waterDropInitial = waterDrop.position;
    }

    private void Update() {
        //  Detect mouse click on the leaf
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            OnClick();
        }
        // Animate the water drop
        if (waterDropping) {
            waterDrop.position = Vector2.MoveTowards(waterDrop.position, waterDropTarget, waterDropSpeed * Time.deltaTime);
        }
        // Put the water drop back after the animation is done
        if (Vector2.Distance(waterDrop.position, waterDropTarget) < 0.1f) {
            waterDrop.position = waterDropInitial;
            waterDropping = false;
        }
        // Do not allow acorn cap to be picked up unless at the last sprite
        if (acornCap && !(currentSprite >= jarSprites.Length - 1)) {
            acornCapClickable.enabled = false;
        }
    }

    private void OnClick() {
        // Use raycast to determine if the mouse is over this object's collider
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        // The leaf was clicked
        if (hit.collider != null && hit.collider.gameObject == gameObject && currentSprite < jarSprites.Length - 1 && acornCap) {
            Audio.instance.PlaySound("waterDrop");
            MoveWaterDrop();
            StartCoroutine(ChangeJarSprite());
        }
    }

    private void MoveWaterDrop() {
        waterDrop.position = waterDropInitial;
        waterDropping = true;
    }

    private IEnumerator ChangeJarSprite() {
        // Wait for water drop animation to finish
        yield return new WaitForSeconds(animationTime);
        // Change to next sprite in the sprites list
        if (jarSprites.Length > 0) {
            if (currentSprite < jarSprites.Length-1) {
                currentSprite += 1;
            }
            jar.sprite = jarSprites[currentSprite];
            // When leaf can no longer be clicked, remove the collider
            if (currentSprite == jarSprites.Length-1) {
                Destroy(coll);
            }
        }
        MoveAcornCap();
    }

    // Moves the acorn cap object to the correct position, based on the `currentSprite` index
    private void MoveAcornCap() {
        acornCap.transform.localPosition = acornCapPositions[currentSprite];
    }
}

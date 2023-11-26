using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

// This script keeps track of the order the flowers in the garden are clicked in.
public class Flowers : MonoBehaviour
{
    [SerializeField] Collider2D[] flowers;
    [SerializeField] SpriteRenderer moss;
    [SerializeField] Clickable pocketWatch;
    // Total time (in seconds) that it should take for the moss to fade away when disappearing
    [SerializeField] float fadeTime;
    private List<string> orderClicked = new();
    private List<string> correctOrder = new List<string>() {"blue", "orange", "pink"};
    // This boolean indicates whether the player is near enough to the moss to not need to move the camera
    private bool nearMoss = false;

    private void Update() {
        //  Detect mouse click on any of the flowers
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            OnClick();
        }
    }

    private void OnClick() {
        // Use raycast to determine which flower's collider was clicked on
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        if (hit.collider != null) {
            // Clicked on orange flower
            if (hit.collider == flowers[0]) {
                orderClicked.Add("orange");
            }
            // Clicked on pink flower
            else if (hit.collider == flowers[1]) {
                orderClicked.Add("pink");
            }
            // Clicked on blue flower
            else if (hit.collider == flowers[2]) {
                orderClicked.Add("blue");
            }
        }
        // If all three flowers have been clicked, reset the order clicked
        if (orderClicked.Count >= 3) {
            // Clicked in correct order
            if (orderClicked.SequenceEqual(correctOrder)) {
                // If the moss is not in view, move the camera to disappearing moss temporarily, to indicate puzzle solved
                if (!nearMoss) {
                    Camera.main.GetComponent<FollowCamera>().Disable();
                    Camera.main.transform.position = moss.gameObject.transform.position + Camera.main.GetComponent<FollowCamera>().GetCameraDistance();
                }

                StartCoroutine(MossDisappear());
            }
            // Clicked in incorrect order
            else {
                // Color flowers gray temporarily
                StartCoroutine(GrayFlowers());
            }
            orderClicked.Clear();
        }
    }

    // Detect if player is touching the trigger (the trigger is near the moss)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            nearMoss = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            nearMoss = false;
        }
    }

    // Fade out the moss sprite to transparent
    // Code idea from LeftyRighty at https://forum.unity.com/threads/fading-in-out-gui-text-with-c-solved.380822/
    private IEnumerator MossDisappear() {
        // Wait for a while
        yield return new WaitForSeconds(0.5f);
        while (moss.color.a > 0.0f) {
            moss.color = new Color(moss.color.r, moss.color.g, moss.color.b, moss.color.a - (Time.deltaTime / fadeTime));
            yield return null;
        }
        // Enable pocket watch object to be clickable
        pocketWatch.enabled = true;
        // Wait for a while
        yield return new WaitForSeconds(0.5f);
        // Reenable follow camera
        Camera.main.GetComponent<FollowCamera>().Enable();
        // Disable this script, so the flowers can no longer be clicked
        this.enabled = false;
    }

    // Temporarily make flowers gray
    private IEnumerator GrayFlowers() {
        foreach (Collider2D flower in flowers) {
            flower.gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        yield return new WaitForSeconds(1.5f);
        foreach (Collider2D flower in flowers) {
            flower.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}

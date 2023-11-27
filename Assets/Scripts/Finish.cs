using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// This script controls the ending of the game. It should be attached to the cottage (where the player must go to finish the game)
public class Finish : MonoBehaviour
{
    private void Awake() {
        // Make sure finish script is disabled
        enabled = false;
    }

    private void OnEnable() {
        // Enable clicking on the cottage to end the game
        GetComponent<Cottage>().enabled = true;
        // Show final dialogue
        StartCoroutine(FinalDialogue());
    }

    // Wait for a while, then show the final text to prompt player back to cottage
    private IEnumerator FinalDialogue() {
        yield return new WaitForSeconds(4f);
        GameManager.instance.dialogueUI.ShowText(ItemsToCollect.texts["finish"]);
    }

    // When the player touches the cottage, show dialogue to prompt click
    private void OnTriggerEnter2D(Collider2D other) {
        if (enabled && other.gameObject.tag == "Player") {
            GameManager.instance.dialogueUI.ShowText(ItemsToCollect.texts["cottage"]);
        }
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// This script controls the ending of the game. It should be attached to the cottage (where the player must go to finish the game)
public class Finish : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject endScreen;

    private void Awake() {
        // Make sure finish script is disabled
        enabled = false;
    }

    private void OnEnable() {
        StartCoroutine(FinalDialogue());
    }

    // Wait for a while, then show the final text to prompt player back to cottage
    private IEnumerator FinalDialogue() {
        yield return new WaitForSeconds(4f);
        GameManager.instance.dialogueUI.ShowText(ItemsToCollect.texts["finish"]);
    }

    // When the player touches the cottage, end the game (i.e. cover everything up with the end screen)
    private void OnTriggerEnter2D(Collider2D other) {
        if (enabled && other.gameObject.tag == "Player") {
            // Disable player input
            playerInput.enabled = false;
            // Activate end screen
            endScreen.SetActive(true);
        }
    }
}
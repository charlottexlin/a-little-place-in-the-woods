using TMPro;
using System.Collections;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    private TextMeshProUGUI text;
    private CanvasGroup canvasGroup;
    [SerializeField] private float fadeTime;

    private void Awake() {
        text = transform.Find("dialogue-text").GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ShowText(string newText) {
        // Cancel any previous fading
        StopAllCoroutines();
        // Make it visible again
        canvasGroup.alpha = 1f;
        text.text = newText;
        StartCoroutine(FadeOut());
    }

    // Fade out the dialogue UI to transparent
    private IEnumerator FadeOut() {
        // Wait for a while, so the player can read the text
        yield return new WaitForSeconds(2f);
        while (canvasGroup.alpha > 0f) {
            canvasGroup.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}
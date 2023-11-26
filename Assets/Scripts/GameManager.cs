using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance { get; private set; }
    public DialogueUI dialogueUI;
    [SerializeField] private Finish finish;

    private void Awake() {
        if (instance != null && instance != this) 
            Destroy(this); 
        else 
            instance = this; 
    }

    private void Start() {
        // Display game start dialogue
        dialogueUI.ShowText(ItemsToCollect.texts["start"]);
    }

    private void Update() {
        // If player has collected all items, initiate the ending of the game
        if (ItemsToCollect.itemsCollected >= 10) {
            finish.enabled = true;
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame() {
        Application.Quit();
    }
}
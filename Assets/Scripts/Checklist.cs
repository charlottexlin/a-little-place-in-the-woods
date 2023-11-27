using System.Collections.Generic;
using UnityEngine;

public class Checklist : MonoBehaviour
{
    private GameObject[] checkmarks = new GameObject[10];

    // Mapping of item name to checkmark index
    private Dictionary<string, int> checkmarkIndices = new() {
        {"Postage stamp", 0},
        {"Thimble", 1},
        {"Button", 2},
        {"Acorn cap", 3},
        {"Christmas light", 4},
        {"Postcard", 5},
        {"Feather", 6},
        {"Pocket watch", 7},
        {"Bottle cap", 8},
        {"Seashell", 9},
    };
    
    private void Awake() {
        // Populate `checkmarks` array with of this game object's children
        int i = 0;
        foreach (Transform child in transform) {
            if (i >= 10) break;
            checkmarks[i] = child.gameObject;
            i++;
        }
    }

    private void OnEnable() {
        foreach (var (itemName, index) in checkmarkIndices) {
            // This item has been collected, so enable its checkmark
            if (ItemsToCollect.items[itemName]) {
                checkmarks[index].SetActive(true);
            }
        }
    }
}

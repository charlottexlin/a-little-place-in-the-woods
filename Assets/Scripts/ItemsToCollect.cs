using System.Collections.Generic;

// Map of all items to collect in the game
// Key is the name of the item, value is whether it has been collected or not
public static class ItemsToCollect {
    public static Dictionary<string, bool> items = new() {
        {"Postage stamp", false},
        {"Thimble", false},
        {"Button", false},
        {"Acorn cap", false},
        {"Feather", false},
        {"Pocket watch", false},
        {"Bottle cap", false},
        {"Postcard", false},
        {"Christmas light", false},
        {"Seashell", false},
    };
}
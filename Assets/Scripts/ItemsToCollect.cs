using System.Collections.Generic;

// Map of all items to collect in the game
// Key is the name of the item, value is whether it has been collected or not
public static class ItemsToCollect {
    public static int itemsCollected = 0;
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

    public static Dictionary<string, string> texts = new() {
        {"start", "I just moved into this corner of the woods. Please help me find all the furniture I need for my cottage!"},
        {"jar", "I can't reach it."},
        {"water", "The water's too deep to cross. Can we make a bridge of some kind?"},
        {"finish", "I found everything on my checklist! Let's bring it all back to my cottage now, where we started."},
        {"Postage stamp", "Such a pretty picture! It'll look great on my living room wall."},
        {"Thimble", "A thimble! This would be a cute footstool."},
        {"Button", "A pretty button! This will make a nice dinner plate, if I patch up the holes."},
        {"Acorn cap", "This acorn cap is the perfect size for a lampshade!"},
        {"Feather", "What a gorgeous feather! I think I'll put it outside my house as a flag. Then my friends will always know which house is mine."},
        {"Pocket watch", "It's a pocket watch, and it's such a lovely color! I'll put it on my wall, so I always know what time it is."},
        {"Bottle cap", "Nice, a shiny bottle cap! It's the perfect size to bake pies in. I wonder what kind of pie I should make first?"},
        {"Postcard", "Woah, what is this a picture of? It'll make a great poster for my room...if I can drag it all the way home."},
        {"Christmas light", "This Christmas light is so sparkly and pretty! It'll be perfect for my lamp."},
        {"Seashell", "Wow, a seashell! I can hear the ocean! This will be perfect for my bathroom sink."},
    };
}
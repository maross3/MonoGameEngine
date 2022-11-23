using System.Collections.Generic;

namespace TestMonogame.Data
{
    public class Inventory
    {
        public int OwnerId { get; set; }
        public int SpaceAllotment { get; set; }

        // todo item == enum, quantity == int
        public Dictionary<string, string> ItemAndQuantityDictionary { get; set; }
    }
}

using UnityEngine;
using Mono.Data.Sqlite;

namespace Models {
    public class Character
    {
        private static string tableName = "characters";
        
        public int id;
        public int worldId;
        public int classId;
        public int professionId;
        public string name;
        public int level;
        public Vector3 location;
        public int healthMax;
        public int healthCurrent;
        public int energyMax;
        public int energyCurrent;
        public int manaMax;
        public int manaCurrent;
        public int inventorySlots;
    }
}
using UnityEngine;
using Mono.Data.Sqlite;

namespace Models {
    public class World
    {
        private static string tableName = "worlds";
        
        public int id;
        public int profileId;
    }
}
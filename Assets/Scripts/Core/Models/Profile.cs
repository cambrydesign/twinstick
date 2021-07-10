using UnityEngine;
using Mono.Data.Sqlite;

namespace Models {
    public class Profile
    {
        private static string tableName = "profiles";
        
        public int id;
        public string name;
    }
}
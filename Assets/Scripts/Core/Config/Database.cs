using UnityEngine;
using Mono.Data.Sqlite;

namespace Config {
    public class Database
    {

        // SQLite connection path
        public static SqliteConnection conn = new SqliteConnection("URI=file:" + Application.persistentDataPath + "/ih.db");

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{

    public Vector2 worldSize = new Vector2(4,4);
    Room[,] rooms;
    List<Vector2> takenPositions = new List<Vector2>();

    int gridSizeX;
    int gridSizeY;
    int numberOfRooms = 20;
    public GameObject roomWhiteObj;


    // Start is called before the first frame update
    void Start()
    {
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2)) {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        gridSizeX = Mathf.RoundToInt(worldSize.x);
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms();
        SetRoomDoors();
        
    }

    private void CreateRooms() {
        // Create starting room
        rooms = new Room[gridSizeX * 2, gridSizeY * 2];
        rooms[gridSizeX, gridSizeY] = new Room(Vector2.zero, "start");
        takenPositions.Insert(0, Vector2.zero);
        Vector2 checkPos = Vector2.zero;

        // These comparisons are used to change the tendency of generation towards clustered rooms or snaked branches
        float randomCompare = 0.2f;
        float randomCompareStart = 0.2f;
        float randomCompareEnd = 0.2f;

        // Adding rooms
        for (int i = 0; i < numberOfRooms -1; i++) {
            float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);

            // Get New Position
            checkPos = NewPosition();

            // Test New Position
            if (NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare) {
                int iterations = 0;
                do {
                    checkPos = SelectiveNewPosition();
                    iterations++;
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1 && iterations < 100);
                if (iterations >= 50) {
                    Debug.Log("Error: Could not create room with fewer than " + NumberOfNeighbors(checkPos, takenPositions) + " neighbors");
                }
            }

            // Finalise positions
            rooms[(int)checkPos.x + gridSizeX, (int)checkPos.y + gridSizeY] = new Room(checkPos, "next");
            takenPositions.Insert(0, checkPos);
        }
    }

    Vector2 NewPosition() {
        int x = 0;
        int y = 0;
        Vector2 checkingPos = Vector2.zero;
        do {
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool upDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (upDown) {
                if (positive) {
                    y++;
                } else {
                    y--;
                }
            } else {
                if (positive) {
                    x++;
                } else {
                    x--;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >=gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        return checkingPos;
    }

    Vector2 SelectiveNewPosition() {
        int index = 0;
        int inc = 0;
        int x = 0;
        int y = 0;
        Vector2 checkingPos = Vector2.zero;
        do {
            inc = 0;
            do {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
                inc++;
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1 && inc < 100);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool upDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (upDown) {
                if (positive) {
                    y++;
                } else {
                    y--;
                }
            } else {
                if (positive) {
                    x++;
                } else {
                    x--;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >=gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        if (inc >= 100) {
            Debug.Log("Error: Could not find a position with only one neighbor");
        }
        return checkingPos;
    }

    int NumberOfNeighbors (Vector2 checkingPos, List<Vector2> usedPositions) {
        int ret = 0;
        if (usedPositions.Contains(checkingPos + Vector2.right)) {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left)) {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up)) {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down)) {
            ret++;
        }
        return ret;
    }

    void SetRoomDoors() {
        for (int x = 0; x < ((gridSizeX * 2)); x++) {
            for (int y = 0; y < ((gridSizeY * 2)); y++) {
                if (rooms[x ,y] == null) {
                    continue;
                }
                Vector2 gridPosition = new Vector2(x, y);
                if (y - 1 < 0) {
                    rooms[x, y].doorBottom = false;
                } else {
                    rooms[x, y].doorBottom = (rooms[x, y - 1] != null);
                }
                if (y + 1 >= gridSizeY * 2) {
                    rooms[x, y].doorTop = false;
                } else {
                    rooms[x, y].doorTop = (rooms[x, y + 1] != null);
                }
                if (x - 1 < 0) {
                    rooms[x, y].doorLeft = false;
                } else {
                    rooms[x, y].doorLeft = (rooms[x - 1, y] != null);
                }
                if (x + 1 >= gridSizeX * 2) {
                    rooms[x, y].doorRight = false;
                } else {
                    rooms[x, y].doorRight = (rooms[x + 1, y] != null);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

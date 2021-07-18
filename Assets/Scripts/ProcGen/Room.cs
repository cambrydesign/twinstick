using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2 gridPos;
    public string type;
    public bool doorTop;
    public bool doorBottom;
    public bool doorLeft;
    public bool doorRight;

    public Room (Vector2 _gridPos, string _type) {
        gridPos = _gridPos;
        type = _type;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private Object[] sides;
    private Object[] corners;
    private Object[] centers;

    private List<int> usedSides = new List<int>();
    private List<int> usedCorners = new List<int>();

    private GameObject center;
    private GameObject top;
    private GameObject bottom;
    private GameObject left;
    private GameObject right;
    private GameObject topRight;
    private GameObject bottomRight;
    private GameObject topLeft;
    private GameObject bottomLeft;

    void Awake() 
    {
        sides = Resources.LoadAll("Plots/Side", typeof(GameObject));
        corners = Resources.LoadAll("Plots/Corner", typeof(GameObject));
        centers = Resources.LoadAll("Plots/Center", typeof(GameObject));

        AssignPlots();

        BuildPlots();
    }

    private void AssignPlots() 
    {
        int centerIndex = Random.Range(1, centers.Length);

        int topIndex = GetSideIndex();
        int leftIndex = GetSideIndex();
        int rightIndex = GetSideIndex();
        int bottomIndex = GetSideIndex();
        
        int topRightIndex = GetCornerIndex();
        int topLeftIndex = GetCornerIndex();
        int bottomRightIndex = GetCornerIndex();
        int bottomLeftIndex = GetCornerIndex();

        center = (GameObject)centers[centerIndex];

        top = (GameObject)sides[topIndex];
        left = (GameObject)sides[leftIndex];
        right = (GameObject)sides[rightIndex];
        bottom = (GameObject)sides[bottomIndex];

        topRight = (GameObject)corners[topRightIndex];
        topLeft = (GameObject)corners[topLeftIndex];
        bottomRight = (GameObject)corners[bottomRightIndex];
        bottomLeft = (GameObject)corners[bottomLeftIndex];
    }

    private void BuildPlots() 
    {
        BuildPlot(center, new Vector3(0, 0, 0), new Vector3(0, 0, 0), "Center");

        BuildPlot(top, new Vector3(7, 0, 31), new Vector3(0, -90, 0), "Top");
        BuildPlot(left, new Vector3(-31, 0, 7), new Vector3(0, -180, 0), "Left");
        BuildPlot(right, new Vector3(31, 0, -7), new Vector3(0, 0, 0), "Right");
        BuildPlot(bottom, new Vector3(-7, 0, -31), new Vector3(0, 90, 0), "Bottom");

        BuildPlot(topRight, new Vector3(29.8f, 0, 29.8f), new Vector3(0, 0, 0), "TopRight");
        BuildPlot(topLeft, new Vector3(-29.8f, 0, 29.8f), new Vector3(0, -90, 0), "TopLeft");
        BuildPlot(bottomRight, new Vector3(29.8f, 0, -29.8f), new Vector3(0, 90, 0), "BottomRight");
        BuildPlot(bottomLeft, new Vector3(-29.8f, 0, -29.8f), new Vector3(0, -180, 0), "BottomLeft");
    }

    private void BuildPlot(GameObject target, Vector3 position, Vector3 rotation, string name) 
    {
        GameObject plot = Instantiate(target);
        //GameObject parent = GameObject.FindGameObjectWithTag(name);
        plot.transform.parent = transform;
        plot.transform.localPosition = position;
        plot.transform.Rotate(rotation);
    }

    private int GetCornerIndex() {
        int index = Random.Range(1, corners.Length);
        if (usedCorners.Contains(index)) {
            return GetCornerIndex();
        } else {
            usedCorners.Add(index);
            return index;
        }
    }

    private int GetSideIndex() {
        int index = Random.Range(1, sides.Length);
        if (usedSides.Contains(index)) {
            return GetSideIndex();
        } else {
            usedSides.Add(index);
            return index;
        }
    }
}

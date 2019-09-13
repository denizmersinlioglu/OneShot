using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DottedLineDemo : MonoBehaviour {
    [Header("Points for line 1")]
    public Vector2 pointA;
    public Vector2 pointB;

    void Update() {
        DottedLine.DottedLine.Instance.DrawDottedLine(pointA, pointB);

    }
}

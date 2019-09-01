using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScreenCollider : MonoBehaviour {
    public float colThickness = 4f;
    public float zPosition;
    private Vector2 screenSize;

    void Start() {
        var colliders = new Dictionary<string, Transform> {
            { "Top", new GameObject().transform },
            { "Bottom", new GameObject().transform },
            { "Right", new GameObject().transform },
            { "Left", new GameObject().transform }
        };

        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        foreach (KeyValuePair<string, Transform> valPair in colliders) {
            valPair.Value.gameObject.AddComponent<BoxCollider2D>();
            valPair.Value.name = valPair.Key + "Collider";
            valPair.Value.parent = transform;

            if (valPair.Key == "Left" || valPair.Key == "Right")
                valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
            else
                valPair.Value.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);
        }

        colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["Top"].localScale.y * 0.5f), zPosition);
        colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f), zPosition);
    }

    private List<GameObject> GetNearestGameObject(List<GameObject> gameObjects, int count = 1) {
        return gameObjects
            .OrderBy(t => Vector3.Distance(transform.position, t.transform.position))
            .Take(count) as List<GameObject>;
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Creates a 2D boundary for Dynamic Screens.
/// </summary>
public class ScreenCollider : MonoBehaviour {
    public float colThickness = 4f;
    public float zPosition = 0f;
    private Vector2 screenSize;

    void Start() {
        //Create a Dictionary to contain all our Objects/Transforms
        Dictionary<string, Transform> colliders = new Dictionary<string, Transform>();

        //Create our GameObjects and add their Transform components to the Dictionary we created above
        colliders.Add("Top", new GameObject().transform);
        colliders.Add("Bottom", new GameObject().transform);
        colliders.Add("Right", new GameObject().transform);
        colliders.Add("Left", new GameObject().transform);

        //Generate world space point information for position and scale calculations
        //Grab the world-space position values of the start and end positions of the screen, 
        //then calculate the distance between them and store it as half, 
        //since we only need half that value for distance away from the camera to the edge
        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        //For each Transform/Object in our Dictionary
        //Add our colliders. Remove the "2D", if you would like 3D colliders.
        //Set the object's name to it's "Key" name, and take on "Collider".  i.e: TopCollider
        //Make the object a child of whatever object this script is on (preferably the camera)
        //Scale the object to the width and height of the screen, using the world-space values calculated earlier
        foreach (KeyValuePair<string, Transform> valPair in colliders) {
            valPair.Value.gameObject.AddComponent<BoxCollider2D>();
            valPair.Value.name = valPair.Key + "Collider";
            valPair.Value.parent = transform;

            if (valPair.Key == "Left" || valPair.Key == "Right")
                valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
            else
                valPair.Value.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);
        }
        //Change positions to align perfectly with outter-edge of screen, 
        //adding the world-space values of the screen we generated earlier, 
        //and adding/subtracting them with the current camera position, 
        //as well as add/subtracting half out objects size so it's not just half way off-screen
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
using UnityEngine;
using UnityEngine.AI;
using System;

public class MoveTo : MonoBehaviour {

    /// <summary>
    /// The speed base multiplier for the player game object.
    /// </summary>
    public float speed = 10.0f;
    
    /// <summary>
    /// The rotation base speed multiplier for the player game object.
    /// </summary>
    public float rotationSpeed = 100.0f;

    public Transform floor;
    public TriggerBridge triggerBridge;

    private float maxX = 0;
    private float maxZ = 0;
    private Vector3[] corners = new Vector3[4];

    void Start () {
        maxX = floor.localScale.x / 2 + 1;
        maxZ = floor.localScale.z / 2 + 1;
        float myX = transform.localScale.x / 2;
        float myY = transform.localScale.z / 2;
        corners = new Vector3[4] {
        new Vector3(myX, 0, myY),
        new Vector3(myX, 0, -myY),
        new Vector3(-myX, 0, myY),
        new Vector3(-myX, 0, -myY)};
    }

    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        transform.Translate(0, 0, translation);

        if (!checkForFloor()) {
            transform.Translate(0, 0, -translation);
            return;
        }
        transform.position = new Vector3(transform.position.x, 2f, transform.position.z);

        // Rotate around our y-axis
        transform.Rotate(0, rotation, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void OnCollisionEnter(Collision collision) {
        try {
            collision.gameObject.GetComponent<MemoryLink>().gateLink.SetActive(true);
            triggerBridge.ShowBridge();
            Destroy(collision.gameObject);
            return;
        } catch (NullReferenceException e) {
            e.ToString();
        }
        try {
            collision.gameObject.GetComponent<Traverse>().NextStage();
        } catch (NullReferenceException e) {
            e.ToString();
        }
    }

    bool checkForFloor() {

        RaycastHit hit;
        Vector3 position = transform.position;
        foreach (Vector3 corner in corners) {
        // Does the ray intersect any objects excluding the player layer
            if (!Physics.Raycast(position + corner, Vector3.down, out hit, 50))
            {
                return false;
            }
        }
        return true;
    }

    
}
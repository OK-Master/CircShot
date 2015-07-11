using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = (Vector2)transform.TransformDirection(Vector3.up) * speed;
    }
}

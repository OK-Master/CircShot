using UnityEngine;
using System.Collections;

public class StarMover : MonoBehaviour
{
    public float speed;
    public float sizeFactor;

    private Vector3 startScale;

    // Use this for initialization
    void Start()
    {
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = (Vector2)transform.TransformDirection(Vector3.up) * speed;
        transform.localScale = startScale * Vector2.Distance(Vector2.zero, transform.position) * sizeFactor;
    }
}

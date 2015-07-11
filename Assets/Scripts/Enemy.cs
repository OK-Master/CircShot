using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed;
    public GameObject bomb;
    public Transform bombSpawn;
    public float minFireRate;
    public float maxFireRate;

    private float nextShot = 0f;
    private GameController gameController;

    // Use this for initialization
    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
            gameController = gameControllerObject.GetComponent<GameController>();
        nextShot = Time.time + Random.Range(minFireRate, maxFireRate);
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.Rotate(0, 0, speed);

        if (Time.time > nextShot)
        {
            nextShot = Time.time + Random.Range(minFireRate, maxFireRate);
            Instantiate(bomb, bombSpawn.position, bombSpawn.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Shot")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            gameController.AddScore(10);
        }
    }
}

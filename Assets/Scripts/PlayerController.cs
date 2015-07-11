using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public GameController gameController;
    public float speed;
    public GameObject shot;
    public Transform shotSpawn;
    public float shotRate;
    public Color colliderColor;

    private float nextShot = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float direction = 0;
        direction = Input.GetAxis("Horizontal");
        transform.parent.Rotate(0, 0, direction * speed);
        if (Input.GetButton("Jump") && Time.time > nextShot)
        {
            nextShot = Time.time + shotRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bomb")
        {
            Destroy(other.gameObject);
            gameController.ChangeLives(-1);
            StartCoroutine(collideFlash());
        }
    }

    IEnumerator collideFlash()
    {
        GetComponent<SpriteRenderer>().color = colliderColor;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}

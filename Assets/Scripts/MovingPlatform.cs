using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private GameObject topBorder;
    public float speed;
    public bool beenHit { get; private set; }
    public float Speed { get => speed; set => speed = value; }

    //Platforms are visible longer than the player so we have to destroy them later.
    //How much later is controlled by this offset
    public float offsetY;

    void Start()
    {
        //for tests
        Speed = 2f;
        beenHit = false;
        //Not the mose efficient but it's called only one per object. Also this game doesn't have a lot of objects
        topBorder = GameObject.Find("TopBorder");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * Speed * Time.deltaTime;

        if (topBorder.transform.position.y + offsetY < transform.position.y)
        {
            Destroy(gameObject);
        }
    }

    public void onHit()
    {
        beenHit = true;
    }
}
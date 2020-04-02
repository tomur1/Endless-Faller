using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    public bool beenHit { get; private set; }

    void Start()
    {
        beenHit = false;
    }

    // Update is called once per frame
    void Update()
    {
            transform.position += Vector3.up * speed;
    }

    public void onHit()
    {
        beenHit = true;
    }
}
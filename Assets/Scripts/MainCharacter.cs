using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public GameObject LevelManager;
    [SerializeField] private float speed;
    LevelManager ManagerScript;

    void Start()
    {
        ManagerScript = LevelManager.GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.collider.tag.Equals("Platform"))
        {
            MovingPlatform platform = collision.collider.GetComponentInParent<MovingPlatform>();
            if (!platform.beenHit)
            {
                platform.onHit();
                ManagerScript.IncrementScore();
            }
            
        }
    }
}

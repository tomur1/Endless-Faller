using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] private float speed;

    public GameObject LevelManager;
    LevelManager ManagerScript;
    public GameObject topBorder;
    public GameObject bottomBorder;

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

        //check if player failed
        if (GameManager.managerInstance.currentState == GameManager.GameState.Gameplay &&
            (transform.position.y > topBorder.transform.position.y || transform.position.y < bottomBorder.transform.position.y))
        {
            ManagerScript.GameOver();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject PlatformPrefab;
    //could also just get parent object and find the specific walls. Wonder which would be better?
    public GameObject Floor;
    public GameObject WallLeft;
    public GameObject WallRight;
    private LevelManager levelManager;
    private int platformCount;
    private float startTime;
    //Time between platform spawns (in seconds)
    [SerializeField] float generationInterval;
    [SerializeField] float initialPlatformSpeed;
    public IEnumerator generationRoutine;
    // Start is called before the first frame update
    void Start()
    {
        platformCount = 0;
        levelManager = gameObject.GetComponent<LevelManager>();
        LoadStartingValuesFromJSON();
        //save the couroutine in case we want to stop it.
        generationRoutine = generatePlatforms();
        startTime = Time.time;
        
        StartCoroutine(generationRoutine);
    }

    private void LoadStartingValuesFromJSON()
    {
        InitialPreferences initialPreferences = SaverLoader.LoadStartingValues();
        initialPlatformSpeed = initialPreferences.initialPlatformSpeed;
    }

    void generateHoleInARandomSpot(GameObject platform)
    {
        GameObject leftPlatform = platform.transform.GetChild(0).gameObject;
        GameObject rightPlatform = platform.transform.GetChild(1).gameObject;

        //platform has scale length of 10
        float holePosition = UnityEngine.Random.Range(0.0f, 10.0f);
        //clam the value betwwen bounds
        holePosition =  Mathf.Clamp(holePosition, 0.25f, 9.75f);

        float leftPlatScale = holePosition - 0.25f;
        float rightPlatScale = 9.75f - holePosition;

        leftPlatform.transform.localScale = new Vector3(leftPlatScale, leftPlatform.transform.localScale.y, leftPlatform.transform.localScale.z);
        rightPlatform.transform.localScale = new Vector3(rightPlatScale, rightPlatform.transform.localScale.y, rightPlatform.transform.localScale.z);
        //now calculate the position.
        //The position of the wall should be half of the scale from the wall.

        float wallLeftX = WallLeft.transform.position.x;
        float wallRightX = WallRight.transform.position.x;

        float leftPosX = ((leftPlatScale) / 2) + wallLeftX + 0.5f;
        float rightPosX = wallRightX - 0.5f - ((rightPlatScale) / 2);

        leftPlatform.transform.position = new Vector3(leftPosX, leftPlatform.transform.position.y, leftPlatform.transform.position.z);
        rightPlatform.transform.position = new Vector3(rightPosX, rightPlatform.transform.position.y, rightPlatform.transform.position.z);

    }

    IEnumerator generatePlatforms()
    {
        
        while (true)
        {
            float runningTime = Time.time - startTime;
            float shortenInterval = runningTime / 100;
            float speedUpPlatform = shortenInterval * 2;

            GameObject initializedPlatform = Instantiate(PlatformPrefab, Floor.transform.position, Quaternion.identity);
            generateHoleInARandomSpot(initializedPlatform);
            initializedPlatform.GetComponent<MovingPlatform>().Speed = initialPlatformSpeed + speedUpPlatform;

            //if this is the highscore platform change it's color
            if (platformCount == levelManager.HighScore)
            {
                foreach (MeshRenderer renderer in initializedPlatform.GetComponentsInChildren<MeshRenderer>())
                {
                    renderer.material.color = Color.blue;
                }
            }
            platformCount++;
            yield return new WaitForSeconds(generationInterval - shortenInterval);
        }
    }

    public void ResetGeneration()
    {
        if (generationRoutine != null)
        {
            platformCount = 0;
            StopCoroutine(generationRoutine);
            startTime = Time.time;
            StartCoroutine(generationRoutine);
        }
    }
}

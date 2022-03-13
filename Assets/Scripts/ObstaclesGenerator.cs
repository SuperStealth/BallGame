using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    [SerializeField] private GameObject wall;

    [SerializeField] private float generatorTick;
    [SerializeField] private float screenBottomY;

    [SerializeField] private int obstaclesPoolSize;

    private List<GameObject> obstacles = new List<GameObject>();
    
    private int currentObstacleNumber;
    
    private bool generatorActive;
    private IGlobalSpeedMultiplierContainer speedMultiplier;
    
    private void Awake()
    {
        for (int i = 0; i < obstaclesPoolSize; i++)
        {
            var obstacle = Instantiate(wall);
            obstacles.Add(obstacle);
            obstacle.SetActive(false);
            
        }
        currentObstacleNumber = 0;
    }

    public void StartGeneration()
    {
        generatorActive = true;
        StartCoroutine(StartGenerating());
    }

    public void SetSpeedMultiplier(IGlobalSpeedMultiplierContainer multiplier)
    {
        speedMultiplier = multiplier;
    }

    public void StopGeneration()
    {
        generatorActive = false;
        for (int i = 0; i < obstaclesPoolSize; i++)
        {
            obstacles[i].SetActive(false);
        }
    }

    private IEnumerator StartGenerating()
    {
        yield return new WaitForSeconds(generatorTick);
        
        while (generatorActive)
        {
            float x = Random.Range(-4f, 4f);

            var obstacle = obstacles[currentObstacleNumber];
            obstacle.SetActive(true);
            obstacle.transform.position = new Vector3(x, 5, 0);

            var obstacleComponent = obstacle.GetComponent<Obstacle>();
            obstacleComponent.SetSpeedMultiplier(speedMultiplier);
            obstacleComponent.SetBottomYPosition(screenBottomY);


            int gambleResult = Random.Range(0, 2);
            if (gambleResult == 0)
            {
                obstacle.tag = "Wall";
                obstacle.GetComponent<MeshRenderer>().material.color = Color.red;
            }
            else
            {
                obstacle.tag = "Bonus";
                obstacle.GetComponent<MeshRenderer>().material.color = Color.green;
            }

            currentObstacleNumber++;
            if (currentObstacleNumber >= obstaclesPoolSize)
            {
                currentObstacleNumber = 0;
            }
            yield return new WaitForSeconds(generatorTick);
        }

    }
}

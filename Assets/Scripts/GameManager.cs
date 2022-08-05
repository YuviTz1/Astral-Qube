using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cubeEnemy;
    public GameObject sphereEnemy;
    public GameObject humanoidEnemy;

    float p;
    float enemyProb;
    public float timedelay=5f;
    public float lastCallTime = 0.0f;
    public float spawnDecayRate = 0.15f;

    public int score = 0;
    public int highScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //spawning enemy
        if(Time.time-lastCallTime>timedelay)
        {
            spawnEnemyRoutine();
            lastCallTime = Time.time;
            if (timedelay > 0.8)
            {
                timedelay -= spawnDecayRate;
            }
        }

    }

    
    void spawnEnemyRoutine()
    {
        p = Random.Range(0.0f, 1.0f);
        if (p <= 0.1)
        {
            //spawn below
            p = Random.Range(-40.0f, 40.0f);
            enemyProb = Random.Range(0.0f, 1.0f);

            Vector3 pos = new Vector3(p, 1.893f, -40f);
            spawnEnemy(pos, enemyProb);
        }
        else if (p <= 0.43)
        {
            //spawn right
            p = Random.Range(-40.0f, 40.0f);
            enemyProb = Random.Range(0.0f, 1.0f);

            Vector3 pos = new Vector3(40f, 1.893f, p);
            spawnEnemy(pos, enemyProb);
        }
        else if (p <= 0.77)
        {
            //spawn above
            p = Random.Range(-40.0f, 40.0f);
            enemyProb = Random.Range(0.0f, 1.0f);

            Vector3 pos = new Vector3(p, 1.893f, 40f);
            spawnEnemy(pos, enemyProb);
        }
        else
        {
            //spawn left
            p = Random.Range(-40.0f, 40.0f);
            enemyProb = Random.Range(0.0f, 1.0f);

            Vector3 pos = new Vector3(-40f, 1.893f, p);
            spawnEnemy(pos, enemyProb);
        }
    }

    void spawnEnemy(Vector3 pos, float enemyProb)
    {
        if (enemyProb <= 0.33)
        {
            Instantiate(cubeEnemy, pos, Quaternion.identity);
        }
        else if(enemyProb<=0.66)
        {
            Instantiate(sphereEnemy, pos, Quaternion.identity);
        }
        else
        {
            Instantiate(humanoidEnemy, pos, Quaternion.identity);
        }
    }
}

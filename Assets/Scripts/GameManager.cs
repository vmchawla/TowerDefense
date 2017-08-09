using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int maxEnemiesOnScreen;
    [SerializeField] private int totalEnemies;
    [SerializeField] private int enemiesPerSpawn;
    [SerializeField] private float spawnDelay = 0.5f;

    private int enemiesOnScreen = 0;




	void Start ()
	{

	    StartCoroutine(spawn());
	}
	
	// Update is called once per frame


    IEnumerator spawn()
    {
        if (enemiesPerSpawn > 0 && enemiesOnScreen < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (enemiesOnScreen < maxEnemiesOnScreen)
                {
                    var newEnemy = Instantiate(enemies[1]);
                    newEnemy.transform.position = spawnPoint.transform.position;
                    enemiesOnScreen += 1;
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(spawn());
        }
    }

    public void removeEnemyFromScreen()
    {
        if (enemiesOnScreen > 0)
        {
            enemiesOnScreen -= 1;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum gameStatus
{
    next, play, gameover, win
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private int totalWaves = 10;
    [SerializeField] private Text totalMoneyLbl;
    [SerializeField] private Text currentWaveLbl;
    [SerializeField] private Text totalEscapedLbl;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private int maxEnemiesOnScreen;
    [SerializeField] private int totalEnemies = 3;
    [SerializeField] private int enemiesPerSpawn;
    [SerializeField] private float spawnDelay = 0.5f;

    [SerializeField] private Text playBtnLbl;
    [SerializeField] private Button playBtn;

    private int waveNumber = 0;
    private int totalMoney = 10;
    private int totalEscaped = 0;
    private int roundEscaped = 0;
    private int totalKilled = 0;
    private int whichEnemiesToSpawn = 0;
    private gameStatus currentState = gameStatus.play;



    public List<Enemy> EnemyList = new List<Enemy>();

    public int TotalMoney
    {
        get { return totalMoney; }
        set
        {
            totalMoney = value;
            totalMoneyLbl.text = totalMoney.ToString();
        }
    }

    public int TotalEscaped
    {
        get { return totalEscaped; }
        set { totalEscaped = value; }
    }

    public int RoundEscaped
    {
        get { return roundEscaped; }
        set { roundEscaped = value; }
    }

    public int TotalKilled
    {
        get { return totalKilled; }
        set { totalKilled = value; }
    }


    void Start ()
	{
        playBtn.gameObject.SetActive(false);
	    ShowMenu();
	}


    // Update is called once per frame


    IEnumerator Spawn()
    {
        if (enemiesPerSpawn > 0 && EnemyList.Count < totalEnemies)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    var newEnemy = Instantiate(enemies[0]);
                    newEnemy.transform.position = spawnPoint.transform.position;
                    
                }
            }
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(Spawn());
        }
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies()
    {
        foreach (Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
    }

    public void subtractMoney(int amount)
    {
        totalMoney -= amount;
    }

    public void ShowMenu()
    {
        switch (currentState)
        {
            case gameStatus.gameover:
                playBtnLbl.text = "Play again!";
                //add game over sound
                break;
            case gameStatus.next:
                playBtnLbl.text = "Next Wave";
                break;
            case gameStatus.play:
                playBtnLbl.text = "Play";
                break;
            case gameStatus.win:
                playBtnLbl.text = "Play";
                break;
        }
        playBtn.gameObject.SetActive(true);
    }

    public void IsWaveOver()
    {
        totalEscapedLbl.text = "Escaped " + TotalEscaped + "/10";
        if ((roundEscaped + totalKilled) == totalEnemies)
        {
            SetCurrentGameState();
            ShowMenu();
        }

    }

    public void SetCurrentGameState()
    {
        if (totalEscaped >= 10)
        {
            currentState = gameStatus.gameover;
        } else if (waveNumber == 0 || (totalKilled + roundEscaped) == 0)
        {
            currentState = gameStatus.play;
        } else if (waveNumber >= totalWaves)
        {
            currentState = gameStatus.win;
        }
        else
        {
            currentState = gameStatus.next;
        }
    }

    public void PlayBtnPressed()
    {
        switch (currentState)
        {
            case gameStatus.next:
                waveNumber += 1;
                totalEnemies += waveNumber;
                break;
            default:
                totalEnemies = 3;
                totalEscaped = 0;
                totalMoney = 10;
                totalMoneyLbl.text = totalMoney.ToString();
                totalEscapedLbl.text = "Escaped " + totalEscaped + "/10";
                break;
        }
        DestroyAllEnemies();
        totalEscaped = 0;
        roundEscaped = 0;
        currentWaveLbl.text = "Wave " + (waveNumber + 1);
        StartCoroutine(Spawn());
        playBtn.gameObject.SetActive(false);


    }

}

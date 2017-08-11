using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{ 
    [SerializeField] private Transform exitPoint;
    [SerializeField] private Transform[] wayPoints;
    [SerializeField] private float navigationUpdate;
    [SerializeField] private float healthPoints;
    [SerializeField] private int rewardAmt;

    private int target = 0;
    private Transform enemy;
    private float navigationTime = 0;
    private bool isDead = false;
    private Collider2D enemyCollider;
    private Animator anim;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }


	void Start ()
	{
        GameManager.Instance.RegisterEnemy(this);
	    enemy = GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {

	    if (wayPoints != null && !isDead)
	    {
	        navigationTime += Time.deltaTime;
	        if (navigationTime > navigationUpdate)
	        {
	            if (target < wayPoints.Length)
	            {
	                enemy.position = Vector2.MoveTowards(enemy.position, wayPoints[target].position, navigationTime);
	            }
	            else
	            {
	                enemy.position = Vector2.MoveTowards(enemy.position, exitPoint.position, navigationTime);
	            }
	            navigationTime = 0;
	        }
	    }
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "checkpoint")
        {
            target += 1;
        } else if (other.tag == "Finish")
        {
            print(this.gameObject.name);
            GameManager.Instance.RoundEscaped += 1;
            GameManager.Instance.TotalEscaped += 1;
            GameManager.Instance.UnregisterEnemy(this);
            GameManager.Instance.IsWaveOver();
        } else if (other.tag == "projectile")
        {
            Projectile newp = other.gameObject.GetComponent<Projectile>();
            OnEnemyHit(newp.AttaackStrength);
            Destroy(other.gameObject);
        }
        
    }

    public void OnEnemyHit(int hitpoints)
    {
        print(healthPoints);
        if ((healthPoints - hitpoints) > 0)
        {
            healthPoints -= hitpoints;
            anim.Play("Hurt");
        } else
        {
            anim.SetTrigger("didDie");
            Die();
        }
        
    }

    public void Die()
    {
        isDead = true;
        enemyCollider.enabled = false;
        GameManager.Instance.TotalKilled += 1;
        GameManager.Instance.AddMoney(rewardAmt);
        GameManager.Instance.IsWaveOver();

    }
  
}

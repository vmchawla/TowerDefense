﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackRadius;
    [SerializeField] private Projectile projectile;

    private Enemy targetEnemy = null;
    private float attackCounter;
    private bool isAttacking = false;


    public float TimeBetweenAttacks
    {
        get
        {
            return timeBetweenAttacks;
        }
    }


    void Start () {
		
	}


	
	// Update is called once per frame
	void Update ()
	{
	    attackCounter -= Time.deltaTime;
	    if (targetEnemy == null || targetEnemy.IsDead)
	    {
	        Enemy nearestEnemy = GetNearestEnemyInRange();
	        if (nearestEnemy != null && Vector2.Distance(transform.localPosition, nearestEnemy.transform.localPosition) <=
	            attackRadius)
	        {
	            targetEnemy = nearestEnemy;
	        }
	    } else
        {
            if (attackCounter <= 0)
            {
                isAttacking = true;
                attackCounter = timeBetweenAttacks;
            } else
            {
                isAttacking = false;
            }
            if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
            {
                targetEnemy = null;
            }
        }



    }

    private void FixedUpdate()
    {
        if (isAttacking)
        {
            Attack();
        }
    }

    public void Attack()
    {
        Projectile newProjectile = Instantiate(projectile);
        newProjectile.transform.localPosition = transform.localPosition;
        if (newProjectile.ProjectileType == projectileType.arrow)
        {
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Arrow);
        } else if (newProjectile.ProjectileType == projectileType.fireball)
        {
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Fireball);
        } else
        {
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.Rock);
        }

        if (targetEnemy == null)
        {
            Destroy(newProjectile);
        }
        else
        {
            //move projectile
            StartCoroutine(MoveProjectile(newProjectile));
        }
    }

    IEnumerator MoveProjectile(Projectile projectile)
    {
        
        while (GetTargetDistance(targetEnemy) > 0.2f && projectile != null && targetEnemy != null && !targetEnemy.IsDead)
        {
            var dir = targetEnemy.transform.localPosition - transform.localPosition;
            var andleDirection = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(andleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, 5f * Time.deltaTime);
            yield return null;
        }
        if (projectile != null && targetEnemy == null)
        {
            Destroy(projectile.gameObject);
        }
        if (projectile != null && projectile.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            Destroy(projectile.gameObject);
        }
        if (projectile != null && projectile.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            Destroy(projectile.gameObject);
        }

    }

    private float GetTargetDistance(Enemy thisEnemy = null)
    {
        if (thisEnemy == null)
        {
            thisEnemy = GetNearestEnemyInRange();
            if (thisEnemy == null)
            {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.localPosition, thisEnemy.transform.localPosition));
    }

    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (var enemy in GameManager.Instance.EnemyList) 
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius)
            {
                enemiesInRange.Add(enemy);
            }
        }
        return enemiesInRange;
    }

    private Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        foreach (var enemy in GetEnemiesInRange())
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance)
            {
                smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy; 
    }
}

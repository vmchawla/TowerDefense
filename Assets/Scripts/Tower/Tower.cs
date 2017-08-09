using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackRadius;

    private Projectile projectile;
    private Enemy targetEnemy = null;
    private float attackCounter;


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
	    if (targetEnemy == null)
	    {
	        Enemy nearestEnemy = GetNearestEnemyInRange();
	        if (nearestEnemy != null && Vector2.Distance(transform.position, nearestEnemy.transform.position) <=
	            attackRadius)
	        {
	            targetEnemy = nearestEnemy;
	        }
	    }

	    if (Vector2.Distance(transform.position, targetEnemy.transform.position) > attackRadius)
	    {
	        targetEnemy = null;
	    }
	}

    public void Attack()
    {
        Projectile newProjectile = Instantiate(projectile);

        if (targetEnemy == null)
        {
            Destroy(newProjectile);
        }
        else
        {
            //move projectile
            MoveProjectile(newProjectile);
        }
    }

    IEnumerator MoveProjectile(Projectile projectile)
    {
        while (GetTargetDistance(targetEnemy) > 0.2f && this.projectile !=null && targetEnemy != null)
        {
            var dir = targetEnemy.transform.position - transform.position;
            var andleDirection = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(andleDirection, Vect);
        }
    }

    private float GetTargetDistance(Enemy thisEnemy)
    {
        if (thisEnemy == null)
        {
            thisEnemy = GetNearestEnemyInRange();
            if (thisEnemy == null)
            {
                return 0f;
            }
        }
        return Mathf.Abs(Vector2.Distance(transform.position, thisEnemy.transform.position));
    }

    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (var enemy in GameManager.Instance.EnemyList) 
        {
            if (Vector2.Distance(transform.position, enemy.transform.position) <= attackRadius)
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
            if (Vector2.Distance(transform.position, enemy.transform.position) < smallestDistance)
            {
                smallestDistance = Vector2.Distance(transform.position, enemy.transform.position);
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy; 
    }
}

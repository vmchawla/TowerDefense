using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum projectileType
{
    rock,
    arrow,
    fireball
};

public class Projectile : MonoBehaviour
{

    [SerializeField] private int attackStrength;
    [SerializeField] private projectileType projectileType;

    public int AttaackStrength
    {
        get
        {
            return attackStrength;
        }
    }

    public projectileType ProjectileType
    {
        get
        {
            return projectileType;
        }
    }




    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

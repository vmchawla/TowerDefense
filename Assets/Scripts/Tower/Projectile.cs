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

    [SerializeField] private int attaackStrength;
    [SerializeField] private projectileType projectileType;

    public int AttaackStrength
    {
        get
        {
            return attaackStrength;
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

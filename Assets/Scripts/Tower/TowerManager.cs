﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : Singleton<TowerManager>
{

    private TowerBtn towerBtnPressed;


	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void selectedTower(TowerBtn towerSelected)
    {
        towerBtnPressed = towerSelected;
        print("Pressed: " +towerBtnPressed);
    }

}
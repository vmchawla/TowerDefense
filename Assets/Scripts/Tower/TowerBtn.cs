using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TowerBtn : MonoBehaviour
{

    [SerializeField] private GameObject towerObject;
    [SerializeField] private Sprite dragSprite;

    void Awake()
    {
        Assert.IsNotNull(towerObject);
        Assert.IsNotNull(dragSprite);
    }

    public GameObject TowerObject
    {
        get
        {
            return towerObject;
        }
    }

    public Sprite DragSprite
    {
        get
        {
            return dragSprite;
        }
    }
}

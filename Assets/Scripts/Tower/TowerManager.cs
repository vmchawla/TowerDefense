using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager>
{

    private TowerBtn towerBtnPressed = null;
    private SpriteRenderer spriteRenderer;


	void Start ()
	{

	    spriteRenderer = GetComponent<SpriteRenderer>();

	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetMouseButtonDown(0))
	    {
	        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

	        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
	        Debug.DrawLine(worldPoint, Vector2.zero, Color.blue);
            if (hit.collider.tag == "buildsite")
            {
                hit.collider.tag = "buildsitefull";
                placeTower(hit);
                
            }
        }
	    if (spriteRenderer.enabled)
	    {
	        followMouse();
	    }
	    if (Input.GetMouseButtonDown(1))
	    {
	         deSelectTower();
	    }

    }

    public void placeTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            GameObject newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            disabledDragSprite();
        }

    }

    public void selectedTower(TowerBtn towerSelected)
    {
        towerBtnPressed = towerSelected;
        print("Pressed: " +towerBtnPressed);
        enabledDragSprite(towerBtnPressed.DragSprite);
    }

    public void deSelectTower()
    {
        towerBtnPressed = null;
        print("Tower Btn deselected");
        disabledDragSprite();
    }

    public void followMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void enabledDragSprite(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void disabledDragSprite()
    {
        spriteRenderer.enabled = false;
    }



}

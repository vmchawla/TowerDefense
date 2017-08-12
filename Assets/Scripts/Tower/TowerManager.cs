using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : Singleton<TowerManager>
{

    private TowerBtn towerBtnPressed = null;
    private SpriteRenderer spriteRenderer;
    private List<Tower> TowerList = new List<Tower>();
    private List<Collider2D> BuildList = new List<Collider2D>();
    private Collider2D buildTile;


	void Start ()
	{

	    spriteRenderer = GetComponent<SpriteRenderer>();
        buildTile = GetComponent<Collider2D>();

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
                buildTile = hit.collider;
                buildTile.tag = "buildsitefull";
                RegisterBuildSite(buildTile);
                PlaceTower(hit);
                
            }
        }
	    if (spriteRenderer.enabled)
	    {
	        FollowMouse();
	    }
	    if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Escape))
	    {
	         DeSelectTower();
	    }

    }

    public void RegisterBuildSite(Collider2D buildTag)
    {
        BuildList.Add(buildTag);
    }

    public void RegisterTower(Tower tower)
    {
        TowerList.Add(tower);
    }

    public void RenameTagsBuildSites()
    {
        foreach (var buildTag in BuildList)
        {
            buildTag.tag = "buildsite";
        }
        BuildList.Clear();

    }

    public void DestroyAllTowers()
    {
        foreach (var tower in TowerList)
        {
            Destroy(tower.gameObject);
        }
        TowerList.Clear();
    }

    public void PlaceTower(RaycastHit2D hit)
    {
        if (!EventSystem.current.IsPointerOverGameObject() && towerBtnPressed != null)
        {
            Tower newTower = Instantiate(towerBtnPressed.TowerObject);
            newTower.transform.position = hit.transform.position;
            GameManager.Instance.AudioSource.PlayOneShot(SoundManager.Instance.TowerBuilt);
            BuyTower(towerBtnPressed.TowerPrice);
            RegisterTower(newTower);
            DeSelectTower();
        }
    }

    public void BuyTower(int price)
    {
        GameManager.Instance.SubtractMoney(towerBtnPressed.TowerPrice);
    }

    public void SelectedTower(TowerBtn towerSelected)
    {
        if (towerSelected.TowerPrice <= GameManager.Instance.TotalMoney)
        {
            towerBtnPressed = towerSelected;
            EnabledDragSprite(towerBtnPressed.DragSprite);
        }

    }

    public void DeSelectTower()
    {
        towerBtnPressed = null;
        DisabledDragSprite();
    }

    public void FollowMouse()
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector2(transform.position.x, transform.position.y);
    }

    public void EnabledDragSprite(Sprite sprite)
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = sprite;
    }

    public void DisabledDragSprite()
    {
        spriteRenderer.enabled = false;
    }



}

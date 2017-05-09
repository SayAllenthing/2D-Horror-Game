using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	public Transform sprite;

	public Vector2 LastAngle = Vector2.right;

	bool bIsLocal = false;

	Rigidbody2D rigidbody;

	public float Speed = 250;

	public DynamicLight_Flashlight FlashLight;
	public MuzzleLight MuzzleLight;

	public NetworkPlayer NetPlayer;
	public Inventory inventory;

	public int InventorySpaces = 5;

	// Use this for initialization
	void Start () 
	{
		rigidbody = GetComponent<Rigidbody2D>();

		

		//GameUIManager.Instance.SetInventory(inventory);
	}

	public void InitLocalPlayer()
	{
		bIsLocal = true;

		DebugManager.Instance.Character = transform;
		GameUIManager.Instance.RegisterLocalPlayer(this);

		GameMapData.Instance.AddActor(transform);

		transform.position = new Vector3(2,2,0);
		NetPlayer = GetComponent<NetworkPlayer>();
		inventory = GetComponent<Inventory>();

        inventory.AddItem("SmallLamp", 12);
    }

    public void SetSprite(Sprite s)
    {
        sprite.GetComponent<SpriteRenderer>().sprite = s;
    }
	
	// Update is called once per frame
	void Update () 
	{
		if(bIsLocal)
			HandleInput();

	}

	void HandleInput()
	{
		Vector2 WantMove = Vector2.zero;

		WantMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

		if(WantMove.magnitude > 0.4f)
		{		
			WantMove = WantMove.normalized;
			SetLookDirection(WantMove);
		}

		rigidbody.velocity = WantMove * Speed * Time.deltaTime;

		if(Input.GetButtonDown("Fire1"))
		{
			HandleFire();
			NetPlayer.CmdFire();
		}
        else if(Input.GetButtonDown("Fire2"))
        {
            OnPickObject();
        }

		if(Input.GetKeyDown(KeyCode.F))
		{
			//FlashLight.renderer.enabled = !FlashLight.renderer.enabled;
			inventory.AddItem("SmallLamp", 1);
		}
	}

    public void OnPickObject()
    {
        LayerMask mask = 1 << LayerMask.NameToLayer("Item");

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, mask);

        if (hit.collider != null)
        {
            MapObject mo = hit.collider.GetComponent<MapObject>();
            string item = mo.Data.Name;

			if(mo.Data.Type == ItemData.eItemType.MATERIAL)
			{
				if(inventory.AddMaterial(item, 1) <= 0)
				{
					NetworkHelper.Instance.DestroyObject(hit.collider.gameObject);
				}
				//else
				//Leave scrap on ground
			}
			else if(inventory.AddItem(item, 1))
            {
                NetworkHelper.Instance.DestroyObject(hit.collider.gameObject);
                //Destroy(hit.collider.gameObject);
            }
        }
    }

	public void HandleFire()
	{
		MuzzleLight.Refresh();

        if(bIsLocal)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 5f, Vector2.zero);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Enemy")
                {
                    if (Fire(hits[i].collider.gameObject))
                    {
                        NetworkHelper.Instance.DestroyObject(hits[i].collider.gameObject);
                        break;
                    }
                }
            }
        }		
	}    

	bool Fire(GameObject target)
	{
		Vector3 diff = (target.transform.position - transform.position);
		float dot = Vector3.Dot(diff.normalized, LastAngle);

		float baseChance = dot * 50 - diff.sqrMagnitude;
		float shot = Random.Range(0,100);

		if(baseChance > 0)
		{
			if(shot > (100 - baseChance))
			{
				Debug.Log("Killed " + baseChance + "%" + " " + shot + " diff: " + diff.sqrMagnitude);
				return true;
			}
		}

		Debug.Log("Miss " + baseChance + "%" + " " + shot + " diff: " + diff.sqrMagnitude);
		return false;
	}

	public void SetLookDirection(Vector2 look)
	{
		LastAngle = look;
		SetDirection(look.x);
	}

	void SetDirection(float dir)
	{
		if(dir > 0)
			sprite.transform.localScale = new Vector3(1, 1, 1);
		else if(dir < 0)
			sprite.transform.localScale = new Vector3(-1, 1, 1);
	}

}

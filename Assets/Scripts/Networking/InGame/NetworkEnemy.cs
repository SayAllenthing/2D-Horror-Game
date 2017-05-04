using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkEnemy : NetworkBehaviour
{
    Animator anim;
    Rigidbody2D rigidbody;
    Transform sprite;

    public float speed = 150;
    public GameObject target = null;

    [SyncVar]
	Vector2 SyncPos;

    [SyncVar]
    bool SyncIsRunning = false;

	public override void OnStartClient()
	{
        sprite = transform.GetChild(0);
        anim = sprite.GetComponent<Animator>();

        rigidbody = GetComponent<Rigidbody2D>();        

        GameMapData.Instance.AddActor(transform);
    }


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (isServer)
        {
            SendPosition();

            if (target)
            {
                ChaseTarget();
            }
            else
            {
                LookForTarget();
            }
        }
        else
        {
            LerpPosition();
        }

        anim.SetBool("IsRunning", SyncIsRunning);
    }

	//Position and direction
	void SendPosition()
	{
		CmdUpdatePosition(transform.position);
	}

	[Command]
	void CmdUpdatePosition(Vector3 pos)
	{
		SyncPos = pos;
	}

	void LerpPosition()
	{
		if(!isServer)
		{
            float diff = SyncPos.x - transform.position.x;
            anim.SetBool("IsRunning", diff > 0.025f);

            sprite.localScale = new Vector3(diff > 0 ? -1 : 1, 1, 1);
            transform.position = Vector3.Lerp(transform.position, SyncPos, 5* Time.deltaTime);
		}
	}

    void ChaseTarget()
    {
        GameMapData.Instance.FindPath(transform.position, target.transform.position);

        if (GameMapData.Instance.ThePath != null)
        {
            Node n = GameMapData.Instance.ThePath[0];

            Vector3 wantMove = (n.Position - transform.position).normalized;
            rigidbody.velocity = new Vector2(wantMove.x, wantMove.y) * speed * Time.deltaTime;

            SyncIsRunning = rigidbody.velocity.magnitude > 0.5f;

            sprite.localScale = new Vector3(rigidbody.velocity.x > 0 ? -1 : 1, 1, 1);
        }
    }

    void LookForTarget()
    {
        target = GameObject.Find("NetworkPlayer(Clone)");
    }

    [Command]
	public void CmdFire()
	{
		RpcFire();
	}

	[ClientRpc]
	public void RpcFire()
	{
		
	}
}

using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	Animator anim;
	Rigidbody2D rigidbody;
	Transform sprite;


	public float speed = 150;
	public GameObject target = null;

	// Use this for initialization
	void Start () 
	{
		sprite = transform.GetChild(0);
		anim = sprite.GetComponent<Animator>();

		rigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(target)
		{
			ChaseTarget();
		}
		else
		{
			LookForTarget();
		}
	}

	void ChaseTarget()
	{
		Vector3 wantMove = (target.transform.position - transform.position).normalized;
		rigidbody.velocity = new Vector2(wantMove.x, wantMove.y) * speed * Time.deltaTime;

		anim.SetBool("IsRunning", rigidbody.velocity.magnitude > 0.5f);

		sprite.localScale = new Vector3(rigidbody.velocity.x > 0 ? -1 : 1, 1, 1);
	}

	void LookForTarget()
	{
		target = GameObject.Find("NetworkPlayer(Clone)");

	}
}

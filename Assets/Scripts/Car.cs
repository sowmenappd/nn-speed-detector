using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour {

	Transform target;

	public float minSpeed;
	public float maxSpeed;

	[HideInInspector] public float speed;
	[HideInInspector] public float direction;
	[HideInInspector] public float speeding;
	[HideInInspector] public bool tracked;

	// Use this for initialization

	void Awake()
	{
		speed = Random.Range(minSpeed, maxSpeed);//FindObjectOfType<Spawner>().GenerateSpeed();
	}
	void Start () {
		tracked = false;
		direction = 1;
		speeding = (speed >= (minSpeed + maxSpeed) /2f ) ? 1f : -1f;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
		if(direction == -1f)
		{
			transform.GetComponent<SpriteRenderer>().flipX = true;
		}
	}

}

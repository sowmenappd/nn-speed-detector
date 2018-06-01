using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrier : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D obj)
	{
		Car c = obj.GetComponent<Car>();
		if(c != null)
		{
			gameObject.SetActive(false);
			c.direction = -1f;
			Destroy(c.gameObject, .75f);
		}
	}
}

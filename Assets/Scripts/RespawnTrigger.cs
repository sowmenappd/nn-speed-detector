using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D _object)
	{
		Car detectedCar = _object.GetComponent<Car>(); 
		if(detectedCar != null)
		{	
			Destroy(_object.gameObject);
		}
	}
}

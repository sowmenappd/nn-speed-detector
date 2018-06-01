using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

	public Car car;
	Transform spawn;
	public TextMesh actualSpeedText;

	public float spawnInterval;
	float nextSpawnTime;

	public int carsSpawned = 0;
	// Use this for initialization
	void Start () {
		spawn = transform;
		nextSpawnTime = spawnInterval;
	}
	
	// Update is called once per frame
	void Update () {
		Car temp = FindObjectOfType<Car>();
		if(Time.time > nextSpawnTime)
		{
			nextSpawnTime = Time.time + spawnInterval;
			Instantiate(car, spawn.position, spawn.rotation);
			carsSpawned++;
		}
		if(temp != null)
			actualSpeedText.text = temp.speed.ToString() + ((temp.speeding >= 0) ? " Speeding" : " Not speeding");
	}


}

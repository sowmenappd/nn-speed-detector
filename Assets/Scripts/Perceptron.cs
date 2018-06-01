using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron : MonoBehaviour {

	public Transform cam, target;
	public GameObject barrier;
	public TextMesh speedText;
	public Vector3 offset;
	public LineRenderer line;
	bool isTracking;
	public static float baseError;
	public int totalGuess = 0;
	
	Car car;

	public static float minSpeed = 15f;
	public static float maxSpeed = 80f;

	public Brain brain;

	#region GameFunctions
	// Use this for initialization
	void Start () {
		cam = GetComponent<Transform>();
		brain = new Brain();
		brain.weight0 = Random.Range(-1f, 1f);
		print("Initial weight0: " + brain.weight0);		
		barrier.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
			
		//this is the tracking section
		if(target == null)
		{
			car = null;
			if(FindObjectOfType<Car>() != null)
			{
				target = FindObjectOfType<Car>().GetComponent<Transform>();
				car = target.GetComponent<Car>();
				isTracking = true;
				StartCoroutine(TrackCar());
			}		
		}
		else if(isTracking) // because the tracking is done for only a short interval
		{
			SpeedData s = new SpeedData(car.speed); // this constructs a speed data with a correctly adjusted speeding factor 
			brain.TrainGuess(s, s.speedingFactor);	// using this correct speed data to train the perceptron			
			float guess;
			guess = brain.guessIfSpeeding(car.speed);
			print(brain.weight0);


			if(guess >= 0f)
			{
				line.SetColors(Color.red, Color.clear);
				speedText.text = "Speeding";
				barrier.SetActive(true);
			}
			else if(guess < 0f)
			{
				line.SetColors(Color.green, Color.clear);
				speedText.text = "Not Speeding";
				barrier.SetActive(false);
			}


			if(!car.tracked)
			{
				totalGuess++;
				car.tracked = true;
				//print("Camera guessed: " + guess + "\nCorrect guess is: " + s.speedingFactor);

				if(guess == s.speedingFactor)
				{
					brain.correctGuess++;
				}
				
				
			}

			Vector3[] positions = new Vector3[2];
			positions[0] = transform.position + offset;
			positions[1] = target.position;
			line.SetPositions(positions);
			line.gameObject.SetActive(true);
		}
		else
		{
			line.gameObject.SetActive(false);
			//provide information whether the car is speeding or not						
		}
		
	}

	IEnumerator TrackCar()
	{
		car.tracked = true;
		yield return new WaitForSeconds(.5f);
		isTracking = false;
	}

	#endregion

	#region NeuralNetwork

	[System.Serializable]
	public class Brain
	{
		public int correctGuess;
		public float weight0;
		public float learningRate = 0.00001f; // learning to be accurate 1 / 10^3 <-- default I set to 
		

		public Brain()
		{
			correctGuess = 0;
		}

		//return +1 if speeding and -1 if not
		public int guessIfSpeeding(float x)
		{	
			float sum = x * weight0;
			int result = Sign(sum);
			return result;
		}

		//the supervised learning algorithm occurs in this TrainGuess() func
		// this function does : 
		// the perceptron takes a guess for the given speedData, depending on whether 
		//the guess was correct, the weight for the perceptron is corrected
		// and it also returns the guess
		public void TrainGuess(SpeedData speedData, int correctValue)
		{
			int guess = guessIfSpeeding(speedData.speed);
			int error = correctValue - guess;
			weight0 += speedData.speed * error * learningRate; //adjust weight for the necessary correction					
		}

		//return 1 or -1 if positive / negative
		public static int Sign(float a)
		{
			return (a >= 0f) ? 1 : -1;
		}

	}
	#endregion

	#region KnownData
	public class SpeedData
	{
		public float speed;
		public int speedingFactor;

		public SpeedData()
		{
			speed = Random.Range(minSpeed, maxSpeed);

			if(speed >= (minSpeed + maxSpeed) / 2f)
			{
				speedingFactor = 1;
			}
			else
			{
				speedingFactor = -1;
			}
		}

		public SpeedData(float speed)
		{
			this.speed = speed;
			if(speed >= (minSpeed + maxSpeed) / 2f)
			{
				speedingFactor = 1;
			}
			else
			{
				speedingFactor = -1;
			}
		}
	}

	#endregion
}
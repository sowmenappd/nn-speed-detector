using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeDisplay : MonoBehaviour {

	Text timeText;
	public Text totalguessDisplay;
	public Text efficiencyDisplay;
	Perceptron brain;

	// Use this for initialization
	void Start () {
		timeText = GetComponent<Text>();
		brain = FindObjectOfType<Perceptron>();
	}
	
	// Update is called once per frame
	void Update () {
		timeText.text = Time.time.ToString();
		totalguessDisplay.text = "Total guessed: "+ brain.totalGuess.ToString();
		efficiencyDisplay.text = "Efficiency: " + (brain.brain.correctGuess * 100f / brain.totalGuess).ToString();
	}
}

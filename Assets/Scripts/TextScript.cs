using UnityEngine;
using System.Collections;

public class TextScript : MonoBehaviour {

	private GameObject scoreText;
	private GameObject completeText;
	private GameObject attentionText;
	private GameObject blinkText;
	private int score;
	private Vector2 NeuroNum;

	public Font goodDog;

	// Use this for initialization
	void Start () {
		score = 0;
		NeuroNum = new Vector2 (0, 0);
		initText (ref attentionText);
		initText (ref blinkText);
		initText (ref scoreText);
		initText (ref completeText);
	}
	
	// Update is called once per frame
	void Update () {

		attentionText.guiText.text = "attention: " + NeuroNum.x.ToString();
		attentionText.guiText.transform.position = new Vector3(0.05f, 1f, 0);
		blinkText.guiText.text = "blink: " + NeuroNum.y.ToString();
		blinkText.guiText.transform.position = new Vector3(0.4f, 1f, 0);
		scoreText.guiText.text = "score: " + score.ToString();
		scoreText.guiText.transform.position = new Vector3(0.8f, 1f, 0);

	}

	void initText(ref GameObject gO) {
		gO = new GameObject ();
		gO.AddComponent("GUIText");
		gO.guiText.font = goodDog;
		gO.guiText.fontSize = 30;
		gO.guiText.color = new Color(255, 0, 0);
		
	}

	public void AlertText(string action)
	{
		completeText = new GameObject();
		completeText.AddComponent("GUIText");
		completeText.guiText.font = goodDog;
		completeText.guiText.fontSize = 50;
		completeText.guiText.color = new Color(255, 0, 0);
		
		if (action == "complete")
		{
			completeText.guiText.text = "Level Complete!";
			completeText.guiText.transform.position = new Vector3(0.24f, 0.88f, 0);
		} 
		else
		{
			completeText.guiText.text = "Game Over";
			completeText.guiText.transform.position = new Vector3(0.36f, 0.88f, 0);		
		}

	}

	public int Score
	{
		set 
		{
			score = value;
		}
	}
	
	public Vector2 NeuroskyNum
	{
		set 
		{
			NeuroNum = value;
		}
	}
}

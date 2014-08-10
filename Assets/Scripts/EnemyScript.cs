using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour
{
    public Vector3 moveSpeed;
    public AudioClip hitSound;
    public GameObject alertBridge;

	float myTimer;
   
	// Use this for initialization
    void Start()
    {
		//Time to wait before updating
		myTimer = 4; 
    }
    
    // Update is called once per frame
    void Update()
    {
		if (myTimer > 0) 
		{
			myTimer -= Time.deltaTime;
		} else 
		{
			transform.position += moveSpeed;
		}
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Hero")
		{
			AudioSource.PlayClipAtPoint(hitSound, transform.position);
			Destroy(other.gameObject.collider2D); //Remove collider to avoid audio replaying
			other.gameObject.renderer.enabled = false; //Make object invisible
			//Destroy(other.gameObject, 0.626f); //Destroy object when audio is done playing, destroying it before will cause the audio to stop
			alertBridge.GetComponent<HeroScript>().AlertAction("gameover");
		}
	}
}

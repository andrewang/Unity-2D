using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Vector3 moveSpeed;
    public AudioClip hitSound;
    public GameObject alertBridge;

	float myTimer;
    // Use this for initialization
    void Start()
    {
		myTimer = 5; 
    }
    
	IEnumerator Example() {
		yield return new WaitForSeconds(5);

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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            AudioSource.PlayClipAtPoint(hitSound, transform.position);
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Hero")
        {
            other.gameObject.audio.Play(); //Play audio
            Destroy(other.gameObject.collider2D); //Remove collider to avoid audio replaying
            other.gameObject.renderer.enabled = false; //Make object invisible
            Destroy(other.gameObject, 0.626f); //Destroy object when audio is done playing, destroying it before will cause the audio to stop
            alertBridge.GetComponent<MoveRight>().Alert("gameover");
        }
    }
}
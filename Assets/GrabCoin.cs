using UnityEngine;
using System.Collections;

public class GrabCoin : MonoBehaviour
{   
    void OnTriggerEnter2D(Collider2D other)
	{
		MoveRight hero = other.gameObject.GetComponent<MoveRight>();
        if (other.gameObject.name == "Hero")
        {
			hero.score++;
            audio.Play();
            Destroy(gameObject.collider2D);
            gameObject.renderer.enabled = false;
            Destroy(gameObject, 0.47f);
        }
    }
}
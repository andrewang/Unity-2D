using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour
{   
    void OnTriggerEnter2D(Collider2D other)
	{
        if (other.gameObject.name == "Hero")
        {
			other.gameObject.GetComponent<HeroScript>().Score = 1 ;
            audio.Play();
            Destroy(gameObject.collider2D);
            gameObject.renderer.enabled = false;
            Destroy(gameObject, 0.47f);
        }
    }
}
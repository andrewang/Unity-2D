using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour
{
	public NeuroskyScript NeuroskyInstance;
	public TextScript TextInstance;
	public float jumpForce;
    public Vector3 moveSpeed = new Vector3();
	public AudioClip completeSound;
	
	private bool moving = false;
    private GameObject[] scenes;
    private GameObject bg;
	private bool ended = false;
	private int score;
	private float preBlink;
	private float jumpTimer=0;

	// Use this for initialization
    void Start()
    {	
        scenes = GameObject.FindGameObjectsWithTag("Moveable");
		bg = GameObject.Find("Background2");
		score = 0;
		preBlink = 0;
    }
    
    // Update is called once per frame
    void Update()
    {
			TextInstance.NeuroskyNum = new Vector2(NeuroskyInstance.Attention, NeuroskyInstance.Blink);
			TextInstance.Score = this.score;
		 	
			//if (NeuroskyInstance.Attention > 40)
			if(Input.GetKeyDown("right"))
            {
                CheckMove("began");
            }
			
			//if(NeuroskyInstance.Attention <= 40 )
			if(Input.GetKeyUp("right"))
            {
                CheckMove( "ended");
            }
            
			jumpTimer = jumpTimer+1;
			//if(NeuroskyInstance.Blink != preBlink)
			if(Input.GetKeyDown("space"))
            {
				if (jumpTimer>50)
				{
                	CheckJump("began");
					jumpTimer=0;
				}
            }
			preBlink = NeuroskyInstance.Blink;
        
        // Move if button is pressed && stage is not over
        if (moving && bg.transform.position.x > -7.5f)
        {
            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes [i] != null)
                {
                    scenes [i].transform.position -= moveSpeed;
                }
            }
        }

        // Stage Completed
        if (bg.transform.position.x <= -7.5f && ended == false)
        {
			//bg.transform.position = new Vector3(0,bg.transform.position.y,0);
			//floor.transform.position = new Vector3(0,floor.transform.position.y,0);        
			AlertAction("complete");     
        }
    }

    void CheckMove(string phase)
    {         
        if (phase == "began")
        {
            moving = true;
        }
                
        if (phase == "ended")
        {
           moving = false;
        }
    }

    void CheckJump(string phase)
	{
        
        if (phase == "began")
        {
            gameObject.rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            audio.Play();
        }
    }

    public void AlertAction(string action)
    {
        ended = true;
        if (action == "complete") 
		{
			AudioSource.PlayClipAtPoint (completeSound, transform.position);
			TextInstance.AlertText ("complete");
		} else 
		{
			TextInstance.AlertText ("gameover");
		}

		NeuroskyInstance.Disconnect ();
        bg.GetComponent<AudioSource>().Stop();      
		Invoke("restart", 2);
    }

	public int Score
	{
		set 
		{
			score += value;
		}
	}


    void restart()
    {   
        Application.LoadLevel(Application.loadedLevel);
    }	
}
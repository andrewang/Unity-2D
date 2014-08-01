using UnityEngine;
using System.Collections;

public class MoveRight : MonoBehaviour
{public float jumpForce;
    private GameObject hero;
    public Vector3 moveSpeed = new Vector3();
    private bool moving = false;
    private GameObject[] scene;
    private GameObject bg;
    public AudioClip completeSound;
    private GameObject[] buttons;
	private GameObject scoreText;
	private GameObject completeText;
    private GameObject attentionText;
    private GameObject blinkText;
    private bool ended = false;
    public Font goodDog;

	public int score;

    public s_ReadNeuro readNeuroInstance;

    private float preBlink=0;
    // Use this for initialization
	void initText(ref GameObject gO) {
		gO = new GameObject ();
		gO.AddComponent("GUIText");
		gO.guiText.font = goodDog;
		gO.guiText.fontSize = 50;
		gO.guiText.color = new Color(255, 0, 0);

		}
    void Start()
    {	
		score = 0;
		hero = GameObject.Find("Hero");
        scene = GameObject.FindGameObjectsWithTag("Moveable");
        bg = GameObject.Find("Background");
        buttons = GameObject.FindGameObjectsWithTag("Buttons");

		initText (ref attentionText);
		initText (ref blinkText);
		initText (ref scoreText);
		initText (ref completeText);

    }
    
    // Update is called once per frame
    void Update()
    {
        var attention = readNeuroInstance.attention;
        var blink = readNeuroInstance.blink;

        attentionText.guiText.text = "attention: " + attention.ToString();
        attentionText.guiText.transform.position = new Vector3(0.05f, 1f, 0);

        blinkText.guiText.text = "blink: " + blink.ToString();
        blinkText.guiText.transform.position = new Vector3(0.4f, 1f, 0);

		scoreText.guiText.text = "score: " + score.ToString();
		scoreText.guiText.transform.position = new Vector3(0.8f, 1f, 0);

        
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            //if (attention > 35)
			if(Input.GetKeyDown("right"))
            {
                CheckMove(Input.mousePosition, "began");
            }

            //if (attention < 35)
			if(Input.GetKeyUp("right"))
            {
                CheckMove(Input.mousePosition, "ended");
            }
            //if (blink!=preBlink)
			if(Input.GetKeyDown("space"))
            {
                CheckJump(Input.mousePosition, "began");
            }
            
            //if (blink==preBlink)
			if(Input.GetKeyUp("space"))
            {
                CheckJump(Input.mousePosition, "ended");
            }

            preBlink=blink;
        }
        
        // Move if button is pressed && stage is not over
        if (moving && bg.transform.position.x > -4.8f)
        {
            for (int i = 0; i < scene.Length; i++)
            {
                if (scene [i] != null)
                {
                    scene [i].transform.position -= moveSpeed;
                }
            }
        }

        // Stage Completed
        if (bg.transform.position.x <= -4.8f && ended == false)
        {
            Alert("complete");
         
        }
    }

    void CheckMove(Vector3 pos, string phase)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);
            
        if (phase == "began")
        {
            moving = true;
        }
                
        if ( phase == "ended")
        {
           moving = false;
        }
    }
    void CheckJump(Vector3 pos, string phase)
    {
        Vector3 wp = Camera.main.ScreenToWorldPoint(pos);
        Vector2 touchPos = new Vector2(wp.x, wp.y);
        Collider2D hit = Physics2D.OverlapPoint(touchPos);
        
        if (phase == "began")
        {
            hero.rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            audio.Play();
        }
    }


    public void Alert(string action)
    {
        ended = true;

        completeText = new GameObject();
        completeText.AddComponent("GUIText");
        completeText.guiText.font = goodDog;
        completeText.guiText.fontSize = 50;
        completeText.guiText.color = new Color(255, 0, 0);
        
        if (action == "complete")
        {
            AudioSource.PlayClipAtPoint(completeSound, transform.position);

            completeText.guiText.text = "Level Complete!";
            completeText.guiText.transform.position = new Vector3(0.24f, 0.88f, 0);
            

        } else
        {
            completeText.guiText.text = "Game Over";
            completeText.guiText.transform.position = new Vector3(0.36f, 0.88f, 0);
        }
		ThinkGear.TG_Disconnect(readNeuroInstance.tgHandleId);
        bg.GetComponent<AudioSource>().Stop();
        
        for(int i = 0; i < buttons.Length; i++)
        {
//            buttons[i].renderer.enabled = false;
            Invoke("restart", 10);
        }
    }

    void restart()
    {   
        Application.LoadLevel(Application.loadedLevel);
    }
}
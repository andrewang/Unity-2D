using UnityEngine;
using System.Collections;

public class NeuroskyScript : MonoBehaviour {
	
	public int tgHandleId;
	public int tgConnectionStatus;
	public int tgPacketCount;
	public float attention;
	public float blink;
	
	// Use this for initialization
	void Start () {
		setupNeuro();

	}
	
	// Update is called once per frame
	void Update () {
		readNeuro();
	}
		
	void setupNeuro() {
		tgHandleId = ThinkGear.TG_GetNewConnectionId();
		
		tgConnectionStatus = ThinkGear.TG_Connect(tgHandleId,
		                                          "\\\\.\\COM3", 
		                                          ThinkGear.BAUD_9600, 
		                                          ThinkGear.STREAM_PACKETS);
		ThinkGear.TG_EnableBlinkDetection (tgHandleId, 1);
		
	}

	void readNeuro() {
		tgPacketCount = ThinkGear.TG_ReadPackets(tgHandleId, -1);
	
		attention = ThinkGear.TG_GetValue (tgHandleId, ThinkGear.DATA_ATTENTION);
		blink = ThinkGear.TG_GetValue (tgHandleId, ThinkGear.DATA_BLINK_STRENGTH);
	
	}

	public float Attention
	{
		get
		{
			return attention;
		}
	}

	public float Blink
	{
		get
		{
			return blink;
		}
	}


	public void Disconnect()
	{
		ThinkGear.TG_Disconnect(tgHandleId);
	}

	/*void OnGUI () {
		// Make a background box
		//GUI.Box(new Rect(10,10,100,90), "Loader Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,80,20), "Disconnect")) {
			ThinkGear.TG_Disconnect(tgHandleId);
			//ThinkGear.TG_FreeConnection(tgHandleId);
		}
	}*/
}
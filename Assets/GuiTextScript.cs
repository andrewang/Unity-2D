using UnityEngine;
using System.Collections;

public class GuiTextScript : MonoBehaviour {

    public s_ReadNeuro readNeuroInstance;

    void Start() {

    }
    
    void Update() {
        var attention = readNeuroInstance.attention;
        
        // do something with it.
        this.gameObject.guiText.text = attention.ToString ();
    }
    
    /*void OnGUI () {
        // Make a background box
        //GUI.Box(new Rect(10,10,100,90), "Loader Menu");
        
        // Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
        if(GUI.Button(new Rect(20,40,80,20), "Disconnect")) {
            ThinkGear.TG_Disconnect(readNeuroInstance.tgHandleId);
            //ThinkGear.TG_FreeConnection(tgHandleId);
        }
    }*/
}


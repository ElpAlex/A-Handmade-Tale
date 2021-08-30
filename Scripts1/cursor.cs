using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour {

    public Texture2D cursorTexture;
    public Texture2D invCursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale==0)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
        else if (Time.timeScale == 1)
        {
            Cursor.SetCursor(invCursorTexture, hotSpot, cursorMode);
        }
	}
}

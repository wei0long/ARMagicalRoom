using UnityEngine;
using System.Collections;

public class MyUI : MonoBehaviour {
	public Material bed3;
    Color targetColor;
    float changeColorspeed = 0.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        bed3.color = Color.Lerp(bed3.color,targetColor,changeColorspeed*Time.deltaTime);
	}
	public void ButtondownRed()
	{
        //bed3.color = Color.red;
        targetColor = Color.red;
	}
	public void ButtondownBlue()
	{
        //bed3.color = Color.blue;
        targetColor = Color.blue;
	}
    public  void print()
    {
      
    Debug.Log(GameObject.Find("ImageTarget").transform.position);
    }
}



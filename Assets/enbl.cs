using UnityEngine;
using System.Collections;

public class enbl : MonoBehaviour {
    float alphaval = 0.0f;
    float tmp = 0.0f;
    bool isfadingin = false;
    public GameObject spinningWheel;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isfadingin)
        {
            if (GetComponent<CanvasGroup>().alpha < 1)
            {
                GetComponent<CanvasGroup>().alpha += tmp * Time.deltaTime;
                isfadingin = false;
            }
        }

    }

    void OnEnable()
    {
        isfadingin = true;
       // spinningWheel.SetActive(false);


    }
}

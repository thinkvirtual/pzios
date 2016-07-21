using UnityEngine;
using System.Collections;

public class fakewater : MonoBehaviour {
    public float uvAnimationRate = 2.0f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    Vector2 uvOffset = Vector2.zero;
    void LateUpdate()
    {
        uvOffset =new Vector2(Mathf.PingPong(Time.time/2, uvAnimationRate), Mathf.PingPong(Time.time/2, uvAnimationRate));


        GetComponent<Renderer>().sharedMaterial.mainTextureOffset = uvOffset;
       // if (GetComponent<Renderer>().enabled)
       // {
       //    GetComponent<Renderer>().materials[0].SetTextureOffset("Water-01", uvOffset);
       // }
    }
}

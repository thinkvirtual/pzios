using UnityEngine;
using System.Collections;

public class animcontrol : MonoBehaviour {
    public AnimationClip jumpup;
    public AnimationClip failure;
    public AnimationClip success;

    public Animation animctrl;
    // Use this for initialization
    void Start () {
        animctrl = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(Camera.main.transform);
	}

    public void Success()
    {
        animctrl.Play("Success Owl");
        Invoke("Idle", 1f);

    }

    public void Fail()
    {
       // animctrl.Play("Failure Owl");
       // Invoke("Idle", 1f);

    }

    public void Idle()
    {
        animctrl.Play("Idle Owl");
        

    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using HutongGames.PlayMaker;

public class selectorlogic : MonoBehaviour {
    public TextMesh disp;
    public Rigidbody wheel;
    public Text playerpoints;
    public Text wheelResultDisplay;
    public GameObject solvekeyboard;
    public GameObject spinbutton;
    public static bool isspinning = false;
    public Collider currentCollider;
    public Text earneddisp;
    public float magtimer = 0.0f;
    public Collider lastPeg;
    public Collider fs;
    bool hasstopped = false;
    public TextMesh ispin;
    public AudioClip pinsound;
    public float dmp;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (isspinning)
        {
            
            
            wheel.GetComponent<ConstantForce>().torque = wheel.GetComponent<ConstantForce>().torque - new Vector3(0,0 , dmp * Time.deltaTime);
            //if (wheel.GetComponent<ConstantForce>().torque.y < 10)
          //  {
           //     wheel.GetComponent<ConstantForce>().torque = Vector3.zero;

         //   }
            if (wheel.angularVelocity.magnitude < .28)
            {
                hasstopped = true;
            }
            if (hasstopped) { 



                disp.text = "staying:" + currentCollider.transform.name;
                string transname = currentCollider.transform.name;
                wheelResultDisplay.text = transname;

                disp.text = "result:" + currentCollider.transform.name;
                if (transname.Contains("vowel"))
                {
                    PlayMakerFSM.BroadcastEvent("freevowel");

                }
                else if (transname.Contains("hint"))
                {
                    PlayMakerFSM.BroadcastEvent("freehint");

                }
                else
                {
                    earneddisp.text = currentCollider.transform.name + " Points!";
                    FsmVariables.GlobalVariables.GetFsmString("spinresulttext").Value = transname + " Points";
                    int pt = int.Parse(currentCollider.transform.name);

                    FsmVariables.GlobalVariables.GetFsmString("finalearned").Value = currentCollider.transform.name;

                   FsmVariables.GlobalVariables.GetFsmInt("pointsifcorrect").Value= pt;
                    playerpoints.text = FsmVariables.GlobalVariables.GetFsmInt("playerpoints").Value.ToString() + " Points";
                    FsmVariables.GlobalVariables.GetFsmString("spinresulttext").Value = transname + " Points";
                    Invoke("defferedEnable", .5f);

                }
                spinbutton.SetActive(false);
                isspinning = false;
                wheel.angularVelocity = new Vector3(0, 0, 0);
                hasstopped = false;

            }
        }
        
    }


   public void defferedEnable()
    {
        solvekeyboard.SetActive(true);

    }

    public void OnTriggerEnter(Collider d)
    {
       

        if (d.isTrigger)
        {
            currentCollider = d;




        }
    }
    public void delayedEnable()
    {

    }

    public void OnCollisionEnter(Collision c)
    {
        

        if (c.collider.transform.name.Contains("Pin"))
        {
            if (isspinning) { 
            GetComponent<AudioSource>().PlayOneShot(pinsound);
            }
        }
        fs = c.collider;
        if (isspinning) {


                if (wheel.angularVelocity.magnitude < .1f)
        {
                wheel.angularDrag = 2f;
            if (c.collider != lastPeg)
            {
                    lastPeg = c.collider;
                    //hasstopped = true;
            }
        }
        }

    }
    public void OnTriggerStay(Collider d)
    {

        currentCollider = d;
        /*
        magtimer += Time.deltaTime;
        if (magtimer > 1f)
        {
            if (wheel.angularVelocity.magnitude < .05f)
            {
                hasstopped = true;
                magtimer = 0.0f;
            }
        }*/
    }
}

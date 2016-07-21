using UnityEngine;
using System.Collections;

public class popupwindow : MonoBehaviour {
    RectTransform mytrans;
    public AudioClip popins;
    public AudioClip popouts;
    AudioSource myaudio;
    public bool soundplayed = false;

    // Use this for initialization
    void Start() {
        mytrans = GetComponent<RectTransform>();
        myaudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update() {
        Vector3 relativePos = Camera.main.transform.position - transform.position;
        Quaternion rt = Quaternion.LookRotation(relativePos*-1);
        transform.rotation = rt;

    }

    float elapsedTime = 0;
    public bool hideme = false;

    public void hideit()
    {
        elapsedTime = 0;
        hideme = true;
        soundplayed = false;
    }

    public void showitW()
    {
        elapsedTime = 0;

        soundplayed = false;

        hideme = false;
    }
    void LateUpdate()
    {
        bool RPressed = false;
        bool BPressed = false;

        if (Input.GetKeyDown(KeyCode.R))
        {
            hideme = true;
            soundplayed = false;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            soundplayed = false;
            hideme = false;
        }
        if (hideme)
        {
            if (mytrans.localScale != new Vector3(0, 0, 0)) {
                elapsedTime += Time.deltaTime;
                GetComponent<RectTransform>().localScale = Vector3.Lerp(new Vector3(mytrans.localScale.x, mytrans.localScale.y, 0), new Vector3(0, 0, 0), elapsedTime / 2);
            }
            else
            {
                elapsedTime = 0.0f;
            }

            if (hideme && !soundplayed)
            {
                myaudio.PlayOneShot(popouts);
                soundplayed = true;
            }
            else if (!hideme && !soundplayed)
            {
                myaudio.PlayOneShot(popins);
                soundplayed = true;

            }
        }
        else if (mytrans.localScale != new Vector3(1f, 1f, 1f))
        {
            Vector3 tmp = new Vector3(.9f, .91f, .9f);

            elapsedTime += Time.deltaTime;
            GetComponent<RectTransform>().localScale = Vector3.Lerp(new Vector3(mytrans.localScale.x, mytrans.localScale.y, 0), new Vector3(1f, 1f, 1f), elapsedTime / 2);

            if (hideme && !soundplayed)
            {
                myaudio.PlayOneShot(popouts);
                soundplayed = true;
            }
            else if (!hideme && !soundplayed)
            {
                myaudio.PlayOneShot(popins);
                soundplayed = true;

            }
        }
    
        else
        {
            if (hideme && !soundplayed)
            {
                myaudio.PlayOneShot(popouts);
                soundplayed = true;
            }
            else if (!hideme && !soundplayed)
            {
                myaudio.PlayOneShot(popins);
                soundplayed = true;

            }

        
            
            elapsedTime = 0.0f;
        }

    }
}

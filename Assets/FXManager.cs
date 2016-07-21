using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FXManager : MonoBehaviour {
    public Transform coconutdrop;
    public GameObject coconutPrefab;
        public GameObject letterHighlightPrefab;
    public List<GameObject> letterSuccessParticles=new List<GameObject>();
    // Use this for initialization
    void Start () {
        Transform[] tmp = GetComponentsInChildren<Transform>();
        for(int i = 1; i < 20; i++)
        {
            GameObject al = Instantiate(letterHighlightPrefab) as GameObject;
            al.SetActive(false);
            letterSuccessParticles.Add(al);
            //letterSuccessParticles.Add(tmp[i].gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TriggerLetterLevelEffect()
    {

        
    }

    public void dropapple()
    {
       // Instantiate(coconutPrefab, coconutdrop.position, Quaternion.identity);


    }



    public void AddHighLightEffect(Vector3 ltrpos)
    {
        if (letterHighlightPrefab != null)
        {
            letterSuccessParticles[0].transform.position = ltrpos;
            letterSuccessParticles[0].SetActive(true);
            //Instantiate(letterHighlightPrefab, ltrpos, Quaternion.identity);
            // foreach(Transform t in WordMaker.fxpositions)
            //      {

            //    }
        }
    }

                
}

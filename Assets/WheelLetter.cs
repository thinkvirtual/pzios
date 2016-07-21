using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using GazeInput;
public class WheelLetter : MonoBehaviour
{
    public GameObject wheelGameMan;
    public VREventSystem vreventsys;
    public bool stickingout = false;
    public Color originalColor;
    RectTransform myTransform;
    public Sprite x;
    // Use this for initialization
    void Start () {

        wheelGameMan=GameObject.FindGameObjectWithTag("WheelGameManager");
        myTransform = GetComponent<RectTransform>();
        vreventsys = Camera.main.transform.GetComponent<VREventSystem>();
        this.GetComponentInChildren<Text>().text = transform.name;
        originalColor = GetComponent<Image>().color;
    }





    // Update is called once per frame
    void Update () {

        if (vreventsys.CurrentInteractible == null)
        {

        }
        else if ((!vreventsys.CurrentInteractible.transform.name.Contains(transform.name)))
        {


        }
        else if(vreventsys.CurrentInteractible.transform.name.Equals(transform.name))
        {


        }
    }
}

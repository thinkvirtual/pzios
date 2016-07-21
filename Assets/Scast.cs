using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class Scast : MonoBehaviour, IPointerEnterHandler
{
    private PointerEventData eventSystem;
    public Camera camera;
    public bool msd = false;

    public void OnPointerEnter(PointerEventData eventData)
    {

        eventSystem = eventData;

    }

    void Start()
    {

        camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            msd = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            msd = false;
        }
        if (msd) { 
        RaycastHit hit;
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            Debug.Log(objectHit.name);
                //if (objectHit != null) {
                objectHit.SendMessage("fakeClick");

               // }

                // Do something with the object that was hit by the raycast.
            }

        }
        
    }
}
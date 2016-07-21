using UnityEngine;
using System.Collections;

public class WheelTorque : MonoBehaviour {

    public Rigidbody wheel;
    public TextMesh speeddisp;
    private bool launched;
    private Vector3 defaultPosition;
    private Quaternion defaultRotation;
    public GameObject spinbutton;

    public float minspeed = -1000f;
    public float maxspeed = -300f;

    public void Start()
    {
        spinbutton = GameObject.FindGameObjectWithTag("spinbutton");
        transform.root.transform.gameObject.SetActive(false);
    }
    public void Awake()
    {
        defaultPosition = wheel.transform.position;
        defaultRotation = wheel.transform.rotation;
    }

    public void Update()
    {
        if (speeddisp != null)
        {
            speeddisp.text = wheel.angularVelocity.magnitude.ToString();
        }
        /*
        if (Input.GetMouseButtonDown(0)||Input.GetKeyDown(KeyCode.C))
        {
            if (Hidden())
            {
                DisplayWheel();
                return;
            }

            if (!launched)
            {
                Spin();
            }
            else
            {
                InitializeWheel();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (Hidden())
            {
                DisplayWheel();
            }
            else
            {
                Hide();
                InitializeWheel();
            }
        }
        */
    }

    bool Hidden()
    {
        return Camera.main.cullingMask == 0;
    }

    public void DisplayWheel()
    {

    }

    public void Hide()
    {
        Camera.main.cullingMask = 0;

    }

    public void Spin()
    {
        spinbutton.SetActive(false);


       wheel.GetComponent<ConstantForce>().torque = new Vector3(0, 0, -800);
        //wheel.AddTorque(0f, ApplyTorque(), 0f);
      // wheel.AddTorque(0f, 0f, -800f);

        selectorlogic.isspinning = true;

        launched = true;
    }

    public void InitializeWheel()
    {
        wheel.transform.position = defaultPosition;
        wheel.angularDrag = 1f;
        wheel.angularVelocity = Vector3.zero;
        launched = false;
    }

    float ApplyTorque()
    {
        return Random.Range(minspeed, maxspeed);
    }
}

using UnityEngine;// Applies an explosion force to all nearby rigidbodies
public class expl : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public GameObject ef;
    void Start()
    {

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            //ef.SetActive(true);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

            }
        }
    }
}
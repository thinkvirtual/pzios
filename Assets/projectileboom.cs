using UnityEngine;// Applies an explosion force to all nearby rigidbodies
public class projectileboom : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public GameObject ef;

    bool hasExploded = false;


    void OnCollisionEnter(Collision c)
    {
        if (!hasExploded)
        {
            if (!c.collider.name.Contains("Capsule"))
            {
               // hasExploded = true;
                Vector3 explosionPos = transform.position;
                Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
                //ef.SetActive(true);
                foreach (Collider hit in colliders)
                {
                    if (!hit.name.Contains("Capsule")) {
                      //  GetComponent<Rigidbody>().isKinematic = true;
                    Rigidbody rb = hit.GetComponent<Rigidbody>();

                    if (rb != null)
                        rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                    }
                }


            }
        }
    }
}
using UnityEngine;

public class GunRaycast : MonoBehaviour
{
    public float distance = 100f;
    public string targetTag = "bom";
    public ParticleSystem explosionEffect;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            Debug.Log("Vurulan: " + hit.collider.name);

            if (hit.collider.CompareTag(targetTag))
            {
                Instantiate(explosionEffect, hit.point, Quaternion.identity);

                Destroy(hit.collider.gameObject);
            }
        }
    }
}
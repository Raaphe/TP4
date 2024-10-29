using UnityEngine;


public class Raycast : MonoBehaviour
{
    void Update()
    {
        FireRay();
    }

    void FireRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData))
        {
            Debug.Log($"Item: {hitData.collider.name} Distance: {hitData.distance}");
        }
    }
    
}
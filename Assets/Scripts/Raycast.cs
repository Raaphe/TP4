using UnityEngine;


public class Raycast : MonoBehaviour
{
    // Déclaration d'un rayon qui sera utilisé pour détecter les obstacles devant le robot
    Ray rayon;

    // Déclaration d'une variable pour stocker les informations de collision lorsque le rayon touche un objet
    RaycastHit hit;

    // Variables sérialisées pour assigner les capteurs gauche et droit du robot dans l'éditeur Unity
    [SerializeField] Transform leftShoulder, rightShoulder;

    void Update()
    {
        rayon = new Ray(leftShoulder.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(rayon, out hit, Mathf.Infinity))
        {
            Debug.Log("Head Sensor Objet:" + hit.collider.name + " Distance:" + hit.distance);
        }

        Debug.DrawRay(leftShoulder.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);

        rayon = new Ray(rightShoulder.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(rayon, out hit, Mathf.Infinity))
        {
            Debug.Log("Foot Sensor Objet:" + hit.collider.name + " Distance:" + hit.distance);
        }

        Debug.DrawRay(rightShoulder.position, transform.TransformDirection(Vector3.forward) * 10f, Color.yellow);
    }
    
}
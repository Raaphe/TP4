using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class PlayerController : MonoBehaviour
{
    private float speed = 3.0f;  // Vitesse de déplacement vers l'avant/arrière
 
    private float turnSpeed = 60.0f;
 
    public float strafeSpeed = 5.0f;  // Vitesse de déplacement latéral (gauche/droite)
    private float horizontalInput;
    private float forwardInput;
 
    // Start is called before the first frame update
    void Start()
    {
        // Vous pouvez initialiser des variables ici si nécessaire
    }
 
    // Update is called once per frame
    private void FixedUpdate() {
         {
        // Obtenir l'entrée utilisateur
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
 
        // Déplacer l'objet vers l'avant/en arrière en fonction de l'entrée verticale
        transform.Translate(Vector3.forward * Time.fixedDeltaTime * speed * forwardInput);
 
        // Déplacer l'objet latéralement (gauche/droite) en fonction de l'entrée horizontale
        // transform.Translate(Vector3.right * Time.deltaTime * strafeSpeed * horizontalInput);
 
          // Rotates the car based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.fixedDeltaTime);
    }
}
 
}
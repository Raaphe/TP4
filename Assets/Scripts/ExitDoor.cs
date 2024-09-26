using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class ExitDoor : MonoBehaviour
{
   // public bool CanOpen = false;
    Animator animator;

    void Start(){
       // GetComponent<Animator>().enabled = true;
        animator = this.GetComponent<Animator>();
        animator.SetBool("CanClose", true);
    }
    private void OnTriggerEnter(Collider other)
    {
        // Vérifiez si l'objet qui est entré dans le trigger a le tag "Player"
        if ((other.CompareTag("Player")))
        {
            animator.SetBool("CanClose", false);
            // Exécutez l'action souhaitée, par exemple afficher un message
            Debug.Log("Le joueur est entré dans la zone de trigger!");
 
            // Vous pouvez également appeler une méthode ou changer l'état du jeu
            // Exemple : augmenter un score, terminer un niveau, etc.
            // IncreaseScore();
            // EndLevel();
        }
        }
}
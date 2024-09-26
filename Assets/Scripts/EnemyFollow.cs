using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;        // Référence au joueur
    public float speed = 0.05f;      // Vitesse de déplacement vers le joueur
    public const float activationDistance = 2.0f;  // Distance d'activation de l'ennemi
    private Animator animator;      // Référence à l'Animator

    Vector3 direction;

    void Start()
    {
        // Si le joueur n'est pas assigné manuellement dans l'inspecteur, trouvez-le automatiquement
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Récupérer le composant Animator
        animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.enabled = false;  // Désactiver l'Animator au début
        }
    }

    void Update()
    {
        // Vérifier que le joueur est assigné et existe toujours
        if (player != null && animator != null)
        {

            // Calculer la distance entre l'ennemi et le joueur
            float distance = Vector3.Distance(transform.position, player.position);
            // Si le joueur est dans la distance d'activation, activer l'animation
            if (distance <= activationDistance)
            {

                if (!animator.enabled)
                {
                    animator.enabled = true;  // Activer l'animation
                    Debug.Log("Le joueur s'approche. Activation de l'ennemi  1.");
                }
                // // Calculer la direction vers le joueur
                direction = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;

                 Debug.Log("Le joueur s'approche. Activation de l'ennemi."+ direction);

                // // Déplacer l'ennemi vers le joueur
                transform.position += (direction /5) * speed * Time.deltaTime;

                // Faire face au joueur
                transform.LookAt(player);
            }
            else
            {
                if (animator.enabled)
                {
                    animator.enabled = false;  // Désactiver l'animation si le joueur est trop loin
                    Debug.Log("Le joueur est trop loin. Désactivation de l'ennemi.");
                      // // Calculer la direction vers le joueur

                // // Déplacer l'ennemi vers le joueur
                transform.position += (direction /5) * speed * Time.deltaTime;
                }
            }
        }
    }
}

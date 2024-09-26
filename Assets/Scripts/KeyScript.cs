using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    public List<AudioClip> listAudioClip;

    public AudioSource audioSource;

    public static int keysInPocket = 0;

    public int totalKeysToGet = 3;
    Animator animator;


    void Start(){
        GameObject gameObject = GameObject.FindGameObjectWithTag("DoorFinish");
        
            animator = gameObject.GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            keysInPocket++;
            
            // GameObject.FindGameObjectWithTag("KeyAudio").GetComponent<AudioSource>().PlayOneShot(listAudioClip[Random.Range(0, listAudioClip.Count)]);
            Debug.Log("Vous avez trouvé " + keysInPocket + "clés.  Il vous en faut " + totalKeysToGet);



            if (keysInPocket >= totalKeysToGet)
            {
                animator.SetBool("CanOpen", true);
                //GameObject.Find("ExitDoor").GetComponent<ExitDoor>().CanOpen=true;

                Debug.Log("Vous avez toutes les clés, Canopen true");
            }
            Destroy(gameObject);
        }

    }
}
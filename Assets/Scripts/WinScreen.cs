using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{

    [SerializeField]
    private GameObject character;
    [SerializeField]
    public GameObject winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == character)
        {

            if (winScreen != null)
            {
                Time.timeScale = 0f;
                winScreen.SetActive(true);

                GameObject gameCanvas = GameObject.Find("Canvas");
                if (gameCanvas != null)
                {
                    gameCanvas.SetActive(false);
                }
                else
                {
                    Debug.LogError("Main game canvas not found!");
                }
            }
        }
    }
}

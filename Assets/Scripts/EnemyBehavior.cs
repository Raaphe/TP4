using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    [SerializeField]
    private Slider slider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == character)
        {
            float healthDecrease = slider.maxValue * 0.05f; // 5% of max health
            float newHealth = Mathf.Clamp(slider.value - healthDecrease, 0, slider.maxValue);
            slider.value = newHealth;
        }
    }
}

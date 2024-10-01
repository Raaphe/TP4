using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ConsumableType
{
    public enum Type { Health, Energy, Hunger };
    public Type selectedType;
}

public class Consumables : MonoBehaviour
{
    [SerializeField]
    private ConsumableType consumableType = new ConsumableType();
    [SerializeField]
    public GameObject character;
    public float consumableValue = 20;

    private void OnTriggerEnter(Collider other)
    {
        Slider bar;

        if (other.gameObject == character)
        {
            gameObject.SetActive(false);

            switch (consumableType.selectedType)
            {
                case ConsumableType.Type.Health:
                    bar = GameObject.Find("Health").GetComponent<Slider>();
                    break;
                case ConsumableType.Type.Energy:
                    bar = GameObject.Find("Energy").GetComponent<Slider>();
                    break;
                case ConsumableType.Type.Hunger:
                    bar = GameObject.Find("Hunger").GetComponent<Slider>();
                    break;
                default:
                    return;
            }

            if (bar != null && bar.value + consumableValue > 100)
            {
                bar.value = 100;
                return;
            }

            if (bar != null)
            {
                bar.value += consumableValue;
            }
        }
    }
}

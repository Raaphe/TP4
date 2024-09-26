using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BarType
{
    public enum Type { Health, Energy, Hunger };
    public Type selectedType;
}

public class BarManager : MonoBehaviour
{
    [SerializeField]
    private BarType barType = new BarType();
    public Slider slider;
    public Text textComponent;
    public GameObject gameOverCanvas;

    private float duration = 90f;
    Animator claireAnimator;

    private void Start()
    {
        if (slider == null || textComponent == null)
        {
            Debug.LogError("Slider or Text component not assigned");
            return;
        }

        slider.maxValue = 100; 
        slider.value = 100; 
        UpdateText(slider.value);
        claireAnimator = GetComponent<Animator>();
        if (barType.selectedType == BarType.Type.Health)
        {
            return;
        }
        else if(barType.selectedType == BarType.Type.Hunger) 
        {
            duration = 200f;
        }
        slider.onValueChanged.AddListener(UpdateText);
        StartCoroutine(DepleteHealth());

        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    private void UpdateText(float value)
    {
        float percentage = Mathf.Clamp01(value / slider.maxValue);
        textComponent.text = string.Format("{0:P0}", percentage);
    }

    public void UpdateTextFromSlider()
    {
        UpdateText(slider.value); 
    }

    public void SetSliderFromText(string newText)
    {
        if (!string.IsNullOrEmpty(newText))
        {
            try
            {
                float parsedValue = float.Parse(newText);
                parsedValue = Mathf.Clamp(parsedValue, 0f, slider.maxValue);
                slider.value = parsedValue;
                UpdateText(slider.value);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error parsing text value: {e.Message}");
            }
        }
    }

    private void EndGame()
    {
        Debug.Log("Game Over!");


        Time.timeScale = 0f;
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);
            claireAnimator.SetTrigger("dead");

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



    private IEnumerator DepleteHealth()
    {
        float initialValue = slider.value;
        float timePassed = 0f;

        while (timePassed < duration && slider.value > 0f)
        {
            timePassed += Time.deltaTime;
            slider.value = Mathf.Lerp(initialValue, 0f, timePassed / duration);
            UpdateText(slider.value);
            yield return null; 
        }

        slider.value = 0; 
        UpdateText(slider.value);
        Debug.Log("Health depleted!");
    }

    public void SetHealth(float health)
    {
        StartCoroutine(SmoothHealthTransition(health));
    }

    private IEnumerator SmoothHealthTransition(float targetHealth)
    {
        while (Mathf.Abs(slider.value - targetHealth) > 0.01f)
        {
            slider.value = Mathf.Lerp(slider.value, targetHealth, Time.deltaTime * 5);
            UpdateText(slider.value);
            yield return null;
        }
        slider.value = targetHealth;
        UpdateText(slider.value); 
        Canvas.ForceUpdateCanvases();
    }

    void Update()
    {
        SetHealth(slider.value);
        var healthSlider = GameObject.Find("Health").GetComponent<Slider>();
        var energySlider = GameObject.Find("Energy").GetComponent<Slider>();
        var hungerSlider = GameObject.Find("Hunger").GetComponent<Slider>();

        if ((energySlider.value <= 0 || hungerSlider.value <= 0) && this.barType.selectedType == BarType.Type.Health)
        {
            duration = 35f;
            StartCoroutine(DepleteHealth());
        }

        if (barType.selectedType == BarType.Type.Health && slider.value <= 0)
        {
            EndGame();
        }
    }
}

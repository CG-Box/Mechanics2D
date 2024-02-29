using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ProgressBar : MonoBehaviour
{
    public float fillTime = 3f;  // Время заполнения полоски
    private bool isFilling = false;
    private float fillTimer = 0f;
    private IEnumerator coroutine;
    private UnityEngine.UI.Image progressBar;

    [SerializeField]private TextMeshProUGUI progressText;

    [Header("Unity Events")]
    public UnityEvent OnComplete;
    public UnityEvent OnFail;

    void CompleteRaise()
	{
		if (OnComplete != null)
			OnComplete.Invoke();
	}
    void FailRaise()
    {
		if (OnFail != null)
			OnFail.Invoke();
    }

    void Start()
    {
        progressBar = GetComponent<UnityEngine.UI.Image>();
        progressBar.fillAmount = 0f;  // Начальное значение нулевое
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isFilling)
        {
            isFilling = true;  // Начинаем заполнение
            coroutine = FillProgressBar();
            StartCoroutine(coroutine);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && isFilling)
        {
            EndTimer();
            StopCoroutine(coroutine);
            FailRaise();
        }
    }

    void EndTimer()
    {
        isFilling = false;  // Остановить заполнение
        fillTimer = 0f;  // Сбросить таймер
        progressBar.fillAmount = 0f;  // Сбросить полоску
    }

    IEnumerator FillProgressBar()
    {
        while (fillTimer < fillTime)
        {
            fillTimer += Time.deltaTime;
            float progress = fillTimer / fillTime;
            progressBar.fillAmount = progress;  // Заполнение полоски по мере времени
            progressText.text = progress.ToString();
            yield return null;
        }
        EndTimer();
        CompleteRaise();
    }
}
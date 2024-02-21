using System.Collections;
using TMPro;
using UnityEngine;

public class InfoCanvasController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    readonly float shortDelay = 3f;

    protected Coroutine m_DeactivationCoroutine;

    protected readonly int m_HashActivePara = Animator.StringToHash ("Active");

    IEnumerator SetAnimatorParameterWithDelay (float delay)
    {
        yield return new WaitForSeconds (delay);
        animator.SetBool(m_HashActivePara, false);
    }

    public void ActivateCanvasWithText (string text)
    {
        if (m_DeactivationCoroutine != null)
        {
            StopCoroutine (m_DeactivationCoroutine);
            m_DeactivationCoroutine = null;
        }

        gameObject.SetActive (true);
        animator.SetBool (m_HashActivePara, true);
        textMeshProUGUI.text = text;
    }

    public void ActivateCanvasWithTranslatedText (string phraseKey)
    {
        if (m_DeactivationCoroutine != null)
        {
            StopCoroutine(m_DeactivationCoroutine);
            m_DeactivationCoroutine = null;
        }

        gameObject.SetActive(true);
        animator.SetBool(m_HashActivePara, true);
        textMeshProUGUI.text = Translator.Instance[phraseKey];
    }

    public void DeactivateCanvasWithDelay (float delay)
    {
        m_DeactivationCoroutine = StartCoroutine (SetAnimatorParameterWithDelay (delay));
    }

    public void ShortAcivateWithText(string text)
    {
        ActivateCanvasWithText(text);
        DeactivateCanvasWithDelay(shortDelay);
    }
    public void ShortAcivateWithTranslatedText(string phraseKey)
    {
        ActivateCanvasWithTranslatedText(phraseKey);
        DeactivateCanvasWithDelay(shortDelay);
    }
}
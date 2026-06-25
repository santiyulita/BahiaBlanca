using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup fadePanel;
    [SerializeField] private GameObject interactText;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text continueText;
    [SerializeField] private PlayerController _player;
    [SerializeField] private GameObject itemMessageText;
    [SerializeField] private TMPro.TextMeshProUGUI itemMessageTMP;
    [SerializeField] private CanvasGroup sickOverlay;
    private float baseAlpha = 0.12f;
    public float typingSpeed = 0.05f;
    private List<string> _lines;
    private int _currentLine = 0;
    private bool _isDialogueActive = false;
    private bool _justClosedDialogue = false;
    private bool _isTyping = false;
    private string _currentFullLine;
    public bool JustClosedDialogue => _justClosedDialogue;

    public bool IsDialogueActive => _isDialogueActive;

    void Start()
    {
        interactText.SetActive(true);
        interactText.SetActive(false);
        sickOverlay.alpha = baseAlpha;

        dialogueBox.SetActive(true);
        dialogueBox.SetActive(false);
    }
    private void Update()
    {
        if (!_player.IsSick) return;

        if (sickOverlay.alpha > 0.01f)
        {
            sickOverlay.alpha = baseAlpha + Mathf.Sin(Time.time * 4f) * 0.05f;
        }
    }
    public void ShowDialogue(List<string> lines)
    {
        interactText.SetActive(false);

        _lines = lines;
        _currentLine = 0;
        _isDialogueActive = true;

        dialogueBox.SetActive(true);
        continueText.gameObject.SetActive(true);
        _player.DisableMovement();
        if (itemMessageCoroutine != null)
        {
            StopCoroutine(itemMessageCoroutine);
        }

        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        StopAllCoroutines();
        StartCoroutine(TypeLine(_lines[_currentLine]));
    }
    IEnumerator TypeLine(string line)
    {
        _isTyping = true;
        _currentFullLine = line;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        _isTyping = false;
    }
    public void AdvanceDialogue()
    {
        if (!_isDialogueActive) return;

        if (_isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = _currentFullLine;
            _isTyping = false;
            return;
        }
        NextLine();
    }

    public void NextLine()
    {
        if (!_isDialogueActive) return;

        _currentLine++;

        if (_currentLine >= _lines.Count)
        {
            EndDialogue();
        }
        else
        {
            ShowCurrentLine();
        }
    }

    void EndDialogue()
    {
        dialogueBox.SetActive(false);
        _isDialogueActive = false;
        _player.EnableMovement();
        _justClosedDialogue = true;
        Invoke(nameof(ResetCloseFlag), 0.5f);
    }
    void ResetCloseFlag()
    {
        _justClosedDialogue = false;
    }
    public void ShowInteract()
    {
        interactText.SetActive(true);
    }

    public void HideInteract()
    {
        if (interactText != null)
        {
            interactText.SetActive(false);
        }
    }
    public IEnumerator FadeFromBlack(float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            fadePanel.alpha = 1f - (time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        fadePanel.alpha = 0f;
    }
    private Coroutine itemMessageCoroutine;

    public void ShowItemMessage(string message)
    {
        if (itemMessageCoroutine != null)
        {
            StopCoroutine(itemMessageCoroutine);
            itemMessageCoroutine = null;
        }

        itemMessageCoroutine = StartCoroutine(ShowItemMessageRoutine(message));
    }

    IEnumerator ShowItemMessageRoutine(string message)
    {
        Debug.Log("USANDO OBJETO: " + itemMessageText.name);

        itemMessageTMP.text = message;
        itemMessageText.SetActive(true);

        yield return new WaitForSeconds(2f);

        Debug.Log("INTENTANDO APAGAR: " + itemMessageText.name);

        itemMessageText.SetActive(false);
    }
    public void RemoveSickEffect()
    {
        StartCoroutine(RemoveSickEffectRoutine());
    }

    IEnumerator RemoveSickEffectRoutine()
    {

        float duration = 2f;
        float time = 0f;

        float startAlpha = sickOverlay.alpha;

        while (time < duration)
        {
            sickOverlay.alpha = Mathf.Lerp(startAlpha, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        sickOverlay.alpha = 0f;

    }
    public void DisableSickEffect()
    {
        sickOverlay.alpha = 0f;
    }
    public void ForceHideItemMessage()
    {
        itemMessageText.SetActive(false);
    }
}
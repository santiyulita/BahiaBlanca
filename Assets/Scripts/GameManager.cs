using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CanvasManager _canvasManager;
    [SerializeField] private PlayerController _player;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        StartCoroutine(IntroSequence());
    }

    IEnumerator IntroSequence()
    {
        _player.DisableMovement();

        yield return StartCoroutine(
     _canvasManager.FadeFromBlack(3f)
     );

        yield return new WaitForSeconds(1f);

        _canvasManager.typingSpeed = 0.1f;

        _canvasManager.ShowDialogue(new List<string>
    {
        "....",
        "¿D-dónde estoy?",
        "Siento que mi cabeza está por explotar…"
    });
    }
}
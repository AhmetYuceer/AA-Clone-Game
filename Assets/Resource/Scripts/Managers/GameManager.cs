using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public bool isPlay;

    [SerializeField] private TextMeshProUGUI levelText;

    //UI
    [SerializeField] private GameObject MainCircle;
    [SerializeField] private GameObject circleParent;

    [SerializeField] private Image gameOverPanel;
    [SerializeField] private Color targetColor;
    [SerializeField] private Color targetColor2;
    [SerializeField] private Color targetColor3;
    [SerializeField] private float duration;
    
    [SerializeField] private Vector3 mainCircleScale;
    [SerializeField] private TextMeshProUGUI mainCircleText;


    private AudioSource audioSource;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip positiveClip;
    [SerializeField] private AudioClip negativeClip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        MainCircle.transform.localScale = Vector3.one * 13;
        StartCoroutine(StartGame());
    }

    public void HitSoundPlay()
    {
        audioSource.PlayOneShot(hitClip);
    }
    private void NegativeSoundPlay()
    {
        audioSource.PlayOneShot(negativeClip);
    }
    private void PositivePlay()
    {
        audioSource.PlayOneShot(positiveClip);
    }

    public IEnumerator GameOver()
    {
        isPlay = false;
        NegativeSoundPlay();

        gameOverPanel.DOColor(targetColor, duration)
          .From(Color.white)
          .SetEase(Ease.Linear);

        gameOverPanel.DOColor(targetColor2, duration)
          .From(targetColor)
          .SetEase(Ease.Linear);

        circleParent.transform.DOMoveY(-100, duration);

        mainCircleText.text = "";
        MainCircle.transform.DOScale(mainCircleScale, duration);

        yield return new WaitForSeconds(duration);

        StartCoroutine(StartGame());
    }
 
    private IEnumerator StartGame()
    {
        List<GameObject> circles = CircleController.Instance.circles;
        List<GameObject> circles2 = CircleController.Instance.circlesInRotatableObjectParent;

        foreach (var item in circles)
        {
            Destroy(item);
        }
        foreach (var item in circles2)
        {
            Destroy(item);
        }

        CircleController.Instance.circles.Clear();
        CircleController.Instance.circlesInRotatableObjectParent.Clear();

        MainCircle.transform.DOScale(Vector3.one, duration);
        circleParent.transform.DOMoveY(-77, duration);
        LevelManager.Instance.FirstLevel();
        levelText.text = LevelManager.currentLevel.ToString();
        
        yield return new WaitForSeconds(duration);

        isPlay = true;
    }

    public IEnumerator NextLevel()
    {
        PositivePlay();

        gameOverPanel.DOColor(targetColor3, duration)
         .From(Color.white)
         .SetEase(Ease.Linear);

        circleParent.transform.DOMoveY(-100, duration);

        CircleController.Instance.SetMove(false);
        List<GameObject> circles = CircleController.Instance.circles;
        List<GameObject> circles2 = CircleController.Instance.circlesInRotatableObjectParent;

        foreach (var item in circles)
        {
            Destroy(item);
        }
        foreach (var item in circles2)
        {
            Destroy(item);
        }

        CircleController.Instance.circles.Clear();
        CircleController.Instance.circlesInRotatableObjectParent.Clear();
       
        mainCircleText.text = "";
        
        MainCircle.transform.DOScale(13, duration);
        circleParent.transform.DOMoveY(-100, duration);

        yield return new WaitForSeconds(duration);

        gameOverPanel.DOColor(targetColor2, duration)
       .From(targetColor3)
       .SetEase(Ease.Linear);

        MainCircle.transform.DOScale(Vector3.one, duration);
        circleParent.transform.DOMoveY(-77, duration);

        LevelManager.Instance.NextLevel();
        levelText.text = LevelManager.currentLevel.ToString();
        isPlay = true;
    }

    public IEnumerator FinishGame()
    {
        isPlay = false;
        PositivePlay();
        gameOverPanel.DOColor(targetColor3, duration)
          .From(Color.white)
          .SetEase(Ease.Linear);

        circleParent.transform.DOMoveY(-100, duration);

        mainCircleText.text = "";
        MainCircle.transform.DOScale(mainCircleScale, duration);

        yield return new WaitForSeconds(5);

        gameOverPanel.DOColor(targetColor2, duration)
         .From(targetColor3)
         .SetEase(Ease.Linear);

        StartCoroutine(StartGame());
    }
}
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    private const int minLevel = 1;
    private const int maxLevel = 50;
    private const int circleCount = 6;
    private int numberOfCirclesConnectedToTheMainCircle = 3;
    
    public static int currentLevel;

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
        currentLevel = minLevel;
    }

    public void FirstLevel()
    {
        currentLevel = minLevel;
        numberOfCirclesConnectedToTheMainCircle = 3;
        SpawnManager.Instance.CreatingCirclesConnectedToMasterCircle(numberOfCirclesConnectedToTheMainCircle);
        SpawnManager.Instance.SpawnCircle(circleCount);
    }

    public void NextLevel()
    {
        if (currentLevel < maxLevel)
        {
            numberOfCirclesConnectedToTheMainCircle += 1;
            currentLevel += 1;
            SpawnManager.Instance.CreatingCirclesConnectedToMasterCircle(numberOfCirclesConnectedToTheMainCircle);
            SpawnManager.Instance.SpawnCircle(circleCount);
        }
        else
        {
            StartCoroutine(GameManager.Instance.FinishGame());
        }
    }
}
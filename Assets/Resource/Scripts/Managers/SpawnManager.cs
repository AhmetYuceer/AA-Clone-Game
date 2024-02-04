using System.Collections.Generic;
using UnityEngine; 

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;    
    private static List<GameObject> circles = new List<GameObject>();

    [SerializeField] private GameObject mainCircle; 
    [SerializeField] private GameObject circlePrefab; 
    [SerializeField] private GameObject circlesParent;
    [SerializeField] private GameObject rotatableObjectParent;

    int radius = 50;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(Instance);
    }
 
    public static List<GameObject> GetCirclesList()
    {
        return circles;
    }

    public void CreatingCirclesConnectedToMasterCircle(int spawnCount)
    {
        GameObject circleClone;
        Circle circle;
        for (int i = 0; i < spawnCount; i++)
        {
            float angle = i * Mathf.PI * 2 / spawnCount;
         
            circleClone = Instantiate(circlePrefab, rotatableObjectParent.transform);
            circle = circleClone.GetComponent<Circle>();
            circle.SetNumber(0);
            circle.LineEnable();
            circleClone.transform.position = rotatableObjectParent.transform.position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;

            Vector3 direction = rotatableObjectParent.transform.position - circleClone.transform.position; 
            float lineAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            circleClone.transform.rotation = Quaternion.AngleAxis(lineAngle - 90, Vector3.forward);

            CircleController.Instance.AddCirclesInRotatableObjectParent(circleClone);
        }
    }

    public void SpawnCircle(int spawnCount)
    {
        Circle circle;
        GameObject circleClone;
        float posY = 0f;

        for (int i = spawnCount; i > 0; i--)
        {
            circleClone = Instantiate(circlePrefab, circlesParent.transform);
            circle = circleClone.GetComponent<Circle>();
            circle.SetNumber(i);
            circles.Add(circleClone);
            circleClone.transform.localPosition = new Vector3(0, posY);
            posY -= 14f;
        }
    }
}
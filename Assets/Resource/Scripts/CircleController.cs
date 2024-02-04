using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{
    public static CircleController Instance;

    public List<GameObject> circles;
    public List<GameObject> circlesInRotatableObjectParent;
    
    [SerializeField] GameObject rotatableObjectParent;
    [SerializeField] Vector2 targetCirclePos;
    [SerializeField] float moveSpeed;

    private bool isMove;
    private int circleIndex = 0;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(Instance);
    }

    private void Update()
    {
        if (GameManager.Instance.isPlay)
        {
            SetList();
            CircleThrow();
        }
    }

    public void AddCirclesInRotatableObjectParent(GameObject circle)
    {
        circlesInRotatableObjectParent.Add(circle);
    }

    private void SetList()
    {
        circles = SpawnManager.GetCirclesList();
    }

    public void SetMove(bool status)
    {
        isMove = status;
        circleIndex = 0;
    }

    private void CircleThrow()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        { 
            if (circles.Count > circleIndex)
            {
                isMove = true;
            }
           
        }
        else if(isMove)
        {
            if (Vector2.Distance(circles[circleIndex].transform.position, targetCirclePos) >= 0.01f)
            {
                circles[circleIndex].transform.position = Vector3.MoveTowards(circles[circleIndex].transform.position, targetCirclePos, moveSpeed);
            }
            else
            {
                GameManager.Instance.HitSoundPlay();

                for (int i = circleIndex; i < circles.Count; i++)
                {
                    var pos = circles[i].transform.position;
                    pos.y += 14;
                    circles[i].transform.position = pos;
                }
                circles[circleIndex].transform.SetParent(rotatableObjectParent.transform);
                
                circlesInRotatableObjectParent.Add(circles[circleIndex]);
                
                circles[circleIndex].transform.position = targetCirclePos;
                circles[circleIndex].GetComponent<Circle>().LineEnable();
                circles[circleIndex].GetComponent<Circle>().SetNumber(0);
                circleIndex++;
                isMove = false;

                if (circles.Count == circleIndex)
                {
                    StartCoroutine(GameManager.Instance.NextLevel());
                }
            }
        }
    }
}
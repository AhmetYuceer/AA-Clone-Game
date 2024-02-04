using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    void Update()
    {
        if (GameManager.Instance.isPlay)
        {
            if (LevelManager.currentLevel %2 == 0)
                transform.Rotate(0, 0, 90 * Time.deltaTime);
            else
                transform.Rotate(0, 0, -90 * Time.deltaTime);
          
        }
    }
}

using TMPro;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] private GameObject line;
    [SerializeField] private TextMeshProUGUI _numberText;
    private int _number;
    public void LineEnable()
    {
        line.SetActive(true);
    }

    public void SetNumber(int number)
    {
        if (number == 0)
        {
            _numberText.text = "";
            return;
        }
        _number = number;
        _numberText.text = _number.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("circle") && GameManager.Instance.isPlay)
        {
            StartCoroutine(GameManager.Instance.GameOver());
            CircleController.Instance.SetMove(false);
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _resultText;
	[SerializeField] private Button _restartBtn;
	private void OnEnable()
	{
		_restartBtn.onClick.AddListener(Restart);
	}

	private void OnDisable()
	{
		_restartBtn.onClick.RemoveAllListeners();

	}

	public void Show(bool isWin)
    {
        _resultText.text = isWin ? "WIN" : "LOSE";
        this.gameObject.SetActive(true);
	}

    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

	public void Restart()
	{
		GameManager.Instance.Restart();
	}
}

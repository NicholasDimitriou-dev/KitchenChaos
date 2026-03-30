using TMPro;
using UnityEngine;

public class GameSatrtCountdownUI : MonoBehaviour
{


    [SerializeField] private TextMeshProUGUI countdownText;

    
    private int previousCountdownNumber;

    

    private void Start() {
        GameHandler.Instance.OnStateChanged += GameHandler_OnStateChanged;
        Hide();
    }

    private void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameHandler.Instance.GetCountdownToStartTimer());
        countdownText.text = countdownNumber.ToString();
    }

    private void GameHandler_OnStateChanged(object sender, System.EventArgs e) {
        if (GameHandler.Instance.IsCountdownToStartActive()) {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}


using UnityEngine;
using UnityEngine.UI;

public class GamPlayCountdown : MonoBehaviour
{

    [SerializeField] private Image timerImage;


    private void Update()
    {
        timerImage.fillAmount = GameHandler.Instance.GetGamePlayingTimerNormalized();
    }
}
    

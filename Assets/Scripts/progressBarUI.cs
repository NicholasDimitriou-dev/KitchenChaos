using System;
using UnityEngine;
using UnityEngine.UI;
public class progressBarUI : MonoBehaviour
{
    [SerializeField] private Image barImage;
    [SerializeField] private GameObject hasProgresGameObject;
    private IHasProgress hasProgress;

    public void Start()
    {
        hasProgress = hasProgresGameObject.GetComponent<IHasProgress>();
        if (hasProgress == null)
        {
            Debug.LogError("has no progressbar");
        }
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
        barImage.fillAmount = 0f;
        hide();
    }
    

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgessChangedEventArgs e)
    {
        barImage.fillAmount = e.progressNormalized;
        if (barImage.fillAmount == 0f || barImage.fillAmount == 1f)
        {
            hide();
        }
        else
        {
            show();
        }
    }

    private void show()
    {
        gameObject.SetActive(true);
    }

    private void hide()
    {
        gameObject.SetActive(false);
    }
    
}

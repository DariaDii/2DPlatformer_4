using UnityEngine;
using UnityEngine.UI;

public class PlayerUserInterface : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Health _health;
    [SerializeField] private Vampirism _vampirism;
    [SerializeField] private Text _textValueCoins;
    [SerializeField] private Image _screenOfDeath;
    [SerializeField] private Text _timer;
    [SerializeField] private Text _reloadTimer;
    [SerializeField] private Text _reloadText;

    private void OnEnable()
    {
        _wallet.ValueChanged += ChangedValue;
        _health.Death += ShowDeathSceen;
        _vampirism.ChangeTimerValue += ChangedTimerValue;
        _vampirism.ChangeReloadValue += ChangedReloadValue;
    }

    private void OnDisable()
    {
        _wallet.ValueChanged -= ChangedValue;
        _health.Death -= ShowDeathSceen;
        _vampirism.ChangeTimerValue -= ChangedTimerValue;
        _vampirism.ChangeReloadValue -= ChangedReloadValue;
    }

    private void ChangedValue(float currentCoinAmount)
    {
        _textValueCoins.text = currentCoinAmount.ToString();
    }

    private void ShowDeathSceen()
    {
        _screenOfDeath.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void ChangedTimerValue(float remainingTime)
    {
        if (remainingTime > 0.2)
        {
            int roundedTime = Mathf.CeilToInt(remainingTime);
            _timer.text = roundedTime.ToString();
            _timer.gameObject.SetActive(true);
        }
        else
        {
            _timer.gameObject.SetActive(false);
        }
    }

    private void ChangedReloadValue(float remainingTime)
    {
        if (remainingTime > 0.2)
        {
            int roundedTime = Mathf.CeilToInt(remainingTime);
            _reloadTimer.text = roundedTime.ToString();
            _reloadTimer.gameObject.SetActive(true);
            _reloadText.gameObject.SetActive(true);
        }
        else
        {
            _reloadTimer.gameObject.SetActive(false);
            _reloadText.gameObject.SetActive(false);
        }
    }
}
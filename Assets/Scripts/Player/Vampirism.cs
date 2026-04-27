using System;
using System.Collections;
using UnityEngine;

public class Vampirism : MonoBehaviour
{
    [SerializeField] private VampirismZone _vampirismZone;
    [SerializeField] private float _damagePerSecond = 10f;
    [SerializeField] private float _healPerDamage = 1f;

    private float _abilityOperatingTime = 6f;
    private float _reloadTime = 4f;
    private bool _isReload = true;
    private bool _isActive = false;

    public event Action<float> ChangeTimerValue;
    public event Action<float> ChangeReloadValue;
    public event Action TimerStarted;
    public event Action ReloadStarted;

    public void StartAbility()
    {
        if (_isReload && !_isActive)
        {
            StartCoroutine(AbilityTimer());
        }
    }

    private IEnumerator AbilityTimer()
    {
        _isActive = true;
        _isReload = false;
        float valueTimer = 0f;

        TimerStarted?.Invoke();
        _vampirismZone.gameObject.SetActive(true);
        _vampirismZone.StartVampirism(_damagePerSecond,_healPerDamage);

        while (valueTimer <= _abilityOperatingTime)
        {
            ChangeTimerValue?.Invoke(_abilityOperatingTime - valueTimer);
            valueTimer +=Time.deltaTime;
            yield return null;
        }        

        _isActive = false;
        _vampirismZone.StopVampirism();
        _vampirismZone.gameObject.SetActive(false);

        StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        float reloadValue = 0f;
        ReloadStarted?.Invoke();

        while (reloadValue <= _reloadTime)
        {
            ChangeReloadValue?.Invoke(_reloadTime - reloadValue); 
            reloadValue += Time.deltaTime;
            yield return null;
        }

        _isReload = true;
    }
}
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBarViewer : Viewer
{
    [SerializeField] private Slider _smoothHealhtBar;
    [SerializeField] private float _fillSpeed = 100f;

    private float _targetValue;
    private bool _isInitialized = false;

    protected override void ChangeHealthIndicator(float currentValue, float maxValue)
    {
        if (!_isInitialized)
        {
            _smoothHealhtBar.maxValue = maxValue;
            _smoothHealhtBar.value = currentValue;
            _isInitialized = true;
        }
        else
        {
            _targetValue = currentValue;
            StartCoroutine(AnimateHealthBar());
        }
    }

    private IEnumerator AnimateHealthBar()
    {
        while (!Mathf.Approximately(_smoothHealhtBar.value, _targetValue))
        {
            _smoothHealhtBar.value = Mathf.MoveTowards(_smoothHealhtBar.value,_targetValue,_fillSpeed * Time.deltaTime);
            yield return null; 
        }

        _smoothHealhtBar.value = _targetValue;
    }
}
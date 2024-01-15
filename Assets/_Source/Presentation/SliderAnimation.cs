using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SliderAnimation : MonoBehaviour
{
  [SerializeField] private Slider _slider;
  
  [SerializeField] private AnimationCurve _statusCurve;
  [SerializeField] private float _fillDuration = 2f;

  
  public void ChangeFillDuration(float newDuration)
  {
    _fillDuration = newDuration;
  }

  public void StartAnimation()
  {
    StartCoroutine(LerpSliderValue());
  }
    
  private IEnumerator LerpSliderValue()
  {
    float elapsedTime = 0f;

    while (elapsedTime < _fillDuration)
    {
      elapsedTime += Time.deltaTime;
      _slider.value = _statusCurve.Evaluate(elapsedTime / _fillDuration);
      yield return null;
    }
  }
}
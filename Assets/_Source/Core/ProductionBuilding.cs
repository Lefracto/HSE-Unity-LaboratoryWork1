using System;
using System.Collections;
using UnityEngine;

namespace Core
{
  public class ProductionBuilding : MonoBehaviour
  {
    private const float START_PRODUCTION_TIME = 3;

    private float _productionTime = START_PRODUCTION_TIME;
    private int _productionLevel;
    [Header("Production Settings")] 
    [SerializeField] private int _productionValue;
    [SerializeField] private GameResource _productionResource;

    [Space(5)] [Header("Functional Settings")] [SerializeField]
    private GameManager _manager;

    [SerializeField] private SliderAnimation _sliderAnimation;

    public Action<float> OnProductionLevelUp { get; set; }
    private ResourceBank _bank;


    private void Start()
    {
      OnProductionLevelUp += _sliderAnimation.ChangeFillDuration;
      _bank = _manager.GetResourceBank();
      CalculateProductionTime();
    }

    // calculate time: time = time * e^(-level/6)
    private const float DECREASING_TIME_SPEED = 6f;

    private void CalculateProductionTime()
      => _productionTime = START_PRODUCTION_TIME * Mathf.Exp(-_productionLevel / DECREASING_TIME_SPEED);

    private bool _inProduction;

    public void StartProduction()
    {
      if (_inProduction is false)
        StartCoroutine(FinishProduction());
    }

    public void IncreaseProductionLevel()
    {
      ++_productionLevel;
      CalculateProductionTime();
      OnProductionLevelUp.Invoke(_productionTime);
    }

    private IEnumerator FinishProduction()
    {
      _inProduction = true;
      yield return new WaitForSeconds(_productionTime);
      _bank.ChangeResource(_productionResource, _productionValue);
      _inProduction = false;
    }
  }
}
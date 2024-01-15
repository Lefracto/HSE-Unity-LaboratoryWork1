using System.Collections;
using UnityEngine;

namespace Core
{
  public class ProductionBuilding : MonoBehaviour
  {
    private const float START_PRODUCTION_TIME = 3;

    [Header("Production Settings")] private float _productionTime = START_PRODUCTION_TIME;
    private int _productionLevel;
    [SerializeField] private int _productionValue;
    [SerializeField] private GameResource _productionResource;
    [SerializeField] private GameManager _manager;
    private ResourceBank _bank;

    private void Start()
    {
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
      => ++_productionLevel;

    private IEnumerator FinishProduction()
    {
      _inProduction = true;
      yield return new WaitForSeconds(_productionTime);
      _bank.ChangeResource(_productionResource, _productionValue);
      _inProduction = false;
    }
  }
}
using System.Collections;
using UnityEngine;

namespace Core
{
  public class ProductionBuilding : MonoBehaviour
  {
    private const float START_PRODUCTION_TIME = 3;

    [Header("Production Settings")] [SerializeField]
    private float _productionTime = START_PRODUCTION_TIME;

    [SerializeField] private int _productionLevel;
    [SerializeField] private int _productionValue;

    [Space(15)] [SerializeField] private GameResource _productionResource;
    [Space(5)] [SerializeField] private GameManager _manager;
    private ResourceBank _bank;

    private void Awake()
    {
      _bank = _manager.GetResourceBank();
      CalculateProductionTime();
    }

    // calculate time: time = time * e^(-level/6)
    private void CalculateProductionTime()
      => _productionTime = START_PRODUCTION_TIME * Mathf.Exp(-_productionLevel / 6f);

    public void StartProduction()
      => StartCoroutine(FinishProduction());

    public void IncreaseProductionLevel()
      => ++_productionLevel;

    private IEnumerator FinishProduction()
    {
      yield return new WaitForSeconds(_productionTime);
      _bank.ChangeResource(_productionResource, _productionValue);
    }
  }
}
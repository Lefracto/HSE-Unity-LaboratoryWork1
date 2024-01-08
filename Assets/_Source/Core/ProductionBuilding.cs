using System.Collections;
using UnityEngine;

namespace Core
{
  public class ProductionBuilding : MonoBehaviour
  {
    [Header("Production Settings")] [SerializeField]
    private float _productionTime;

    [SerializeField] private int _productionLevel;
    [SerializeField] private int _productionValue = 2;

    [Space(15)] [SerializeField] private GameResource _productionResource;
    [Space(5)] [SerializeField] private GameManager _manager;
    private ResourceBank _bank;


    private void Awake()
    {
      _bank = _manager.GetResourceBank();
      CalculateProductionTime();
    }

    private void CalculateProductionTime()
      => _productionTime -= _productionTime * _productionLevel;

    public void StartProduction()
      => StartCoroutine(FinishProduction());

    private IEnumerator FinishProduction()
    {
      yield return new WaitForSeconds(_productionTime);
      _bank.ChangeResource(_productionResource, _productionValue);
    }
  }
}
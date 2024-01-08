using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Core
{
  public class ProductionBuilding : MonoBehaviour
  {
    [SerializeField] private GameResource _productionResource;
    [SerializeField] private float _productionTime;
    [SerializeField] private int _productionValue;

    [SerializeField] private GameManager _manager;
    private ResourceBank _bank;

    
    
    private void Awake()
      => _bank = _manager.GetResourceBank();

    public void StartProduction()
      => StartCoroutine(FinishProduction());

    private IEnumerator FinishProduction()
    {
      yield return new WaitForSeconds(_productionTime);
      _bank.ChangeResource(_productionResource, _productionValue);
    }
  }
}

/*
  
  Добавить к кнопке слайдер, который должен отображать прогресс выполнения корутины. Кнопка должна становиться неактивной при запуске корутины и активной при окончании.
  
  */
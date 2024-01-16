using System.Collections.Generic;
using Core;
using TMPro;
using UnityEngine;

public class ProductionVisual : MonoBehaviour
{
  [SerializeField] private List<TMP_Text> _productionTexts;
  [SerializeField] private List<ProductionBuilding> _buildings;

  private void Start()
  {
    for (int i = 0; i < _buildings.Count; i++)
    {
      int i1 = i;
      _buildings[i].GetProductionLevel().OnValueChanged += (newValue =>
        _productionTexts[i1].text = newValue.ToString());
    }
  }
}
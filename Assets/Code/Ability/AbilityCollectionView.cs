using System;
using System.Collections.Generic;
using Code.Items;
using UnityEngine;

namespace Code.Ability
{
    public class AbilityCollectionView: MonoBehaviour, IAbilityCollectionView
    {
        public event Action<IItem> UseRequesed;

        private IReadOnlyList<IItem> _abilityItems;

        private void OnUseRequested(IItem item)
        {
            UseRequesed.Invoke(item);
        }
        
        public void Display(IReadOnlyList<IItem> abilityItems)
        {
            _abilityItems = abilityItems;
        }
        
        public void Show()
        {
            
        }

        public void Hide()
        {
            
        }

        
    }
}
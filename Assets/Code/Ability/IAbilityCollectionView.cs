using System;
using System.Collections.Generic;
using Code.BaseView;
using Code.Items;

namespace Code.Ability
{
    public interface IAbilityCollectionView: IView
    {
        event Action<IItem> UseRequesed;
        void Display(IReadOnlyList<IItem> abilityItems);
    }
}
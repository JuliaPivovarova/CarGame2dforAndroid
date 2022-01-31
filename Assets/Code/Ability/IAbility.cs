namespace Code.Ability
{
    public interface IAbility
    {
        void Apply();
        AbilityItemConfig GetConfig();
    }
}
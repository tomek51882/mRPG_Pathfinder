[System.Serializable]
public class StatModifier
{
    public ModifierType type;
    public int value;
}

public enum ModifierType { FlatBonus, PercentStack, PercentTotal }
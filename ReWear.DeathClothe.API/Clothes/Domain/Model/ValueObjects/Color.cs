namespace ReWear.DeathClothe.API.Clothes.Domain.Model.ValueObjects;

public record Color
{
    public EColor Value { get; }
public Color(){}
    public Color(string colorString)
    {
        if (!Enum.TryParse(typeof(EColor), colorString, true, out var result))
            throw new ArgumentException($"Color inválido: {colorString}");

        Value = (EColor)result!;
    }

    public override string ToString() => Value.ToString();
}
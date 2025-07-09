namespace ReWear.DeathClothe.API.Clothes.Domain.Model.ValueObjects;

public record Talla
{
    public ESize Value { get; }

    public Talla()
    {
    }

    public Talla(string tallaString)
    {
        if (!Enum.TryParse(typeof(ESize), tallaString, true, out var result))
            throw new ArgumentException($"Talla inválida: {tallaString}");

        Value = (ESize)result!;
    }

    public override string ToString() => Value.ToString();
}
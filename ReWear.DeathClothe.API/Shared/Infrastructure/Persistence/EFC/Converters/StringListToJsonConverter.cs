using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;
namespace ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Converters;

public class StringListToJsonConverter : ValueConverter<List<string>, string>
{
    public StringListToJsonConverter()
        : base(
            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null!),
            v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null!)
        )
    { }
}
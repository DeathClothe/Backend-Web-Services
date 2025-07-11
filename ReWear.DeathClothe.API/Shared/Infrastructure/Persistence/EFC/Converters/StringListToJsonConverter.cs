using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace ReWear.DeathClothe.API.Shared.Infrastructure.Persistence.EFC.Converters;

public class StringListToJsonConverter : ValueConverter<List<string>, string>
{
    public StringListToJsonConverter()
        : base(
            v => JsonConvert.SerializeObject(v),
            v => JsonConvert.DeserializeObject<List<string>>(v) ?? new List<string>()
        )
    { }
}
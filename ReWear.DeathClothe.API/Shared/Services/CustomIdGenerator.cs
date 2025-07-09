namespace ReWear.DeathClothe.API.Shared.Services;

public static class CustomIdGenerator
{
    public static string GenerateNextId(string? lastId, string prefix)
    {
       

        if (string.IsNullOrEmpty(lastId)) 
        {
            var id = $"{prefix}001";
          
            return id;
        }

        var numericPart = new string(lastId.SkipWhile(c => !char.IsDigit(c)).ToArray());
        var lastNumber = int.TryParse(numericPart, out int n) ? n : 0;
        var newId = $"{prefix}{(lastNumber + 1).ToString("D3")}";
        
        return newId;
    }
}

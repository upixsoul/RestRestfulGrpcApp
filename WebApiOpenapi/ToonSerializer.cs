using System.Reflection;

namespace WebApiOpenapi;
public static class ToonSerializer
{
    public static string ToToon(object obj, string? arrayName = null)
    {
        // Si es una colección → usar formato TOON de arreglo
        if (obj is IEnumerable<object> list)
        {
            return SerializeArray(list, arrayName);
        }

        // Si es un objeto normal → serializar propiedades
        return SerializeObject(obj);
    }

    private static string SerializeObject(object obj)
    {
        var props = obj.GetType().GetProperties();
        var lines = props.Select(p =>
        {
            var value = p.GetValue(obj);

            return value switch
            {
                string s => $"{p.Name}: \"{s}\"",
                bool b => $"{p.Name}: {b.ToString().ToLower()}",
                _ => $"{p.Name}: {value}"
            };
        });

        return string.Join("\n", lines);
    }

    private static string SerializeArray(IEnumerable<object> list, string? arrayName)
    {
        var items = list.ToList();
        if (!items.Any())
            return $"{arrayName}[0]{{}}:";

        // Obtener propiedades del primer elemento
        var props = items.First().GetType().GetProperties();

        string header = $"{arrayName}[{items.Count}]{{{string.Join(",", props.Select(p => p.Name))}}}:";

        // Construir filas
        var rows = items.Select(item =>
        {
            var values = props.Select(p =>
            {
                var value = p.GetValue(item);

                return value switch
                {
                    string s => s,
                    bool b => b.ToString().ToLower(),
                    _ => value?.ToString()
                };
            });

            return "  " + string.Join(",", values);
        });

        return header + "\n" + string.Join("\n", rows);
    }
}


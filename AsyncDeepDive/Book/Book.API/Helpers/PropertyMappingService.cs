
using Book.API.Entities;
using Book.API.Models;

namespace Book.API.Helpers;

public class PropertyMappingService : IPropertyMappingService
{
    private readonly Dictionary<string, PropertyMappingValue> _bookPropertyMapping =
        new(StringComparer.OrdinalIgnoreCase)
        {
            {"Id", new(new[] { "Id"}) },
            {"Title", new(new[] { "Title"}) },
        };

    private readonly IList<IPropertyMapping> _propertyMapping = new List<IPropertyMapping>();
    public PropertyMappingService()
    {
        _propertyMapping.Add(new PropertyMapping<BookDto, Books>(_bookPropertyMapping));
    }

    public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
    {
        // get matching mapping
        var matchingMapping = _propertyMapping.OfType<PropertyMapping<TSource, TDestination>>();

        if (matchingMapping.Count() == 1)
            return matchingMapping.First().MappingDictionary;

        throw new Exception($"Cannot find exact property mapping instance for {typeof(TSource)}, {typeof(TDestination)}");
    }

    public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
    {
        var propertyMapping = GetPropertyMapping<TSource, TDestination>();
        
        if (string.IsNullOrEmpty(fields)) return true;

        // the string is separated by ",", so we split it
        var filedsAfterSplit = fields.Split(',');

        foreach(var field in filedsAfterSplit)
        {
            var trimmedField = field.Trim();

            // remove everything after the first " " - if the fields
            // are comming from an orderBy string, this part must be ignored
            var indexOfFirstSpace = trimmedField.IndexOf(" ");
            var propertyName = indexOfFirstSpace == -1 ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

            // find the matching property
            if (!propertyMapping.ContainsKey(propertyName))
                return false;
        }

        return true;
    }
}

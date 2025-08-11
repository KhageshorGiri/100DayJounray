namespace Book.API.Helpers;
using System.Linq.Dynamic.Core;

public static class IQueryableExtensions
{
    public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy, 
        Dictionary<string, PropertyMappingValue> mappingDictionary)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if(mappingDictionary == null) throw new ArgumentNullException(nameof(mappingDictionary));

        if (string.IsNullOrEmpty(orderBy)) return source;

        var orderByString = string.Empty;
        var orderByStringAfterSplit = orderBy.Split(',');

        foreach(var orderByClause in orderByStringAfterSplit)
        {
            var trimmedValue = orderByClause.Trim();

            var orderByDesc = trimmedValue.EndsWith(" desc");

            // remove " asc" or " desc" from the orderBy clause, so we get the 
            // property nae to look for in the mapping dictionary
            var indexOfFirstSpace = trimmedValue.IndexOf(" ");
            var propertyName = indexOfFirstSpace == -1 ? trimmedValue : trimmedValue.Remove(indexOfFirstSpace);

            // find the matching property
            if (!mappingDictionary.ContainsKey(propertyName))
                throw new ArgumentException($"Key mapping for {propertyName} is missing");

            // get the PropertyMappingValue
            var propertyMappingValue = mappingDictionary[propertyName];
            if (propertyMappingValue is null)
                throw new ArgumentNullException(nameof(propertyMappingValue));

            // revert sort order if necessary
            if (propertyMappingValue.Revert)
                orderByDesc = !orderByDesc;

            // Run through the property names
            foreach(var destinationProperty in propertyMappingValue.DestinationProperties)
            {
                orderByString = orderByString +
                                (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
                                + destinationProperty
                                + (orderByDesc ? "descending" : "ascending");
            }
        }
        return source.OrderBy(orderByString);
    }
}

namespace Book.API.Helpers;

public interface IPropertyMappingService
{
    Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
    public bool ValidMappingExistsFor<TSource, TDestination>(string fields);
}

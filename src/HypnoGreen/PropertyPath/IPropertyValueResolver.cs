namespace HypnoGreen.PropertyPath
{
    internal interface IPropertyValueResolver
    {
        object ResolveProperty(Property property, object data);
        object ResolveArrayItemProperty(ArrayItemProperty arrayItemProperty, object data);
    }
}

using Domain.Dispatcher.Core;
using Domain.Dispatcher.Mappers.Common;

namespace Domain.Dispatcher.Mappers;

public class BooleanStringMapper : ITypeMapper<bool, string>
{
    public string Map(bool source) => source
        ? $"{MappingResources.Yes_Emoji} {MappingResources.Yes_String}"
        : $"{MappingResources.No_Emoji} {MappingResources.No_String}";
}
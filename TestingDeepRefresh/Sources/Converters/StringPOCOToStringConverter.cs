using System.Globalization;
using TestingDeepRefresh.Sources.Models;

namespace TestingDeepRefresh.Sources.Converters
{
  public class StringPOCOToStringConverter : IValueConverter
  {
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      StringPOCO? stringPOCO = value as StringPOCO;
      StringDTO? stringDTO = stringPOCO?.DTO;
      if (stringDTO == null)
        return $"Fail to convert {value?.GetType().Name ?? $"NULL {nameof(value)}"} as a {nameof(StringPOCO)} to a string.";

      return stringDTO.Data;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}

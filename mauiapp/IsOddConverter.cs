using System.Collections;
using System.Globalization;

namespace TodoREST;

public class IsOddConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || !(value is int))
            return Colors.Transparent; // возвращаем прозрачный фон, если значение не является индексом или равно null

        int index = (int)value;

        if (index % 2 == 0)
            return Colors.White; // четный индекс
        else
            return Colors.LightGray; // нечетный индекс
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


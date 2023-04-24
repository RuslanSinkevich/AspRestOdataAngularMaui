using System.Collections;
using System.Globalization;

namespace TodoREST;

public class IsOddConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || !(value is int))
            return Colors.Transparent; // ���������� ���������� ���, ���� �������� �� �������� �������� ��� ����� null

        int index = (int)value;

        if (index % 2 == 0)
            return Colors.White; // ������ ������
        else
            return Colors.LightGray; // �������� ������
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


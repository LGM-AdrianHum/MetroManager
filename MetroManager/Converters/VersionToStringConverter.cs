// File: MetroManager/MetroManager/VersionToStringConverter.cs
// User: Adrian Hum/
//
// Created:  2018-06-06 8:34 PM
// Modified: 2018-06-06 10:02 PM

using System;
using System.Globalization;
using System.Windows.Data;
using Windows.ApplicationModel;

namespace MetroManager.Converters
{
    /// <summary>
    /// Very useful routine... I have been writing toString override method all this time
    /// </summary>
    internal class VersionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var version = (PackageVersion)value;
            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
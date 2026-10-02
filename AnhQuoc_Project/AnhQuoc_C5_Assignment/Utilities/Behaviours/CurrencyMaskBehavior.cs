using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment.Utilities.Behaviours
{
    public static class CurrencyMaskBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "IsEnabled",
                typeof(bool),
                typeof(CurrencyMaskBehavior),
                new PropertyMetadata(false, OnIsEnabledChanged));

        public static bool GetIsEnabled(DependencyObject obj) => (bool)obj.GetValue(IsEnabledProperty);
        public static void SetIsEnabled(DependencyObject obj, bool value) => obj.SetValue(IsEnabledProperty, value);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                if ((bool)e.NewValue)
                {
                    textBox.TextChanged += TextBox_TextChanged;
                    textBox.SelectionChanged += TextBox_SelectionChanged;
                    FormatAndSetCaret(textBox);
                }
                else
                {
                    textBox.TextChanged -= TextBox_TextChanged;
                    textBox.SelectionChanged -= TextBox_SelectionChanged;
                }
            }
        }

        private static void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                FormatAndSetCaret(textBox);
            }
        }

        private static void FormatAndSetCaret(TextBox textBox)
        {
            textBox.TextChanged -= TextBox_TextChanged;

            string text = textBox.Text;

            // Extract only the leading numeric digits typed by the user
            string digitsOnly = Regex.Replace(text, @"[^\d]", "");

            // If user cleared text or typed nothing, default to 0
            if (string.IsNullOrEmpty(digitsOnly))
            {
                digitsOnly = "0";
            }
            else
            {
                digitsOnly = digitsOnly.TrimStart('0');
                if (string.IsNullOrEmpty(digitsOnly)) digitsOnly = "0";
            }

            // Format number with vi-VN dot separators (e.g. 1240 -> "1.240")
            if (long.TryParse(digitsOnly, out long rawValue))
            {
                string formattedPrefix = rawValue.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"));

                // Append suffix
                string suffix = ".000 VND";
                textBox.Text = formattedPrefix + suffix;

                // Position cursor immediately after the main numeric part (before ".000 VND")
                textBox.SelectionStart = formattedPrefix.Length;
                textBox.SelectionLength = 0;
            }

            textBox.TextChanged += TextBox_TextChanged;
        }

        private static void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                // Prevent user from clicking or placing caret inside the ".000 VND" suffix
                string text = textBox.Text;
                int suffixLength = ".000 VND".Length;
                int maxAllowedCaretIndex = text.Length - suffixLength;

                if (maxAllowedCaretIndex >= 0 && textBox.SelectionStart > maxAllowedCaretIndex)
                {
                    textBox.SelectionStart = maxAllowedCaretIndex;
                }
            }
        }
    }
}

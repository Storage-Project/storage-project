using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace storage_app.Components
{
    /// <summary>
    /// Interação lógica para IntegerUpDown.xam
    /// </summary>
    public partial class IntegerUpDown : UserControl
    {
        public int Integer
        {
            get { return (int)GetValue(IntegerProperty); }
            set { SetValue(IntegerProperty, value); }
        }

        public static readonly DependencyProperty IntegerProperty
            = DependencyProperty
            .Register(nameof(Integer), typeof(int), typeof(IntegerUpDown));

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public static readonly DependencyProperty MaximumProperty
            = DependencyProperty.Register(nameof(Maximum), typeof(int), typeof(IntegerUpDown));

        public IntegerUpDown()
        {
            InitializeComponent();
        }

        private void Change_Integer(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).Content.ToString() is not string content)
                return;

            int value = int.Parse(content, NumberStyles.AllowLeadingSign);
            if (Integer + value <= Maximum &&
                Integer + value >= 0)
                Integer += value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TimePickerUserControl
{
    [ValueConversion(typeof(Double), typeof(Int32))]
    public class DoubleToIntConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var newValue = (double) value;
            int neIntValue = (Int32) newValue;
            return neIntValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }

    public class ValueChangedRoutedEventArghs : RoutedEventArgs
    {
        public TimeSpan Value { get; set; }

        public ValueChangedRoutedEventArghs(RoutedEvent routedEvent,TimeSpan value) : base(routedEvent)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Interaction logic for TimePickerUC.xaml
    /// </summary>
    public partial class TimePickerUC : UserControl
    {
        // dependency properties
        public static readonly DependencyProperty HoursProperty = DependencyProperty.Register(
    "Hours", typeof(Int32), typeof(TimePickerUC), new FrameworkPropertyMetadata(0));
        public static readonly DependencyProperty MinutesProperty = DependencyProperty.Register(
    "Minutes", typeof(Int32), typeof(TimePickerUC), new PropertyMetadata(0));
        public static readonly DependencyProperty SecondsProperty = DependencyProperty.Register(
    "Seconds", typeof(Int32), typeof(TimePickerUC), new PropertyMetadata(0));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
    "Value", typeof(TimeSpan), typeof(TimePickerUC), new PropertyMetadata(new TimeSpan()));

        //routed event
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
        "ValueChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(TimePickerUC));
        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public int Hours
        {
            get
            {
                if (TextBlockHours.Text != "")
                {
                    this.SetValue(HoursProperty, Int32.Parse(TextBlockHours.Text));
                    return (Int32)this.GetValue(HoursProperty);
                }
                return -1;
            }
            set
            {
                TextBlockHours.Text = value.ToString();
                this.SetValue(HoursProperty, value);
                RaiseEvent(new ValueChangedRoutedEventArghs(TimePickerUC.ValueChangedEvent, Value));
            }
        }
        public int Minutes
        {
            get
            {
                if (TextBlockMinutes.Text != "")
                {
                    this.SetValue(MinutesProperty, Int32.Parse(TextBlockMinutes.Text));
                    return (Int32)this.GetValue(MinutesProperty);
                }
                return -1;
            }
            set
            {
                TextBlockMinutes.Text = value.ToString();
                this.SetValue(MinutesProperty, value);
                RaiseEvent(new ValueChangedRoutedEventArghs(TimePickerUC.ValueChangedEvent, Value));
            }

        }
        public int Seconds
        {
            get
            {
                if (TextBlockSeconds.Text != "")
                {
                    this.SetValue(SecondsProperty, Int32.Parse(TextBlockSeconds.Text));
                    return (Int32)this.GetValue(SecondsProperty);
                }
                return -1;
            }
            set
            {
                TextBlockSeconds.Text = value.ToString();
                this.SetValue(SecondsProperty, value);
                RaiseEvent(new ValueChangedRoutedEventArghs(TimePickerUC.ValueChangedEvent, Value));
            }
        }
        public TimeSpan Value
        {
            get
            {
                this.SetValue(ValueProperty, new TimeSpan(Hours, Minutes, Seconds));
                return (TimeSpan)this.GetValue(ValueProperty); ;
            }
            set
            {
                if (value != null)
                {
                    Hours = value.Hours;
                    Minutes = value.Minutes;
                    Seconds = value.Seconds;
                    this.SetValue(ValueProperty, new TimeSpan(Hours, Minutes, Seconds));
                    RaiseEvent(new ValueChangedRoutedEventArghs(TimePickerUC.ValueChangedEvent, Value));
                }
            }
        }

        public TimePickerUC()
        {
            InitializeComponent();
            var now = DateTime.Now.TimeOfDay;
            TextBlockHours.Text = now.Hours.ToString();
            TextBlockMinutes.Text = now.Minutes.ToString();
            TextBlockSeconds.Text = now.Seconds.ToString();
            RoutedEventArgs newEventArgs = new ValueChangedRoutedEventArghs(TimePickerUC.ValueChangedEvent, Value);
            RaiseEvent(newEventArgs);
        }

        private void ButtonAddHour_Click(object sender, RoutedEventArgs e)
        {
            if (Hours < 23)
            {
                Hours++;
            }
            else
            {
                Hours = 0;
            }
        }
        private void ButtonAddMinute_Click(object sender, RoutedEventArgs e)
        {
            if (Minutes < 59)
            {
                Minutes++;
            }
            else
            {
                Minutes = 0;
                ButtonAddHour_Click(null, null);
            }
        }
        private void ButtonAddSecond_Click(object sender, RoutedEventArgs e)
        {
            if (Seconds < 59)
            {
                Seconds++;
            }
            else
            {
                Seconds = 0;
                ButtonAddMinute_Click(null, null);
            }
        }
        private void ButtonTakeAwayHour_Click(object sender, RoutedEventArgs e)
        {
            if (Hours > 0)
            {
                Hours--;
            }
            else
            {
                Hours = 23;
            }
        }
        private void ButtonTakeAwayMinute_Click(object sender, RoutedEventArgs e)
        {
            if (Minutes > 0)
            {
                Minutes--;
            }
            else
            {
                Minutes = 59;
                ButtonTakeAwayHour_Click(null, null);
            }
        }
        private void ButtonTakeAwaySecond_Click(object sender, RoutedEventArgs e)
        {
            if (Seconds > 0)
            {
                Seconds--;
            }
            else
            {
                Seconds = 59;
                ButtonTakeAwayMinute_Click(null, null);
            }
        }
    }
}

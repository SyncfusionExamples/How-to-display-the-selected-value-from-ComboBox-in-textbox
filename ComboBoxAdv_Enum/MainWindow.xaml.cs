using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

namespace ComboBoxAdv_Enum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public object comboBoxSelectedValue;
        public object ComboBoxSelectedValue
        {
            get { return comboBoxSelectedValue; }
            set { comboBoxSelectedValue = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ComboBoxSelectedValue")); }
        }
        public Dictionary<Enum_Type, string> SelectedEngineTypeCollection { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            SelectedEngineTypeCollection = new Dictionary<Enum_Type, string>();
            SelectedEngineTypeCollection.Add(Enum_Type.E004, GetDescription(Enum_Type.E004));
            SelectedEngineTypeCollection.Add(Enum_Type.E005, GetDescription(Enum_Type.E005));
            SelectedEngineTypeCollection.Add(Enum_Type.E006, GetDescription(Enum_Type.E006));

            this.DataContext = this;
        }

        public static T GetEnumAttribute<T>(Enum source) where T : Attribute
        {
            Type type = source.GetType();
            var sourceName = Enum.GetName(type, source);
            FieldInfo field = type.GetField(sourceName);
            object[] attributes = field.GetCustomAttributes(typeof(T), false);
            foreach (var o in attributes)
            {
                if (o is T)
                    return (T)o;
            }
            return null;
        }

        public static string GetDescription(Enum source)
        {
            var str = GetEnumAttribute<DescriptionAttribute>(source);
            if (str == null)
                return null;
            return str.Description;
        }
    }

    public enum Enum_Type
    {
        [Description("Engine1")]
        E004,
        [Description("Engine2")]
        E005,
        [Description("Engine3")]
        E006,
    }
}

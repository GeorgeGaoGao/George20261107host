using Company.Core.Helper;
using Company.Core.Model;
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

namespace Company.Core.Extension
{

    /// <summary>
    /// combobox的枚举绑定扩展类
    /// </summary>
    public static class ComboBoxExtension
    {
        public static string GetEnumDescription(this object obj)
        {
            Assert.NotNull(obj);
            var type = obj.GetType();
            if (!type.IsEnum)
            {
                throw new Exception($"{type.FullName}必须是枚举类型。");
            }
            FieldInfo fieldInfo = obj.GetType().GetField(obj.ToString());
            if (fieldInfo != null)
            {
                DescriptionAttribute attribute = Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute!=null)
                {
                    return attribute.Description;
                }
                else
                {
                    return fieldInfo.Name;
                }
            }
            return string.Empty;
        }
        public static IEnumerable<BindableSourceItem<T>> GetBindableSourceItems<T>(this object obj, Func<T, bool> filter=null) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new Exception($"{type.FullName}必须为枚举类型。");
            }
            var items = type.GetEnumValues().Cast<T>().Select(x => new BindableSourceItem<T>()
            {
                Descritpion = x.GetEnumDescription(),
                Value = x,
            }
            );
            if (filter!=null)
            {
                items = items.Where(p => filter(p.Value));
            }
            return items;
        }
        public static void Binding<T>(this ComboBox comboBox, Func<T, bool> filter = null, bool icon = false) where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new Exception($"{type.FullName}必须为枚举类型。");
            }
            var items = type.GetBindableSourceItems(filter);
            comboBox.DoBinding(items, icon);
        }
        public static void DoBinding<T>(this ComboBox comboBox,IEnumerable<BindableSourceItem<T>> items,bool icon = false)
        {
            if (icon)
            {
                FrameworkElementFactory f = new FrameworkElementFactory(typeof(Image));
                Binding binding = new Binding();
                binding.Path=new PropertyPath(nameof(BindableSourceItem<T>.Descritpion));
                f.SetBinding(Image.SourceProperty, binding);
                f.SetValue(FrameworkElement.HeightProperty, 30.0);
                var template=new DataTemplate { VisualTree = f };
                template.Seal();
                comboBox.ItemTemplate = template;
            }
            else
            {
                comboBox.DisplayMemberPath=nameof(BindableSourceItem<T>.Descritpion);
            }
            comboBox.SelectedValuePath = nameof(BindableSourceItem<T>.Value);
            comboBox.ItemsSource = items;
        }
    }
}

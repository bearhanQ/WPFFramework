﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFTemplate
{
    public class StepsItem : ListBoxItem
    {
        static StepsItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(StepsItem), new FrameworkPropertyMetadata(typeof(StepsItem)));
        }
    }
}

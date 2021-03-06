﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace MediaBrowser.Theater.Presentation.Controls
{
    /// <summary>
    /// Interaction logic for SelectList.xaml
    /// </summary>
    public partial class SelectList : UserControl
    {
        public event EventHandler<EventArgs> SelectedItemChanged;

        public List<SelectListItem> Options { get; set; }

        public bool Enabled
        {
            get { return BtnNextOption.IsEnabled; }
            set
            {
                BtnNextOption.IsEnabled = value;
                BtnPreviousOption.IsEnabled = value;
            }
        }

        public SelectListItem SelectedItem
        {
            get
            {
                var val = TxtValue.Text;
                return Options.FirstOrDefault(i => string.Equals(i.Text, val));
            }
        }

        public string SelectedValue
        {
            get
            {
                var item = SelectedItem;

                return item == null ? null : item.Value;
            }
            set
            {
                var item = Options.FirstOrDefault(i => string.Equals(i.Value, value));

                if (item == null)
                {
                    throw new ArgumentOutOfRangeException();
                }

                TxtValue.Text = item.Text;
            }
        }

        public bool ContainsValue(string value)
        {
            return Options.Any(i => string.Equals(value, i.Value));
        }

        public SelectList()
        {
            InitializeComponent();

            Options = new List<SelectListItem>();

            BtnNextOption.Click += BtnNextOption_Click;
            BtnPreviousOption.Click += BtnPreviousOption_Click;
        }

        void BtnPreviousOption_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null && Options.Count == 0)
            {
                return;
            }

            var currentIndex = Options.IndexOf(SelectedItem);

            var newIndex = currentIndex == 0 ? Options.Count - 1 : currentIndex - 1;

            SelectedValue = Options[newIndex].Value;

            if (SelectedItemChanged != null)
            {
                SelectedItemChanged(this, EventArgs.Empty);
            }
        }

        void BtnNextOption_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == null && Options.Count == 0)
            {
                return;
            }

            var currentIndex = Options.IndexOf(SelectedItem);

            var newIndex = currentIndex == Options.Count - 1 ? 0 : currentIndex + 1;

            SelectedValue = Options[newIndex].Value;

            if (SelectedItemChanged != null)
            {
                SelectedItemChanged(this, EventArgs.Empty);
            }
        }
    }

    public class SelectListItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }
}

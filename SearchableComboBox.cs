using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class SearchableComboBox : UserControl
    {
        private TextBox txtSearch;
        private ComboBox cboItems;
        private object fullDataSource;
        private string displayMember;
        private string valueMember;

        public event EventHandler SelectedIndexChanged;

        public SearchableComboBox()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.txtSearch = new TextBox();
            this.cboItems = new ComboBox();
            this.SuspendLayout();

            // txtSearch
            this.txtSearch.Location = new Point(0, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new Size(250, 20);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new EventHandler(this.txtSearch_TextChanged);

            // cboItems
            this.cboItems.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboItems.FormattingEnabled = true;
            this.cboItems.Location = new Point(0, 22);
            this.cboItems.Name = "cboItems";
            this.cboItems.Size = new Size(250, 21);
            this.cboItems.TabIndex = 1;
            this.cboItems.SelectedIndexChanged += new EventHandler(this.cboItems_SelectedIndexChanged);

            // SearchableComboBox
            this.Controls.Add(this.cboItems);
            this.Controls.Add(this.txtSearch);
            this.Name = "SearchableComboBox";
            this.Size = new Size(250, 43);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        public object DataSource
        {
            get { return fullDataSource; }
            set
            {
                fullDataSource = value;
                cboItems.DataSource = value;
            }
        }

        public string DisplayMember
        {
            get { return displayMember; }
            set
            {
                displayMember = value;
                cboItems.DisplayMember = value;
            }
        }

        public string ValueMember
        {
            get { return valueMember; }
            set
            {
                valueMember = value;
                cboItems.ValueMember = value;
            }
        }

        public object SelectedValue
        {
            get { return cboItems.SelectedValue; }
            set { cboItems.SelectedValue = value; }
        }

        public int SelectedIndex
        {
            get { return cboItems.SelectedIndex; }
            set { cboItems.SelectedIndex = value; }
        }

        public object SelectedItem
        {
            get { return cboItems.SelectedItem; }
            set { cboItems.SelectedItem = value; }
        }

        public string SearchText
        {
            get { return txtSearch.Text; }
            set { txtSearch.Text = value; }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (fullDataSource == null || string.IsNullOrEmpty(displayMember))
                return;

            string searchText = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(searchText))
            {
                cboItems.DataSource = fullDataSource;
                return;
            }

            // Filter based on display member
            var filtered = FilterData(searchText);

            // Store current selection
            object currentValue = cboItems.SelectedValue;

            cboItems.DataSource = null;
            cboItems.DataSource = filtered;
            cboItems.DisplayMember = displayMember;
            cboItems.ValueMember = valueMember;

            // Restore selection if still exists in filtered list
            if (currentValue != null)
            {
                try
                {
                    cboItems.SelectedValue = currentValue;
                }
                catch { }
            }
        }

        private object FilterData(string searchText)
        {
            if (fullDataSource is System.Collections.IEnumerable enumerable)
            {
                var list = new List<object>();
                foreach (var item in enumerable)
                {
                    if (item == null) continue;

                    var property = item.GetType().GetProperty(displayMember);
                    if (property != null)
                    {
                        var value = property.GetValue(item, null);
                        if (value != null)
                        {
                            string displayValue = value.ToString().ToLower();
                            // Remove diacritics for Vietnamese search
                            displayValue = QuanLyThietBi.Common.StringHelper.RemoveDiacritics(displayValue);
                            searchText = QuanLyThietBi.Common.StringHelper.RemoveDiacritics(searchText);

                            if (displayValue.Contains(searchText))
                            {
                                list.Add(item);
                            }
                        }
                    }
                }

                // Return the same type as original
                var listType = fullDataSource.GetType();
                if (listType.IsGenericType)
                {
                    var genericType = listType.GetGenericArguments()[0];
                    var typedList = typeof(List<>).MakeGenericType(genericType);
                    var result = Activator.CreateInstance(typedList);
                    var addMethod = typedList.GetMethod("Add");
                    foreach (var item in list)
                    {
                        addMethod.Invoke(result, new[] { item });
                    }
                    return result;
                }

                return list;
            }

            return fullDataSource;
        }

        private void cboItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(this, e);
        }

        public void ClearSearch()
        {
            txtSearch.Text = string.Empty;
        }
    }
}

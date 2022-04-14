﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HCIMiniProjekat
{
    /// <summary>
    /// Interaction logic for TableWindow.xaml
    /// </summary>
    public partial class TableWindow : Window
    {

        public TableWindow(List<TableData> tableData)
        {
            InitializeComponent();

            foreach (TableData td in tableData)
            {
                DataTable.Items.Add(td);
            }
        }

        public void UpdateData(List<TableData> tableData)
        {
            DataTable.Items.Clear();
            foreach (TableData td in tableData)
            {
                DataTable.Items.Add(td);
            }
        }

        public void ClearData()
        {
            DataTable.Items.Clear();
        }
    }
}

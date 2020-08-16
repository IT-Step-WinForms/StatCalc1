using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StatCalc1
{
    public partial class Form1 : Form
    {
        List<double> data;
        CalcManager manager;
        public Form1()
        {
            InitializeComponent();
            data = new List<double>();
            manager = new CalcManager();
        }
        private void addValue()
        {
            string input = InputField.Text;
            if (String.IsNullOrEmpty(input))
            {
                MessageBox.Show("Вы не ввели исходное значение", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                InputField.Focus();
            }
            else
            {
                try
                {
                    double value = Convert.ToDouble(input);
                    valuesList.Items.Add(value);
                    data.Add(value);
                    InputField.Clear();
                    InputField.Focus();
                }
                catch (Exception error)
                {
                    MessageBox.Show($"Ошибка при добавлении величины: \n{error.Message}", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    InputField.Clear();
                    InputField.Focus();
                }
            }
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            addValue();
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            InputField.Clear();
            InputField.Focus();
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            int k = valuesList.SelectedIndex;
            if (k == -1)
            {
                MessageBox.Show("Вы не указали удаляемое значение", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                valuesList.Items.RemoveAt(k);
                data.RemoveAt(k);
            }
        }
        private void clearButton_Click(object sender, EventArgs e)
        {
            valuesList.Items.Clear();
            data.Clear();
        }
        private void calcButton_Click(object sender, EventArgs e)
        {
            if (data.Count == 0)
            {
                MessageBox.Show("Отсутствуют данные для вычисления", "Предупреждение",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                manager.Data = data;
                manager.SortData();
                manager.CalcAverage();
                manager.BuildDeviations();
                manager.CalcError();
                foreach (double x in manager.Data)
                {
                    SortDataList.Text += $"{x:F}  _  ";
                }
                foreach (double y in manager.Deviations)
                {
                    DeviationList.Text += $"{y:F}  _  ";
                }
                ResultField.Text = $"{manager.Average:F} +- {manager.MeasureError:F}";
            }
        }
        private void saveButton_Click(object sender, EventArgs e)
        {

        }
        private void valuesList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void InputField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                addValue();
            }
        }
    }
}
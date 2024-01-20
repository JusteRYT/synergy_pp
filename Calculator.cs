using System;
using System.Drawing;
using System.Windows.Forms;

namespace MathGameCalculator
{
    public partial class CalculatorForm : Form
    {
        private double currentResult = 0;
        private double previousValue = 0;
        private char currentOperation = '\0';
        private bool isNewNumber = true;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (isNewNumber)
            {
                textBox_Result.Text = button.Text;
                isNewNumber = false;
            }
            else
            {
                textBox_Result.Text += button.Text;
            }
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            PerformOperation();
            currentOperation = button.Text[0];
            isNewNumber = true;
        }

        private void PerformOperation()
        {
            double currentValue = double.Parse(textBox_Result.Text);

            switch (currentOperation)
            {
                case '+':
                    currentResult += currentValue;
                    break;
                case '-':
                    currentResult -= currentValue;
                    break;
                case '*':
                    currentResult *= currentValue;
                    break;
                case '/':
                    if (currentValue != 0)
                        currentResult /= currentValue;
                    else
                        MessageBox.Show("Ошибка: деление на ноль!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    currentResult = currentValue;
                    break;
            }

            textBox_Result.Text = currentResult.ToString();
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            PerformOperation();
            currentOperation = '\0';
            isNewNumber = true;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            textBox_Result.Text = "0";
            currentResult = 0;
            previousValue = 0;
            currentOperation = '\0';
            isNewNumber = true;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // Добавьте код сохранения результата, если необходимо
            MessageBox.Show("Результат сохранен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            // Добавьте код загрузки результата, если необходимо
            MessageBox.Show("Результат загружен!", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Дополнительная опция: изменение цветовой схемы
        private void ChangeColorSchemeButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                this.BackColor = colorDialog.Color;
            }
        }

        // Дополнительная опция: изменение размера шрифта
        private void ChangeFontSizeButton_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_Result.Font = fontDialog.Font;
            }
        }
    }
}

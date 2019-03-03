using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp2.testDataSetTableAdapters;
using static WindowsFormsApp2.testDataSet;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDataSet.SingleTable". При необходимости она может быть перемещена или удалена.
            this.singleTableTableAdapter.Fill(this.testDataSet.SingleTable);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDataSet.ParentTable". При необходимости она может быть перемещена или удалена.
            this.parentTableTableAdapter.Fill(this.testDataSet.ParentTable);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDataSet.Child_Table". При необходимости она может быть перемещена или удалена.
            this.child_TableTableAdapter.Fill(this.testDataSet.Child_Table);

        }

        private void N_Single_button_Click(object sender, EventArgs e)
        {
            bool i = true;
            DialogResult result = MessageBox.Show("Czy chesz dodać?", "dodanie sali", MessageBoxButtons.YesNo);
            {

                if (result == DialogResult.Yes)
                {
                    SingleTableRow workRow = testDataSet.SingleTable.NewSingleTableRow();
                    workRow["string"] = textBox2.Text;

                    try
                    {
                        workRow["int"] = Convert.ToInt32(textBox1.Text);
                        DataRow lastRow = testDataSet.SingleTable.AsEnumerable().Last();
                        workRow["Id"] = Convert.ToInt32(lastRow[0]) + 1;
                    }
                    catch (NoNullAllowedException)
                    {
                        workRow["Id"] = 1;
                    }
                    catch (InvalidOperationException)
                    {
                        workRow["Id"] = 1;
                    }
                    

                    testDataSet.SingleTable.AddSingleTableRow(workRow);
                    singleTableTableAdapter.Update(testDataSet.SingleTable);
                }


                textBox1.Clear();
                textBox2.Clear();
            }



        }
            private void string_ColComboBox_SelectedIndexChanged(object sender, EventArgs e)
            {
                string_ColComboBox.DisplayMember = "string_Col";
                string_ColComboBox.ValueMember = "Id";
            }

            private void N_Child_button_Click(object sender, EventArgs e)
            {
                DialogResult result = MessageBox.Show("Czy chesz dodać?", "dodanie sali", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    Child_TableRow workRow = testDataSet.Child_Table.NewChild_TableRow();


                    workRow["str_col"] = textBox5.Text;

                    int sel_inx = string_ColComboBox.SelectedIndex;
                    DataRow we = testDataSet.ParentTable.Rows[sel_inx];
                    workRow["fk"] = we[0];


                    try
                    {

                        workRow["INTcol"] = Convert.ToInt32(textBox6.Text);

                        DataRow lastRow = testDataSet.Child_Table.AsEnumerable().Last();
                        workRow["Id"] = Convert.ToInt32(lastRow[0]) + 1;
                    }
                    catch (NoNullAllowedException)
                    {
                        workRow["Id"] = 1;
                    }
                    catch (InvalidOperationException)
                    {
                        workRow["Id"] = 1;
                    }
                    testDataSet.Child_Table.AddChild_TableRow(workRow);
                    child_TableTableAdapter.Update(testDataSet.Child_Table);

                    textBox5.Clear();
                    textBox6.Clear();
                }


            }

            private void N_Parent_button_Click(object sender, EventArgs e)
            {
                DialogResult result = MessageBox.Show("Czy chesz dodać?", "dodanie sali", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    ParentTableRow workRow = testDataSet.ParentTable.NewParentTableRow();


                    workRow["string_COL"] = textBox3.Text;

                    try
                    {
                        workRow["INT_COL"] = Convert.ToInt32(textBox4.Text);
                        DataRow lastRow = testDataSet.ParentTable.AsEnumerable().Last();
                        workRow["Id"] = Convert.ToInt32(lastRow[0]) + 1;
                    }
                    catch (NoNullAllowedException)
                    {
                        workRow["Id"] = 1;
                    }
                    catch (InvalidOperationException)
                    {
                        workRow["Id"] = 1;
                    }
                    
                    testDataSet.ParentTable.AddParentTableRow(workRow);
                    parentTableTableAdapter.Update(testDataSet.ParentTable);
                    textBox4.Clear();
                    textBox3.Clear();
                }
            }

        
    }
}
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
        int Rand_For_U()
        {
            int rand;
            var x = new Random();
            rand = Convert.ToInt32(x.Next(-1, 150));
            return rand;
        }
     void f1()
        {  
            DataRow workRow = testDataSet.Child_Table.NewRow();

            
            workRow["str_col"] = textBox5.Text;
            
            
            
                
            int sel_inx = string_ColComboBox.SelectedIndex;
            DataRow lastRow;
            
             
            // int t = Convert.ToInt32(we[0]);
            
                DataRow we = testDataSet.ParentTable.Rows[sel_inx];
                workRow["fk"] = we[0];
                
            
            

            try { workRow["INTcol"] = Convert.ToInt32(textBox6.Text); }
            catch (FormatException)
            {
                int x = Rand_For_U() ;
                MessageBox.Show("To nie była liczba. Zostanie wpisana liczba " + x , "uwaga");
                textBox6.Clear();
                workRow["INTcol"] = x;
            }

            try
            {



                lastRow = testDataSet.Child_Table.AsEnumerable().Last();
                workRow["Id"] = Convert.ToInt32(lastRow[0]) + 1;
                // child_TableTableAdapter.Insert(Convert.ToInt32(lastRow[0]) + 1, textBox5.Text, Convert.ToInt32(textBox6.Text), t);
            }

            catch (NoNullAllowedException)
            {
                workRow["Id"] = 1;
            }
            catch (InvalidOperationException)
            {
                workRow["Id"] = 1;
            }

            
            //child_TableTableAdapter.Insert(21, "vse", 23,34 );
            testDataSet.Child_Table.Rows.Add(workRow);
            child_TableTableAdapter.Update(testDataSet.Child_Table);

            textBox5.Clear();
            textBox6.Clear();
        }
        void f2()
        {
            DataRow workRow = testDataSet.ParentTable.NewRow();


            workRow["string_COL"] = textBox3.Text;
            try
            {
                workRow["INT_COL"] = Convert.ToInt32(textBox4.Text);
            }
            catch (FormatException)
            {
                int x = Rand_For_U();
                MessageBox.Show("To nie była liczba. Zostanie wpisana liczba " + x, "uwaga");
                textBox4.Clear();
                workRow["INT_COL"] = x;
            }
            try
            {

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

            testDataSet.ParentTable.Rows.Add(workRow);
            parentTableTableAdapter.Update(testDataSet.ParentTable);
            textBox4.Clear();
            textBox3.Clear();
        }

        void f3()
        {
            DataRow workRow = testDataSet.SingleTable.NewRow();
            workRow["string"] = textBox2.Text;
            try
            {
                workRow["int"] = Convert.ToInt32(textBox1.Text);
            }
            catch (FormatException)
            {
                int x = Rand_For_U();
                MessageBox.Show("To nie była liczba. Zostanie wpisana liczba" + x, "uwaga");
                textBox4.Clear();
                workRow["int"] = x;
            }
            try
            {
                
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


            testDataSet.SingleTable.Rows.Add(workRow);
            singleTableTableAdapter.Update(testDataSet.SingleTable);

            textBox1.Clear();
            textBox2.Clear();
        }

        void v()
        {
            try
            {
                var g = child_TableDataGridView.CurrentRow.Cells[3].Value;
                int t = Convert.ToInt32(g);
                DataRow r = testDataSet.ParentTable.FindById(t);
                string s = Convert.ToString(r[1]);
                comboBox1.Text = s;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("w tej tabeli nie ma elementów. Dodaj nowe dane i sprubój ponownie");
            }
            
        }
    

        void UpdateAll()
        {
            // update database
            child_TableTableAdapter.Update(testDataSet.Child_Table);
            parentTableTableAdapter.Update(testDataSet.ParentTable);
            singleTableTableAdapter.Update(testDataSet.SingleTable);
            // update dataGridView
            child_TableDataGridView.Update();
            parentTableDataGridView.Update();
            singleTableDataGridView.Update();
            

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDataSet.Couple". При необходимости она может быть перемещена или удалена.
            this.coupleTableAdapter.Fill(this.testDataSet.Couple);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDataSet.SingleTable". При необходимости она может быть перемещена или удалена.
            this.singleTableTableAdapter.Fill(this.testDataSet.SingleTable);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDataSet.ParentTable". При необходимости она может быть перемещена или удалена.
            this.parentTableTableAdapter.Fill(this.testDataSet.ParentTable);
            // TODO: данная строка кода позволяет загрузить данные в таблицу "testDataSet.Child_Table". При необходимости она может быть перемещена или удалена.
            this.child_TableTableAdapter.Fill(this.testDataSet.Child_Table);

        }
        // add buttons
             private void N_Child_button_Click(object sender, EventArgs e)
             { bool b = true;
        
               
           
                Main:if (b) {
                DialogResult result = MessageBox.Show("Czy chesz dodać?", "dodanie sali", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (textBox5.Text != "")
                    {
                        try {
                            f1();
                            v();
                        }
                        catch (IndexOutOfRangeException)
                        {
                            MessageBox.Show("w tablicy Rodziecielskiej nie ma wpisu do wyboru");
                            b = false;
                            textBox5.Clear();
                            textBox6.Clear();
                            goto Main;
                              
                        }
                    }
                    else
                    {
                        MessageBox.Show("wprowadź tekst");
                    }

                }
            }


             }
             private void N_Single_button_Click(object sender, EventArgs e)
             {

                   DialogResult result = MessageBox.Show("Czy chesz dodać?", "dodanie sali", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                if (textBox5.Text != "")
                {
                  f3();

                }
                else
                {
                    MessageBox.Show("wprowadź tekst");
                }

            }

                   
             }
             private void N_Parent_button_Click(object sender, EventArgs e)
            {
                DialogResult result = MessageBox.Show("Czy chesz dodać?", "dodanie sali", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    if (textBox5.Text != "")
                    {
                        f2();
                        UpdateAll();
                    }
                else
                {
                    MessageBox.Show("wprowadź tekst");
                }

            }
            }
             private void string_ColComboBox_SelectedIndexChanged(object sender, EventArgs e)
            {
                string_ColComboBox.DisplayMember = "string_Col";
                string_ColComboBox.ValueMember = "Id";
            }
             private void button4_Click(object sender, EventArgs e)
        {
            //
            // string str = Convert.ToString(listBox2.SelectedItem);
            int t_Single = listBox2.SelectedIndex;
            DataRow rw_Single = testDataSet.SingleTable.Rows[t_Single];
            int Id_Single = Convert.ToInt32(rw_Single[0]);
            /////////////////////////////////////////////////////////////
            int t_Parent = listBox3.SelectedIndex;
            DataRow rw_Parent = testDataSet.ParentTable.Rows[t_Parent];
            int Id_parent = Convert.ToInt32(rw_Parent[0]);
            //////////////////////////////////////////////////////////////
            DataRow lastRow;
            DataRow workRow = testDataSet.Couple.NewRow();

            workRow["fk_Single"] = Id_Single;
            workRow["fk_Parent"] = Id_parent;
            try
            {
                lastRow = testDataSet.Couple.AsEnumerable().Last();
                workRow["Id"] = Convert.ToInt32(lastRow[0]) + 1;
            }

            catch (NoNullAllowedException)
            {
                workRow["Id"] = 1;
            }
            catch (InvalidOperationException)
            {
                workRow["Id"] = 1;
            }catch(DeletedRowInaccessibleException)
            {
                workRow["Id"] = 1;

            }
            testDataSet.Couple.Rows.Add(workRow);
            coupleTableAdapter.Update(testDataSet.Couple);
            UpdateAll();
            //////////////////////////////
            string Item1 =Convert.ToString(rw_Single[1]);
            string Item2 = Convert.ToString(rw_Parent[1]);
            listBox1.Items.Add(Item1);
            listBox4.Items.Add(Item2);

        }

        //edits

             private void button2_Click(object sender, EventArgs e)// parent
        {
               UpdateAll();
        }

             private void button3_Click(object sender, EventArgs e)// single
            {
              UpdateAll();
               
            }

             private void button1_Click(object sender, EventArgs e)//child
             {
              int sel_inx = comboBox1.SelectedIndex;
              var g = child_TableDataGridView.CurrentRow.Cells[0].Value;
              DataRow we = testDataSet.ParentTable.Rows[sel_inx];
              int SL_ID = Convert.ToInt32(g);
              DataRow rw = testDataSet.Child_Table.FindById(SL_ID);
              rw[3] = we[0];
              UpdateAll();

            
               
             }

             private void button5_Click(object sender, EventArgs e)
             {
                 try
                 {
                      int t_Single = listBox1.SelectedIndex;
                      DataRow rw_single = testDataSet.Couple.Rows[t_Single];
                      int Id_single = Convert.ToInt32(rw_single[2]);
                      ///////////////////////////////////////////////////
                      int t_Parent = listBox5.SelectedIndex;
                      DataRow rw_Parent = testDataSet.ParentTable.Rows[t_Parent];
                      int Id_parent = Convert.ToInt32(rw_Parent[0]);
                      string Item2 = Convert.ToString(rw_Parent[1]);
                      rw_single[2] = Id_parent;
                      UpdateAll();
                 }
                 catch (IndexOutOfRangeException)
                 {
                    MessageBox.Show("znaczenie nie zostało wybrane");
                 }

             }

             private void button6_Click(object sender, EventArgs e)
            {
               try
               {
                int t_Parent = listBox4.SelectedIndex;
                  DataRow rw_parent = testDataSet.Couple.Rows[t_Parent];
                  int Id_parent = Convert.ToInt32(rw_parent[2]);
                  ///////////////////////////////////////////////////
                  int t_Single = listBox5.SelectedIndex;
                  DataRow rw_Single = testDataSet.SingleTable.Rows[t_Single];
                  int Id_single = Convert.ToInt32(rw_Single[0]);
                  string Item2 = Convert.ToString(rw_Single[1]);
                  rw_parent[1] = Id_single;
                  UpdateAll();
               }
                  catch (IndexOutOfRangeException)
               {
                   MessageBox.Show("znaczenie nie zostało wybrane");
               }
        }

        //delete
        private void Del_Single_button_Click(object sender, EventArgs e)
            {
                if (singleTableDataGridView.CurrentRow != null)
                {
                    singleTableDataGridView.Rows.Remove(singleTableDataGridView.CurrentRow);

                UpdateAll();
                }
            }

        private void Del_parent_button_Click(object sender, EventArgs e)
        {
            if (parentTableDataGridView.CurrentRow != null)
            {
                parentTableDataGridView.Rows.Remove(parentTableDataGridView.CurrentRow);
                UpdateAll();
            }
        }

        private void del_child_button_Click(object sender, EventArgs e)
        {
            if (child_TableDataGridView.CurrentRow != null)
            {
                child_TableDataGridView.Rows.Remove(child_TableDataGridView.CurrentRow);
                UpdateAll();
            }
        }

        private void child_TableDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // it's here by mistake
        }
        // Show information in editing area.
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int t_Single = listBox2.SelectedIndex;
            DataRow rw_Single = testDataSet.SingleTable.Rows[t_Single];
            int Id_Single = Convert.ToInt32(rw_Single[0]);
            string Item1 = Convert.ToString(rw_Single[1]);
            textBox7.Text = Item1;
    
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

            int t_Parent = listBox3.SelectedIndex;
            DataRow rw_Parent = testDataSet.ParentTable.Rows[t_Parent];
            int Id_parent = Convert.ToInt32(rw_Parent[0]);
            string Item2 = Convert.ToString(rw_Parent[1]);
            textBox8.Text = Item2;

        }

        private void child_TableDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            v();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (listBox4.SelectedItem == null) { 
            int l1 = listBox1.SelectedIndex;
                 coupleDataGridView.Rows.RemoveAt(l1);
                listBox1.Items.RemoveAt(l1);
                listBox4.Items.RemoveAt(l1);
                textBox7.Clear();
                textBox8.Clear();
            } else
            {
                int l4 = listBox4.SelectedIndex;
                coupleDataGridView.Rows.RemoveAt(l4);
                listBox1.Items.RemoveAt(l4);
                listBox4.Items.RemoveAt(l4);
                textBox7.Clear();
                textBox8.Clear();
            }
        }
    }
}
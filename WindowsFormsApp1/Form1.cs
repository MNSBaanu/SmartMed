using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtMarks.Text = "";
        }
    private void BtnInsert_Click(object sender, EventArgs e)
        {
            student newStudent = new student

            {
                id = int.Parse(txtID.Text),
                name = txtName.Text,
                marks = int.Parse(txtMarks.Text)
            };
            newStudent.addStudent();
            MessageBox.Show("Student addeded successfully");
        }

        private void BtnInsertM_Click(object sender, EventArgs e)
        {
            student newStudent = new student();
            int id = int.Parse(txtID.Text);
            string name = txtName.Text;
            int marks = int.Parse(txtMarks.Text);

        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            string name = txtName.Text;
            int marks = int.Parse(txtMarks.Text);
            student student = new student();
            student.updateStudent(id, name, marks);
            MessageBox.Show("Student Updated Successfully");
            Clear();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            student student = new student();
            student s = student.searchbyID(id);
            txtName.Text = s.name;
            txtMarks.Text = (s.marks).ToString();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtID.Text);
            student student = new student();
            MessageBox.Show("Student Updated Successfully");
            Clear();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

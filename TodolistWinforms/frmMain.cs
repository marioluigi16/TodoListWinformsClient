using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TodoApi.Models;

namespace TodolistWinforms
{
    public partial class frmMain : Form
    {
        BindingList<TodoItem> lst;

        public frmMain()
        {
            InitializeComponent();
        }

        private async void BindTodoList()
        {
            lst = new BindingList<TodoItem>();
            TodoItemAdapter mgr = new TodoItemAdapter();

            lst = await mgr.GetItemList();
            this.dataGridView1.DataSource = lst;

            lblCount.Text = lst.Count + " to-do";
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            BindTodoList();
        }

        private async void Button1_Click(object sender, EventArgs e)
        {
            TodoItem ti = new TodoItem();
            ti.IsComplete = false;
            ti.Name = textBox1.Text;

            TodoItemAdapter mgr = new TodoItemAdapter();
            bool ret = await mgr.PostItem(ti);
            if (ret)
                MessageBox.Show("success");
            else
                MessageBox.Show("fail");

            BindTodoList();
        }

        private async void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            TodoItem ti = dataGridView1.Rows[e.RowIndex].DataBoundItem as TodoItem;

            TodoItemAdapter mgr = new TodoItemAdapter();
            bool ret = await mgr.UpdateItem(ti);
            if (ret)
                MessageBox.Show("success");
            else
                MessageBox.Show("fail");

            BindTodoList();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
        }

        private async void DataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            TodoItem ti = e.Row.DataBoundItem as TodoItem;

            TodoItemAdapter mgr = new TodoItemAdapter();
            bool ret = await mgr.DeleteItem(ti);
            if (ret)
                MessageBox.Show("success");
            else
                MessageBox.Show("fail");

            BindTodoList();
        }
    }
}

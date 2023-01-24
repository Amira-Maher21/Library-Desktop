using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using LibraryMainForm.Database;

namespace LibraryMainForm.Forms
{
    public partial class EmployeeForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();



        public EmployeeForm1()
        {
            InitializeComponent();
        }
        void InsertOrUpdate()
        {
            try
            {
                Employee et = new Employee();
                et.EmployeeName = Convert.ToString(textEdit1.EditValue);
                et.EmployeeBirthDate = Convert.ToDateTime(dateEdit1.EditValue);
                et.EmployeeHierDate = Convert.ToDateTime(dateEdit2.EditValue);
                et.EmployeeSalary = Convert.ToDecimal(textEdit2.EditValue);
                et.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                dbcontxt.Employees.InsertOnSubmit(et);
                dbcontxt.SubmitChanges();
                MessageBox.Show("تم الحفظ");
               gridControl1.DataSource = dbcontxt.Employees.ToList();
             


                Clear();
            }
            catch
            {

                MessageBox.Show("لم يتم الحفظ ");

            }
        }
        void Clear()
        {
            textEdit1.EditValue = null;
            dateEdit1.EditValue = null;
            dateEdit2.EditValue = null;
            textEdit2.EditValue = null;
            checkEdit1.Checked = false;



        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void EmployeeForm1_Load(object sender, EventArgs e)
        {
           
        }
        void Fill()
        {

           
        }
    }
}
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using LibraryMainForm.Database;
using LibraryMainForm.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LibraryMainForm
{
    public partial class MainForm : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm

    {
        enum FormMode
        {
            Book,category,author,nationality,employee,borrower,borrowerbook,store,Bookrequest


        }

        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();
        FormMode formMode;
        private object bookForm;

        public object NationalityForm { get; set; }


        public MainForm()
        {

            InitializeComponent();
        }





        private void accordionControlElement1_Click(object sender, EventArgs e)
        {


        }
        // الكتب
        private void accordionControlElement3_Click(object sender, EventArgs e)
        {
            formMode = FormMode.Book;
        
            iNew.Enabled = true;
            iSave.Enabled = true;
           fill();
            gridView1.BestFitColumns();
        }




        private void fluentDesignFormContainer1_Click(object sender, EventArgs e)
        {

        }
        //  الفئه
        private void accordionControlElement4_Click(object sender, EventArgs e)
        {
            formMode = FormMode.category;

            iNew.Enabled = true;
            iSave.Enabled = true;
            fill();
            gridView1.BestFitColumns();
        }
        // التعديل

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {

            iNew.Enabled = true;
            iSave.Enabled = true;
            switch (formMode)
            {
                case FormMode.Book:
                    



                    break;
                case FormMode.category:
                    categoryForm1 categoryForm1 = new categoryForm1();
                    categoryForm1.ShowDialog();
                    fill();

                    break;
                case FormMode.author:
                    AuthorForm1 AuthorForm1 = new AuthorForm1();
                    AuthorForm1.IsEdite = false;
                    AuthorForm1.Code = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "AuthorId"));
                    AuthorForm1.ShowDialog();
                    fill();
                    break;
                case FormMode.nationality:
                    NationalityForm1 NationalityForm1 = new NationalityForm1();
                    NationalityForm1.ShowDialog();
                    NationalityForm1.ShowDialog();
                    fill();

                    break;
                case FormMode.employee:
                    EmployeeForm1 EmployeeForm1 = new EmployeeForm1();

                    EmployeeForm1.ShowDialog();
                    fill();
                    break;
                case FormMode.borrower:
                    BorrowerForm1 BorrowerForm1 = new BorrowerForm1();
                    BorrowerForm1.IsEdite = false;
                    BorrowerForm1.Code =  Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BorrowerId"));
                    BorrowerForm1.ShowDialog();
                    fill();

                    break;
                case FormMode.borrowerbook:

                    BorrowerBookForm1 BorrowerBookForm1 = new BorrowerBookForm1();
                    BorrowerBookForm1.IsEdite = false;
                    BorrowerBookForm1.Code = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BorrowerBookId"));
                    BorrowerBookForm1.ShowDialog();
                  
                    fill();

                    break;
                case FormMode.store:

                    StoreForm1 StoreForm1 = new StoreForm1();
                    StoreForm1.ShowDialog();


                    fill();
                    break;
                case FormMode.Bookrequest:


                    BookRequestForm1 BookRequestForm1 = new BookRequestForm1();
                    BookRequestForm1.IsEdite = false;
                    BookRequestForm1.Code = Convert.ToInt32(gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "BookRequestId"));
                    BookRequestForm1.ShowDialog();
                    fill();
                    break;
                default:
                    break;
            }
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }
          
        //fill

        void fill()

           
        {
            gridView1.Columns.Clear();
           
            gridView1.FocusedRowHandle = -1;
            
            switch (formMode)
            {
                case FormMode.Book:
                    var bo = (from b in dbcontxt.Books
                              join c in dbcontxt.Categories on b.CategoryId equals c.CategoryId
                              join a in dbcontxt.Authors on b.AuthorId equals a.AuthorId
                              select new { b.BookId, b.BookName,a.AuthorName, c.CategoryName, b.DateOfPublication, b.BeginingBalance,b.BookBarcode });
                    gridControl1.DataSource = bo;
                    break;


                case FormMode.category:

                    gridControl1.DataSource = dbcontxt.Categories.ToList();




                    break;

                case FormMode.author:
                        var au =    (from a in dbcontxt.Authors
                                join n in dbcontxt.Nationalities on a.NationalityId equals n.NationalityId

                                select new { a.AuthorName, a.AuthorDeathdate, a.AuthorBookNumber, a.AuthorBirthdate, a.NationalityId,n.NationalityDesc });
                    gridControl1.DataSource = au;
                    break;

                case FormMode.nationality:

                    gridControl1.DataSource = dbcontxt.Nationalities.ToList();
                    break;

                case FormMode.employee:


                    gridControl1.DataSource = dbcontxt.Employees.Where(a => a.IsActive == true);

                    break;

                case FormMode.borrower:


                    gridControl1.DataSource = dbcontxt.Borrowers.ToList();
                    break;


                case FormMode.borrowerbook:

                    gridControl1.DataSource = dbcontxt.BorrowerBooks.ToList();

                    break;

                case FormMode.store:
                    gridControl1.DataSource = dbcontxt.Stores.ToList();

                    break;

                case FormMode.Bookrequest:

                    gridControl1.DataSource = dbcontxt.BookRequests.ToList();

                    break;
                default:
                    break;
            }

        }
        // جديد
        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            iNew.Enabled = true;
            iSave.Enabled = true;

            switch (formMode)
            {
                case FormMode.Book:

                    using (BookForm bookForm = new BookForm() { IsEdite = false, Code = 0 })
                    {
                        bookForm.ShowDialog();
                    }
                    fill();
                    break;
                case FormMode.category:

                    using (categoryForm1 categoryform1 = new categoryForm1())
                    {
                        categoryform1.ShowDialog();
                    }
                    fill();
                    break;
                case FormMode.author:
                    using (AuthorForm1 Authorform1 = new AuthorForm1() { IsEdite = false, Code = 0 })
                    {
                        
                        Authorform1.ShowDialog();
                    }
                    fill();
                    break;
                case FormMode.nationality:
                    using (NationalityForm1 Nationalityform = new NationalityForm1())
                    {
                        Nationalityform.ShowDialog();
                    }
                      
                  
                        
                    

                    fill();

                    break;



                case FormMode.employee:
                    using (EmployeeForm1 Employeeform1 = new EmployeeForm1())
                    {
                        Employeeform1.ShowDialog();
                    }
                       

                    

                    fill();

                    break;
                case FormMode.borrower:
                    using (BorrowerForm1 Borrowerform1 = new BorrowerForm1() { IsEdite = false, Code = 0 })

                    {
                        Borrowerform1.ShowDialog();
                    }
                   
                 
                    fill();


                    break;
                case FormMode.borrowerbook:
                    using ( BorrowerBookForm1 BorrowerBookForm1 = new BorrowerBookForm1() { IsEdite = false, Code = 0 })
                  {
                        BorrowerBookForm1.ShowDialog();
                    }
                 
                    fill();

                    break;
                case FormMode.store:
                    using (StoreForm1 StoreForm1 = new StoreForm1())
                    {
                        StoreForm1.ShowDialog();
                    }
                   
                    fill();


                    break;
                case FormMode.Bookrequest:

                    using (BookRequestForm1 BookRequestForm = new BookRequestForm1() { IsEdite = false, Code = 0 })
                    {
                        BookRequestForm.ShowDialog();
                    }
                   
                    fill();
                    break;
                default:
                    break;
            }
        }

        private void accordionControlElement5_Click(object sender, EventArgs e)
        {
            formMode = FormMode.author;

            iNew.Enabled = true;
            iSave.Enabled = true;
            fill();
            gridView1.BestFitColumns();

       
    }

        private void accordionControlElement6_Click(object sender, EventArgs e)
        {
            formMode = FormMode.nationality;

            iNew.Enabled = true;
            iSave.Enabled = true;
            fill();
            gridView1.BestFitColumns();
        }

        private void accordionControlElement7_Click(object sender, EventArgs e)
        {
            formMode = FormMode.employee;

            iNew.Enabled = true;
            iSave.Enabled = true;
            fill();
            gridView1.BestFitColumns();
        }

        private void accordionControlElement8_Click(object sender, EventArgs e)
        {
            formMode = FormMode.borrower;

            iNew.Enabled = true;
            iSave.Enabled = true;
            fill();
            gridView1.BestFitColumns();
        }

        private void accordionControlElement9_Click(object sender, EventArgs e)
        {
            formMode = FormMode.borrowerbook;

            iNew.Enabled = true;
            iSave.Enabled = true;
            fill();
            gridView1.BestFitColumns();
        }

        private void accordionControlElement10_Click(object sender, EventArgs e)
        {
            formMode = FormMode.store;

            iNew.Enabled = true;
            iSave.Enabled = true;
            fill();
            gridView1.BestFitColumns();
        }

        private void accordionControlElement11_Click(object sender, EventArgs e)
        {
            formMode = FormMode.Bookrequest;

            iNew.Enabled = true;
            iSave.Enabled = true;
            fill();
            gridView1.BestFitColumns();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void accordionControl1_Click(object sender, EventArgs e)
        {

        }

        private void accordionControlElement2_Click(object sender, EventArgs e)
        {

        }
    }
}

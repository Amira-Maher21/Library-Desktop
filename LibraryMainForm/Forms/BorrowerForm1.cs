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
    public partial class BorrowerForm1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        LibraryDataClassesDataContext dbcontxt = new LibraryDataClassesDataContext();

        public bool IsEdite;
        public int Code;
        public byte InsurancePolicyPic;
        public string fullpath;
        public BorrowerForm1()
        {
            InitializeComponent();
        }
        public string loadedPath;

        private void pictureEdit1_DoubleClick(object sender, EventArgs e)
        {


            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png;*.ico)|*.jpg; *.jpeg; *.gif; *.bmp;*.png;*.ico";
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Image loadedImage = Image.FromFile(ofd.FileName);
                loadedPath = ofd.FileName;
                pictureEdit1.Image = loadedImage;
                pictureEdit1.Tag = ofd.FileName;




            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertOrUpdate();
        }

        void InsertOrUpdate()
        {
            try
            {
                if (IsEdite)
                {
                    var getdata = dbcontxt.Borrowers.Where(a => a.BorrowerId == Code).FirstOrDefault();

                    getdata.BorrowerName = Convert.ToString(textEdit1.EditValue);
                    getdata.BorrowerNationalityId = Convert.ToString(searchLookUpEditBorrowerNationality.EditValue);

                    getdata.BorrowerCardIdPic1 = Convert.ToString(pictureEdit1.Tag);
                   
                    getdata.BorrowerCardIdPic2 = Convert.ToString(pictureEdit2.Tag);

                    getdata.IsActive = Convert.ToBoolean(checkEdit1.Checked);
                    dbcontxt.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    IsEdite = false;
                    Clear();
                    fill();







                }
                else
                {

                    Borrower bo = new Borrower();
                    bo.BorrowerName = Convert.ToString(textEdit1.EditValue);
                    bo.BorrowerNationalityId = Convert.ToString(searchLookUpEditBorrowerNationality.EditValue);
                    bo.IsActive = Convert.ToBoolean(checkEdit1.Checked);

                    Image img1 = pictureEdit1.Image;
                    ImageConverter img1Co = new ImageConverter();
                    byte[] img1Arry = (byte[])img1Co.ConvertTo(img1, typeof(byte[]));
                    bo.BorrowerCardIdPic1 = Convert.ToString(pictureEdit1.Tag);


                    Image img2 = pictureEdit2.Image;
                    ImageConverter img2Co = new ImageConverter();
                    byte[] img2Arry = (byte[])img2Co.ConvertTo(img2, typeof(byte[]));
                    bo.BorrowerCardIdPic2 = Convert.ToString(pictureEdit2.Tag);



                    dbcontxt.Borrowers.InsertOnSubmit(bo);
                    dbcontxt.SubmitChanges();
                    MessageBox.Show("تم الحفظ");
                    Clear();
                    fill();


                }
            }
            catch
            {
                MessageBox.Show("لم يتم الحفظ ");

            }
        }


        void Clear()
        {
            textEdit1.EditValue = null;

            searchLookUpEditBorrowerNationality.EditValue = 0;
            checkEdit1.Checked = false;


            pictureEdit1.Image = null;
            pictureEdit2.Image = null;
            pictureEdit1.Tag = null;
            pictureEdit2.Tag = null;
                
        }
        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }


        void fill()
        {


            gridControl1.DataSource = dbcontxt.Borrowers.ToList();



        }

        private void ribbon_DoubleClick(object sender, EventArgs e)
        {







        }

        private void pictureEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void pictureEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void pictureEdit2_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofs = new OpenFileDialog();
            ofs.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png;*.ico)|*.jpg; *.jpeg; *.gif; *.bmp;*.png;*.ico";
            DialogResult br = ofs.ShowDialog();
            if (br == DialogResult.OK)
            {
                Image loadedImage = Image.FromFile(ofs.FileName);
                loadedPath = ofs.FileName;
                pictureEdit2.Image = loadedImage;
                pictureEdit2.Tag = ofs.FileName;
            }
        }

        private void BorrowerForm1_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dbcontxt.Borrowers.ToList();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

            Code = Convert.ToInt32(gridView1.GetFocusedRowCellValue("BorrowerId"));
            var getdata = dbcontxt.Borrowers.Where(a => a.BorrowerId == Code).FirstOrDefault();

            textEdit1.EditValue = Convert.ToString(getdata.BorrowerName);

            searchLookUpEditBorrowerNationality.EditValue = Convert.ToString(getdata.BorrowerNationalityId);
            checkEdit1.Checked = Convert.ToBoolean(getdata.IsActive);

            pictureEdit1.Image = Image.FromFile(getdata.BorrowerCardIdPic1);
            pictureEdit2.Image = Image.FromFile(getdata.BorrowerCardIdPic2);
            IsEdite = true;
        }

    

    }

    }

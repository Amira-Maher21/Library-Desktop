using System;

namespace LibraryMainForm.Database
{
    partial class LibraryDataClassesDataContext
    {
        public object InvoiceMaster { get; internal set; }

        internal void InsertInDetail(int invoice_Id, int id, int qty, decimal pri, decimal tot)
        {
            throw new NotImplementedException();
        }
    }
}
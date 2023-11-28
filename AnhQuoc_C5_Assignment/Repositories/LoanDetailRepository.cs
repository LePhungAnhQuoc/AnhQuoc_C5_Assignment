﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnhQuoc_C5_Assignment
{
    public class LoanDetailRepository : Repository<LoanDetail>
    {
        public LoanDetailRepository() : base()
        {
        }
        public LoanDetailRepository(ObservableCollection<LoanDetail> items) : base(items)
        {
        }

        public override void LoadList()
        {
            _Items = dbSource.LoanDetails.ToObservableCollection();
        }

        public override void WriteAdd(LoanDetail item)
        {
            dbSource.LoanDetails.Add(item);
            dbSource.SaveChanges();
        }

        public override void WriteDelete(LoanDetail item)
        {
            dbSource.LoanDetails.Remove(item);

            dbSource.SaveChanges();
        }

        public override void WriteUpdate(LoanDetail updated)
        {
            var LoanDetailViewModel = UnitOfViewModel.Instance.LoanDetailViewModel;

            LoanDetail itemSource = dbSource.LoanDetails.FirstOrDefault(sourceItem => sourceItem.Id == updated.Id);
            LoanDetailViewModel.Copy(itemSource, updated);

            dbSource.SaveChanges();
        }
    }
}

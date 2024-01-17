﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AnhQuoc_C5_Assignment
{
    public class PenaltyReasonViewModel: BaseViewModel<PenaltyReason>
    {
        public PenaltyReasonViewModel()
        {
            Item = new PenaltyReason();
            Repo = new PenaltyReasonRepository();
            prefix = "PR";
            numberPrefix = 1;
        }
        
        public PenaltyReason FindById(string id, bool? statusValue)
        {
            var source = Repo.Gets();
            source = FillByStatus(source, statusValue);
            return source.FirstOrDefault(item => item.Id == id);
        }
        
        public ObservableCollection<PenaltyReason> FillContainsName(ObservableCollection<PenaltyReason> source, string valueName, bool igNoreCase, bool? statusValue)
        {
            ObservableCollection<PenaltyReason> newList = new ObservableCollection<PenaltyReason>();
            foreach (PenaltyReason item in source)
            {
                if (item.Name.ContainsCorrectly(valueName, igNoreCase))
                {
                    newList.Add(item);
                }
            }
            newList = FillByStatus(newList, statusValue);
            return newList;
        }

        public ObservableCollection<PenaltyReason> FillContainsName(ObservableCollection<PenaltyReasonDto> source, string valueName, bool igNoreCase, bool? statusValue)
        {
            return FillContainsName(CreateByDto(source), valueName, igNoreCase, statusValue);
        }
        

        public bool IsCheckEmptyItem(PenaltyReason item)
        {
            return Utilities.IsCheckEmptyItem(item, false);
        }

        public void Copy(PenaltyReason dest, PenaltyReasonDto source)
        {
            Utilities.Copy(dest, source);
        }

        public PenaltyReason CreateByDto(PenaltyReasonDto source)
        {
            var result = new PenaltyReason();
            Copy(result, source);
            return result;
        }

        public ObservableCollection<PenaltyReason> CreateByDto(ObservableCollection<PenaltyReasonDto> source)
        {
            var dest = new ObservableCollection<PenaltyReason>();
            foreach (PenaltyReasonDto item in source)
            {
                dest.Add(CreateByDto(item));
            }
            return dest;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnhQuoc_C5_Assignment
{
    /// <summary>
    /// Interaction logic for ucStatisticalPage.xaml
    /// </summary>
    public partial class ucStatisticalPage : UserControl
    {
        private BookViewModel bookViewModel;
        private LoanSlipViewModel loanSlipViewModel;


        public ucStatisticalPage()
        {
            InitializeComponent();
            bookViewModel = UnitOfViewModel.Instance.BookViewModel;
            loanSlipViewModel = UnitOfViewModel.Instance.LoanSlipViewModel;



            ucBookQuantities.Content = bookViewModel.Repo.Count.ToString();
            ucLoanQuantitiesLastYear.Content = loanSlipViewModel.FillByLastYear().Count.ToString();

            var loanSlipFind = loanSlipViewModel.FindByMostQuantity();
            ucBookQuantitiesMostOfReader.Content = loanSlipFind != null ? loanSlipFind.Quantity.ToString() : 0.ToString();

            foreach (ucStatisticalCard card in LogicalTreeHelper.GetChildren(wrapContainer))
            {
                card.MouseDoubleClick += Card_MouseDoubleClick;
            }
        }

        private void Card_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ucStatisticalCard card = sender as ucStatisticalCard;

            StatisticalDescription frmDescription = new StatisticalDescription();

            frmDescription.Icon = card.Icon;
            frmDescription.Description = card.ToolTip.ToString();
            frmDescription.Value = card.Content;

            frmDescription.Show();
        }
    }
}

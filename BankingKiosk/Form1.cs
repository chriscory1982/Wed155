using BankingDomain;

namespace BankingKiosk
{
    public partial class Form1 : Form
    {
        private BankAccount _account;
        public Form1()
        {
            InitializeComponent();
            _account = new BankAccount(new StandardBonusCalculator(), new Compliance());
            UpdateBalance();
        }

        private void UpdateBalance()
        {
            Text = _account.GetBalance().ToString("c");
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            DoTransaction(_account.Deposit);
        }

        private void DoTransaction(Action<decimal> operation)
        {
            try
            {
                var amount = decimal.Parse(txtAmount.Text);
                operation(amount);
               
            }
            catch (FormatException)
            {

                MessageBox.Show("Error", "Enter a number, fool!", MessageBoxButtons.OK);
            }
            catch(OverdraftException)
            {
                MessageBox.Show("Error", "You do not have enough money!", MessageBoxButtons.OK);
            }
            finally
            {
                txtAmount.SelectAll(); // select all the text in that box
                txtAmount.Focus(); // put the cursor focus there.
                UpdateBalance();
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            DoTransaction(_account.Withdraw);
        }
    }
}
namespace BankingDomain
{
    public class BankAccount
    {
        private decimal _balance = 5000; // JFHCI
        private ICalculateBonuses _bonusCalculator;
        private INotifyTheFeds _fedNotifier;

        public BankAccount(ICalculateBonuses bonusCalculator, INotifyTheFeds fedNotifier)
        {
            _bonusCalculator = bonusCalculator;
            _fedNotifier = fedNotifier;
        }

        public decimal GetBalance()
        {
            return _balance;
        }

        public void Deposit(decimal amountToDeposit)
        {
            // I have to figure the bonus - how do I do that..

            // add that bonus to balance.

            _balance += amountToDeposit +
                // "Query" (Func) Ask
                _bonusCalculator.GetBonusForDeposit(_balance, amountToDeposit);
        }

        public void Withdraw(decimal amountToWithdraw)
        {
            // "Guard" - don't do any work unless these preconditions are met.
            if (amountToWithdraw > _balance)
            {
                // Abnormally End.
                throw new OverdraftException();
            }
            // "Command", (Action) Tell
            _fedNotifier.NotifyOfWithdrawal(this, amountToWithdraw);
            _balance -= amountToWithdraw;
        }
    }
}
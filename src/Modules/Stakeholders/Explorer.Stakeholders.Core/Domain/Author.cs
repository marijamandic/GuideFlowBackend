using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class Author : User
    {
        public double Wallet {  get; set; }

        public Author(double Wallet)
        {
            this.Wallet = Wallet;
        }

        public void UpdateWallet(double wallet)
        {
            Wallet = wallet;
        }

        public void AddMoney(double amount)
        {
            Wallet += amount;
            UpdateWallet(Wallet);
        }

        public void RemoveMoney(double amount)
        {
            Wallet -= amount;
            UpdateWallet(Wallet);
        }
    }
}

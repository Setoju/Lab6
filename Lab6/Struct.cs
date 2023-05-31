using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    [Serializable]
    public struct Order : IComparable<Order>
    {
        public string paymentAccPay;
        public string paymentAccRec;
        public int amount;

        
        public Order(string lineWithData)
        {
            string[] line = lineWithData.Trim().Split();
            paymentAccPay = line[0];
            paymentAccRec = line[1];
            amount = Convert.ToInt32(line[2]);
        }
        public int CompareTo(Order other)
        {
            return this.paymentAccPay.CompareTo(other.paymentAccPay);
        }
    }
}

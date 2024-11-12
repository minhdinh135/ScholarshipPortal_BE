using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Transaction
{
	public class TransactionDto
	{
			public decimal Amount { get; set; }
			public string PaymentMethod { get; set; }
			public string Description { get; set; }
			public string TransactionId { get; set; }
			public DateTime TransactionDate { get; set; }
			public string Status { get; set; }
			public int WalletSenderId { get; set; }
			public int WalletReceiverId { get; set; }
		}
}

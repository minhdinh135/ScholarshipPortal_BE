using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Account
{
	public class UpdateWalletBankInformationDto
	{
		public string? BankAccountName { get; set; }
		public string? BankAccountNumber { get; set; }
	}
}


using LearnHub.Infrastructure.Persistence.Configuration.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnHub.Infrastructure.Identity
{
	public class IdentityException:Exception
	{
		public string ErrorMessage
		{
			get;
			set;
		}
		public IdentityException(string message)
		{

			var errorMessage = JsonConvert.DeserializeObject<List<PersianIdentityErrorResponse>>(message);

			ErrorMessage = string.Join("\n", errorMessage.Select(x => x.Description).ToList());


		}
	}
}

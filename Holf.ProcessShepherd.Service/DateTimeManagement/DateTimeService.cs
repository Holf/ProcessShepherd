using System;
using System.Collections.Generic;
using System.Text;

namespace Holf.ProcessShepherd.Service.DateTimeManagement
{

	public class DateTimeService : IDateTimeService
	{
		public DateTime Now => DateTime.Now;
	}
}

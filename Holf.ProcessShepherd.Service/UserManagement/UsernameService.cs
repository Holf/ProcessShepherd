using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace Holf.ProcessShepherd.Service.ProcessManagement
{
	public interface IUsernameService
	{
		string GetLoggedOnUsername();

		bool GetShouldShepherdLoggedOnUser(List<string> shepherdedUsers, string loggedOnUsername);
	}

	public class UsernameService : IUsernameService
	{
		private string loggedOnUsername;
		
		public string GetLoggedOnUsername()
		{

            if (loggedOnUsername == null)
			{
				ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT UserName FROM Win32_ComputerSystem");
				ManagementObjectCollection collection = searcher.Get();
				string username = (string)collection.Cast<ManagementBaseObject>().First()["UserName"];
				loggedOnUsername = username.Split("\\").Last();
			}

			return loggedOnUsername;
		}

		public bool GetShouldShepherdLoggedOnUser(List<string> shepherdedUsers, string loggedOnUsername)
		{
			return shepherdedUsers.Any(x => GetShouldUserBeShepherded(x, loggedOnUsername));
		}

		private bool GetShouldUserBeShepherded(string usernameFromConfig, string loggedOnUsername)
		{
			return LikeOperator.LikeString(loggedOnUsername, usernameFromConfig, CompareMethod.Text);
		}
	}
}

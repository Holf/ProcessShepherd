using System.Collections.Generic;

namespace Holf.ProcessShepherd.Service.ProcessManagement
{
	public interface IUsernameService
	{
		string GetLoggedOnUsername();

		bool GetShouldShepherdLoggedOnUser(List<string> shepherdedUsers, string loggedOnUsername);
	}
}

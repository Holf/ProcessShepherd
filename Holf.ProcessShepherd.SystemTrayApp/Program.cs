using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Holf.ProcessShepherd.SystemTrayApp
{
	static class Program
	{
		/// <summary>
		///  The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.SetHighDpiMode(HighDpiMode.SystemAware);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			

			NotifyIcon icon = new NotifyIcon();
			icon.Icon = new System.Drawing.Icon("icon.ico");
			icon.Visible = true;
			icon.BalloonTipText = "Hello from My Kitten";
			icon.Text = "boo";
			icon.BalloonTipTitle = "Cat Talk";
			icon.BalloonTipIcon = ToolTipIcon.Info;
			icon.ShowBalloonTip(2000);

			Application.Run();


		}
	}
}

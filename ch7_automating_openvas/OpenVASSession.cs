using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Security.Authentication;

namespace ch8_automating_openvas
{
	public class OpenVASSession : IDisposable
	{
		private SslStream _stream;

		public OpenVASSession (string username, string password, string host, int port = 9390)
		{
			this.ServerIPAddress = IPAddress.Parse (host);
			this.ServerPort = port;
			this.Authenticate (username, password);
		}

		public string Username { get; set; }

		public string Password { get; set; }

		public IPAddress ServerIPAddress { get; set; }

		public int ServerPort { get; set; }

		public SslStream Stream { 
			get {
				if (_stream == null)
					GetStream ();
				
				return _stream;
			}
			
			set { _stream = value; }
		}

		public XDocument Authenticate (string username, string password)
		{
			XDocument authXML = new XDocument (
				                    new XElement ("authenticate",
					                    new XElement ("credentials", 
						                    new XElement ("username", username),
						                    new XElement ("password", password)
					                    )));
			
			XDocument response = this.ExecuteCommand (authXML);

			if (response.Root.Attribute ("status").Value != "200")
				throw new Exception ("Authentication failed");

			this.Username = username;
			this.Password = password;

			return response;		
		}

		public XDocument ExecuteCommand (XDocument doc)
		{
			ASCIIEncoding enc = new ASCIIEncoding ();
			
			string xml = doc.ToString ();
			this.Stream.Write (enc.GetBytes (xml), 0, xml.Length);

			return ReadMessage (this.Stream);
		}

		private XDocument ReadMessage (SslStream sslStream)
		{
			using (var stream = new MemoryStream ()) {	
				int bytesRead = 0;
				do {
					byte[] buffer = new byte[2048];
					bytesRead = sslStream.Read (buffer, 0, buffer.Length);
					stream.Write (buffer, 0, bytesRead);
					if (bytesRead < buffer.Length) {
						try { 
							string xml = System.Text.Encoding.ASCII.GetString (stream.ToArray ());
							return XDocument.Parse (xml);
						} catch {
							continue;
						}
					}
				} while (bytesRead > 0);
			}

			return null;
		}

		private void GetStream ()
		{
			if (_stream == null || !_stream.CanRead) {
				TcpClient client = new TcpClient (this.ServerIPAddress.ToString (), this.ServerPort);

				_stream = new SslStream (client.GetStream (), false, 
				    new RemoteCertificateValidationCallback (ValidateServerCertificate), 
					(sender, targetHost, localCertificates, remoteCertificate, acceptableIssuers) => null);
				
				_stream.AuthenticateAsClient ("OpenVAS", null, SslProtocols.Tls, false);
			}
		}

		private bool ValidateServerCertificate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		public void Dispose ()
		{
			if (_stream != null) {
				_stream.Dispose ();
			}
		}
	}
}


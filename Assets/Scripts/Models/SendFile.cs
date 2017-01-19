using UnityEngine;
using System.Collections;
using System.IO;
using UdpKit;

[BoltGlobalBehaviour()]
public class SendFile : Bolt.GlobalEventListener {
	public static UdpKit.UdpChannelName fileChannel;
	public FileUtils.File file;
	public string path;
	public string projectPathEvt;

	public override void BoltStartBegin()
	{
		fileChannel = BoltNetwork.CreateStreamChannel ("file", UdpKit.UdpChannelMode.Reliable, 1);
	}

	public void sendFile(FileUtils.File fileSend)
	{
		file = fileSend;
		path = PathUtils.GetPathFrom (Application.persistentDataPath);
		//Debug.Log ("path" + path + "\\tmp" + file.ProjectPath);
		//Debug.Log ("hello path " + file.ProjectPath);
		//Debug.Log (Application.persistentDataPath + "/tmp" + file.ProjectPath);
		var sendFileEvt = sendFileEvent.Create ();
		sendFileEvt.projectPath = file.ProjectPath;
		sendFileEvt.Send ();

		if (BoltNetwork.isServer) {
			byte[] data = File.ReadAllBytes (fileSend.RealPath);
			BoltLog.Info ("data test");
			foreach (var connection in BoltNetwork.connections) {
				//Debug.Log (connection);
				connection.StreamBytes (fileChannel, data);
			}

		} else if (BoltNetwork.isClient) {

		}
	}

	public override void StreamDataReceived(BoltConnection connection, UdpStreamData data)
	{
		path = PathUtils.GetPathFrom (Application.persistentDataPath);

		//Debug.Log ("path " + path + "\\tmp"+ projectPathEvt);

		if (BoltNetwork.isClient) {
			System.IO.File.WriteAllBytes (path + "\\tmp" + projectPathEvt, data.Data);
			if (gameObject.GetComponent<ImageObject> ()) {
				gameObject.GetComponent<ImageObject> ().pathToFile = path + "\\tmp" + projectPathEvt;
				gameObject.GetComponent<ImageObject> ().Apply ();
			} else if (gameObject.GetComponent<AudioPlayer> ()) {
				gameObject.GetComponent<AudioPlayer> ().pathToFile = path + "\\tmp" + projectPathEvt;
				gameObject.GetComponent<AudioPlayer> ().playClient ();
			}
		}
	}

	public override void OnEvent(sendFileEvent e)
	{
		path = PathUtils.GetPathFrom (Application.persistentDataPath);
		projectPathEvt = e.projectPath;
		//Debug.Log ("on a reçut l'event");
	}
}

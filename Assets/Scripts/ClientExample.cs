using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class ClientExample : MonoBehaviour
{
    private string host = "192.168.10.1";
    private int port = 8889;
    private UdpClient client;

    void Start()
    {
        client = new UdpClient();
        client.Connect(host, port);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Send");
            var message = Encoding.UTF8.GetBytes("command");
            client.Send(message, message.Length);
        }
    }

    private void OnDestroy()
    {
        client.Close();
    }
}
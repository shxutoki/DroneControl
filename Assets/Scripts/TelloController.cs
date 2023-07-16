using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TelloController : MonoBehaviour
{
    private UdpClient client;
    private Thread recvThread;
    private IPEndPoint telloAddress = new IPEndPoint(IPAddress.Parse("192.168.10.1"), 8889);
    private IPEndPoint localAddress = new IPEndPoint(IPAddress.Any, 9000);

    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    TextMeshPro text;

    void Start()
    {
        client = new UdpClient();
        client.Client.Bind(localAddress);

        recvThread = new Thread(new ThreadStart(Receive));
        recvThread.Start();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Send(inputField.text);
        }
    }

    void Receive()
    {
        while (true)
        {
            byte[] data = client.Receive(ref telloAddress);
            string message = Encoding.UTF8.GetString(data);
            text.text = message;
            Debug.Log("Received: " + message);
        }
    }

    void Send(string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        client.Send(data, data.Length, telloAddress);
    }

    void OnDestroy()
    {
        client.Close();
        recvThread.Abort();
    }
}

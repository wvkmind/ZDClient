using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System;
using System.Text;
using System.Net;
using UnityEngine;
using System.Threading;
using System.Collections.Concurrent;
using MsgPack.Serialization;

public class NetWork {
	
	private static UdpClient udpClient = null ;
	protected static ConcurrentQueue<Byte[]> receive_queue =  null;
	protected static ConcurrentQueue<Byte[]> send_queue = null;
	private static Thread receive_thread = null;
	private static Thread send_thread = null;
	public static bool si_loop = true;
	public static string token = null;
	public static string gete_ip = "127.0.0.1";
	private static float timer = 0;
	private static bool heartbeat = false;

	public static void ClearQueue(){
		if(receive_queue!=null)
		receive_queue = null;
		if(send_queue!=null)
		send_queue = null;
	}
	public static void PrepareType(){
		//MsgPack.Serialization.MessagePackSerializer.PrepareType<MsgPack.MessagePackObject>();
		//MsgPack.Serialization.MessagePackSerializer.PrepareType<double>();
	}
	public static void ConnectGate(){
		NetWork.Connect(NetWork.gete_ip,6666);
	}
	public static void ConnectNode(string ip ,int port){
		NetWork.Connect(ip,port);
	}
	private static void Connect(string ip,int port){
		PrepareType();
		if(udpClient!=null)udpClient.Close();
		if(receive_thread!=null&&receive_thread.ThreadState== ThreadState.Running)receive_thread.Abort();
		if(send_thread!=null&&send_thread.ThreadState== ThreadState.Running)send_thread.Abort();
		receive_queue =  new ConcurrentQueue<Byte[]>();
		send_queue = new ConcurrentQueue<Byte[]>();
		udpClient = new UdpClient(0);
        udpClient.Connect(ip, port);
		receive_thread = new Thread(new ParameterizedThreadStart(ReceiveLoop));
		send_thread = new Thread(new ThreadStart(SendLoop));
		send_thread.Start();
		receive_thread.Start(udpClient);
	}
	public static void StartPing(){
		
		NetWork.heartbeat = true;
		NetEventDispatch.RegisterEvent("ping",data =>{Pinged(data);});
	}
	public static void ClosePing(){
		NetWork.heartbeat = false;
		NetEventDispatch.UnRegisterEvent("ping");
	}
	public static void Pinged(Dictionary<string, MsgPack.MessagePackObject> dic){
		MsgPack.MessagePackObject tmp;
		dic.TryGetValue("status", out tmp);
		int status = tmp.AsInt32();
		if(status == 0){
			Debug.Log("心跳成功");
		}
		else{
		//[TODO]心跳失败处理
			Debug.Log("心跳失败");
		}
	}
    public static void Send(byte[] s){
        send_queue.Enqueue(s);
	}
	public static void Send(String s){
        Byte[] sendBytes = Encoding.UTF8.GetBytes(s);
        send_queue.Enqueue(sendBytes);
	}
	public static Byte[] Get(){
		Byte[] s = null;
		if(receive_queue!=null&&receive_queue.TryDequeue(out s))
			return s;
		else
			return null;
	}
	public static void Update () {
		
		if(NetWork.heartbeat){
			timer += Time.deltaTime;

			if(timer>=10){
				timer = 0;

				Dictionary<string, object> dic = NetWork.getSendStart();
				dic.Add("name", "ping");
				NetWork.Push(dic);

			}
		}
		if(send_queue!=null&&!send_queue.IsEmpty){
			send_thread.Resume();
		}
	}
	public static void SendLoop(){
		while(true){
			byte[] s = null;
			if(send_queue!=null&&send_queue.TryDequeue(out s))
			{	
				udpClient.Send(s,s.Length);
			}
			else{
				send_thread.Suspend();
			}
		}
	}
	private static void ReceiveLoop(object data){
		UdpClient udpClient = data as UdpClient;
		while(si_loop){
			IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
			Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
			receive_queue.Enqueue(receiveBytes);
		}
	}
	// Update is called once per frame
	public static void OnDestory() {
		si_loop = false;
		if(udpClient!=null)
			udpClient.Close();
		if(receive_thread.ThreadState== ThreadState.Running)
			receive_thread.Abort();
		if(send_thread.ThreadState== ThreadState.Running||send_thread.ThreadState == ThreadState.Suspended)
			send_thread.Abort();
	}

	public static Dictionary<string, object> getSendStart(){
		Dictionary<string, object> dic = new Dictionary<string, object>();
        if(NetWork.token!=null){
			dic.Add("token",NetWork.token);
		}
		return dic;
	}
	
	public static void Push(Dictionary<string, object> dic){
		var serializer = MessagePackSerializer.Get<Dictionary<string, object>>();
        byte[] pack = serializer.PackSingleObject(dic);
        NetWork.Send(pack);
	}
}

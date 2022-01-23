﻿using System;
using System.Collections.Generic;
using System.Net;
using KCPNet;

// All these code is generated by tool
namespace GameProtocol
{
    public class ProtocolHandler
    {
        private static Dictionary<string, uint> name_id = new Dictionary<string, uint>()
        {
            {"LoginReq", 1},
            {"LoginAck", 2},
            {"HeartBeatReq", 3},
        };

        public static Dictionary<uint, Action<byte[], KCPSession>> id_parse = new Dictionary<uint, Action<byte[], KCPSession>>()
        {
            {1, null},
            {2, LoginAckParse},
            {3, null},
        };

        public static Action<object, KCPSession> onLoginReq;
        public static Action<object, KCPSession> onLoginAck;
        public static Action<object, KCPSession> onHeartBeatReq;

        private static void LoginAckParse(byte[] bytes, KCPSession targetSession)
        {
            object ack = LoginAck.Parser.ParseFrom(bytes);
            onLoginAck?.Invoke(ack, targetSession);
        }

        public static void RegisterProtocol(string name, Action<object, KCPSession> callback)
        {
            var id = name_id[name];
            switch (id)
            {
                case 1:
                    break;
                case 2:
                    onLoginAck += callback;
                    break;
                case 3:
                    onHeartBeatReq += callback;
                    break;
                default:
                    break;
            }
        }

        public static void UnRegisterProtocol(string name, Action<object, KCPSession> callback)
        {
            var id = name_id[name];
            switch (id)
            {
                case 1:
                    break;
                case 2:
                    onLoginAck -= callback;
                    break;
                case 3:
                    onHeartBeatReq -= callback;
                    break;
                default:
                    break;
            }
        }
    }
}